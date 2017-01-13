using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DTO
{
    public class StaffDTO
    {
        [Required]
        public DateTime Birthdate { get; set; }

        [Required]
        public string FullName { get; set; }

        [Required]
        public string Identity { get; set; }

        public List<JobHistoryDTO> JobHistorys { get; set; }

        [Required]
        public string Password { get; set; }

        [Required]
        public string PermanentAddress { get; set; }

        [Required]
        public DateTime RecruitmentDate { get; set; }

        [Required]
        public double Salary { get; set; }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string StaffId { get; set; }

        [ForeignKey(nameof(WorkplaceId))]
        public StationDTO Workplace { get; set; }

        public string WorkplaceId { get; set; }
    }
}