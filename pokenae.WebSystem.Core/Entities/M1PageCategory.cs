using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using pokenae.Commons.Models;

namespace pokenae.WebSystem.Core.Entities
{
    /// <summary>
    /// ページ分類を表すエンティティクラス
    /// </summary>
    [Table("M1PageCategory")]
    public class M1PageCategory : BaseEntity
    {
        [Key]
        [StringLength(5)]
        public string Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string Name { get; set; }

        /// <summary>
        /// 表示順を表すプロパティ
        /// </summary>
        [Required]
        public int DisplayOrder { get; set; }

        /// <summary>
        /// ヘッダに表示するか否かを表すプロパティ
        /// </summary>
        [Required]
        public bool IsHeader { get; set; }
    }
}
