using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace Intelli.AgentPortal.Shared.DTO
{
    /// <summary>
    /// The file dto.
    /// Used to sent file from form http request to api server.
    /// </summary>
    public class FileDto
    {
        /// <summary>
        /// Gets or sets the file from form http request.
        /// </summary>
        [Required]
        public IFormFile File { get; set; }
    }
}
