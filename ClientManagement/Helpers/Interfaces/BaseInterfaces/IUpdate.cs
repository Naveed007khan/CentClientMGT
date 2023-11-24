using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Threading.Tasks;

namespace ClientManagement.Helpers.Interfaces.BaseInterfaces
{
    public interface IUpdate<T>
    {
        Task<long> Update(T model, ModelStateDictionary modelState);
    }
}
