using System.Data;
using System.Data.SqlClient;
using System.Reflection.PortableExecutable;

namespace BasicConnectivity;

public class Region
{
    public int Id { get; set; }
    public string Name { get; set; }

    private readonly string connectionString =
        "Data Source=RAVEN;Integrated Security=True;Database=DB_HR;Connect Timeout=30;";

    // GET ALL: Region
    public List<Region> GetAll()
    {
        var regions = new List<Region>();

        using var connection = new SqlConnection(connectionString);
        using var command = new SqlCommand();

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
                    regions.Add(new Region
                    {
                        Id = reader.GetInt32(0),
                        Name = reader.GetString(1)
                    });
                }
                reader.Close();
                connection.Close();

                return regions;
            }
            reader.Close();
            connection.Close();

            return new List<Region>();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }

        return new List<Region>();
    }


    // GET BY ID: Region
    public Region GetById(int id)
    {
        using var connection = new SqlConnection(connectionString);
        using var command = new SqlCommand();

        command.Connection = connection;
        command.CommandText = "SELECT * FROM tbl_regions WHERE Id = @id";
        command.Parameters.Add(new SqlParameter("@id", id));

        try
        {
            connection.Open();

            using var reader = command.ExecuteReader();

            if (reader.HasRows)
            {
                reader.Read();
                return new Region
                {
                    Id = reader.GetInt32(0),
                    Name = reader.GetString(1)
                };
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }
        return new Region();
    }

    // INSERT: Region
    public string Insert(string name)
    {
        using var connection = new SqlConnection(connectionString);
        using var command = new SqlCommand();

        command.Connection = connection;
        command.CommandText = "INSERT INTO tbl_regions VALUES (@name);";

        try
        {
            command.Parameters.Add(new SqlParameter("@name", name));

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

    // UPDATE: Region
    public string Update(int id, string name)
    {
        using var connection = new SqlConnection(connectionString);
        using var command = new SqlCommand();

        command.Connection = connection;
        command.CommandText = "UPDATE tbl_regions SET Name = @name WHERE Id = @id";
        command.Parameters.Add(new SqlParameter("@id", id));
        command.Parameters.Add(new SqlParameter("@name", name));

        try
        {
            connection.Open();
            using var transaction = connection.BeginTransaction();
            try
            {
                command.Transaction = transaction;

                var result = command.ExecuteNonQuery();

                transaction.Commit();
                connection.Close();

                return result > 0 ? "Update Success" : "Update Failed";
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


    // DELETE: Region
    public string Delete(int id)
    {
        using var connection = new SqlConnection(connectionString);
        using var command = new SqlCommand();

        command.Connection = connection;
        command.CommandText = "DELETE FROM tbl_regions WHERE Id = @id";
        command.Parameters.Add(new SqlParameter("@id", id));

        try
        {
            connection.Open();
            using var transaction = connection.BeginTransaction();
            try
            {
                command.Transaction = transaction;

                var result = command.ExecuteNonQuery();

                transaction.Commit();
                connection.Close();

                return result > 0 ? "Delete Success" : "Delete Failed";
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

}
