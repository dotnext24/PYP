using PYP.Domain.Entities.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PYP.Domain.Entities
{
    public class UserInRole:IEntity
    {
        [Key]
        public Guid Key { get; set; }

        [Required]
        public Guid UserKey { get; set; }

        [Required]
        public Guid RoleKey { get; set; }

        public User User { get; set; }
        public Role Role { get; set; }
    }
}
