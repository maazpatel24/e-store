using COMN.Attributes;
using DAL.Entities.Base;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;

namespace DAL.Entities.Login
{
    [Table("Users")]
    [Include("Role")]
    public class User : BaseEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }

        [Required]
        [Column(TypeName = nameof(DbType.AnsiString))]
        [MinLength(3), MaxLength(128)]
        public string Username { get; set; }

        [Required]
        [Column(TypeName = nameof(DbType.AnsiString))]
        public string Token { get; set; }

        [Required]
        [Exclude]
        public byte[] PasswordHash { get; set; }

        [Required]
        [Exclude]
        public byte[] PasswordSalt { get; set; }

        [Column(TypeName = nameof(DbType.DateTime))]
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime? CreatedAt { get; set; }

        [Column(TypeName = nameof(DbType.DateTime))]
        [DefaultValue(null)]
        public DateTime? UpdatedAt { get; set; }

        [DefaultValue(false)]
        public bool IsDeleted { get; set; }

        [ForeignKey("Role")]
        [Required]
        public long RoleId { get; set; }

        public virtual Role Role { get; set; }
    }
}