using Centangle.ClientManager.Entity;
using ClientManagement.Controllers.SharedController;
using ClientManagement.Helpers.Interfaces;
using ClientManagement.Helpers.ViewModels;
using ClientManagement.Helpers.ViewModels.CRUD;
using ClientManagement.Helpers.ViewModels.DataTable;
using ClientManagement.Helpers.ViewModels.Select2;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ClientManagement.Controllers
{
    [Authorize]
    public class ClientController : IdentityBaseController<Guid, ClientVM<Guid>, ClientSearchVM<Guid>, Client<Guid>, IClient<Guid, Guid>>
    {
        public ClientController(IClient<Guid, Guid> clientService, ILogger<ClientController> logger) : base(clientService, logger, "Client", "Client")
        {

        }
        public override CrudUpdateViewModel GetUpdateViewModel(string action, ClientVM<Guid> model)
        {
            return SetUpdateViewModel($"{action} Client", model, action, null, "~/Views/Client/_Update.cshtml", "client-form");
        }
        internal override List<DataTableViewModel> GetColumns()
        {
            return new List<DataTableViewModel>()
                {
                    new DataTableViewModel{title = "Id",data = "ClientId",orderable = true},
                    new DataTableViewModel{title = "Name",data = "Name"},
                    new DataTableViewModel{title = "Description",data = "Description"},
                    new DataTableViewModel{title = "Action",data = null}
                };
        }
        internal override List<DataTableActionViewModel> GetActions()
        {
            return new List<DataTableActionViewModel>()
                {
                    new DataTableActionViewModel() {Action="Detail",Title="Detail",Href=$"/Client/Detail/ClientId"},
                    new DataTableActionViewModel() { Action = "Update", Title = "Update", Href = $"/Client/Update/ClientId" },
                    new DataTableActionViewModel() { Action = "Delete", Title = "Delete", Href = $"/Client/Delete/ClientId" }
                };
        }
        internal override List<Select2OptionModel<ClientVM<Guid>>> GetSelect2OptionModel(List<ClientVM<Guid>> list)
        {
            if (list != null && list.Count() > 0)
            {
                return list.Select(m => new Select2OptionModel<ClientVM<Guid>>
                {
                    id = m.ClientId.ToString(),
                    text = m.Name,
                    additionalAttributesModel = m,
                }).OrderBy(m => m.id).ToList();
            }
            return new List<Select2OptionModel<ClientVM<Guid>>>();
        }
    }
}
