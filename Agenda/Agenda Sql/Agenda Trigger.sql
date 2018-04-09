Use [Agenda];

GO

CREATE TRIGGER Tr_CheckUser1
ON [Froup]
INSTEAD OF INSERT
AS
BEGIN
	IF (Select [GroupId] From Inserted) in 
	(Select [GroupID] from [Group] where [AgendaId] IN 
		(SELECT [AgendaId] FROM [Agenda] WHERE [UserId] = 
			(select [UserId1] from [Friends] where [FriendsId] = 
				(select [FriendsId] from INSERTED ))))
	BEGIN
	Insert into [Froup] ([GroupId], [FriendsId]) VALUES
	((SELECT [GroupId] FROM INSERTED), (SELECT [FriendsId] FROM INSERTED));
	END
END
GO

CREATE TRIGGER Tr_MutualFriends
ON [Friends]
INSTEAD OF update
AS
BEGIN
	IF update ([IsFriend]) 	
	BEGIN	
	if  ((select IsFriend from deleted) = 0 and (select IsFriend from inserted) = 1)
		BEGIN

				IF(
		(SELECT [FriendsId] FROM [Friends] WHERE (
		[UserId1] = (SELECT [UserId2] FROM DELETED) AND
		[UserId2] = (SELECT [UserId1] FROM DELETED) 
												  )
		) IS NULL
		)
			BEGIN
			insert into [Friends] ([UserId1], [UserId2], [InvitationOnGoing]) 
			values ((select [UserId2] from deleted ), (select [UserId1] from deleted ), 0)		
			END  

		update [Friends]
		Set IsFriend =1 , InvitationOnGoing = 0
		where [FriendsId] = (select [FriendsId] from deleted);
		update [Friends]
		Set IsFriend =1 , InvitationOnGoing = 0 
		WHERE(
				[FriendsId] = (SELECT [FriendsId] FROM [Friends] 
								WHERE 
								[UserId1] = (select [UserId2] from deleted)
								AND
								[UserId2] = (select [UserId1] from deleted)
							));

			END
		else if ((select IsFriend from deleted) = 1 and (select IsFriend from inserted) = 0)
			BEGIN
			update [Friends]
			Set IsFriend = 0  , InvitationOnGoing = 0 
			where 
			(
				[FriendsId] = (select [FriendsId] from deleted)
				OR
				[FriendsId] = (SELECT [FriendsId] FROM [Friends] 
								WHERE 
								[UserId1] = (select [UserId2] from deleted)
								AND
								[UserId2] = (select [UserId1] from deleted)
							)
			)
			END
	END
	IF update ([InvitationOnGoing])
	BEGIN
	UPDATE [Friends] SET [InvitationOnGoing] = (SELECT [InvitationOnGoing] FROM INSERTED)
	WHERE [FriendsId] = (SELECT [FriendsId] FROM DELETED);
	END
END
