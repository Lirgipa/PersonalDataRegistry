using Microsoft.AspNetCore.Mvc;

namespace PersonalDataDirectory.Controllers;

public partial class PersonController
{
    [HttpPost("{id}/photo")]
    public async Task<IActionResult> UploadPhoto(int id, IFormFile file)
    {
        var photoPath = await _photoService.UploadPhotoAsync(id, file);
        return Ok(new { Message = "Photo uploaded successfully.", Path = photoPath });
    }
    
    [HttpGet("{id}/photo")]
    public async Task<IActionResult> GetPhoto(int id)
    {
        var person = await _personService.GetByIdAsync(id);
        if (person == null || string.IsNullOrEmpty(person.PhotoPath))
            return NotFound("Photo not found.");

        var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", person.PhotoPath.TrimStart('/'));

        if (!System.IO.File.Exists(filePath))
            return NotFound("Photo file not found.");

        return PhysicalFile(filePath, "image/jpeg");
    }
}