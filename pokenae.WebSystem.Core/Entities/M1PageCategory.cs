using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using pokenae.Commons.Models;

namespace pokenae.WebSystem.Core.Entities
{
    /// <summary>
    /// �y�[�W���ނ�\���G���e�B�e�B�N���X
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
        /// �\������\���v���p�e�B
        /// </summary>
        [Required]
        public int DisplayOrder { get; set; }

        /// <summary>
        /// �w�b�_�ɕ\�����邩�ۂ���\���v���p�e�B
        /// </summary>
        [Required]
        public bool IsHeader { get; set; }
    }
}
