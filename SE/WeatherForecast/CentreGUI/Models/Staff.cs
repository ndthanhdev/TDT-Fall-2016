using System;
using System.ComponentModel;

namespace CentreGUI.Models
{
    public class Staff : IEditableObject
    {
        private Staff backupCopy;
        private bool inEdit;
        public DateTime Birthdate { get; set; }
        public string FullName { get; set; }
        public string Identity { get; set; }
        public double Income { get; set; }
        public string Password { get; set; }
        public string PermanentAddress { get; set; }
        public DateTime RecruitmentDate { get; set; }
        public double Salary { get; set; }
        public string StaffId { get; set; }
        public string WorkplaceId { get; set; }

        public void BeginEdit()
        {
            if (inEdit) return;
            inEdit = true;
            backupCopy = this.MemberwiseClone() as Staff;
        }

        public void CancelEdit()
        {
            if (!inEdit) return;
            inEdit = false;
            this.Password = backupCopy.Password;
            this.FullName = backupCopy.FullName;
            this.Birthdate = backupCopy.Birthdate;
            this.PermanentAddress = backupCopy.PermanentAddress;
            this.Identity = backupCopy.Identity;
            this.Salary = backupCopy.Salary;
        }

        public void EndEdit()
        {
            if (!inEdit) return;
            inEdit = false;
            backupCopy = null;
        }

        //public double Income { get; set; }
    }
}