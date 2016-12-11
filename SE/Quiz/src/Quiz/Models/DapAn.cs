using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Quiz.Models
{
    public class DapAn
    {
        [ForeignKey(nameof(CauHoiId))]
        public CauHoi CauHoi { get; set; }

        [Required]
        public int CauHoiId { get; set; }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int DapAnId { get; set; }

        public bool IsTrue { get; set; }

        [Required]
        public string NoiDung { get; set; }
    }
}