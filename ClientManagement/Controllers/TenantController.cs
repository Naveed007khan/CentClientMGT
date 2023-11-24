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
    public class TenantController : IdentityBaseController<Guid, TenantVM<Guid>, TenantSearchVM<Guid>, Tenant<Guid>, ITenant<Guid, Guid>>
    {
        private readonly ITenant<Guid, Guid> _tenant;
        private readonly ILogger<TenantController> _logger;

        public TenantController(
            ITenant<Guid, Guid> tenant,
            ILogger<TenantController> logger
            ) : base(tenant, logger, "Tenant", "Tenant")
        {
            _tenant = tenant;
            _logger = logger;
        }
        internal override List<DataTableViewModel> GetColumns()
        {
            return new List<DataTableViewModel>()
                {
                    new DataTableViewModel{title = "Tenant Id",data = "TenantId",orderable = true},
                    new DataTableViewModel{title = "Name",data = "Name",orderable = true},
                    new DataTableViewModel{title = "Client",data = "Client.Name"},
                    new DataTableViewModel{title = "Action",data = null}
                };
        }
        internal override List<DataTableActionViewModel> GetActions()
        {
            return new List<DataTableActionViewModel>()
                {
                    new DataTableActionViewModel() {Action="Detail",Title="Detail",Href="/Tenant/Detail/TenantId"},
                    new DataTableActionViewModel() { Action = "Update", Title = "Update", Href = "/Tenant/Update/TenantId" },
                    new DataTableActionViewModel() { Action = "Delete", Title = "Delete", Href = "/Tenant/Delete/TenantId" }
                };
        }
        internal override List<Select2OptionModel<TenantVM<Guid>>> GetSelect2OptionModel(List<TenantVM<Guid>> list)
        {
            if (list != null && list.Count() > 0)
            {
                return list.Select(m => new Select2OptionModel<TenantVM<Guid>>
                {
                    id = m.TenantId.ToString(),
                    text = m.Name,
                    additionalAttributesModel = m,
                }).OrderBy(m => m.id).ToList();
            }
            return new List<Select2OptionModel<TenantVM<Guid>>>();
        }
        public override CrudUpdateViewModel GetUpdateViewModel(string action, TenantVM<Guid> model)
        {
            return SetUpdateViewModel($"{action} Tenant", model, action, null, "~/Views/Tenant/_Update.cshtml", "Tenant-form");
        }
    }
}
