using Obsidian.Application.OAuth20;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Obsidian.Application.ProcessManagement
{
    public static class SagaBusExtensions
    {
        public static void RegisterSagas(this SagaBus bus)
        {
            bus.Register<OAuth20Saga>();
        }
    }
}
