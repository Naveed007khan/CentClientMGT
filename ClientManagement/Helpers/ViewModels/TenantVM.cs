using ClientManagement.Helpers.ViewModels.DataTable;
using ClientManagement.Helpers.ViewModels.SharedVM;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ClientManagement.Helpers.ViewModels
{
    public class TenantVM<TClientKey> : BaseVM
        where TClientKey : IEquatable<TClientKey>
    {
        public TClientKey TenantId { get; set; }
        [DisplayName("Name")]
        [Display(Prompt = "Add Name")]
        [Required(ErrorMessage = "Name is required.")]
        public string Name { get; set; }
        [DisplayName("Host")]
        [Display(Prompt = "Add Host")]
        public string Host { get; set; }
        [DisplayName("Folder")]
        [Display(Prompt = "Add Folder")]
        public string? Folder { get; set; }
        [DisplayName("Description")]
        [Display(Prompt = "Add Description")]
        public string? Description { get; set; }

        public ClientBreifVM<TClientKey> Client { get; set; } = new ClientBreifVM<TClientKey>();

        public List<BranchVM<TClientKey>> Branches { get; set; } = new List<BranchVM<TClientKey>>();

    }

    public class TenantSearchVM<TClientKey> : BaseDateSearchModel
        where TClientKey : IEquatable<TClientKey>
    {
        public string Name { get; set; }
        public Guid? ClientId { get; set; } = new Guid();
        public ClientBreifVM<TClientKey> Client { get; set; } = new ClientBreifVM<TClientKey>();
    }
}
