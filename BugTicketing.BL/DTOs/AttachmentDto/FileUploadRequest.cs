using Microsoft.AspNetCore.Http;

namespace BugTicketing.BL
{
    public record FileUploadRequest(IFormFile File);
}
