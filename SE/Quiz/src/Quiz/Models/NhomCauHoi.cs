using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Quiz.Models
{
    public class Nhom
    {
        public List<CauHoi> CauHois { get; set; }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int NhomId { get; set; }

        [Required]
        public string Ten { get; set; }
    }
}