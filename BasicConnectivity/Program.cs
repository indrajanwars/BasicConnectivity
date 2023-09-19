using System.Data;
using System.Data.SqlClient;

namespace BasicConnectivity;

public class Program
{
    private static readonly string connectionString =
        "Data Source=DESKTOP-MQGRL05;Integrated Security=True;Database=DB_HR;Connect Timeout=30;";
    private static void Main()
    {
        //GetAllRegions();
        int regionId = 1; GetRegionById(regionId);
        //InsertRegion("Jawa Timur");
    }

    // GET ALL: Region
    public static void GetAllRegions()
    {
        using var connection = new SqlConnection(connectionString);
        using var command = new SqlCommand();

        command.Connection = connection;
        command.CommandText = "SELECT * FROM tbl_regions";

        try
        {
            connection.Open();

            using var reader = command.ExecuteReader();

            if (reader.HasRows)
                while (reader.Read())
                {
                    Console.WriteLine("Id: " + reader.GetInt32(0));
                    Console.WriteLine("Name: " + reader.GetString(1));
                }
            else
                Console.WriteLine("No rows found.");

            reader.Close();
            connection.Close();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }
    }

    // GET BY ID: Region
    public static void GetRegionById(int id)
    {
        using var connection = new SqlConnection(connectionString);
        using var command = new SqlCommand();

        command.Connection = connection;
        command.CommandText = "SELECT * FROM tbl_regions WHERE Id = @id";

        try
        {
            var pId = new SqlParameter
            {
                ParameterName = "@id",
                Value = id,
                SqlDbType = SqlDbType.Int
            };
            command.Parameters.Add(pId);

            connection.Open();
            using var reader = command.ExecuteReader();

            if (reader.HasRows)
                while (reader.Read())
                {
                    Console.WriteLine("Id: " + reader.GetInt32(0));
                    Console.WriteLine("Name: " + reader.GetString(1));
                }
            else
                Console.WriteLine("No rows found.");

            reader.Close();
            connection.Close();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }
    }

    // INSERT: Region
    public static void InsertRegion(string name)
    {
        using var connection = new SqlConnection(connectionString);
        using var command = new SqlCommand();

        command.Connection = connection;
        command.CommandText = "INSERT INTO tbl_regions VALUES (@name);";

        try
        {
            var pName = new SqlParameter
            {
                ParameterName = "@name",
                Value = name,
                SqlDbType = SqlDbType.VarChar
            };
            command.Parameters.Add(pName);

            connection.Open();
            using var transaction = connection.BeginTransaction();
            try
            {
                command.Transaction = transaction;

                var result = command.ExecuteNonQuery();

                transaction.Commit();
                connection.Close();

                switch (result)
                {
                    case >= 1:
                        Console.WriteLine("Insert Success");
                        break;
                    default:
                        Console.WriteLine("Insert Failed");
                        break;
                }
            }
            catch (Exception ex)
            {
                transaction.Rollback();
                Console.WriteLine($"Error Transaction: {ex.Message}");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }
    }

    // UPDATE: Region
    public static void UpdateRegion(int id, string name) { }

    // DELETE: Region
    public static void DeleteRegion(int id) { }
}
