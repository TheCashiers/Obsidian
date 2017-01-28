using Obsidian.Application.ClientManagement;
using Obsidian.Application.OAuth20;
using Obsidian.Application.ScopeManagement;
using Obsidian.Application.UserManagement;
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
            bus.Register<CreateUserSaga>();
            bus.Register<CreateClientSaga>();
            bus.Register<CreateScopeSaga>();
            bus.Register<UpdateUserSaga>();
            bus.Register<UpdateClientSaga>();
            bus.Register<UpdateScopeSaga>();
        }
    }
}
