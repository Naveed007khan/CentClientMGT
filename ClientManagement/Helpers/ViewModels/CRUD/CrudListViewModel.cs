using Centangle.Common.Pagination.Models;
using ClientManagement.Helpers.ViewModels.DataTable;
using ClientManagement.Helpers.ViewModels.SharedVM;
using System.Text.Json;

namespace ClientManagement.Helpers.ViewModels.CRUD
{
    public class CrudListViewModel<TClientKey> where TClientKey : IEquatable<TClientKey>
    {
        public CrudListViewModel()
        {
            SearchViewPath = "_Search";
        }
        public string Title { get; set; }
        public List<DataTableViewModel> DatatableColumns { get; set; }
        public IBaseSearchModel Filters { get; set; }
        public string DatatableColumnsJson
        {
            get
            {
                return JsonSerializer.Serialize(DatatableColumns);
            }
        }
        public BaseBriefVM<TClientKey> Parent { get; set; }
        public string ParentUrl { get; set; }
        public string DataUrl { get; set; }
        public string SearchViewPath { get; set; }
        //public DisplayOptionsCatalog DisplayOptions { get; set; } = DisplayOptionsCatalog.None;
        public bool HideCreateButton { get; set; } = true;
        public bool HideSearchFiltersButton { get; set; } = false;
        public string CreateButtonAction { get; set; } = "Create";
        public string CreateButtonTitle { get; set; } = "";
        public bool DisableSearch { get; set; } = true;
        public string ActionName { get; set; }
        public string ControllerName { get; set; }
    }
}
