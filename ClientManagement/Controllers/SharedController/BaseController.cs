using Centangle.Common.Pagination.Models;
using ClientManagement.Helpers.ViewModels.CRUD;
using ClientManagement.Helpers.ViewModels.InterfaceVM;
using ClientManagement.Helpers.ViewModels.SharedVM;
using Microsoft.AspNetCore.Mvc;

namespace ClientManagement.Controllers.SharedController
{
    public class BaseController<TClientKey> : Controller where TClientKey : IEquatable<TClientKey>
    {
        public BaseController()
        {

        }
        public DatatablePaginatedResultVM<T> ConvertToDataTableModel<T>(PaginatedResultModel<T> model, IBaseSearchModel searchModel) where T : new()
        {
            return new DatatablePaginatedResultVM<T>(searchModel.Draw, model._meta.TotalCount, model.Items);
        }
        public ActionResult DataTableIndexView(CrudListViewModel<TClientKey> vm)
        {
            return View("~/Views/Shared/Crud/ListView/DataTable/_Index.cshtml", vm);
        }
        public ActionResult UpdateView(CrudUpdateViewModel vm)
        {
            return View("~/Views/Shared/Crud/UpdateView/_UpdateForm.cshtml", vm);
        }
        protected CrudUpdateViewModel SetUpdateViewModel<T>(string title, T updateModel, string formAction = null, string formType = null, string updateViewPath = "", string formId = "") where T : IBaseViewModel, new()
        {
            CrudUpdateViewModel vm = new CrudUpdateViewModel();
            vm.Title = title;
            if (!string.IsNullOrEmpty(formType))
            {
                vm.FormType = formType;
            }
            if (!string.IsNullOrEmpty(formAction))
            {
                vm.FormAction = formAction;
            }
            if (!string.IsNullOrEmpty(updateViewPath))
            {
                vm.UpdateViewPath = updateViewPath;
            }
            if (!string.IsNullOrEmpty(formId))
            {
                vm.FormId = formId;
            }
            vm.UpdateModel = (updateModel == null ? new T() : updateModel);
            return vm;

        }
    }
}
