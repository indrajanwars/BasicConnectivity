namespace BasicConnectivity;

public class JobHistory
{
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public int EmployeeId { get; set; }
    public int DepartmentId { get; set; }
    public int JobId { get; set; }

    public override string ToString()
    {
        return $"{StartDate} - {EndDate} - {EmployeeId} - {DepartmentId} - {JobId}";
    }

    public List<JobHistory> GetAll()
    {
        var tbl_job_history = new List<JobHistory>();

        using var connection = Provider.GetConnection();
        using var command = Provider.GetCommand();

        command.Connection = connection;
        command.CommandText = "SELECT * FROM tbl_job_history";

        try
        {
            connection.Open();

            using var reader = command.ExecuteReader();

            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    tbl_job_history.Add(new JobHistory()
                    {
                        StartDate = reader.GetDateTime(0),
                        EndDate = reader.GetDateTime(1),
                        EmployeeId = reader.GetInt32(2),
                        DepartmentId = reader.GetInt32(3),
                        JobId = reader.GetInt32(4)
                    });
                }
                reader.Close();
                connection.Close();

                return tbl_job_history;
            }
            reader.Close();
            connection.Close();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }

        return new List<JobHistory>();
    }
}