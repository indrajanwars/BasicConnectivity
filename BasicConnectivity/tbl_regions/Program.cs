using System.Data;
using System.Data.SqlClient;
using System.Reflection.PortableExecutable;

namespace BasicConnectivity;

public class Program
{
    private static void Main()
    {
        var region = new Region();

        while (true)
        {
            Console.Clear();
            Console.WriteLine("== Menu ==\n");
            Console.WriteLine("1. Tampilkan Semua Region");
            Console.WriteLine("2. Tampilkan Region Berdasarkan ID");
            Console.WriteLine("3. Tambah Region Baru");
            Console.WriteLine("4. Update Region");
            Console.WriteLine("5. Hapus Region");
            Console.WriteLine("6. Keluar");
            Console.Write("\nPilihan: ");

            if (int.TryParse(Console.ReadLine(), out int choice))
            {
                switch (choice)
                {
                    case 1:
                        ShowAllRegions(region);
                        break;
                    case 2:
                        ShowRegionById(region);
                        break;
                    case 3:
                        AddNewRegion(region);
                        break;
                    case 4:
                        UpdateRegion(region);
                        break;
                    case 5:
                        DeleteRegion(region);
                        break;
                    case 6:
                        Environment.Exit(0);
                        break;
                    default:
                        Console.WriteLine("Pilihan tidak valid. Silakan coba lagi.");
                        break;
                }
            }
            else
            {
                Console.WriteLine("Pilihan tidak valid. Silakan coba lagi.");
            }

            Console.ReadLine();
        }

        // GET ALL: Region
        static void ShowAllRegions(Region region)
        {

            var getAllRegion = region.GetAll();

            if (getAllRegion.Count > 0)
            {
                foreach (var region1 in getAllRegion)
                {
                    Console.WriteLine($"Id: {region1.Id}, Name: {region1.Name}");
                }
            }
            else
            {
                Console.WriteLine("No data found");
            }
        }

        // GET BY ID: Region
        static void ShowRegionById(Region region)
        {
            Console.Write("\nMasukkan ID yang ingin dicari: ");
            if (int.TryParse(Console.ReadLine(), out int id))
            {
                var getRegion = region.GetById(id);

                if (getRegion != null)
                {
                    Console.WriteLine($"Id: {getRegion.Id}, Name: {getRegion.Name}");
                }
                else
                {
                    Console.WriteLine("Region not found");
                }
            }
            else
            {
                Console.WriteLine("Input ID tidak valid.");
            }

        }

        // INSERT: Region
        static void AddNewRegion(Region region)
        {
            Console.Write("\nNama Region Baru: ");
            string regionName = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(regionName) || regionName.Length > 25)
            {
                Console.WriteLine("Nama region tidak boleh kosong atau lebih dari 25 karakter.");
                return;
            }

            var insertResult = region.Insert(regionName);
            int.TryParse(insertResult, out int result);

            if (result > 0)
            {
                Console.WriteLine("Insert Success");
            }
            else
            {
                Console.WriteLine("Insert Failed");
                Console.WriteLine(insertResult);
            }
        }

        // UPDATE: Region
        static void UpdateRegion(Region region)
        {
            Console.Write("\nMasukkan ID Region yang Ingin Diupdate: ");

            if (!int.TryParse(Console.ReadLine(), out int updateId))
            {
                Console.WriteLine("Input ID tidak valid.");
                return;
            }

            Console.Write("Nama Region Baru: ");
            string newName = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(newName) || newName.Length > 25)
            {
                Console.WriteLine("Nama region tidak boleh kosong atau lebih dari 25 karakter.");
                return;
            }

            var updateRegion = region.Update(updateId, newName);

            if (updateRegion != null)
            {
                Console.WriteLine("Update Success");
            }
            else
            {
                Console.WriteLine("Update Failed");
            }
        }

        // DELETE: Region
        static void DeleteRegion(Region region)
        {
            Console.Write("Masukkan ID Region yang Ingin Dihapus: ");

            if (!int.TryParse(Console.ReadLine(), out int deleteId))
            {
                Console.WriteLine("Input ID tidak valid.");
                return;
            }

            var allRegions = region.GetAll();
            if (allRegions.All(r => r.Id != deleteId))
            {
                Console.WriteLine("ID Region tidak ditemukan dalam tabel.");
                return;
            }

            var deleteRegion = region.Delete(deleteId);

            if (deleteRegion != null)
            {
                Console.WriteLine("Delete Success");
            }
            else
            {
                Console.WriteLine("Delete Failed");
            }
        }
    }
}