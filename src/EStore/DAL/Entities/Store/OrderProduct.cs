using COMN.Attributes;
using DAL.Entities.Base;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DAL.Entities.Store
{
    [Table("OrderProducts")]
    [Include("Order", "Product")]
    public class OrderProduct : BaseEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }

        [ForeignKey("Order")]
        [Required]
        public long OrderId { get; set; }

        public virtual Order Order { get; set; }

        [ForeignKey("Product")]
        [Required]
        public long ProductId { get; set; }

        public virtual Product Product { get; set; }
    }
}