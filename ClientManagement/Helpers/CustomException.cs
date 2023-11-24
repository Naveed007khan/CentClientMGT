using System;

namespace ClientManagement.Helpers
{
    public class CustomException : Exception
    {
        public CustomException(string message)
           : base(message)
        {
        }
    }
}
