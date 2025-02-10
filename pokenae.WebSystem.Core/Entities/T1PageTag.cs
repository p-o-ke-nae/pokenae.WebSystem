using pokenae.Commons.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace pokenae.WebSystem.Core.Entities
{
    /// <summary>
    /// �y�[�W�ƃ^�O�̊֘A��\���G���e�B�e�B�N���X
    /// </summary>
    public class T1PageTag : BaseEntity
    {
        [Required]
        [StringLength(5)]
        public string PageId { get; set; }

        [ForeignKey("PageId")]
        public M1Page Page { get; set; }

        [Required]
        [StringLength(5)]
        public string TagId { get; set; }

        [ForeignKey("TagId")]
        public M1Tag Tag { get; set; }
    }
}
