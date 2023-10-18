using System.Runtime.Serialization;

namespace CourseProject.Buisness.Exceptions
{
    [Serializable]
    public class TeamNotFoundException : Exception
    {
        public int id;

        public TeamNotFoundException()
        {
        }

        public TeamNotFoundException(int id)
        {
            this.id = id;
        }

        public TeamNotFoundException(string? message) : base(message)
        {
        }

        public TeamNotFoundException(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        protected TeamNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}