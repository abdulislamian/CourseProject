using System.Runtime.Serialization;

namespace CourseProject.Buisness.Exceptions
{
    [Serializable]
    public class JobNotFoundException : Exception
    {
        public int id;

        public JobNotFoundException()
        {
        }

        public JobNotFoundException(int id)
        {
            this.id = id;
        }

        public JobNotFoundException(string? message) : base(message)
        {
        }

        public JobNotFoundException(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        protected JobNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}