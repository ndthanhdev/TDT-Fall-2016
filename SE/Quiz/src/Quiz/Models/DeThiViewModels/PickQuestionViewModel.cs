using System.ComponentModel.DataAnnotations;

namespace Quiz.Models.DeThiViewModels
{
    public class PickQuestionItem
    {
        public CauHoi CauHoi { get; set; }

        public bool IsPicked { get; set; }
    }

    public class PickQuestionViewModel
    {
        public int CauHoiId { get; set; }
        public int DeThiId { get; set; }

        [Required]
        [Display(Name = "Group")]
        public int SelectedNhom { get; set; }
    }
}