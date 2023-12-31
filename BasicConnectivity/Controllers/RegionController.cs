﻿using BasicConnectivity.Models;
using BasicConnectivity.Views;

namespace BasicConnectivity.Controllers;

public class RegionController
{
    private Region _region;
    private RegionView _regionView;

    public RegionController(Region region, RegionView regionView)
    {
        _region = region;
        _regionView = regionView;
    }

    public void GetAll()
    {
        var results = _region.GetAll();
        if (!results.Any())
        {
            Console.WriteLine("No data found");
        }
        else
        {
            RegionView.List(results, "regions");
        }
    }

    public void GetRegionById()
    {
        var id = _regionView.GetRegionIdToRetrieve();
        if (id > 0)
        {
            var result = _region.GetById(id);
            if (result.Id > 0)
            {
                var regionView = new RegionView();
                regionView.DisplayRegion(result);
            }
            else
            {
                Console.WriteLine($"Region with ID {id} not found.");
            }
        }
        else
        {
            Console.WriteLine("Invalid region ID");
        }
    }


    public void Insert()
    {
        string input = "";
        var isTrue = true;
        while (isTrue)
        {
            try
            {
                input = _regionView.InsertInput();
                if (string.IsNullOrEmpty(input))
                {
                    Console.WriteLine("Region name cannot be empty");
                    continue;
                }
                isTrue = false;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        var result = _region.Insert(new Region
        {
            Id = 0,
            Name = input
        });

        RegionView.Transaction(result);
    }

    public void Update()
    {
        var region = new Region();
        var isTrue = true;
        while (isTrue)
        {
            try
            {
                region = _regionView.UpdateRegion();
                if (string.IsNullOrEmpty(region.Name))
                {
                    Console.WriteLine("Region name cannot be empty");
                    continue;
                }
                isTrue = false;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        var result = _region.Update(region);
        RegionView.Transaction(result);
    }

    public void Delete()
    {
        var id = _regionView.GetRegionIdToDelete();
        if (id > 0)
        {
            var result = _region.Delete(id);
            RegionView.Transaction(result);
        }
        else
        {
            Console.WriteLine("Invalid region ID");
        }
    }

}
