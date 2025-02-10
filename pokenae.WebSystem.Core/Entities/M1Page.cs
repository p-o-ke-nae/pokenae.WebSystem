using System.ComponentModel.DataAnnotations;
using pokenae.Commons.Models;

namespace pokenae.WebSystem.Core.Entities
{
    /// <summary>
    /// �y�[�W����\���G���e�B�e�B�N���X
    /// </summary>
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
        /// �y�[�W����ID
        /// </summary>
        [Required]
        [StringLength(5)]
        public string PageCategoryId { get; set; }

        /// <summary>
        /// �y�[�W����
        /// </summary>
        public M1PageCategory PageCategory { get; set; }
    }
}
