using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Threading.Tasks;

namespace ClientManagement.Helpers.Interfaces.BaseInterfaces
{
    public interface IGeneral<Model, TClientKey> : IBase where Model : class, new()
            where TClientKey : IEquatable<TClientKey>
    {
        Task<TClientKey> Update(Model model, ModelStateDictionary modelState);
        Task<bool> Delete(TClientKey id);
        //Task<bool> Approve(IdType id);
        Task<Model> GetById(TClientKey id);
    }
}
