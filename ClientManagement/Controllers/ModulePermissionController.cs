using Centangle.ClientManager.Entity;
using Centangle.Common.ResponseHelpers.Models;
using ClientManagement.Controllers.SharedController;
using ClientManagement.Helpers;
using ClientManagement.Helpers.Interfaces;
using ClientManagement.Helpers.ViewModels;
using ClientManagement.Helpers.ViewModels.CRUD;
using ClientManagement.Helpers.ViewModels.DataTable;
using ClientManagement.Helpers.ViewModels.Select2;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClientManagement.Controllers
{
    [Authorize]
    public class ModulePermissionController : IdentityBaseController<Guid, ModulePermissionVM<Guid>, ModulePermissionSearchVM<Guid>, ModulePermission<Guid>, IModulePermission<Guid,Guid>>
    {
        private readonly IModulePermission<Guid,Guid> _modulePermission;
        private readonly ILogger<ModulePermissionController> _logger;

        public ModulePermissionController(
            IModulePermission<Guid,Guid> modulePermission,
            ILogger<ModulePermissionController> logger
            ) : base(modulePermission, logger, "ModulePermission", "Module Permission")
        {
            _modulePermission = modulePermission;
            _logger = logger;
        }
        public override CrudUpdateViewModel GetUpdateViewModel(string action, ModulePermissionVM<Guid> model)
        {
            return SetUpdateViewModel($"{action} ModulePermission", model, action, null, "~/Views/ModulePermission/_Update.cshtml", "ModulePermission-form");
        }
        internal override List<DataTableViewModel> GetColumns()
        {
            return new List<DataTableViewModel>()
                {
                    new DataTableViewModel{title = "Id",data = "Id",orderable = true},
                    new DataTableViewModel{title = "Name",data = "Name",orderable = true},
                    new DataTableViewModel{title = "Module",data = "Module.Name"},
                    new DataTableViewModel{title = "Client",data = "Client.Name"},
                    new DataTableViewModel{title = "Permission Id",data = "PermissionId",orderable = true},
                    new DataTableViewModel{title = "Screen",data = "Screen"},
                    new DataTableViewModel{title = "Description",data = "Description"},
                    new DataTableViewModel{title = "Action",data = null}
                };
        }
        internal override List<DataTableActionViewModel> GetActions()
        {
            return new List<DataTableActionViewModel>()
                {
                    new DataTableActionViewModel() {Action="Detail",Title="Detail",Href="/ModulePermission/Detail/Id"},
                    new DataTableActionViewModel() { Action = "Update", Title = "Update", Href = "/ModulePermission/Update/Id" },
                    new DataTableActionViewModel() { Action = "Delete", Title = "Delete", Href = "/ModulePermission/Delete/Id" }
                };
        }
        internal override List<Select2OptionModel<ModulePermissionVM<Guid>>> GetSelect2OptionModel(List<ModulePermissionVM<Guid>> list)
        {
            if (list != null && list.Count() > 0)
            {
                return list.Select(m => new Select2OptionModel<ModulePermissionVM<Guid>>
                {
                    id = m.Id.ToString(),
                    text = m.Name,
                    additionalAttributesModel = m,
                }).OrderBy(m => m.id).ToList();
            }
            return new List<Select2OptionModel<ModulePermissionVM<Guid>>>();
        }
    }
}
