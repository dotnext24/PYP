﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PYP.Domain.Entities.Core
{
   public interface IEntity
    {
        Guid Key { get; set; }
    }
}
