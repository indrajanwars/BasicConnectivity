namespace BasicConnectivity.Models;

public class Department
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int ManagerId { get; set; }
    public int LocationId { get; set; }

    public override string ToString()
    {
        return $"{Id} - {Name} - {ManagerId} - {LocationId}";
    }

    public List<Department> GetAll()
    {
        var tbl_departments = new List<Department>();

        using var connection = Provider.GetConnection();
        using var command = Provider.GetCommand();

        command.Connection = connection;
        command.CommandText = "SELECT * FROM tbl_departments";

        try
        {
            connection.Open();

            using var reader = command.ExecuteReader();

            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    tbl_departments.Add(new Department()
                    {
                        Id = reader.GetInt32(0),
                        Name = reader.GetString(1),
                        ManagerId = reader.GetInt32(2),
                        LocationId = reader.GetInt32(3)
                    });
                }
                reader.Close();
                connection.Close();

                return tbl_departments;
            }
            reader.Close();
            connection.Close();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }

        return new List<Department>();
    }
}