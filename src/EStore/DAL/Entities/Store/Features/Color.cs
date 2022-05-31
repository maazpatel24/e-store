using DAL.Entities.Base;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;

namespace DAL.Entities.Store.Features
{
    [Table("Colors")]
    public class Color : BaseEntity
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
    }

    public enum Colors
    {
        [Description("#000000")]
        Black,

        [Description("#DD0000")]
        Red,

        [Description("#FE6230")]
        Orange,

        [Description("#FEF600")]
        Yellow,

        [Description("#00BB00")]
        Green,

        [Description("#009BFE")]
        Blue,

        [Description("#000083")]
        Indigo,

        [Description("#30009B")]
        Violet,

        [Description("#FFFFFF")]
        White
    }
}