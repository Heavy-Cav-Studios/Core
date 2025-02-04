using System;

namespace HeavyCavStudios.Core.Services
{
    public class ServiceAlreadyRegisteredException : Exception
    {
        public ServiceAlreadyRegisteredException(string message)
            : base(message)
        {
        }
    }
}
