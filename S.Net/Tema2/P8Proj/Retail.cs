using System.ComponentModel.DataAnnotations.Schema;

namespace P8Proj
{
    [Table("Retail", Schema = "P8")]
    public class Retail : Business
    {
        public string Address { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string ZIPCode { get; set; }
    }
}