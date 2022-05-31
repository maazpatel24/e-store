using COMN.Attributes;
using DAL.Entities.Base;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;

namespace DAL.Entities.Store
{
    [Table("Markas")]
    [Include("Product")]
    public class Marka : BaseEntity
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

        [Column(TypeName = nameof(SqlDbType.DateTime))]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public DateTime? CreatedAt { get; set; }

        [Column(TypeName = nameof(SqlDbType.DateTime))]
        [DefaultValue(null)]
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime? UpdatedAt { get; set; }

        [DefaultValue(false)]
        public bool IsDeleted { get; set; }

        [InverseProperty("Marka")]
        public virtual List<Product> Products { get; set; } = new List<Product>();
    }
}