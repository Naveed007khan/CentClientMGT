using ClientManagement.Helpers.ViewModels.DataTable;
using System.Collections.Generic;

namespace ClientManagement.Helpers.ViewModels.SharedVM
{
    public class PaginatedResultVM<M>
    {
        public PaginatedResultVM()
        {
            Items = new List<M>();
            AdditionalProperties = new Dictionary<string, string>();
        }
        public int TotalCount { get; set; }
        public List<M> Items { get; set; }
        public Dictionary<string, string> AdditionalProperties { get; set; }
    }
    public class DatatablePaginatedResultVM<M> : PaginatedResultVM<M>
    {
        public DatatablePaginatedResultVM(int draw, int totalCount, List<M> items)
        {
            this.draw = draw;
            recordsTotal = recordsFiltered = totalCount;
            Items = items;
            ActionsList = new List<DataTableActionViewModel>();
        }
        public int draw { get; set; }
        public int recordsTotal { get; set; }// Total Count Without any where clause only for  informational purpose
        public int recordsFiltered { get; set; }// Total Count after all filteration need to be applied. This needs to be set
        public bool ProcessManuallyForDT { get; set; }
        public bool ShowSelectedFilters { get; set; } = true;
        public List<DataTableActionViewModel> ActionsList { get; set; }


    }
}
