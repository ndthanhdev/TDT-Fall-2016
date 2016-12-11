using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Quiz.Models.NhomViewModels
{
    public class DetailViewModel
    {
        public IEnumerable<CauHoi> CauHois { get; set; }
        public int NhomId { get; set; }

        [Display(Name = "Group's Name")]
        public string TenNhom { get; set; }
    }
}