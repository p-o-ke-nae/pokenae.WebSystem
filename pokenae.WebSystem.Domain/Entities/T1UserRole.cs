using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using pokenae.Commons.Models;

namespace pokenae.WebSystem.Core.Entities
{
    /// <summary>
    /// ���[�U�[�Ɩ����̊֘A��\���G���e�B�e�B�N���X
    /// </summary>
    [Table("T1UserRole")]
    public class T1UserRole : BaseEntity
    {
        [Required]
        [StringLength(5)]
        public string UserId { get; set; }

        [Required]
        [StringLength(5)]
        public string RoleId { get; set; }

        [ForeignKey("RoleId")]
        public M1Role Role { get; set; }
    }
}

