using System;

namespace HeavyCavStudios.Core.Services
{
    public class ServiceNotFoundException : Exception
    {
        public ServiceNotFoundException(string message)
            : base(message)
        {
        }
    }
}
