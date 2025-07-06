using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class CloudinaryController : ControllerBase
{
    private readonly CloudinaryService _cloudinaryService;

    public CloudinaryController(CloudinaryService cloudinaryService)
    {
        _cloudinaryService = cloudinaryService;
    }

    [HttpGet("signature")]
    public IActionResult GetSignature([FromQuery] string? folder = null)
    {
        var (signature, timestamp, apiKey, cloudName) = _cloudinaryService.GenerateUploadSignature(folder);
        return Ok(new { signature, timestamp, apiKey, cloudName });
    }
} 