using EmployeeManagement_1.Common;

namespace EmployeeManagement_1.Entities
{
    public class LeadEntity : BaseEntity
    {
        public string Name { get; set; }
        public DateTime DateOfJoining { get; set; }
        public string Designation { get; set; }
        public string Domain { get; set; }
    }
}
