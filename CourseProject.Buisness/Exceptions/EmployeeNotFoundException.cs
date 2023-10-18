using System.Runtime.Serialization;

namespace CourseProject.Buisness.Exceptions
{
    [Serializable]
    public class EmployeeNotFoundException : Exception
    {
        public int id;

        public EmployeeNotFoundException()
        {
        }

        public EmployeeNotFoundException(int id)
        {
            this.id = id;
        }

        public EmployeeNotFoundException(string? message) : base(message)
        {
        }

        public EmployeeNotFoundException(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        protected EmployeeNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}