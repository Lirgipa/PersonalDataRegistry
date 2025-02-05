namespace PersonalDataDirectory.Application.Interfaces;

public interface IPhotoService
{
    Task<string> UploadPhotoAsync(int personId, IFormFile file);
}