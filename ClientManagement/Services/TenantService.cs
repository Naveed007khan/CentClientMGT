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
    public class TenantService<TIdentityKey, TClientKey> : IdentityBaseService<TIdentityKey, TClientKey, TenantVM<TClientKey>, TenantSearchVM<TClientKey>, Tenant<TClientKey>>, ITenant<TIdentityKey, TClientKey> //, IUpdate<TenantVM<TClientKey>>
            where TClientKey : IEquatable<TClientKey>
            where TIdentityKey : IEquatable<TIdentityKey>
    {
        private readonly ClientContext _db;
        private readonly IMapper _mapper;
        private readonly IRepositoryResponse _response;
        private readonly ILogger<TenantService<TIdentityKey, TClientKey>> _logger;

        public TenantService(
            ClientContext db,
            IMapper mapper,
            IRepositoryResponse response,
            ILogger<TenantService<TIdentityKey, TClientKey>> logger
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
        internal override async Task<Tenant<TClientKey>> SetModel(TenantVM<TClientKey> viewModel)
        {
            try
            {
                Tenant<TClientKey> model = viewModel.TenantId.Equals(default(TClientKey)) ? new Tenant<TClientKey>() : await GetModel(viewModel.TenantId);
                model = _mapper.Map(viewModel, model);
                model.Client = null;
                if (viewModel.TenantId.Equals(default(TClientKey)))
                {
                    model.TenantId = GetIdValue();
                    _db.Add(model);
                }
                return model;
            }
            catch (Exception ex)
            {
                _logger.LogError($"TenantService SetModel method threw an exception, Message: {ex.Message}");
                return null;
            }
        }
        internal override TClientKey ReturnUpdate(Tenant<TClientKey> model)
        {
            return model.TenantId;
        }
        internal override string GetOrderingColumnName()
        {
            return "TenantId";
        }
        internal override List<string> GetIncludeColumns()
        {

            return new List<string> { "Client" };
        }
        public override Expression<Func<Tenant<TClientKey>, bool>> SetQueryFilter(IBaseSearchModel filters)
        {
            return p => p.IsDeleted == false
            &&
            (filters.Search == null || string.IsNullOrEmpty(filters.Search.value) || p.Name.ToLower().Contains(filters.Search.value.ToLower()));
        }
        #endregion
    }
}
