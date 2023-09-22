using BasicConnectivity.Models;

namespace BasicConnectivity.Views;

public class RegionView : GeneralView
{
    public string InsertInput()
    {
        Console.WriteLine("Insert region name");
        var name = Console.ReadLine();

        return name;
    }

    public Region UpdateRegion()
    {
        Console.WriteLine("Insert region id");
        var id = Convert.ToInt32(Console.ReadLine());
        Console.WriteLine("Insert region name");
        var name = Console.ReadLine();

        return new Region
        {
            Id = id,
            Name = name
        };
    }

    public int GetRegionIdToDelete()
    {
        Console.WriteLine("Enter the ID of the region to delete:");
        if (int.TryParse(Console.ReadLine(), out int id))
        {
            return id;
        }
        return -1;
    }

    public int GetRegionIdToRetrieve()
    {
        Console.WriteLine("Enter the ID of the region to retrieve:");
        if (int.TryParse(Console.ReadLine(), out int id))
        {
            return id;
        }
        return -1;
    }

    public void DisplayRegion(Region region)
    {
        Console.WriteLine($"Region ID: {region.Id}");
        Console.WriteLine($"Region Name: {region.Name}");
    }

}