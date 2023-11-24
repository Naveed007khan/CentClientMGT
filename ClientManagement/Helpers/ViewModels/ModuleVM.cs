using ClientManagement.Helpers.ViewModels.DataTable;
using ClientManagement.Helpers.ViewModels.SharedVM;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ClientManagement.Helpers.ViewModels
{
    public class ModuleVM<TClientKey> : BaseVM
        where TClientKey : IEquatable<TClientKey>
    {
        public TClientKey Id { get; set; }
        [DisplayName("Name")]
        [Display(Prompt = "Add Name")]
        [Required(ErrorMessage = "Name is required.")]
        public string Name { get; set; }

        [DisplayName("Description")]
        [Display(Prompt = "Add Description")]
        public string? Description { get; set; }
        [DisplayName("Length Of Permissions Per Module")]
        [Display(Prompt = "Add Length Of Permissions Per Module")]
        [Required(ErrorMessage = "Length Of Permissions Per Module is required.")]
        public int LengthOfPermissionsPerModule { get; set; }


        public int ModuleNo { get; set; }
        public ClientBreifVM<TClientKey> Client { get; set; } = new ClientBreifVM<TClientKey>();
        public List<ModulePermissionVM<TClientKey>> ModulePermissions { get; set; } = new List<ModulePermissionVM<TClientKey>>();
    }
    //public class ModuleBreifVM : BaseBriefVM
    //{

    //}
    public class ModuleSearchVM<TClientKey> : BaseDateSearchModel
        where TClientKey : IEquatable<TClientKey>
    {
        public string Name { get; set; }
        public TClientKey ClientId { get; set; } = default;
        public ClientBreifVM<TClientKey> Tenant { get; set; } = new ClientBreifVM<TClientKey>();
    }
}
