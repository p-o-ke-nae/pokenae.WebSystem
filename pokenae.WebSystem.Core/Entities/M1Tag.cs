using pokenae.Commons.Models;
using System.ComponentModel.DataAnnotations;

namespace pokenae.WebSystem.Core.Entities
{
    /// <summary>
    /// タグ情報を表すエンティティクラス
    /// </summary>
    public class M1Tag : BaseEntity
    {
        [Key]
        [StringLength(5)]
        public string Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string Name { get; set; }

        public ICollection<T1PageTag> PageTags { get; set; }
    }
}
