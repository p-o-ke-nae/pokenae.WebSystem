using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using pokenae.Commons.Models;

namespace pokenae.WebSystem.Core.Entities
{
    /// <summary>
    /// �y�[�W��\���G���e�B�e�B�N���X
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
        /// �e�y�[�WID
        /// </summary>
        [StringLength(5)]
        public string? ParentID { get; set; }

        /// <summary>
        /// �w�b�_�[�\���̗L��
        /// </summary>
        [Required]
        public bool IsHeader { get; set; }

        /// <summary>
        /// �\����
        /// </summary>
        [Required]
        public int DisplayOrder { get; set; }

        /// <summary>
        /// �y�[�W�̏��
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
