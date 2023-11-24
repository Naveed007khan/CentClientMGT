using ClientManagement.Helpers.ViewModels.InterfaceVM;
using Models.ViewModels.CRUD.Interface;

namespace ClientManagement.Helpers.ViewModels.CRUD
{
    public class ModalPanelViewModel : IUpdateViewModel
    {
        public ModalPanelViewModel()
        {
            FormType = "application/x-www-form-urlencoded";
            FormAction = "Update";
        }
        public string ControllerName { get; set; }
        public string Title { get; set; }
        public string Name { get; set; }
        public string FormType { get; set; }
        public string FormAction { get; set; }
        public string CustomUpdateViewPath { get; set; }
        public string Select2Id { get; set; }
        public string UpdateViewPath
        {
            get
            {
                if (string.IsNullOrEmpty(CustomUpdateViewPath))
                    return $"~/Views/{ControllerName}/_Update.cshtml";
                else
                    return CustomUpdateViewPath;
            }
        }
        public IBaseViewModel UpdateModel { get; set; }
    }
}
