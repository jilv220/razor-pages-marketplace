namespace Project.Services;

public class UploadService
{
    private readonly IWebHostEnvironment _environment;

    public UploadService(IWebHostEnvironment environment)
    {
        _environment = environment;
    }

    public async Task<string> UploadImageAsync(IFormFile upload, int productId)
    {
        var extOfUpload = Path.GetExtension(upload.FileName);
        var fileName = $"{productId}-img{extOfUpload}";

        // Overwrite the filename
        var file = Path.Combine(_environment.WebRootPath, "images", fileName);
        using var fileStream = new FileStream(file, FileMode.Create);
        await upload.CopyToAsync(fileStream);

        // Return the image URI
        return $"/images/{fileName}?v={DateTime.Now.Ticks}";
    }
}
