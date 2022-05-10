using System;

namespace Customers_DAL.Exceptions
{
    public class EntityNotFoundException : Exception
    {
        public EntityNotFoundException(string message) : base(message)
        {
        }

        public EntityNotFoundException() : base()
        {
        }
    }
}
