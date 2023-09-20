namespace BasicConnectivity;

public class Job
{
    public int Id { get; set; }
    public string Title { get; set; }
    public int MinSalary { get; set; }
    public int MaxSalary { get; set; }

    public override string ToString()
    {
        return $"{Id} - {Title} - {MinSalary} - {MaxSalary}";
    }

    public List<Job> GetAll()
    {
        var tbl_jobs = new List<Job>();

        using var connection = Provider.GetConnection();
        using var command = Provider.GetCommand();

        command.Connection = connection;
        command.CommandText = "SELECT * FROM tbl_jobs";

        try
        {
            connection.Open();

            using var reader = command.ExecuteReader();

            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    tbl_jobs.Add(new Job()
                    {
                        Id = reader.GetInt32(0),
                        Title = reader.GetString(1),
                        MinSalary = reader.GetInt32(2),
                        MaxSalary = reader.GetInt32(3)
                    });
                }
                reader.Close();
                connection.Close();

                return tbl_jobs;
            }
            reader.Close();
            connection.Close();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }

        return new List<Job>();
    }
}