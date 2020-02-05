using System;
using System.ComponentModel.DataAnnotations;

namespace WebApp.ViewModels {
    public class MeetingViewModel {
        public MeetingViewModel () {
            Data = DateTime.UtcNow;
            Active = true;
        }

        [Key]
        public Guid Id { get; set; }

        [Required]
        public DateTime Data { get; set; }
        public string Name { get; set; }

        [Required]
        public string Email { get; set; }
        public bool Active { get; set; }
    }
}