using FiorelloAPI.DTOs.Settings;
using FiorelloAPI.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace FiorelloAPI.Controllers.Admin
{
    public class SettingController : BaseController
    {
        private readonly ISettingService _settingService;

        public SettingController(ISettingService settingService)
        {
            _settingService = settingService;
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Edit([FromRoute] int id, [FromBody] SettingEditDto request)
        {
            var setting = await _settingService.GetByIdAsync(id);

            if (setting == null) return NotFound();

            if (!ModelState.IsValid) return BadRequest(ModelState);

            await _settingService.EditAsync(setting, request);

            return Ok();
        }
    }
}
