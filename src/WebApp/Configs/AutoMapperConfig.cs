using AutoMapper;
using Business.Models;
using WebApp.ViewModels;

namespace WebApp.Configs {
    public class AutoMapperConfig : Profile {
        public AutoMapperConfig () {
            CreateMap<Meeting, MeetingViewModel> ().ReverseMap ();

        }

    }

}