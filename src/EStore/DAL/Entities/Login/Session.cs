using COMN.Attributes;
using DAL.Entities.Base;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;

namespace DAL.Entities.Login
{
    [Table("Sessions")]
    [Include("User")]
    public class Session : BaseEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }

        [Required]
        [Column(TypeName = "VarChar(256)")]
        //[Column(TypeName = nameof(SqlDbType.VarChar))]
        public string Token { get; set; }

        [Column(TypeName = nameof(SqlDbType.DateTime))]
        public DateTime? Expires { get; set; }

        [Column(TypeName = nameof(SqlDbType.DateTime))]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public DateTime? CreatedAt { get; set; }

        [ForeignKey("User")]
        [Required]
        public long UserId { get; set; }

        public virtual User User { get; set; }
    }
}