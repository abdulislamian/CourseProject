using System.Runtime.Serialization;

namespace CourseProject.Buisness.Exceptions
{
    [Serializable]
    public class AddressNotFoundException : Exception
    {
        public int id;

        public AddressNotFoundException()
        {
        }

        public AddressNotFoundException(int id)
        {
            this.id = id;
        }

        public AddressNotFoundException(string? message) : base(message)
        {
        }

        public AddressNotFoundException(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        protected AddressNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}