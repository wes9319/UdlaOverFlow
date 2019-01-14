using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace UdlaOverflow.Models
{
    public class UdlaOverflowDB : DbContext
    {
        public DbSet<UO_User> User { get; set; }
        public DbSet<UO_Question> Question { get; set; }
        public DbSet<UO_Answer> Answer { get; set; }
        public DbSet<UO_Category> Category { get; set; }

    }
}