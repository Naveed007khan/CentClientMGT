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
    public class ModulePermissionService<TIdentityKey, TClientKey> : IdentityBaseService<TIdentityKey, TClientKey, ModulePermissionVM<TClientKey>, ModulePermissionSearchVM<TClientKey>, ModulePermission<TClientKey>>, IModulePermission<TIdentityKey, TClientKey>
        where TClientKey : IEquatable<TClientKey>
        where TIdentityKey : IEquatable<TIdentityKey>
    {
        private readonly ClientContext _db;
        private readonly IMapper _mapper;
        private readonly IRepositoryResponse _response;
        private readonly ILogger<ModulePermissionService<TIdentityKey, TClientKey>> _logger;

        public ModulePermissionService(
            ClientContext db,
            IMapper mapper,
            IRepositoryResponse response,
            ILogger<ModulePermissionService<TIdentityKey, TClientKey>> logger
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
        internal override async Task<ModulePermission<TClientKey>> SetModel(ModulePermissionVM<TClientKey> viewModel)
        {
            try
            {
                ModulePermission<TClientKey> model = viewModel.Id.Equals(default(TClientKey)) ? new ModulePermission<TClientKey>() : await GetModel(viewModel.Id);
                model = _mapper.Map(viewModel, model);
                model.Client = null;
                model.Module = null;
                if (viewModel.Id.Equals(default(TClientKey)))
                {
                    model.Id = GetIdValue();
                    _db.Add(model);
                }
                return model;
            }
            catch (Exception ex)
            {
                _logger.LogError($"ModulePermissionService SetModel method threw an exception, Message: {ex.Message}");
                return null;
            }
        }
        internal override List<string> GetIncludeColumns()
        {

            return new List<string> { "Client", "Module" };
        }
        internal override TClientKey ReturnUpdate(ModulePermission<TClientKey> model)
        {
            return model.Id;
        }

        public override Expression<Func<ModulePermission<TClientKey>, bool>> SetQueryFilter(IBaseSearchModel filters)
        {
            return (
                    p => p.IsDeleted == false
                    &&
                    (filters == null || filters.Search == null || (string.IsNullOrEmpty(filters.Search.value) || p.Name.Contains(filters.Search.value) || p.Module.Name.Contains(filters.Search.value)))
                   );
        }
        #endregion
    }
}
