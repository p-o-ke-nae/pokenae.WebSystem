using System;
using System.ComponentModel.DataAnnotations;

namespace pokenae.Commons.Models
{
    /// <summary>
    /// 共通フィールドを持つ基底クラス
    /// </summary>
    public abstract class BaseEntity
    {
        [Required]
        public string CreatedBy { get; set; }

        [Required]
        public DateTime CreatedAt { get; set; }

        [Required]
        public string CreatedProgramId { get; set; }

        [Required]
        public string UpdatedBy { get; set; }

        [Required]
        public DateTime UpdatedAt { get; set; }

        [Required]
        public string UpdatedProgramId { get; set; }

        public string? DeletedBy { get; set; }

        public DateTime? DeletedAt { get; set; }

        public string? DeletedProgramId { get; set; }

        [ConcurrencyCheck]
        public int Version { get; set; }
    }
}
