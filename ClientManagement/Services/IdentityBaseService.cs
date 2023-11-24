using AutoMapper;
using Centangle.Base.Interface;
using Centangle.Common.Pagination;
using Centangle.Common.Pagination.Models;
using Centangle.Common.ResponseHelpers;
using Centangle.Common.ResponseHelpers.Models;
using Centangle.DatabaseHelper.EntityFrameworkRepository.Pagination;
using ClientManagement.Contexts;
using ClientManagement.Helpers;
using ClientManagement.Helpers.Interfaces.BaseInterfaces;
using ClientManagement.Helpers.ViewModels.SharedVM;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Linq.Expressions;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace ClientManagement.Services
{
    public abstract class IdentityBaseService<TIdentityKey, TClientKey, VM, SVM, TEntity> : IGeneral<VM, TClientKey>
        where TClientKey : IEquatable<TClientKey>
        where TIdentityKey : IEquatable<TIdentityKey>
        where VM : BaseVM, new()
        where SVM : BaseSearchModel
        where TEntity : class, IDeleted, IModifier<TClientKey>,  new()
    {
        private readonly ClientContext _db;
        private readonly IMapper _mapper;
        private readonly IRepositoryResponse _response;
        private readonly ILogger _logger;
        public IdentityBaseService(
            ClientContext db,
            IMapper mapper,
            IRepositoryResponse response,
            ILogger logger)
        {
            _db = db;
            _mapper = mapper;
            _response = response;
            _logger = logger;
        }
        #region CRUD methods
        public virtual async Task<TClientKey> Update(VM viewModel, ModelStateDictionary modelState)
        {
            try
            {
                var model = await SetModel(viewModel);
                if (model != null)
                {
                    await _db.SaveChangesAsync();
                    return ReturnUpdate(model);
                }
                return default;

            }
            catch (Exception ex)
            {
                string errorMessage = ex.InnerException.Message;
                Errors.AddGenericErrorIfRequired(errorMessage, modelState);
                _logger.LogError($"ClientService Update method threw an exception, Message: {ex.Message}");
                return default;
            }
        }
        public async Task<bool> Delete(TClientKey id)
        {
            var model = await GetModel(id);
            model.IsDeleted = true;
            return await _db.SaveChangesAsync() > 0;
        }

        public async Task<VM> GetById(TClientKey id)
        {
            try
            {
                var model = await _db.Set<TEntity>().FindAsync(id);
                foreach (var path in GetIncludeColumns())
                {
                    _db.Entry(model).Reference(path).Load();
                }
                return _mapper.Map<VM>(model);
            }
            catch (Exception ex)
            {
                _logger.LogError($"ClientService GetById method threw an exception, Message: {ex.Message}");
                return new VM();
            }
        }

        public async Task<IRepositoryResponse> GetAll<T>(BaseSearchModel searchModel)
        {
            try
            {
                var serializedParent = JsonConvert.SerializeObject(searchModel);
                SVM searchFilter = JsonConvert.DeserializeObject<SVM>(serializedParent);
                var filters = SetQueryFilter(searchFilter);
                var result = await FindAsync(filters, searchFilter);
                if (result != null)
                {
                    var response = new RepositoryResponseWithModel<PaginatedResultModel<VM>>();
                    response.ReturnModel.Items = _mapper.Map<List<VM>>(result.Items.ToList());
                    return response;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"ClientService GetAll method threw an exception, Message: {ex.Message}");
                return Response.BadRequestResponse(_response);
            }
            return Response.NotFoundResponse(_response);
        }

        #endregion
        #region helping methods
        internal abstract TClientKey ReturnUpdate(TEntity model);
        internal abstract Task<TEntity> SetModel(VM viewModel);

        public async Task<PaginatedResultModel<TEntity>> FindAsync(Expression<Func<TEntity, bool>> expression, IBaseSearchModel search)
        {
            try
            {
                search.OrderByColumn = GetOrderingColumnName();
                search.OrderDir = PaginationOrderCatalog.Desc;
                var dbQuery = _db.Set<TEntity>().AsQueryable();
                foreach (var includeColumn in GetIncludeColumns())
                {
                    dbQuery = dbQuery.Include(includeColumn).AsQueryable();
                }
                return await dbQuery.Where(expression).Paginate(search);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"FindAsync() in Generic Repo threw an Exception");
                throw;
            }
        }
        public virtual Expression<Func<TEntity, bool>> SetQueryFilter(IBaseSearchModel filters)
        {
            return p => p.IsDeleted == false;
        }

        public static TClientKey GetIdValue()
        {
            if (typeof(TClientKey) == typeof(Guid))
            {
                return (TClientKey)Convert.ChangeType(Guid.NewGuid(), typeof(TClientKey));
            }
            if (typeof(TClientKey) == typeof(string))
            {
                return (TClientKey)Convert.ChangeType(Guid.NewGuid().ToString(), typeof(TClientKey));
            }
            return default;
        }
        internal virtual string GetOrderingColumnName() => "Id";
        internal virtual List<string> GetIncludeColumns() => new List<string>();

        internal async Task<TEntity> GetModel(TClientKey id)
        {
            return await _db.Set<TEntity>().FindAsync(id);
        }
        #endregion
    }
}
