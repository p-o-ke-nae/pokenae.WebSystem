using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using pokenae.Commons.Models;

namespace pokenae.WebSystem.Core.Entities
{
    /// <summary>
    /// ���[�U�ƃy�[�W�̌�����Ԃ��Ǘ�����G���e�B�e�B�N���X
    /// </summary>
    [Table("T1UserPageAccess")]
    public class T1UserPageAccess : BaseEntity
    {
        /// <summary>
        /// �y�[�W�̃m�[�hID
        /// </summary>
        [Key]
        [StringLength(5)]
        public string NodeID { get; set; }

        /// <summary>
        /// ���[�UID
        /// </summary>
        [Required]
        [StringLength(50)]
        public string UserID { get; set; }

        /// <summary>
        /// ���[�U�����̃y�[�W�ɑ΂��čs���錠��
        /// </summary>
        [Required]
        public PermissionLevel Permission { get; set; }
    }

    /// <summary>
    /// �������x���������񋓌^
    /// </summary>
    public enum PermissionLevel
    {
        View = 1,
        Edit = 2
    }
}
