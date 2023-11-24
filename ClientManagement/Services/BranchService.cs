using AutoMapper;
using Centangle.ClientManager.Entity;
using Centangle.Common.ResponseHelpers.Models;
using ClientManagement.Contexts;
using ClientManagement.Helpers.Interfaces;
using ClientManagement.Helpers.ViewModels;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace ClientManagement.Services
{
    public class BranchService<TIdentityKey, TClientKey> : IdentityBaseService<TIdentityKey, TClientKey, BranchVM<TClientKey>, BranchSearchVM<TClientKey>, Branch<TClientKey>>, IBranch<TIdentityKey, TClientKey>
        where TClientKey : IEquatable<TClientKey>
        where TIdentityKey : IEquatable<TIdentityKey>
    {
        private readonly ClientContext _db;
        private readonly IMapper _mapper;
        private readonly IRepositoryResponse _response;
        private readonly ILogger<BranchService<TIdentityKey, TClientKey>> _logger;

        public BranchService(
            ClientContext db,
            IMapper mapper,
            IRepositoryResponse response,
            ILogger<BranchService<TIdentityKey, TClientKey>> logger
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
        internal override async Task<Branch<TClientKey>> SetModel(BranchVM<TClientKey> viewModel)
        {
            try
            {
                Branch<TClientKey> model = viewModel.BranchId.Equals(default(TClientKey)) ? new Branch<TClientKey>() : await GetModel(viewModel.BranchId);
                model = _mapper.Map(viewModel, model);
                model.Tenant = null;
                if (viewModel.BranchId.Equals(default(TClientKey)))
                {
                    model.BranchId = GetIdValue();
                    _db.Add(model);
                }
                return model;
            }
            catch (Exception ex)
            {
                _logger.LogError($"BranchService SetModel method threw an exception, Message: {ex.Message}");
                return null;
            }
        }
        internal override TClientKey ReturnUpdate(Branch<TClientKey> model)
        {
            return model.BranchId;
        }
        internal override List<string> GetIncludeColumns()
        {

            return new List<string> { "Tenant" };
        }
        internal override string GetOrderingColumnName()
        {
            return "BranchId";
        }
        #endregion
    }
}
