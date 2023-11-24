using ClientManagement.Helpers.ViewModels.InterfaceVM;
using System;

namespace ClientManagement.Helpers.ViewModels.SharedVM
{
    public class BaseVM : IBaseViewModel
    {
      //  public Guid Id { get; set; }
        public string Action { get; set; }
        public Guid CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; } = DateTime.Now;
        public Guid UpdatedBy { get; set; }
        public DateTime UpdatedOn { get; set; } = DateTime.Now;
        public bool IsDeleted { get; set; }
        public bool IsActive { get; set; } = true;
        public bool IsApproved { get; set; }
    }
}
