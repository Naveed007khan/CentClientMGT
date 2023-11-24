using ClientManagement.Helpers.Interfaces.BaseInterfaces;
using ClientManagement.Helpers.ViewModels;
using System;

namespace ClientManagement.Helpers.Interfaces
{
    public interface ITenant<TIdentityKey,TClientKey> : IGeneral<TenantVM<TClientKey>, TClientKey> //, IUpdate<TenantVM<TClientKey>>
            where TIdentityKey : IEquatable<TIdentityKey>
            where TClientKey : IEquatable<TClientKey>
    {
    }
}
