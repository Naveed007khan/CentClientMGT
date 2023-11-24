using ClientManagement.Helpers.Interfaces.BaseInterfaces;
using ClientManagement.Helpers.ViewModels;
using System;

namespace ClientManagement.Helpers.Interfaces
{
    public interface IModulePermission<TIdentityKey, TClientKey> : IGeneral<ModulePermissionVM<TClientKey>, TClientKey>
        where TIdentityKey : IEquatable<TIdentityKey>
        where TClientKey : IEquatable<TClientKey>  //, IUpdate<ModulePermissionVM>
    {
    }
}
