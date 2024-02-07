using System;

namespace DataAccessLayer.Exceptions
{
    public class EntityNotFoundException : Exception
    {
        public EntityNotFoundException(string message)
            : base(message)
        {
        }

        public EntityNotFoundException()
            : base()
        {
        }
    }
}
