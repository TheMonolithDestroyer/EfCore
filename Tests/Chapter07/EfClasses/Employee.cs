namespace Tests.Chapter07.EfClasses
{
    public class Employee
    {
        public int EmployeeId { get; set; }
        public string Name { get; set; }

        // ----------------------------------
        // Relatioships
        public int? ManagerEmployeeId { get; set; } // #A
        public Employee Manager { get; set; }

        // #A This Foreign Key uses the <navigationalPropertyName><PrimaryKeyName> pattern
    }
}