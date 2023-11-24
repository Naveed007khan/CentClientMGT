using System.Collections.Generic;

namespace ClientManagement.Helpers.ViewModels.DataTable
{
    public class DataTableActionViewModel
    {
        public string Action { get; set; }
        public string Title { get; set; }
        public string LinkTitle { get; set; } = "";
        public string Href { get; set; }
        public string ReturnUrl { get; set; }
        public string Class { get; set; }
        public List<string> Attr { get; set; }
    }
}
