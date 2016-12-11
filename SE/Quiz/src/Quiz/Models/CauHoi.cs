using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Quiz.Models
{
    public class CauHoi
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CauHoiId { get; set; }

        public List<DapAn> DapAns { get; set; }

        [ForeignKey(nameof(NhomId))]
        public Nhom Nhom { get; set; }

        [Required]
        public int NhomId { get; set; }

        [Required]
        public string NoiDung { get; set; }
    }
}