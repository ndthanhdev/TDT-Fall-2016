using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Quiz.Models
{
    public class ChiTietBaiLam
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ChiTietBaiLamId { get; set; }

        public DapAn DapAn { get; set; }
        public int DapAnId { get; set; }
    }
}