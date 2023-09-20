using System.Data;
using System.Data.SqlClient;

namespace BasicConnectivity;

public class Program
{
    private static void Main()
    {
        var choice = true;
        while (choice)
        {
            Console.Clear();
            Console.WriteLine("1. List all regions");
            Console.WriteLine("2. List all countries");
            Console.WriteLine("3. List all locations");
            Console.WriteLine("4. List all department");
            Console.WriteLine("5. List all employee");
            Console.WriteLine("6. List all job history");
            Console.WriteLine("7. List all job");
            Console.WriteLine("8. Exit");
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
                var region = new Region();
                var tbl_regions = region.GetAll();
                GeneralMenu.List(tbl_regions, "regions");
                break;
            case "2":
                var country = new Country();
                var tbl_countries = country.GetAll();
                GeneralMenu.List(tbl_countries, "countries");
                break;
            case "3":
                var location = new Location();
                var tbl_locations = location.GetAll();
                GeneralMenu.List(tbl_locations, "locations");
                break;
            case "4":
                var department = new Department();
                var tbl_departments = department.GetAll();
                GeneralMenu.List(tbl_departments, "departments");
                break;
            case "5":
                var employee = new Employee();
                var tbl_employees = employee.GetAll();
                GeneralMenu.List(tbl_employees, "employees");
                break;
            case "6":
                var jobHistory = new JobHistory();
                var tbl_jobHistory = jobHistory.GetAll();
                GeneralMenu.List(tbl_jobHistory, "job history");
                break;
            case "7":
                var job = new Job();
                var tbl_jobs = job.GetAll();
                GeneralMenu.List(tbl_jobs, "jobs");
                break;
            case "8":
                return false;
            default:
                Console.WriteLine("Invalid choice");
                break;
        }
        return true;
    }
}