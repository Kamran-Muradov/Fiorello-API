using AutoMapper;
using FiorelloAPI.DTOs.Socials;
using FiorelloAPI.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace FiorelloAPI.Controllers
{
    public class SocialController : BaseController
    {
        private readonly ISocialService _socialService;
        private readonly IMapper _mapper;

        public SocialController(
            ISocialService socialService, 
            IMapper mapper)
        {
            _socialService = socialService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(_mapper.Map<IEnumerable<SocialDto>>(await _socialService.GetAllAsync()));
        }
    }
}
