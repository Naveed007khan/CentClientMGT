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
    public class ModuleService<TIdentityKey, TClientKey> : IdentityBaseService<TIdentityKey, TClientKey, ModuleVM<TClientKey>, ModuleSearchVM<TClientKey>, Module<TClientKey>>, IModule<TIdentityKey, TClientKey>
        where TClientKey : IEquatable<TClientKey>
        where TIdentityKey : IEquatable<TIdentityKey>
    {
        private readonly ClientContext _db;
        private readonly IMapper _mapper;
        private readonly IRepositoryResponse _response;
        private readonly ILogger<ModuleService<TIdentityKey, TClientKey>> _logger;

        public ModuleService(
            ClientContext db,
            IMapper mapper,
            IRepositoryResponse response,
            ILogger<ModuleService<TIdentityKey, TClientKey>> logger
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
        internal override async Task<Module<TClientKey>> SetModel(ModuleVM<TClientKey> viewModel)
        {
            try
            {
                Module<TClientKey> model = viewModel.Id.Equals(default(TClientKey)) ? new Module<TClientKey>() : await GetModel(viewModel.Id);
                model = _mapper.Map(viewModel, model);
                model.Client = null;
                if (viewModel.Id.Equals(default(TClientKey)))
                {
                    model.Id = GetIdValue();
                    _db.Add(model);
                }
                return model;
            }
            catch (Exception ex)
            {
                _logger.LogError($"ModuleService SetModel method threw an exception, Message: {ex.Message}");
                return null;
            }
        }

        internal override TClientKey ReturnUpdate(Module<TClientKey> model)
        {
            return model.Id;
        }
        internal override List<string> GetIncludeColumns()
        {

            return new List<string> { "Client" };
        }
        public override Expression<Func<Module<TClientKey>, bool>> SetQueryFilter(IBaseSearchModel filters)
        {
            return p => p.IsDeleted == false
            &&
            (filters.Search == null || string.IsNullOrEmpty(filters.Search.value) || p.Name.ToLower().Contains(filters.Search.value.ToLower()));
        }
        #endregion
    }
}
