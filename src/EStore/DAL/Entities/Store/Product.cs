﻿using COMN.Attributes;
using DAL.Entities.Base;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;

namespace DAL.Entities.Store
{
    [Table("Products")]
    [Include("Marka, ProductFeature")]
    public class Product : BaseEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }

        [Required]
        [MinLength(3), MaxLength(128)]
        [Column(TypeName = nameof(DbType.String))]
        public string Name { get; set; }

        [MinLength(3), MaxLength(256)]
        [Column(TypeName = nameof(DbType.String))]
        public string Description { get; set; }

        [Required]
        [Column(TypeName = nameof(DbType.Currency))]
        public double Price { get; set; }

        [Column(TypeName = nameof(DbType.DateTime))]
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime? CreatedAt { get; set; }

        [Column(TypeName = nameof(DbType.DateTime))]
        [DefaultValue(null)]
        public DateTime? UpdatedAt { get; set; }

        [DefaultValue(false)]
        public bool IsDeleted { get; set; }

        [ForeignKey("Marka")]
        [Required]
        public long MarkaId { get; set; }

        public virtual Marka Marka { get; set; }

        [InverseProperty("ProductId")]
        public virtual List<ProductFeature> Features { get; set; } = new List<ProductFeature>();
    }
}