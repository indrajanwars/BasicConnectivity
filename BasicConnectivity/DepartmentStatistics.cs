namespace BasicConnectivity
{
    public class DepartmentStatistics
    {
        public string DepartmentName { get; set; }
        public int TotalEmployee { get; set; }
        public double MinSalary { get; set; }
        public double MaxSalary { get; set; }
        public double AverageSalary { get; set; }

        public override string ToString()
        {
            return $"{DepartmentName} - {TotalEmployee} - {MinSalary} - {MaxSalary} - {AverageSalary}";
        }
    }
}
