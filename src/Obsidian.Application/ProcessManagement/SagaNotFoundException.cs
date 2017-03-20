using System;

namespace Obsidian.Application.ProcessManagement
{
    internal class SagaNotFoundException : Exception
    {
        public SagaNotFoundException()
        {
        }

        public SagaNotFoundException(string message) : base(message)
        {
        }

        public SagaNotFoundException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}