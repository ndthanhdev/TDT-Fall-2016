using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DTO
{
    public class JobHistoryDTO
    {
        public DateTime? EndDate { get; set; }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string JobHistoryId { get; set; }

        [ForeignKey(nameof(StaffId))]
        public StaffDTO Staff { get; set; }

        [Required]
        public string StaffId { get; set; }

        [Required]
        public DateTime StartDate { get; set; }

        [ForeignKey(nameof(WorkplaceId))]
        public StationDTO Workplace { get; set; }

        [Required]
        public string WorkplaceId { get; set; }
    }
}