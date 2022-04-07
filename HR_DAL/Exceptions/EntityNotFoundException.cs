using System;

namespace MultipleAPIs.HR_DAL.Exceptions
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
