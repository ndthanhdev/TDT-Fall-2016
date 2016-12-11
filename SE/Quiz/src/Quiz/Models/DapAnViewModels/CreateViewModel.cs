using System.ComponentModel.DataAnnotations;

namespace Quiz.Models.DapAnViewModels
{
    public class CreateViewModel
    {
        public int CauHoiId { get; set; }

        [Required]
        [Display(Name = "Is True")]
        public bool IsTrue { get; set; }

        [Display(Name = "Question's Content")]
        public string NoiDungCauHoi { get; set; }

        [Required]
        [Display(Name = "Answer's Content")]
        public string NoiDungDapAn { get; set; }
    }
}