using System;

namespace Customers_BLL.Exceptions
{
    public class ForbiddenAccessException : Exception
    {
        public ForbiddenAccessException() { }
        
        public ForbiddenAccessException(string message)
            : base(message)
        {
        }
    }
}