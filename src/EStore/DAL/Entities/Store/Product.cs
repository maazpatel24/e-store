using COMN.Attributes;
using DAL.Entities.Base;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;

namespace DAL.Entities.Store
{
    [Table("Products")]
    [Include("Marka", "Features", "Comments")]
    public class Product : BaseEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }

        [Required]
        [MinLength(3), MaxLength(128)]
        [Column(TypeName = nameof(SqlDbType.NVarChar))]
        public string Name { get; set; }

        [MinLength(3), MaxLength(256)]
        [Column(TypeName = nameof(SqlDbType.NVarChar))]
        public string Description { get; set; }

        [Required]
        [Column(TypeName = nameof(SqlDbType.Money))]
        public double Price { get; set; }

        [Column(TypeName = nameof(SqlDbType.DateTime))]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public DateTime? CreatedAt { get; set; }

        [Column(TypeName = nameof(SqlDbType.DateTime))]
        [DefaultValue(null)]
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime? UpdatedAt { get; set; }

        [DefaultValue(false)]
        public bool IsDeleted { get; set; }

        [ForeignKey("Marka")]
        [Required]
        public long MarkaId { get; set; }

        public virtual Marka Marka { get; set; }

        [InverseProperty("Product")]
        public virtual List<ProductFeature> Features { get; set; } = new List<ProductFeature>();

        [InverseProperty("Product")]
        public virtual List<Comment> Comments { get; set; } = new List<Comment>();
    }
}