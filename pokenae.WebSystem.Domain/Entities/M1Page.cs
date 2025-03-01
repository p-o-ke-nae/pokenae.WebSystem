using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using pokenae.Commons.Models;

namespace pokenae.WebSystem.Core.Entities
{
    /// <summary>
    /// ページを表すエンティティクラス
    /// </summary>
    [Table("M1Page")]
    public class M1Page : BaseEntity
    {
        [Key]
        [StringLength(5)]
        public string NodeID { get; set; }

        [Required]
        [MaxLength(100)]
        public string Title { get; set; }

        [Required]
        public string Content { get; set; }

        [Required]
        [MaxLength(200)]
        public string Route { get; set; }

        /// <summary>
        /// 親ページID
        /// </summary>
        [StringLength(5)]
        public string? ParentID { get; set; }

        /// <summary>
        /// ヘッダー表示の有無
        /// </summary>
        [Required]
        public bool IsHeader { get; set; }

        /// <summary>
        /// 表示順
        /// </summary>
        [Required]
        public int DisplayOrder { get; set; }

        /// <summary>
        /// ページの状態
        /// </summary>
        [Required]
        [StringLength(1)]
        public string PageState { get; set; }
    }

    public static class PageStates
    {
        public const string Unpublished = "0";
        public const string Published = "1";
        public const string Suspended = "2";
        public const string LimitedAccess = "3";
    }
}
