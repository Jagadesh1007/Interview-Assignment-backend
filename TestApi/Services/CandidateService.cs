using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.IO;
using System.Threading.Tasks;
using TestApi.Models;

namespace TestApi.Services
{
    public class CandidateService : ICandidateService
    {
        private readonly IWebHostEnvironment _env;
        private readonly ILogger<CandidateService> _logger;

        public CandidateService(IWebHostEnvironment env, ILogger<CandidateService> logger)
        {
            _env = env;
            _logger = logger;
        }

        public async Task SaveCandidateAsync(CandidateRequest request)
        {
            var uploadsRoot = Path.Combine(_env.ContentRootPath, "Uploads");
            var resumesPath = Path.Combine(uploadsRoot, "Resumes");
            var photosPath = Path.Combine(uploadsRoot, "Photos");

            Directory.CreateDirectory(resumesPath);
            Directory.CreateDirectory(photosPath);

            // Save resume
            var resumeFileName = $"{Guid.NewGuid():N}_resume{Path.GetExtension(request.Resume!.FileName)}";
            var resumeFullPath = Path.Combine(resumesPath, resumeFileName);
            await SaveFileAsync(request.Resume!, resumeFullPath);

            string? photoFullPath = null;
            if (request.ProfilePhoto != null && request.ProfilePhoto.Length > 0)
            {
                var photoFileName = $"{Guid.NewGuid():N}_photo{Path.GetExtension(request.ProfilePhoto.FileName)}";
                photoFullPath = Path.Combine(photosPath, photoFileName);
                await SaveFileAsync(request.ProfilePhoto, photoFullPath);
            }

            _logger.LogInformation("Candidate received: {FirstName} {LastName} Email:{Email} Phone:{Phone} Resume:{ResumePath} Photo:{PhotoPath} Skills:{Skills}",
                request.FirstName, request.LastName, request.Email, request.PhoneNumber, resumeFullPath, photoFullPath ?? string.Empty, request.Skills ?? string.Empty);
        }

        private static async Task SaveFileAsync(IFormFile file, string path)
        {
            await using var stream = new FileStream(path, FileMode.Create);
            await file.CopyToAsync(stream);
        }
    }
}
