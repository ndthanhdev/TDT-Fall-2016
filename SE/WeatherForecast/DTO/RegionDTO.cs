using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DTO
{
    public class RegionDTO
    {
        [Required]
        public string Name { get; set; }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string RegionId { get; set; }

        public List<StationDTO> Stations { get; set; }
    }
}