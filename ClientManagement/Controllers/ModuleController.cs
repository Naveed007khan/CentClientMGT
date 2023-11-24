using Centangle.ClientManager.Entity;
using ClientManagement.Controllers.SharedController;
using ClientManagement.Helpers.Interfaces;
using ClientManagement.Helpers.ViewModels;
using ClientManagement.Helpers.ViewModels.CRUD;
using ClientManagement.Helpers.ViewModels.DataTable;
using ClientManagement.Helpers.ViewModels.Select2;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ClientManagement.Controllers
{
    [Authorize]
    public class ModuleController : IdentityBaseController<Guid, ModuleVM<Guid>, ModuleSearchVM<Guid>, Module<Guid>, IModule<Guid, Guid>>
    {

        public ModuleController(IModule<Guid, Guid> module, ILogger<ModuleController> logger) : base(module, logger, "Module", "Module")
        {
        }
        public override CrudUpdateViewModel GetUpdateViewModel(string action, ModuleVM<Guid> model)
        {
            return SetUpdateViewModel($"{action} Module", model, action, null, "~/Views/Module/_Update.cshtml", "Module-form");
        }
        internal override List<DataTableActionViewModel> GetActions()
        {
            return new List<DataTableActionViewModel>()
                {
                    new DataTableActionViewModel() {Action="Detail",Title="Detail",Href="/Module/Detail/Id"},
                    new DataTableActionViewModel() { Action = "Update", Title = "Update", Href = "/Module/Update/Id" },
                    new DataTableActionViewModel() { Action = "Delete", Title = "Delete", Href = "/Module/Delete/Id" }
                };
        }
        internal override List<DataTableViewModel> GetColumns()
        {
            return new List<DataTableViewModel>()
                {
                    new DataTableViewModel{title = "Id",data = "Id",orderable = true},
                    new DataTableViewModel{title = "Name",data = "Name",orderable = true},
                    new DataTableViewModel{title = "Client",data = "Client.Name"},
                    new DataTableViewModel{title = "Length of permission per module",data = "LengthOfPermissionsPerModule",orderable = true},
                    new DataTableViewModel{title = "Module No",data = "ModuleNo",orderable = true},
                    new DataTableViewModel{title = "Description",data = "Description"},
                    new DataTableViewModel{title = "Action",data = null}
                };
        }
        internal override List<Select2OptionModel<ModuleVM<Guid>>> GetSelect2OptionModel(List<ModuleVM<Guid>> list)
        {
            if (list != null && list.Count() > 0)
            {
                return list.Select(m => new Select2OptionModel<ModuleVM<Guid>>
                {
                    id = m.Id.ToString(),
                    text = m.Name,
                    additionalAttributesModel = m,
                }).OrderBy(m => m.id).ToList();
            }
            return new List<Select2OptionModel<ModuleVM<Guid>>>();
        }
    }
}
