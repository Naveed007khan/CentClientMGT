using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClientManagement.Helpers
{
    public static class WebException
    {
        public static void HandleException(Exception ex, ModelStateDictionary modelState)
        {
            if (ex.GetType() == typeof(CustomException))
            {
                modelState.AddModelError(string.Empty, ex.Message.ToString());
            }
            else
            {
                modelState.AddModelError(string.Empty, ex.Message.ToString());
            }
        }
    }
}
