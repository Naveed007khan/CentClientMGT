using ClientManagement.Helpers.ViewModels.DataTable;
using ClientManagement.Helpers.ViewModels.SharedVM;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ClientManagement.Helpers.ViewModels
{
    public class ClientVM<TClientKey> : BaseVM
            where TClientKey : IEquatable<TClientKey>
    {
        public TClientKey ClientId { get; set; }
        [DisplayName("Name")]
        [Display(Prompt = "Add Name")]
        [Required(ErrorMessage = "Name is required.")]
        public string Name { get; set; }

        [DisplayName("Description")]
        [Display(Prompt = "Add Description")]
        public string? Description { get; set; }

        public List<TenantVM<TClientKey>>? Tenants { get; set; } = new List<TenantVM<TClientKey>>();
        public List<ModuleVM<TClientKey>>? Modules { get; set; } = new List<ModuleVM<TClientKey>>();
        public List<ModulePermissionVM<TClientKey>>? ModulePermissions { get; set; } = new List<ModulePermissionVM<TClientKey>>();
    }

    public class ClientSearchVM<TClientKey> : BaseDateSearchModel
        where TClientKey : IEquatable<TClientKey>
    {
        public string Name { get; set; }
    }
}
