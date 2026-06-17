using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace TestApi.Models
{
    public class CandidateRequest
    {
        [Required]
        public string FirstName { get; set; } = string.Empty;

        [Required]
        public string LastName { get; set; } = string.Empty;

        [Required]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;

        [Required]
        public string PhoneNumber { get; set; } = string.Empty;

        public string? Skills { get; set; }

        [Required]
        public IFormFile? Resume { get; set; }

        public IFormFile? ProfilePhoto { get; set; }
    }
}
