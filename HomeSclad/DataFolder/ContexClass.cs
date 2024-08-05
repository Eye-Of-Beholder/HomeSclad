using Pennant.ClassFolder;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pennant.DataFolder
{
    public partial class DBEntities : DbContext
    {
     
        public virtual DbSet<Gender> Genders { get; set; }
        public virtual DbSet<Post> Posts { get; set; }
        public virtual DbSet<NumCab> NumCabs { get; set; }

        private static DBEntities contex;         //Обращение к БД//
        public static DBEntities getContex()
        {
            if (contex == null)
            {
                contex = new DBEntities();
            }
            return contex;
        }

        internal static object getContext()
        {
            throw new NotImplementedException();
        }
    }
}
