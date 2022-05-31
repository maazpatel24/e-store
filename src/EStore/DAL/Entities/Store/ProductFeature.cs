using COMN.Attributes;
using DAL.Entities.Base;
using DAL.Entities.Store.Features;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;

namespace DAL.Entities.Store
{
    [Table("ProductFeatures")]
    [Include("Product, Color, Size")]
    public class ProductFeature : BaseEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }

        [Column(TypeName = nameof(SqlDbType.Money))]
        [DefaultValue(0.0d)]
        public double Discount { get; set; }

        [Column(TypeName = nameof(SqlDbType.DateTime))]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public DateTime? CreatedAt { get; set; }

        [Column(TypeName = nameof(SqlDbType.DateTime))]
        [DefaultValue(null)]
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime? UpdatedAt { get; set; }

        [DefaultValue(false)]
        public bool IsDeleted { get; set; }

        [ForeignKey("Product")]
        [Required]
        public long ProductId { get; set; }

        public virtual Product Product { get; set; }

        [ForeignKey("Color")]
        [Required]
        public long ColorId { get; set; }

        public virtual Color Color { get; set; }

        [ForeignKey("Size")]
        [Required]
        public long SizeId { get; set; }

        public virtual Size Size { get; set; }
    }
}