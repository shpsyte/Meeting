using System;
using System.ComponentModel.DataAnnotations;

namespace WebApp.ViewModels {
    public class MeetingViewModel {

        [Key]
        public string Guid { get; set; }

        [Required]
        public DateTime Data { get; set; }
        public string Name { get; set; }

        [Required]
        public string Email { get; set; }
        public bool Active { get; set; }
    }
}