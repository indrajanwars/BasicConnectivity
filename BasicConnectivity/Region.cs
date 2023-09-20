namespace BasicConnectivity;

public class Region
{
    public int Id { get; set; }
    public string Name { get; set; }

    public override string ToString()
    {
        return $"{Id} - {Name}";
    }

    public List<Region> GetAll()
    {
        var tbl_regions = new List<Region>();

        using var connection = Provider.GetConnection();
        using var command = Provider.GetCommand();

        command.Connection = connection;
        command.CommandText = "SELECT * FROM tbl_regions";

        try
        {
            connection.Open();

            using var reader = command.ExecuteReader();

            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    tbl_regions.Add(new Region
                    {
                        Id = reader.GetInt32(0),
                        Name = reader.GetString(1)
                    });
                }
                reader.Close();
                connection.Close();

                return tbl_regions;
            }
            reader.Close();
            connection.Close();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }

        return new List<Region>();
    }

    public Region GetById(int id)
    {
        var tbl_regions = new Region();
        using var connection = Provider.GetConnection();
        using var command = Provider.GetCommand();

        command.Connection = connection;
        command.CommandText = "SELECT * FROM regions where id = @id";
        command.Parameters.Add(Provider.SetParameter("@id", id));

        try
        {
            connection.Open();

            using var reader = command.ExecuteReader();

            if (reader.HasRows)
            {
                reader.Read();

                tbl_regions.Id = reader.GetInt32(0);
                tbl_regions.Name = reader.GetString(1);
            }
            reader.Close();
            connection.Close();

            return tbl_regions;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }

        return new Region();
    }

    public string Insert(Region tbl_regions)
    {
        using var connection = Provider.GetConnection();
        using var command = Provider.GetCommand();

        command.Connection = connection;
        command.CommandText = "INSERT INTO regions VALUES (@name);";

        try
        {
            command.Parameters.Add(Provider.SetParameter("@name", tbl_regions.Name));

            connection.Open();
            using var transaction = connection.BeginTransaction();
            try
            {
                command.Transaction = transaction;

                var result = command.ExecuteNonQuery();

                transaction.Commit();
                connection.Close();

                return result.ToString();
            }
            catch (Exception ex)
            {
                transaction.Rollback();
                return $"Error Transaction: {ex.Message}";
            }
        }
        catch (Exception ex)
        {
            return $"Error: {ex.Message}";
        }
    }

    public string Update(Region tbl_regions)
    {
        return "";
    }

    public string Delete(int id)
    {
        return "";
    }
}