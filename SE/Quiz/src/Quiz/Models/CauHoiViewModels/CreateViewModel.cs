using System.ComponentModel.DataAnnotations;

namespace Quiz.Models.CauHoiViewModels
{
    public class CreateViewModel
    {
        public int NhomId { get; set; }

        [Required]
        [Display(Name = "Question's Content")]
        public string NoiDungCauHoi { get; set; }

        [Display(Name = "Group's Name")]
        public string TenNhom { get; set; }
    }
}