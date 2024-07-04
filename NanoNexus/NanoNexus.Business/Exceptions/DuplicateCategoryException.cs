using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NanoNexus.Business.Exceptions
{
    public class DuplicateCategoryException : Exception
    {
        public DuplicateCategoryException(string? message) : base(message)
        {
        }
    }
}
