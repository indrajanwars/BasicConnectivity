using System.Data;
using System.Data.SqlClient;
using System.Numerics;

namespace BasicConnectivity;

public class Program
{
    private static void Main()
    {
        var choice = true;
        while (choice)
        {
            Console.Clear();
            Console.WriteLine("== Menu Utama ==");
            Console.WriteLine("1. Manage Table Region");
            Console.WriteLine("2. List All Countries");
            Console.WriteLine("3. List All Locations");
            Console.WriteLine("4. List All Department");
            Console.WriteLine("5. List All Employee");
            Console.WriteLine("6. List All Job History");
            Console.WriteLine("7. List All Job");
            Console.WriteLine("8. List Regions with 'Where'");
            Console.WriteLine("9. Join tables: Regions - Countries - Locations");
            Console.WriteLine("10. Employee Data Details");
            Console.WriteLine("11. Department Statistics");
            Console.WriteLine("12. Exit");
            Console.Write("Enter your choice: ");
            var input = Console.ReadLine();
            choice = Menu(input);
            Console.ReadLine();
        }
    }

    public static bool Menu(string input)
    {
        switch (input)
        {
            case "1":
                RegionMenu();
                break;
            case "2":
                Console.Clear();
                var country = new Country();
                var tbl_countries = country.GetAll();
                GeneralMenu.List(tbl_countries, "countries");
                break;
            case "3":
                Console.Clear();
                var location = new Location();
                var tbl_locations = location.GetAll();
                GeneralMenu.List(tbl_locations, "locations");
                break;
            case "4":
                Console.Clear();
                var department = new Department();
                var tbl_departments = department.GetAll();
                GeneralMenu.List(tbl_departments, "departments");
                break;
            case "5":
                Console.Clear();
                var employee = new Employee();
                var tbl_employees = employee.GetAll();
                GeneralMenu.List(tbl_employees, "employees");
                break;
            case "6":
                Console.Clear();
                var jobHistory = new JobHistory();
                var tbl_jobHistory = jobHistory.GetAll();
                GeneralMenu.List(tbl_jobHistory, "job history");
                break;
            case "7":
                Console.Clear();
                var job = new Job();
                var tbl_jobs = job.GetAll();
                GeneralMenu.List(tbl_jobs, "jobs");
                break;
            case "8":
                Console.Write("\nInput region: ");
                string input2 = Console.ReadLine();
                var region2 = new Region();
                var result = region2.GetAll().Where(r => r.Name.Contains(input2)).ToList();
                GeneralMenu.List(result, "regions");
                break;
            case "9":
                Console.Clear();
                var country3 = new Country();
                var region3 = new Region();
                var location3 = new Location();

                var getCountry = country3.GetAll();
                var getRegion = region3.GetAll();
                var getLocation = location3.GetAll();

                var resultJoin = (from r in getRegion
                                  join c in getCountry on r.Id equals c.RegionId
                                  join l in getLocation on c.Id equals l.CountryId
                                  select new RegionAndCountry
                                  {
                                      CountryId = c.Id,
                                      CountryName = c.Name,
                                      RegionId = r.Id,
                                      RegionName = r.Name,
                                      City = l.City
                                  }).ToList();

                var resultJoin2 = getRegion.Join(getCountry,
                                                 r => r.Id,
                                                 c => c.RegionId,
                                                 (r, c) => new { r, c })
                                           .Join(getLocation,
                                                 rc => rc.c.Id,
                                                 l => l.CountryId,
                                                 (rc, l) => new RegionAndCountry
                                                 {
                                                     CountryId = rc.c.Id,
                                                     CountryName = rc.c.Name,
                                                     RegionId = rc.r.Id,
                                                     RegionName = rc.r.Name,
                                                     City = l.City
                                                 }).ToList();

                /*foreach (var item in resultJoin2)
                {
                    Console.WriteLine($"{item.Id} - {item.NameRegion} - {item.NameCountry} - {item.RegionId}");
                }*/

                GeneralMenu.List(resultJoin2, "Regions - Countries - Locations");
                break;
            case "10":
                Console.Clear();
                var employee1 = new Employee();
                var department1 = new Department();
                var location1 = new Location();
                var country1 = new Country();
                var region1 = new Region();

                var getEmployee1 = employee1.GetAll();
                var getDepartment1 = department1.GetAll();
                var getLocation1 = location1.GetAll();
                var getCountry1 = country1.GetAll();
                var getRegion1 = region1.GetAll();

                var resultJoin3 = (from e in getEmployee1
                                   join d in getDepartment1 on e.DepartmentId equals d.Id
                                   join l in getLocation1 on d.LocationId equals l.Id
                                   join c in getCountry1 on l.CountryId equals c.Id
                                   join r in getRegion1 on c.RegionId equals r.Id
                                   select new EmployeeData
                                   {
                                       Id = e.Id,
                                       Full_Name = $"{e.FirstName} {e.LastName}",
                                       Email = e.Email,
                                       Phone = e.PhoneNumber,
                                       Salary = e.Salary,
                                       Department_Name = d.Name,
                                       Street_Address = l.StreetAddress,
                                       Country_Name = c.Name,
                                       Region_Name = r.Name
                                   }).ToList();

                var resultJoin4 = getEmployee1
    .Join(getDepartment1,
    e => e.DepartmentId, d => d.Id, (e, d) => new { Employee = e, Department = d })
    .Join(getLocation1,
    ed => ed.Department.LocationId, l => l.Id, (ed, l) => new { ed.Employee, ed.Department, Location = l })
    .Join(getCountry1,
    edl => edl.Location.CountryId, c => c.Id, (edl, c) => new { edl.Employee, edl.Department, edl.Location, Country = c })
    .Join(getRegion1,
    edlc => edlc.Country.RegionId, r => r.Id, (edlc, r) => new EmployeeData
    {
        Id = edlc.Employee.Id,
        Full_Name = $"{edlc.Employee.FirstName} {edlc.Employee.LastName}",
        Email = edlc.Employee.Email,
        Phone = edlc.Employee.PhoneNumber,
        Salary = edlc.Employee.Salary,
        Department_Name = edlc.Department.Name,
        Street_Address = edlc.Location.StreetAddress,
        Country_Name = edlc.Country.Name,
        Region_Name = r.Name
    }).ToList();

                GeneralMenu.List(resultJoin4, "Employee Details:\nID - Full Name - Email - Phone Number - Salary - Department Name - Street Address - Country Name - Region Name");
                break;
            case "11":
                Console.Clear();
                var department2 = new Department();
                var employee2 = new Employee();

                var departments2 = department2.GetAll();
                var employees2 = employee2.GetAll();

                var departmentStats = from d in departments2
                                      join e in employees2 on d.Id equals e.DepartmentId into employeeGroup
                                      where employeeGroup.Count() > 1
                                      select new DepartmentStatistics
                                      {
                                          DepartmentName = d.Name,
                                          TotalEmployee = employeeGroup.Count(),
                                          MinSalary = employeeGroup.Min(e => e.Salary),
                                          MaxSalary = employeeGroup.Max(e => e.Salary),
                                          AverageSalary = employeeGroup.Average(e => e.Salary)
                                      };

                GeneralMenu.List(departmentStats.ToList(), "Department Statistics");
                break;
            case "12":
                Console.WriteLine("\nExit Program..");
                return false;
            default:
                Console.WriteLine("Invalid choice");
                break;
        }
        return true;
    }

