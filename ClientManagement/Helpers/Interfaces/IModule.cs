using ClientManagement.Helpers.Interfaces.BaseInterfaces;
using ClientManagement.Helpers.ViewModels;
using System;

namespace ClientManagement.Helpers.Interfaces
{
    public interface IModule<TIdentityKey, TClientKey> : IGeneral<ModuleVM<TClientKey>, TClientKey> //, IUpdate<ModuleVM>
        where TIdentityKey : IEquatable<TIdentityKey>
        where TClientKey : IEquatable<TClientKey>
    {
    }
}
