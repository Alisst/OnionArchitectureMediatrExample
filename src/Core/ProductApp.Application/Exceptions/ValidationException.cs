using System;

namespace ProductApp.Application.Exceptions
{
    public class ValidationException : Exception
    {
        public ValidationException(): this("Validation error")
        {
            
        }

        public ValidationException(string message) : base(message)
        {
            
        }

        public ValidationException(Exception ex): this(ex.Message)
        {
            
        }
    }
}