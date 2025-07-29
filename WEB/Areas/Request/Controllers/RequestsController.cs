using AutoMapper;
using BUSINESS.Manager.Interface;
using Microsoft.AspNetCore.Mvc;

namespace WEB.Areas.Request.Controllers
{
    [Area("Request")]
    public class RequestsController(IRequestManager requestManager, IMapper mapper) : Controller
    {
        private readonly IRequestManager _requestManager = requestManager;
        private readonly IMapper _mapper = mapper;

        public IActionResult Index()
        {
            return View();
        }
    }
}
