using System.ComponentModel.DataAnnotations;

namespace BulkyBook.Models
{
    public class Company
    {
        [Key]
        public int Id { set; get; }
        [Required]
        public string Name { set; get; }
        public string? StreetAddress { set; get; }
        public string? City { set; get; }
        public string? State { set; get; }
        public string? PostalCode { set; get; }
        public string? PhoneNumber { set; get; }
    }
}
