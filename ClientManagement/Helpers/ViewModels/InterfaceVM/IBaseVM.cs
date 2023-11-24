using System;

namespace ClientManagement.Helpers.ViewModels.InterfaceVM
{
    public interface IBaseViewModel
    {
        bool IsDeleted { get; set; }
        bool IsActive { get; set; }
        Guid CreatedBy { get; set; }
        DateTime CreatedOn { get; set; }
        Guid UpdatedBy { get; set; }
        DateTime UpdatedOn { get; set; }
    }
   
}
