using System;

namespace backEndDbConnection.Models
{
    public class Employee
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Title { get; set; }
        public string Country { get; set; }
        public string Phone { get; set; }
        public int? ManagerId { get; set; }
        public int? ImgId { get; set; }
        public string? Gender { get; set; }
        public DateTime Created_At { get; set; }
    }
}
