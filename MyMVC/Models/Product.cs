using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyMVC.Models
{
    public class Product
    {
        [Key]
        public int ProductID { get; set; }

        [Column(TypeName = "varchar(40)")]
        [Required]
        public string ProductName { get; set; }

        [Column(TypeName = "varchar(20)")]
        [Required]
        public string Weight { get; set; }

        [Column(TypeName = "decimal(10, 2)")]
        [Required]
        public double UnitPrice { get; set; }

        [Column(TypeName = "int")]
        [Required]
        public int UnitsInStock { get; set; }


    }
}