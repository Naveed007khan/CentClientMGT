using Centangle.Common.Pagination.Models;
using Centangle.Common.ResponseHelpers.Models;

namespace ClientManagement.Helpers.Interfaces.BaseInterfaces
{
    public interface IBase
    {
        Task<IRepositoryResponse> GetAll<T>(BaseSearchModel model);
    }
}
