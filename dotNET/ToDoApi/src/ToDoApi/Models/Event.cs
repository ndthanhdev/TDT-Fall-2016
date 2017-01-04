using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace ToDoApi.Models
{
    public class Event
    {
        [Key]
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string EventId { get; set; }
        public string EventName { get; set; }
        [Required]
        public DateTime Start { get; set; }
        public string Location { get; set; }
        public string Description { get; set; }

        //public string AccountID { get; set; }
        public Account Account { get; set; }

    }
}
