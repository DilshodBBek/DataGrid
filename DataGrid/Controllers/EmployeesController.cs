using CsvHelper;
using DataGrid.Infrastructure.Interfaces;
using DataGrid.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Globalization;

namespace DataGrid.Controllers
{
    public class EmployeesController : Controller
    {
        private readonly IApplicationDbContext _context;

        public EmployeesController(IApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> Index(EmployeeViewModel viewModel = null)// List<Employee> employees = null)
        {
            if (_context.Employees is null)
                return Problem("Entity set 'IApplicationDbContext.Employees' is null.");

            viewModel ??= new EmployeeViewModel();

            viewModel.CurrentSort = viewModel.SortOrder;
            viewModel.SurnameSortParm = string.IsNullOrEmpty(viewModel.SortOrder) || viewModel.SortOrder.Equals("surname_asc") ? "surname_desc" : "surname_asc";
            viewModel.NameSortParm = viewModel.SortOrder == "name_asc" ? "name_desc" : "name_asc";
            viewModel.DateSortParm = viewModel.SortOrder == "date_asc" ? "date_desc" : "date_asc";

            if (viewModel.SearchString != null)
            {
                viewModel.pageNumber = 1;
            }
            else
            {
                viewModel.SearchString = viewModel.CurrentFilter;
            }

            viewModel.CurrentFilter = viewModel.SearchString;
            var employees = from s in _context.Employees
                            select s;

            if (!String.IsNullOrEmpty(viewModel.SearchString))
            {
                employees = employees.Where(s => s.Forename.Contains(viewModel.SearchString)
                                       || s.Surname.Contains(viewModel.SearchString)
                                       || s.StartDate.ToString().Contains(viewModel.SearchString)
                                       || s.Postcode.Contains(viewModel.SearchString)
                                       || s.PayrollNumber.Contains(viewModel.SearchString)
                                       || s.Mobile.Contains(viewModel.SearchString)
                                       || s.EmailHome.Contains(viewModel.SearchString)
                                       || s.Address2.Contains(viewModel.SearchString)
                                       || s.Address.Contains(viewModel.SearchString)
                                       || s.DateOfBirth.ToString().Contains(viewModel.SearchString)
                                       || s.Phone.Contains(viewModel.SearchString));
            }

            switch (viewModel.SortOrder)
            {
                case "name_asc":
                    employees = employees.OrderBy(s => s.Forename);
                    break;
                case "name_desc":
                    employees = employees.OrderByDescending(s => s.Forename);
                    break;
                case "date_asc":
                    employees = employees.OrderBy(s => s.DateOfBirth);
                    break;
                case "date_desc":
                    employees = employees.OrderByDescending(s => s.DateOfBirth);
                    break;
                case "surname_desc":
                    employees = employees.OrderByDescending(s => s.Surname);
                    break;
                default:
                    employees = employees.OrderBy(s => s.Surname);
                    break;
            }

            int pageSize = 2;
            viewModel._employees = await PaginatedList<Employee>.CreateAsync(employees.AsNoTracking(), viewModel.pageNumber ?? 1, pageSize);
            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Index(IFormFile file, [FromServices] Microsoft.AspNetCore.Hosting.IHostingEnvironment hostingEnvironment)
        {
            try
            {
                #region Upload CSV
                string fileName = $"{hostingEnvironment.WebRootPath}\\files\\{file.FileName}";
                using (FileStream stream = System.IO.File.Create(fileName))
                {
                    file.CopyTo(stream);
                    stream.Flush();
                }
                #endregion

                List<Employee> employees = GetEmployeesList(file.FileName, out int failsCount);
                _context.Employees.AddRange(employees);
                await _context.SaveChangesAsync();
                var result = new EmployeeViewModel()
                {
                    FailsCount = failsCount,
                    SuccessCount = employees.Count,
                    Method = MethodType.Uploaded,
                    IsSuccess = failsCount == 0 ? true : false
                };
                return await Index(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = "An error occurred: Selected file excel file is not valid" });
            }
            
        }

        private List<Employee> GetEmployeesList(string fileName, out int fails)
        {
            fails = 0;
            List<Employee> employees = new();

            #region ReadCSV
            var path = $"{Directory.GetCurrentDirectory()}{@"\wwwroot\files"}" + "\\" + fileName;
            using (var reader = new StreamReader(path))
            using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
            {
                csv.Read();
                csv.ReadHeader();

                while (csv.Read())
                {
                    try { 
                    DateTime? _DateOfBirth = csv.GetField(3)?.StringToDateTime();
                    DateTime? _StartDate = csv.GetField(10)?.StringToDateTime();
                    if (_StartDate != null && _DateOfBirth != null)
                    {
                        Employee emp = new()
                        {
                            PayrollNumber = csv.GetField(0),
                            Forename = csv.GetField(1),
                            Surname = csv.GetField(2),
                            DateOfBirth = _DateOfBirth,
                            Phone = csv.GetField(4),
                            Mobile = csv.GetField(5),
                            Address = csv.GetField(6),
                            Address2 = csv.GetField(7),
                            Postcode = csv.GetField(8),
                            EmailHome = csv.GetField(9),
                            StartDate = _StartDate
                        };
                        employees.Add(emp);
                    }
                    else fails++;
                    }
                    catch (Exception e)
                    {
                        fails++;
                    }
                }
                #endregion

                //#region Create CSV
                //path = $"{Directory.GetCurrentDirectory()}{@"\wwwroot\files"}" + "\\NewFile.csv";
                //using (var writer = new StreamWriter(path))
                //using (var csr = new CsvWriter(writer, CultureInfo.InvariantCulture))
                //{
                //    csr.WriteRecord(employees);
                //}
                //#endregion

                return employees;
            }
        }

        [HttpGet]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Employees == null)
            {
                return NotFound();
            }

