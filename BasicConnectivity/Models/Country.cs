namespace BasicConnectivity.Models;

public class Country
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int RegionId { get; set; }

    public override string ToString()
    {
        return $"{Id} - {Name} - {RegionId}";
    }

    public List<Country> GetAll()
    {
        var tbl_countries = new List<Country>();

        using var connection = Provider.GetConnection();
        using var command = Provider.GetCommand();

        command.Connection = connection;
        command.CommandText = "SELECT * FROM tbl_countries";

        try
        {
            connection.Open();

            using var reader = command.ExecuteReader();

            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    tbl_countries.Add(new Country()
                    {
                        Id = reader.GetInt32(0),
                        Name = reader.GetString(1),
                        RegionId = reader.GetInt32(2)
                    });
                }
                reader.Close();
                connection.Close();

                return tbl_countries;
            }
            reader.Close();
            connection.Close();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }

        return new List<Country>();
    }
}