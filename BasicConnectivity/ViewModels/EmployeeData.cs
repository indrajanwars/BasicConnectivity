﻿namespace BasicConnectivity.ViewModels;
public class EmployeeData
{
    public int Id { get; set; }
    public string Full_Name { get; set; }
    public string Email { get; set; }
    public string Phone { get; set; }
    public int Salary { get; set; }
    public string Department_Name { get; set; }
    public string Street_Address { get; set; }
    public string Country_Name { get; set; }
    public string Region_Name { get; set; }

    public override string ToString()
    {
        return $"{Id} - {Full_Name} - {Email} - {Phone} - {Salary} - {Department_Name} - {Street_Address} - {Country_Name} - {Region_Name}";
    }
}