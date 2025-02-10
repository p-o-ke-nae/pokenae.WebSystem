using System.ComponentModel.DataAnnotations;
using pokenae.Commons.Models;

namespace pokenae.WebSystem.Core.Entities
{
    /// <summary>
    /// 役割情報を表すエンティティクラス
    /// </summary>
    public class M1Role : BaseEntity
    {
        [Key]
        [StringLength(5)]
        public string Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string Name { get; set; }

        [Required]
        public int Level { get; set; }
    }
}

