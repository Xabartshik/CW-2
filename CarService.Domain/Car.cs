using System.ComponentModel.DataAnnotations;

namespace CarService.Domain
{
    public class Car
    {
        public int Id { get; set; }
        //public string Brand
        //{
        //    get
        //    {
        //        return _brand;
        //    }
        //    set
        //    {
        //        value ??= "Unknown";
        //        _brand = value.Trim() ?? "Unknown";
        //    }
        //}
        //private string _model;

        //[Required]
        //public string Model
        //{
        //    get
        //    {
        //        return _model;
        //    }
        //    set
        //    {
        //        value ??= "Unknown";
        //        _model = value.Trim() ?? "Unknown";
        //    }
        //}
        //private int _year;
        //[Range(1980, int.MaxValue)]
        //public int Year
        //{
        //    get
        //    {
        //        return _year;
        //    }
        //    set
        //    {
        //        _year = value < 1980? 1980 : value;
        //    }
        //}
        [Required]
        public string Brand { get; set; }
        [Required]
        public string Model { get; set; }
        [Range(1980, int.MaxValue)]
        public int Year { get; set; }
        public string? OwnerName { get; set; }
        public static bool Validate(Car car)
        {
            if (car == null)
                return false;
            if (string.IsNullOrEmpty(car.Brand) || string.IsNullOrEmpty(car.Model))
                return false;
            if (car.Year < 1980)
                return false;
            return true;
        }
    }
}
