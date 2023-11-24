using Centangle.Base.Interface;
using Centangle.Common.Pagination.Models;
using Centangle.Common.ResponseHelpers.Models;
using ClientManagement.Helpers;
using ClientManagement.Helpers.Interfaces.BaseInterfaces;
using ClientManagement.Helpers.ViewModels;
using ClientManagement.Helpers.ViewModels.CRUD;
using ClientManagement.Helpers.ViewModels.DataTable;
using ClientManagement.Helpers.ViewModels.Select2;
using ClientManagement.Helpers.ViewModels.SharedVM;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace ClientManagement.Controllers.SharedController
{
    public abstract class IdentityBaseController<TClientKey, VM, SVM, TModel, IService>: BaseController<TClientKey>
            where TClientKey : IEquatable<TClientKey>
            where VM : BaseVM, new()
            where SVM : BaseSearchModel, new()
            where TModel : class, IDeleted, IModifier<TClientKey>, new()
            where IService : IGeneral<VM, TClientKey>
    {
        private readonly IService _service;
        private readonly ILogger _logger;
        private readonly string _controllerName;
        private readonly string _title;

        public IdentityBaseController(
            IService service,
            ILogger logger,
            string controllerNamer,
            string title
            )
        {
            _service = service;
            _logger = logger;
            _controllerName = controllerNamer;
            _title = title;
        }
        public virtual ActionResult Index()
        {
            var vm = new CrudListViewModel<TClientKey>
            {
                Title = $"{_title}",
                ControllerName = $"{_controllerName}",
                Filters = new SVM(),
                DatatableColumns = GetColumns(),
                DisableSearch = false,
                HideCreateButton = false,
                DataUrl = $"/{_controllerName}/Search",
                SearchViewPath = $"~/Views/{_controllerName}/_Search.cshtml"
            };
            return DataTableIndexView(vm);
        }
        internal abstract List<DataTableViewModel> GetColumns();
        public async virtual Task<JsonResult> Search(BaseSearchModel searchModel)
        {
            try
            {
                var paginatedResult = await _service.GetAll<VM>(searchModel);

                var serializedParent = JsonConvert.SerializeObject(paginatedResult);
                var check = JsonConvert.DeserializeObject<RepositoryResponseWithModel<PaginatedResultModel<TModel>>>(serializedParent);

                var result = ConvertToDataTableModel(check.ReturnModel, searchModel);
                result.ActionsList = GetActions();
                return Json(result);
            }
            catch (Exception ex)
            {
                _logger.LogError($"{_controllerName} Search method threw an exception, Message: {ex.Message}");
                return null;
            }
        }
        internal abstract List<DataTableActionViewModel> GetActions();
        public virtual IActionResult Create()
        {
            return UpdateView(GetUpdateViewModel("Update", null));
        }
        public async virtual Task<ActionResult> Update(TClientKey id)
        {
            try
            {
                var model = await _service.GetById(id);
                if (model != null)
                {
                    model.Action = "Update";
                    return UpdateView(GetUpdateViewModel("Update", model));
                }
                else
                {
                    _logger.LogInformation($"{_controllerName} with id " + id + "not found");
                    return RedirectToAction("Index");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"{_controllerName} GetById method threw an exception, Message: {ex.Message}");
                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        //[ValidateAntiForgeryToken]
        public async virtual Task<ActionResult> Update(VM model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (await _service.Update(model, ModelState) != null)
                    {
                        _logger.LogInformation($"{_controllerName} Created/Updated Successfully at " + DateTime.Now);
                        return RedirectToAction("Index");
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"{_controllerName} Update method threw an exception, Message: {ex.Message}");
                WebException.HandleException(ex, ModelState);
            }
            return UpdateView(GetUpdateViewModel("Update", model));
        }

        public async virtual Task<ActionResult> Delete(TClientKey id)
        {
            try
            {
                if (await _service.Delete(id))
                {
                    _logger.LogInformation($"{_controllerName} Deleted Successfully at " + DateTime.Now);
                    return RedirectToAction("Index");
                }
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                _logger.LogError($"{_controllerName} Delete method threw an exception, Message: {ex.Message}");
                return RedirectToAction("Index");
            }
        }

        [HttpGet]
        public async virtual Task<ActionResult> Detail(TClientKey id)
        {
            var model = await _service.GetById(id);
            ViewData["Action"] = "Detail";
            return View($"~/Views/{_controllerName}/Detail.cshtml", model);
        }
        public async virtual Task<JsonResult> GetItemsForSelect2(string prefix, int pageSize, int pageNumber)
        {
            try
            {
                var items = await _service.GetAll<ClientVM<Guid>>(new BaseSearchModel
                {
                    PerPage = pageSize,
                    CalculateTotal = true,
                    CurrentPage = pageNumber,
                    Search = new DataTableSearchViewModel() { value = prefix },
                });
                var paginatedModel = items as RepositoryResponseWithModel<PaginatedResultModel<VM>>;
                var select2List = GetSelect2OptionModel(paginatedModel.ReturnModel.Items);
                return Json(new Select2Repository().GetSelect2PagedResult(pageSize, pageNumber, select2List));
            }
            catch (Exception ex)
            {
                _logger.LogError($"Client GetItemsForSelect2 method threw an exception, Message: {ex.Message}");
                return null;
            }
        }
        internal abstract List<Select2OptionModel<VM>> GetSelect2OptionModel(List<VM> list);

        public virtual CrudUpdateViewModel GetUpdateViewModel(string action, VM model)
        {
            return SetUpdateViewModel($"{action} Client", model, action, null, "~/Views/Client/_Update.cshtml", "client-form");
        }
    }
}