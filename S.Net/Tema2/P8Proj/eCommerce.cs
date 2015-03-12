using System.ComponentModel.DataAnnotations.Schema;

namespace P8Proj
{
    [Table("eCommerce", Schema = "P8")]
    public class eCommerce : Business
    {
        public string URL { get; set; }
    }
}