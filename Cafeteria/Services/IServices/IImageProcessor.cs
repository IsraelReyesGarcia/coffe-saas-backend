namespace Cafeteria.Services.IServices;

public record ProcessedImage(MemoryStream Content, int Width, int Height);
public interface IImageProcessor
{
    Task<ProcessedImage> ProcessAsync(Stream input, CancellationToken cancellationToken);

}
