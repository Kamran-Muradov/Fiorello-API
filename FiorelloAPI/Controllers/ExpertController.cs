using AutoMapper;
using FiorelloAPI.DTOs.Experts;
using FiorelloAPI.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace FiorelloAPI.Controllers
{
    public class ExpertController : BaseController
    {
        private readonly IExpertService _expertService;
        private readonly IMapper _mapper;

        public ExpertController(
            IExpertService expertService,
            IMapper mapper)
        {
            _expertService = expertService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(_mapper.Map<IEnumerable<ExpertDto>>(await _expertService.GetAllAsync()));
        }
    }
}
