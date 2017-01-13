using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DTO
{
    public class WeatherDataDTO
    {
        public double? Barometer { get; set; }

        public double? Humidity { get; set; }

        [ForeignKey(nameof(StationId))]
        public StationDTO Station { get; set; }

        public string StationId { get; set; }

        public double? Temperature { get; set; }

        [Required]
        public DateTime Time { get; set; }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string WeatherDataId { get; set; }
    }
}