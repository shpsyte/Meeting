using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace WebApp.Controllers {
    public class BaseController : Controller {
        protected readonly IMapper _mapper;
        protected readonly ILogger _Looger;

        public BaseController (IMapper mapper, ILogger looger) {
            _mapper = mapper;
            _Looger = looger;
        }
    }
}