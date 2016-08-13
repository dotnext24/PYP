using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PYP.Domain.Services
{
    public class OperationResult
    {
        public bool IsSuccess { get; set;}

        public OperationResult(bool isSuccsess)
        {
            IsSuccess = isSuccsess;
        }
    }
}
