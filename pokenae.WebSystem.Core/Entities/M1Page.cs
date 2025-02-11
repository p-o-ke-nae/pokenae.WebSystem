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
        public string Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string Title { get; set; }

        [Required]
        public string Content { get; set; }

        [Required]
        [MaxLength(200)]
        public string Route { get; set; }

        /// <summary>
        /// �y�[�W�J�e�S��ID
        /// </summary>
        [Required]
        [StringLength(5)]
        public string PageCategoryId { get; set; }

        /// <summary>
        /// �y�[�W�J�e�S��
        /// </summary>
        public M1PageCategory PageCategory { get; set; }
    }
}
