using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace shop_app.Models
{
    public class Product
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; } = 0;
        [Required]
        [StringLength(50)]
        public string? Name { get; set; } = string.Empty;
        [Required]
        [Precision(10, 2)]
        public decimal Price { get; set; } = decimal.Zero;
        [Required]
        [StringLength(1024)]
        public string? Description { get; set; } = string.Empty;
    }
}
