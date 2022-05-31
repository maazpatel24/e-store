using COMN.Attributes;
using DAL.Entities.Base;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;

namespace DAL.Entities.Login
{
    [Table("Roles")]
    [Include("User")]
    public class Role : BaseEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }

        [Required]
        [MinLength(3), MaxLength(128)]
        [Column(TypeName = nameof(DbType.AnsiString))]
        public string Name { get; set; }

        [Column(TypeName = nameof(DbType.DateTime))]
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime? CreatedAt { get; set; }

        [Column(TypeName = nameof(DbType.DateTime))]
        [DefaultValue(null)]
        public DateTime? UpdatedAt { get; set; }

        [DefaultValue(false)]
        public bool IsDeleted { get; set; }

        [InverseProperty("RoleId")]
        public virtual List<User> Users { get; set; } = new List<User>();
    }

    public enum Roles
    {
        SysAdmin,
        Admin,
        Customer,
    }
}