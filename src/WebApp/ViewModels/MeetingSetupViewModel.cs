using System;
using System.ComponentModel.DataAnnotations;

namespace WebApp.ViewModels {
    public class MeetingSetupViewModel {

        [Key]
        public Guid Id { get; set; }

        [Required]
        public string Link { get; set; }

        [Required]
        public DateTime Data { get; set; }
    }
}