using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Quiz.Models
{
    public class BaiLam
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int BaiLamId { get; set; }

        public List<ChiTietBaiLam> ChiTietBaiLams { get; set; }

        [ForeignKey(nameof(DeThiId))]
        public DeThi DeThi { get; set; }

        [Required]
        public int DeThiId { get; set; }

        [ForeignKey(nameof(UserId))]
        public ApplicationUser User { get; set; }

        [Required]
        public string UserId { get; set; }
    }
}