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
    public class BranchController : IdentityBaseController<Guid, BranchVM<Guid>, BranchSearchVM<Guid>, Branch<Guid>, IBranch<Guid, Guid>>
    {
        public BranchController(
            IBranch<Guid, Guid> branch,
            ILogger<BranchController> logger
            ) : base(branch, logger, "Branch", "Branch")
        {
        }
        public override CrudUpdateViewModel GetUpdateViewModel(string action, BranchVM<Guid> model)
        {
            return SetUpdateViewModel($"{action} Branch", model, action, null, "~/Views/Branch/_Update.cshtml", "Branch-form");
        }
        internal override List<DataTableViewModel> GetColumns()
        {
            return new List<DataTableViewModel>()
                {
                    new DataTableViewModel{title = "Id",data = "BranchId",orderable = true},
                    new DataTableViewModel{title = "Name",data = "Name",orderable = true},
                    new DataTableViewModel{title = "Tenant",data = "Tenant.Name",orderable = true},
                    new DataTableViewModel{title = "Description",data = "Description"},
                    new DataTableViewModel{title = "Action",data = null}
                };
        }
        internal override List<DataTableActionViewModel> GetActions()
        {
            return new List<DataTableActionViewModel>()
                {
                    new DataTableActionViewModel() {Action="Detail",Title="Detail",Href="/Branch/Detail/BranchId"},
                    new DataTableActionViewModel() { Action = "Update", Title = "Update", Href = "/Branch/Update/BranchId" },
                    new DataTableActionViewModel() { Action = "Delete", Title = "Delete", Href = "/Branch/Delete/BranchId" }
                };
        }
        internal override List<Select2OptionModel<BranchVM<Guid>>> GetSelect2OptionModel(List<BranchVM<Guid>> list)
        {
            if(list != null && list.Count() > 0)
            {
                return list.Select(m => new Select2OptionModel<BranchVM<Guid>>
                {
                    id = m.BranchId.ToString(),
                    text = m.Name,
                    additionalAttributesModel = m,
                }).OrderBy(m => m.id).ToList();
            }
            return new List<Select2OptionModel<BranchVM<Guid>>>();
        }
    }
}
