namespace BasicConnectivity.Models;

public class Employee
{
    public int Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public string PhoneNumber { get; set; }
    public DateTime HireDate { get; set; }
    public int JobId { get; set; }
    public int Salary { get; set; }
    public int CommissionPct { get; set; }
    public int ManagerId { get; set; }
    public int DepartmentId { get; set; }

    public override string ToString()
    {
        return $"{Id} - {FirstName} - {LastName} - {Email} - {PhoneNumber} - {HireDate} - {JobId} - {Salary} - {CommissionPct} - {ManagerId} - {DepartmentId}";
    }

    public List<Employee> GetAll()
    {
        var tbl_employees = new List<Employee>();

        using var connection = Provider.GetConnection();
        using var command = Provider.GetCommand();

        command.Connection = connection;
        command.CommandText = "SELECT * FROM tbl_employees";

        try
        {
            connection.Open();

            using var reader = command.ExecuteReader();

            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    tbl_employees.Add(new Employee()
                    {
                        Id = reader.GetInt32(0),
                        FirstName = reader.GetString(1),
                        LastName = reader.GetString(2),
                        Email = reader.IsDBNull(3) ? null : reader.GetString(3),
                        PhoneNumber = reader.IsDBNull(4) ? null : reader.GetString(4),
                        HireDate = reader.IsDBNull(5) ? DateTime.MinValue : reader.GetDateTime(5),
                        JobId = reader.GetInt32(6),
                        Salary = reader.GetInt32(7),
                        CommissionPct = reader.IsDBNull(8) ? 0 : reader.GetInt32(8),
                        ManagerId = reader.GetInt32(9),
                        DepartmentId = reader.GetInt32(10)
                    });
                }
                reader.Close();
                connection.Close();

                return tbl_employees;
            }
            reader.Close();
            connection.Close();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }

        return new List<Employee>();
    }
}