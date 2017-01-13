using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DTO
{
    public class StationDTO
    {
        [Required]
        public double Distance { get; set; }

        public List<JobHistoryDTO> JobHistorys { get; set; }

        [Required]
        public string Name { get; set; }

        public RegionDTO Region { get; set; }

        [Required]
        public string RegionId { get; set; }

        public StaffDTO Staff { get; set; }

        public string StaffId { get; set; }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string StationId { get; set; }

        public List<StatusDTO> Statuss { get; set; }
        public List<WeatherDataDTO> WeatherDatas { get; set; }
    }
}