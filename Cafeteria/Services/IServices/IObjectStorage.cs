namespace Cafeteria.Services.IServices;

public interface IObjectStorage
{
    Task UploadAsync(string key, Stream content, string contentType, CancellationToken cancellationToken);
    Task DeleteAsync(string key, CancellationToken cancellationToken);
    string GetPublicUrl(string key);
}
