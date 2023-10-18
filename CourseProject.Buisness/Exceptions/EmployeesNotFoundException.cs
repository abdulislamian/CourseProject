using System.Runtime.Serialization;

namespace CourseProject.Buisness.Exceptions
{
    [Serializable]
    public class EmployeesNotFoundException : Exception
    {
        public int[] ints { get; }  

        public EmployeesNotFoundException()
        {
        }

        public EmployeesNotFoundException(int[] ints)
        {
            this.ints = ints;
        }

        public EmployeesNotFoundException(string? message) : base(message)
        {
        }

        public EmployeesNotFoundException(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        protected EmployeesNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}