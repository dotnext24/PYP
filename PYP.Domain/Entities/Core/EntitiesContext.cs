using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PYP.Domain.Entities.Core
{
    public class EntitiesContext:DbContext
    {
        public EntitiesContext():base("PYPDbContext")
        {

        }
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<UserInRole> UserInRoles { get; set; }

        public DbSet<ShipmentType> PackageTypes { get; set; }

        public DbSet<Affiliate> Affiliates { get; set; }

        public DbSet<Shipment> Shipments { get; set; }

        public DbSet<ShipmentState> ShipmentStates { get; set;}

    }
}
