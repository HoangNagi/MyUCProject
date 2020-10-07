using Microsoft.AspNetCore.Http;

namespace StudentDocApp.Services
{
    public interface IFileService
    {
        string Upload(IFormFile file);

    }
}
