using LinqToDB.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarService.DAL.Model
{
    [Table("cars")]
    public class CarModel
    {
        [Column("id")]
        [PrimaryKey, Identity]
        public int Id { get; set; }

        [Column("brand"), NotNull]
        public string Brand { get; set; }

        [NotNull]
        [Column("model")]
        public string Model { get; set; }

        [NotNull]
        [Column("year")]
        public int Year { get; set; } = 1980;

        [Column("ownername")]
        public string? OwnerName { get; set; }

        [Column("createdat")]
        public DateTime CreatedAt { get; set; }
    }
}
