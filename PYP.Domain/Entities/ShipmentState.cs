﻿using PYP.Domain.Entities.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PYP.Domain.Entities
{
    public enum ShipmentStatus
    {
        Ordered = 1,
        Scheduled = 2,
        InTransit = 3,
        Delivered = 4
    }
    public class ShipmentState:IEntity
    {
        [Key]
        public Guid Key { get; set;}

        public Guid ShipmentKey { get; set; }

        [Required]
        public ShipmentStatus ShipmentStatus { get; set; }

        public DateTime CreatedOn { get; set;}

        public Shipment Shipment { get; set;}

    }
}
