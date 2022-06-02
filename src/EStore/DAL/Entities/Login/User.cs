using COMN.Attributes;
using DAL.Entities.Base;
using DAL.Entities.Store;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;

namespace DAL.Entities.Login
{
    [Table("Users")]
    [Include("Role", "Comments")]
    public class User : BaseEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }

        [Required]
        [Column(TypeName = nameof(SqlDbType.VarChar))]
        [MinLength(3), MaxLength(128)]
        public string Username { get; set; }

        [Required]
        [Column(TypeName = "VarChar(256)")]
        public string Token { get; set; }

        [Required]
        [Exclude]
        public byte[] PasswordHash { get; set; }

        [Required]
        [Exclude]
        public byte[] PasswordSalt { get; set; }

        [Column(TypeName = nameof(SqlDbType.DateTime))]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public DateTime? CreatedAt { get; set; }

        [Column(TypeName = nameof(SqlDbType.DateTime))]
        [DefaultValue(null)]
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime? UpdatedAt { get; set; }

        [DefaultValue(false)]
        public bool IsDeleted { get; set; }

        [ForeignKey("Role")]
        [Required]
        public long RoleId { get; set; }

        public virtual Role Role { get; set; }

        [InverseProperty("User")]
        public virtual List<Comment> Comments { get; set; } = new List<Comment>();
    }
}