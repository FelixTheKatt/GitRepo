IF EXISTS (SELECT name FROM master.dbo.sysdatabases WHERE name = N'Agenda')
BEGIN
	ALTER DATABASE [Agenda] SET OFFLINE WITH ROLLBACK IMMEDIATE;
	ALTER DATABASE [Agenda] SET ONLINE;
	DROP DATABASE [Agenda];
END

CREATE DATABASE [Agenda];
GO
use [Agenda];
GO
create table [Departement]
(
	[DepartementId] int identity primary key,
	[Name] nvarchar(100)
)

create table [User]
(
	[UserId] int identity primary key,
	[Pseudo] nvarchar(50) not null,
	[Email] nvarchar (255) not null,
	[Password] NVARCHAR(256) NOT NULL,
	[Salt] UNIQUEIDENTIFIER NOT NULL,
	[DepartementId] int constraint Fk_User_Departement references [Departement]([DepartementId]) 
)

create table [Friends]
(
	[FriendsId] int identity primary key,
	[UserId1] int constraint Fk_Friends_User_1 references [User]([UserId]),
	[UserId2] int constraint Fk_Friends_User_2 references [User]([UserId]),
	[IsFriend] bit default  0,
	[InvitationOnGoing] bit,
	constraint Uk_User1_User2 unique ([UserId1] , [UserId2]),
	constraint Ck_User1_User2 check ([UserId1] != [UserId2])

)


create table [Agenda]
(
	[AgendaId] int identity primary key,
	[Name] nvarchar(100) default ('My Agenda'),
	[UserId] int constraint Fk_Agenda_User references [User]([UserId]) 
)

create table [Group]
(
	[GroupID] int identity primary key,
	[Name] NVARCHAR(100) Not Null,
	[RGBAColor] NVARCHAR(12) Not Null,
	[AgendaId] int constraint Fk_Group_Agenda references [Agenda]([AgendaId])
)

create table [Event]
(
	[EventId] int identity primary key,
	[Name] nvarchar(100) not null,
	[Date] Date not null,
	[Time] time,
	[GroupId] int constraint Fk_Event_Group references [Group]([GroupId]),
	[PublisherId] int constraint Fk_EventUser references [User]([UserId])
)
create table [Froup]
(
	[FroupId] int identity primary key,
	[GroupId] int constraint Fk_Froup_Group references [Group]([GroupId]),
	[FriendsId] int constraint Fk_Froup_Friends references [Friends]([FriendsId])	
	constraint Uk_GroupId_FriendsId_Unique unique ([GroupId] , [FriendsId])	
)