    public static void RegionMenu()
    {
        var region = new Region();
        var regionChoice = true;

        while (regionChoice)
        {
            Console.Clear();
            Console.WriteLine("== Manage Table Region ==");
            Console.WriteLine("1. List all regions");
            Console.WriteLine("2. Get region by ID");
            Console.WriteLine("3. Insert new region");
            Console.WriteLine("4. Update region");
            Console.WriteLine("5. Delete region");
            Console.WriteLine("6. Back to Main Menu");
            Console.Write("Enter your choice: ");
            var input = Console.ReadLine();

            switch (input)
            {
                case "1":
                    var tbl_regions = region.GetAll();
                    GeneralMenu.List(tbl_regions, "regions");
                    break;
                case "2":
                    Console.Write("Enter ID: ");
                    if (int.TryParse(Console.ReadLine(), out int regionId))
                    {
                        var singleRegion = region.GetById(regionId);
                        if (singleRegion != null)
                        {
                            Console.WriteLine(singleRegion.ToString());
                        }
                        else
                        {
                            Console.WriteLine("Region not found");
                        }
                    }
                    else
                    {
                        Console.WriteLine("Invalid input");
                    }
                    break;
                case "3":
                    Console.Write("Enter Region Name: ");
                    string regionName = Console.ReadLine();
                    if (!string.IsNullOrEmpty(regionName))
                    {
                        var insertResult = region.Insert(new Region { Name = regionName });
                        Console.WriteLine(insertResult);
                    }
                    else
                    {
                        Console.WriteLine("Region Name cannot be empty");
                    }
                    break;
                case "4":
                    Console.Write("Enter Region ID: ");
                    if (int.TryParse(Console.ReadLine(), out int updateId))
                    {
                        Console.Write("Enter New Region Name: ");
                        string newName = Console.ReadLine();
                        if (!string.IsNullOrEmpty(newName))
                        {
                            var updateResult = region.Update(new Region { Id = updateId, Name = newName });
                            Console.WriteLine(updateResult);
                        }
                        else
                        {
                            Console.WriteLine("New Region Name cannot be empty");
                        }
                    }
                    else
                    {
                        Console.WriteLine("Invalid input");
                    }
                    break;
                case "5":
                    Console.Write("Enter Region ID to delete: ");
                    if (int.TryParse(Console.ReadLine(), out int deleteId))
                    {
                        var deleteResult = region.Delete(deleteId);
                        Console.WriteLine(deleteResult);
                    }
                    else
                    {
                        Console.WriteLine("Invalid input");
                    }
                    break;
                case "6":
                    regionChoice = false;
                    break;
                default:
                    Console.WriteLine("Invalid choice");
                    break;
            }

            Console.ReadLine();
        }
    }
}