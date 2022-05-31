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
        [Column(TypeName = nameof(SqlDbType.VarChar))]
        public string Name { get; set; }

        [Column(TypeName = nameof(SqlDbType.DateTime))]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public DateTime? CreatedAt { get; set; }

        [Column(TypeName = nameof(SqlDbType.DateTime))]
        [DefaultValue(null)]
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime? UpdatedAt { get; set; }

        [DefaultValue(false)]
        public bool IsDeleted { get; set; }

        [InverseProperty("Role")]
        public virtual List<User> Users { get; set; } = new List<User>();
    }

    public enum Roles
    {
        SysAdmin,
        Admin,
        Customer,
    }
}