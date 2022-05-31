using COMN.Attributes;
using DAL.Entities.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

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
        public string Token { get; set; }
        public DateTime? Expires { get; set; }
        public DateTime? CreatedAt { get; set; }
        [ForeignKey("User")]
        [Required]
        public long UserId { get; set; }
        public virtual User User { get; set; }
    }
}