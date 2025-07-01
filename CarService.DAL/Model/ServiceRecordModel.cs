using LinqToDB.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarService.DAL.Model
{
    [Table("servicerecordmodel")]
    public class ServiceRecordModel
    {
        [Column("id")]
        [PrimaryKey, Identity]
        public int Id { get; set; }

        [NotNull]
        [Column("carid")]
        public int CarId { get; set; }

        [NotNull]
        [Column("serviceid")]
        public int ServiceId { get; set; }

        [NotNull]
        [Column("dateservice")]
        public DateTime Date { get; set; }

        [Column("status"), NotNull]
        public string Status { get; set; }
    }
}
