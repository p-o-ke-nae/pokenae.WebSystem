using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using pokenae.Commons.Models;

namespace pokenae.WebSystem.Core.Entities
{
    /// <summary>
    /// ユーザとページの権限状態を管理するエンティティクラス
    /// </summary>
    [Table("T1UserPageAccess")]
    public class T1UserPageAccess : BaseEntity
    {
        /// <summary>
        /// ページのノードID
        /// </summary>
        [Key]
        [StringLength(5)]
        public string NodeID { get; set; }

        /// <summary>
        /// ユーザID
        /// </summary>
        [Required]
        [StringLength(50)]
        public string UserID { get; set; }

        /// <summary>
        /// ユーザがそのページに対して行える権限
        /// </summary>
        [Required]
        public PermissionLevel Permission { get; set; }
    }

    /// <summary>
    /// 権限レベルを示す列挙型
    /// </summary>
    public enum PermissionLevel
    {
        View = 1,
        Edit = 2
    }
}
