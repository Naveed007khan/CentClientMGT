using ClientManagement.Helpers.Interfaces.BaseInterfaces;
using ClientManagement.Helpers.ViewModels;
using System;

namespace ClientManagement.Helpers.Interfaces
{
    public interface IClient<TIdentityKey,TClientKey> : IGeneral<ClientVM<TClientKey>, TClientKey>
        where TIdentityKey : IEquatable<TIdentityKey>
        where TClientKey : IEquatable<TClientKey> //, IUpdate<ClientVM>
    {

    }
}
