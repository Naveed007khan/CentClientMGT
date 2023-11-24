using ClientManagement.Helpers.ViewModels.DataTable;
using ClientManagement.Helpers.ViewModels.SharedVM;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ClientManagement.Helpers.ViewModels
{
    public class BranchVM<TClientKey> : BaseVM where TClientKey : IEquatable<TClientKey>
    {
        public TClientKey BranchId { get; set; }
        [DisplayName("Name")]
        [Display(Prompt = "Add Name")]
        [Required(ErrorMessage = "Name is required.")]
        public string Name { get; set; }

        [DisplayName("Description")]
        [Display(Prompt = "Add Description")]
        public string? Description { get; set; }
        public TenantBreifVM<TClientKey> Tenant { get; set; } = new TenantBreifVM<TClientKey>();
    }
    public class BranchSearchVM<TClientKey> : BaseDateSearchModel where TClientKey : IEquatable<TClientKey>
    {
        public string Name { get; set; }
        public TClientKey TenantId { get; set; } = default;
        public TenantBreifVM<TClientKey> Tenant { get; set; } = new TenantBreifVM<TClientKey>();
    }
}
