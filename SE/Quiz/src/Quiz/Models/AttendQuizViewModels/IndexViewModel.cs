using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Quiz.Models.AttendQuizViewModels
{
    public class IndexViewModel
    {
        [Display(Name = "Quiz Id")]
        public int SelectedDeThi { get; set; }

        [Display(Name = "Lịch Sử Thi")]
        public List<BaiLamViewItem> BaiLamViewItems { get; set; }
    }

    public class BaiLamViewItem
    {
        public int BaiLamId { get; set; }

        public string TenDeThi { get; set; }

        public int Diem { get; set; }

        public int SoCau { get; set; }
    }
}