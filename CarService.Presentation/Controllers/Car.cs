using System.ComponentModel.DataAnnotations;

namespace CarService.Presentation.Controllers
{
    public class Car
    {
        public int Id { get; set; }
        [Required]
        private string _brand;
        public string Brand
        {
            get
            {
                return _brand;
            }
            set
            {
                value ??= "Unknown";
                _brand = value.Trim() ?? "Unknown";
            }
        }
        [Required]
        public string Model { get; set; }

        public int Year { get; set; }

        public string? OwnerName { get; set; }
    }
}
