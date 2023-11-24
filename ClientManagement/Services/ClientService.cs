using AutoMapper;
using Centangle.ClientManager.Entity;
using Centangle.Common.Pagination.Models;
using Centangle.Common.ResponseHelpers.Models;
using ClientManagement.Contexts;
using ClientManagement.Helpers.Interfaces;
using ClientManagement.Helpers.ViewModels;
using System.Linq.Expressions;

namespace ClientManagement.Services
{
    public class ClientService<TIdentityKey, TClientKey> : IdentityBaseService<TIdentityKey, TClientKey, ClientVM<TClientKey>, ClientSearchVM<TClientKey>, Client<TClientKey>>, IClient<TIdentityKey, TClientKey>
         where TClientKey : IEquatable<TClientKey>
        where TIdentityKey : IEquatable<TIdentityKey>
    {
        private readonly ClientContext _db;
        private readonly IMapper _mapper;
        private readonly IRepositoryResponse _response;
        private readonly ILogger<ClientService<TIdentityKey, TClientKey>> _logger;

        public ClientService(
            ClientContext db,
            IMapper mapper,
            IRepositoryResponse response,
            ILogger<ClientService<TIdentityKey, TClientKey>> logger
            ) : base(db, mapper, response, logger)
        {
            _db = db;
            _mapper = mapper;
            _response = response;
            _logger = logger;
        }


        #region CRUD methods

        #endregion
        #region helping methods
        internal override async Task<Client<TClientKey>> SetModel(ClientVM<TClientKey> viewModel)
        {
            try
            {
                Client<TClientKey> model = viewModel.ClientId.Equals(default) ? new Client<TClientKey>() : await GetModel(viewModel.ClientId);
                model = _mapper.Map(viewModel, model);
                if (viewModel.ClientId.Equals(default))
                {
                    model.ClientId = GetIdValue();
                    _db.Add(model);
                }
                return model;
            }
            catch (Exception ex)
            {
                _logger.LogError($"ClientService SetModel method threw an exception, Message: {ex.Message}");
                return null;
            }
        }
        internal override string GetOrderingColumnName()
        {
            return "ClientId";
        }
        internal override TClientKey ReturnUpdate(Client<TClientKey> model)
        {
            return model.ClientId;
        }
        public override Expression<Func<Client<TClientKey>, bool>> SetQueryFilter(IBaseSearchModel filters)
        {
            return p => p.IsDeleted == false
            &&
            (filters.Search == null || string.IsNullOrEmpty(filters.Search.value) || p.Name.ToLower().Contains(filters.Search.value.ToLower()));
        }
        #endregion
    }
}
