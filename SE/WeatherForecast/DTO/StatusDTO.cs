using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DTO
{
    public class StatusDTO
    {
        public string Description { get; set; }

        public bool IsBarometerWorking { get; set; }

        public bool IsHumidityWorking { get; set; }

        public bool IsNew { get; set; }

        public bool IsTemperatureWorking { get; set; }

        public StationDTO Station { get; set; }

        public string StationId { get; set; }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string StatusId { get; set; }

        [Required]
        public DateTime Time { get; set; }
    }
}