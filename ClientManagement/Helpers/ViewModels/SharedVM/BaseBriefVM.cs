using ClientManagement.Helpers.Attributes;
using System;

namespace ClientManagement.Helpers.ViewModels.SharedVM
{
    public interface IBaseBriefViewModel<TClientKey> where TClientKey : IEquatable<TClientKey>
    {
        TClientKey Id { get; set; }
        string Name { get; set; }
    }
    public class BaseBriefVM<TClientKey> : IBaseBriefViewModel<TClientKey> 
        where TClientKey : IEquatable<TClientKey>
    {
        public string ErrorMessage { get; }
        public bool IsValidationEnabled { get; }

        public BaseBriefVM()
        {
            IsValidationEnabled = false;
            ErrorMessage = "";
        }
        public BaseBriefVM(bool isValidationEnabled)
        {
            IsValidationEnabled = isValidationEnabled;
            ErrorMessage = "";
        }
        public BaseBriefVM(bool isValidationEnabled, string errorMessage)
        {
            IsValidationEnabled = isValidationEnabled;
            ErrorMessage = errorMessage;
        }
        [RequiredSelect2("ErrorMessage", "IsValidationEnabled")]
        public virtual TClientKey Id { get; set; }
        public string Name { get; set; }
    }
}
