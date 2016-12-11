using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Quiz.Models
{
    public class DeThi
    {
        public List<BaiLam> BaiLams { get; set; }

        public List<CauHoiDeThi> CauHoiDeThis { get; set; }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int DeThiId { get; set; }

        [Required]
        public string Ten { get; set; }

        [Required]
        public TimeSpan ThoiGian { get; set; }
    }
}