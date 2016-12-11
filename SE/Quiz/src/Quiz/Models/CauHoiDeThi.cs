using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Quiz.Models
{
    public class CauHoiDeThi
    {
        [ForeignKey(nameof(CauHoiId))]
        public CauHoi CauHoi { get; set; }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CauHoiDeThiId { get; set; }

        [Required]
        public int CauHoiId { get; set; }

        [ForeignKey(nameof(DeThiId))]
        public DeThi DeThi { get; set; }

        [Required]
        public int DeThiId { get; set; }
    }
}