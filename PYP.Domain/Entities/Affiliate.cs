using PYP.Domain.Entities.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PYP.Domain.Entities
{
    public class Affiliate:IEntity
    {
        [Key]
        public Guid Key { get; set; }

        [Required]
        [StringLength(50)]
        public string CompanyName { get; set; }

        [Required]
        [StringLength(50)]
        public string Address { get; set; }

        [Required]
        public string TelephoneNumber { get; set; }

        [Required]
        public DateTime CreatedAt { get; set; }

        [Required]
        public User User { get; set; }

        public virtual ICollection<Shipment> Shipments { get; set;}

        public Affiliate()
        {
            Shipments = new HashSet<Shipment>();
        }

    }
}
