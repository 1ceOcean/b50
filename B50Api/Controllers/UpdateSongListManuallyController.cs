using b50.Services;
using Microsoft.AspNetCore.Mvc;

namespace b50.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UpdateSongListManuallyController : ControllerBase
    {
        private readonly IFetchSongList _songlists;

        UpdateSongListManuallyController(IFetchSongList songlists)
        {
            _songlists = songlists;
        }

        [HttpPost("Update")]
        public async Task<IActionResult> Update()
        {
            try
            {
                await _songlists.UpDateDict();
            }
            catch (Exception e)
            {

                return BadRequest(e);
            }

            return Ok();
        }
    }
}
