using ClientManagement.Helpers.ViewModels.InterfaceVM;
using Models.ViewModels.CRUD.Interface;

namespace ClientManagement.Helpers.ViewModels.CRUD
{
    public class CrudUpdateViewModel: IUpdateViewModel
    {
        public CrudUpdateViewModel()
        {
            // FormType = "application/x-www-form-urlencoded";
            FormType = "multipart/form-data";
            FormAction = "Update";
            UpdateViewPath = "_Update";
        }
        public string Title { get; set; }
        public string Name { get; set; }
        public string FormId { get; set; }
        public string FormType { get; set; }
        public string FormAction { get; set; }
        public string UpdateViewPath { get; set; }
        public IBaseViewModel UpdateModel { get; set; }
    }
}
