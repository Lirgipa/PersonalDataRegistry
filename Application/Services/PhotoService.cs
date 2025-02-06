using PersonalDataDirectory.Application.Interfaces;
using PersonalDataDirectory.Infrastructure.Repositories;
using PersonalDataDirectory.Infrastructure.UnitOfWork;

namespace PersonalDataDirectory.Application.Services;

public class PhotoService : IPhotoService
{
    private readonly IPersonRepository _personRepository;
    private readonly IWebHostEnvironment _environment;
    private readonly IUnitOfWork _unitOfWork;

    public PhotoService(IPersonRepository personRepository, IWebHostEnvironment environment, IUnitOfWork unitOfWork)
    {
        _personRepository = personRepository;
        _environment = environment;
        _unitOfWork = unitOfWork;
    }

    public async Task<string> UploadPhotoAsync(int personId, IFormFile file)
    {
        var person = await _personRepository.GetByIdAsync(personId);
        if (person == null)
            throw new KeyNotFoundException("Person not found.");

        if (file == null || file.Length == 0)
            throw new ArgumentException("Invalid file.");

        var uploadsFolder = Path.Combine(_environment.WebRootPath, "uploads");
        if (!Directory.Exists(uploadsFolder))
            Directory.CreateDirectory(uploadsFolder);

        var fileName = $"{Guid.NewGuid()}{Path.GetExtension(file.FileName)}";
        var filePath = Path.Combine(uploadsFolder, fileName);

        await using (var stream = new FileStream(filePath, FileMode.Create))
        {
            await file.CopyToAsync(stream);
        }

        person.PhotoPath = $"/uploads/{fileName}";
        await _unitOfWork.TryCommitAsync(async () =>
        {
            await _personRepository.UpdateAsync(person);
        });

        return person.PhotoPath;
    }
}