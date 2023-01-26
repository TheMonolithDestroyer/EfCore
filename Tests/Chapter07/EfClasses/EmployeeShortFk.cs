using System.ComponentModel.DataAnnotations.Schema;

namespace Tests.Chapter07.EfClasses
{
    public class EmployeeShortFk
    {
        public int EmployeeId { get; set; }
        public string Name { get; set; }
        [ForeignKey(nameof(Manager))]
        public int? ManagerId { get; set; }
        public Employee Manager { get; set; }
    }
}