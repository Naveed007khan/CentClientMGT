using System.Collections.Generic;

namespace ClientManagement.Helpers.ViewModels.Select2
{
    public class Select2ViewModel
    {
        public string id { get; set; }

        public string text { get; set; }
    }

    public class Select2PagedResultViewModel
    {
        public int Total { get; set; }

        public List<Select2ViewModel> Results { get; set; }
    }

    public class Select2OptionModel<T>
    {
        public string id { get; set; }

        public string text { get; set; }

        public T additionalAttributesModel { get; set; }
    }

    public class Select2PagedResult<T>
    {
        public int Total { get; set; }

        public List<Select2OptionModel<T>> Results { get; set; }
    }
}
