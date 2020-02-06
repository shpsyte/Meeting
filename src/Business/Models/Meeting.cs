using System;

namespace Business.Models {
    public class Meeting : Entity {

        public DateTime Data { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public bool Active { get; set; }

    }
}