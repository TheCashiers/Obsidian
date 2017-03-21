using System;

namespace Obsidian.Foundation.ProcessManagement
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