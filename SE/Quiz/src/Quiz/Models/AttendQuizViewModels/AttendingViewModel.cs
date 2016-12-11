using System.Collections.Generic;

namespace Quiz.Models.AttendQuizViewModels
{
    public class AttendingViewModel
    {
        public int DeThiId { get; set; }

        public List<QuestionItem> Questions { get; set; }
    }

    public class QuestionItem
    {
        public int CauHoiId { get; set; }

        public IEnumerable<DapAn> DapAns { get; set; }
        public string NoiDungCauHoi { get; set; }

        public int SelectedDapAn { get; set; }
    }
}