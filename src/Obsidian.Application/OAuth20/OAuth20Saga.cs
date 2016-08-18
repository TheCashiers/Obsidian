using Obsidian.Application.ProcessManagement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Obsidian.Application.OAuth20
{
    public class OAuth20Saga : Saga,
        IStartsWith<AuthorizeCommand, AuthorizeResult>,
        IHandlerOf<SignInMessage, SignInResult>,
        IHandlerOf<PermissionGrantMessage, PermissionGrantResult>,
        IHandlerOf<AccessTokenRequestMessage, AccessTokenResult>
    {
        public Task<AuthorizeResult> StartAsync(AuthorizeCommand command)
        {
            throw new NotImplementedException();
        }

        #region SignIn

        public bool ShouldHandle(SignInMessage message)
        {
            throw new NotImplementedException();
        }

        public Task<SignInResult> HandleAsync(SignInMessage message)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region Permission Grant

        public bool ShouldHandle(PermissionGrantMessage message)
        {
            throw new NotImplementedException();
        }

        public Task<PermissionGrantResult> HandleAsync(PermissionGrantMessage message)
        {
            throw new NotImplementedException();
        }
        #endregion

        #region Access Token Request

        public bool ShouldHandle(AccessTokenRequestMessage message)
        {
            throw new NotImplementedException();
        }

        public Task<AccessTokenResult> HandleAsync(AccessTokenRequestMessage message)
        {
            throw new NotImplementedException();
        }

        #endregion

        protected override bool IsProcessCompleted()
        {
            return false;
        }


    }
}
