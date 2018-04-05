using Dal.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dal.Repository
{
    public class DepartementRepo : BaseRepo<int, Departement>
    {
        #region Singleton
        private static DepartementRepo _Instance;

        public static DepartementRepo Instance
        {
            get
            {
                return _Instance ?? (_Instance = new DepartementRepo());
            }
        }
        #endregion

        private DepartementRepo() : base("Departement") { }

        protected override Departement Selector(IDataRecord Data)
        {
            return new Departement()
            {
                DepartementId = (int)(Data["DepartementId"]),
                Name = (string)Data["Name"],
              

            };
        }

        public override int Insert(Departement Entity)
        {
            throw new NotImplementedException();
        }

        public override bool Update(Departement Entity)
        {
            throw new NotImplementedException();
        }
    }
}
