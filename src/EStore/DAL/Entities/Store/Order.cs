using COMN.Attributes;
using DAL.Entities.Base;
using DAL.Entities.Login;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;

namespace DAL.Entities.Store
{
    [Table("Orders")]
    [Include("User", "OrderProducts")]
    public class Order : BaseEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }

        [Column(TypeName = nameof(SqlDbType.DateTime))]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public DateTime? CreatedAt { get; set; }

        [Column(TypeName = nameof(SqlDbType.DateTime))]
        [DefaultValue(null)]
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime? UpdatedAt { get; set; }

        [DefaultValue(false)]
        public bool IsDeleted { get; set; }

        [InverseProperty("Order")]
        public virtual List<OrderProduct> OrderProducts { get; set; } = new List<OrderProduct>();

        [ForeignKey("User")]
        [Required]
        public long UserId { get; set; }

        public virtual User User { get; set; }
    }
}