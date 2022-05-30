using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PSP.Domain.Exceptions
{
    public class CategoryNotValidException : NotValidException
    {
        internal CategoryNotValidException()
        {
        }

        internal CategoryNotValidException(string message) : base(message)
        {
        }

        internal CategoryNotValidException(string message, Exception inner) : base(message, inner)
        {
        }
    }
}
