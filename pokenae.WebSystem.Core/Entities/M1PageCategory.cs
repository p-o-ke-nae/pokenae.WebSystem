using System.ComponentModel.DataAnnotations;
using pokenae.Commons.Models;

namespace pokenae.WebSystem.Core.Entities
{
    /// <summary>
    /// �y�[�W���ނ�\���G���e�B�e�B�N���X
    /// </summary>
    public class M1PageCategory : BaseEntity
    {
        [Key]
        [StringLength(5)]
        public string Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string Name { get; set; }
    }
}
