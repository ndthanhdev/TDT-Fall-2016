using System.ComponentModel.DataAnnotations;

namespace Quiz.Models.AttendQuizViewModels
{
    public class IndexViewModel
    {
        [Display(Name = "Quiz Id")]
        public int SelectedDeThi { get; set; }
    }
}