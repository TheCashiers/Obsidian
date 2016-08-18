using Obsidian.Application.ProcessManagement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Obsidian.Application.OAuth20
{
    public class OAuth20Saga : Saga,
        IStartsWith<AuthorizeCommand,Guid>, 
        IHandlerOf<SignInMessage,SignInResult>,
        IHandlerOf<PermissionGrantMessage,PermissionGrantResult>,
        IHandlerOf<AccessTokenRequestMessage,AccessTokenResult>
    {
        public bool ShouldHandle(PermissionGrantMessage message)
        {
            throw new NotImplementedException();
        }

        public bool ShouldHandle(SignInMessage message)
        {
            throw new NotImplementedException();
        }

        public Task<PermissionGrantResult> HandleAsync(PermissionGrantMessage message)
        {
            throw new NotImplementedException();
        }

        public Task<SignInResult> HandleAsync(SignInMessage message)
        {
            throw new NotImplementedException();
        }

        public Task<Guid> StartAsync(AuthorizeCommand command)
        {
            throw new NotImplementedException();
        }

        protected override bool IsProcessCompleted()
        {
            return false;
        }

        public bool ShouldHandle(AccessTokenRequestMessage message)
        {
            throw new NotImplementedException();
        }

        public Task<AccessTokenResult> HandleAsync(AccessTokenRequestMessage message)
        {
            throw new NotImplementedException();
        }
    }
}
