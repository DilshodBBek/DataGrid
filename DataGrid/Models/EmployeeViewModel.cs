namespace DataGrid.Models
{
    public class EmployeeViewModel
    {
        public PaginatedList<Employee> _employees { get; set; }
        public Employee employee { get; set; }
        public int FailsCount { get; set; }
        public int SuccessCount { get; set; }
        public MethodType Method { get; set; }
        public bool IsSuccess { get; set; }
        public string? NameSortParm { get; set; }
        public string? SurnameSortParm { get; set; }
        public string? DateSortParm { get; set; }
        public string? SortOrder { get; set; }= "surname_asc";
        public string? SearchString { get; set; }
        public string? CurrentFilter { get; set; }
        public int? pageNumber { get; set;}
        public string CurrentSort { get; set; }
        //public PaginatedList<Employee> Paging { get; set; }

    }
}
