using pokenae.Commons.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace pokenae.WebSystem.Core.Entities
{
    /// <summary>
    /// タグを表すエンティティクラス
    /// </summary>
    [Table("M1Tag")]
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