            var employee = await _context.Employees
                .FirstOrDefaultAsync(m => m.EmployeeId == id);
            if (employee == null)
            {
                return NotFound();
            }

            return View(employee);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("EmployeeId,PayrollNumber,Forename,Surname,DateOfBirth,Phone,Mobile,Address,Address2,Postcode,EmailHome,StartDate")] Employee employee)
        {
            if (ModelState.IsValid)
            {
                _context.Employees.Add(employee);
                await _context.SaveChangesAsync();
                var result = new EmployeeViewModel() { IsSuccess = true, Method = MethodType.Created };
                return RedirectToAction(nameof(Index), result);
            }
            return View(employee);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Employees == null)
            {
                return NotFound();
            }

            var employee = await _context.Employees.FindAsync(id);
            if (employee == null)
            {
                return NotFound();
            }
            return View(employee);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("EmployeeId,PayrollNumber,Forename,Surname,DateOfBirth,Phone,Mobile,Address,Address2,Postcode,EmailHome,StartDate")] Employee employee)
        {
            if (id != employee.EmployeeId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Employees.Update(employee);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EmployeeExists(employee.EmployeeId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                var result = new EmployeeViewModel()
                {
                    Method = MethodType.Updated,
                    IsSuccess = true
                };
                return RedirectToAction(nameof(Index), result);
            }
            return View(employee);
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Employees == null)
            {
                return NotFound();
            }

            var employee = await _context.Employees
                .FirstOrDefaultAsync(m => m.EmployeeId == id);
            if (employee == null)
            {
                return NotFound();
            }

            return View(employee);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Employees == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Employees'  is null.");
            }
            var employee = await _context.Employees.FindAsync(id);
            if (employee != null)
            {
                _context.Employees.Remove(employee);
            }

            await _context.SaveChangesAsync();
            var result = new EmployeeViewModel()
            {
                Method = MethodType.Deleted,
                IsSuccess = true,
                employee = employee
            };
            return RedirectToAction(nameof(Index), result);
        }

        private bool EmployeeExists(int id)
        {
            return (_context.Employees?.Any(e => e.EmployeeId == id)).GetValueOrDefault();
        }
    }
}
