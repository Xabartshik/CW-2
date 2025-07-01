using LinqToDB.Mapping;
using System;
using System.Collections.Generic;

using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarService.DAL.Model
{
    [Table("services")]
    public class ServiceModel
    {
        [Column("id")]
        [PrimaryKey, Identity]
        public int Id { get; set; }

        [NotNull]
        [Column("name")]
        public string Name { get; set; }

        [NotNull]
        [Column("price")]
        public double Price { get; set; }


        [Column("description")]
        public string? Description { get; set; }

    }
}
