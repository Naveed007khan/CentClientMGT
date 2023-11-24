using System;

namespace ClientManagement.Helpers.ViewModels.SharedVM
{
    public class TenantBreifVM<TClientKey> : BaseBriefVM<TClientKey>
        where TClientKey : IEquatable<TClientKey>
    {
        public TenantBreifVM() : base(true, "The field Tenant is required.") { }
    }
    public class ModuleBreifVM<TClientKey> : BaseBriefVM<TClientKey>
        where TClientKey : IEquatable<TClientKey>
    {
        public ModuleBreifVM() : base(true, "The field Module is required.") { }
    }
    public class ClientBreifVM<TClientKey> : BaseBriefVM<TClientKey>
        where TClientKey : IEquatable<TClientKey>
    {
        public ClientBreifVM() : base(true, "The field Client is required.") { }
    }   
}
