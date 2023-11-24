using ClientManagement.Helpers.ViewModels.DataTable;
using ClientManagement.Helpers.ViewModels.SharedVM;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ClientManagement.Helpers.ViewModels
{
    public class ModulePermissionVM<TClientKey> : BaseVM
        where TClientKey : IEquatable<TClientKey>
    {
        public TClientKey Id { get; set; }
        [DisplayName("Permission Id")]
        [Display(Prompt = "Add Permission Id")]
        [Required(ErrorMessage = "Permission Id is required.")]
        public long PermissionId { get; set; }
        [DisplayName("Name")]
        [Display(Prompt = "Add Name")]
        [Required(ErrorMessage = "Name is required.")]
        public string Name { get; set; }
        [DisplayName("Screen")]
        [Display(Prompt = "Add Screen")]
        public string? Screen { get; set; }
        [DisplayName("Description")]
        [Display(Prompt = "Add Description")]
        public string? Description { get; set; }

        public ModuleBreifVM<TClientKey> Module { get; set; } = new ModuleBreifVM<TClientKey>();

        public ClientBreifVM<TClientKey> Client { get; set; } = new ClientBreifVM<TClientKey>();
    }
    public class ModulePermissionSearchVM<TClientKey> : BaseDateSearchModel where TClientKey : IEquatable<TClientKey>   
    {
        public string Name { get; set; }
        public TClientKey ModuleId { get; set; } = default;
        public ModuleBreifVM<TClientKey> Module { get; set; } = new ModuleBreifVM<TClientKey>();
        public Guid? ClientId { get; set; } = new Guid();
        public ClientBreifVM<TClientKey> Client { get; set; } = new ClientBreifVM<TClientKey>();
    }
}
