using Amazon.S3;
using Amazon.S3.Model;
using Cafeteria.Options;
using Cafeteria.Services.IServices;
using Microsoft.Extensions.Options;

namespace Cafeteria.Services;

public class RustFsStorage: IObjectStorage
{
    private readonly IAmazonS3 _s3;
    private readonly RustFsOptions _options;

    public RustFsStorage(IAmazonS3 s3, IOptions<RustFsOptions> options)
    {
        _s3 = s3;
        _options = options.Value;
    }

    public async Task UploadAsync(string key, Stream content, string contentType, CancellationToken cancellationToken)
    {
        var request = new PutObjectRequest
        {
            BucketName = _options.BucketName,
            Key = key,
            InputStream = content,
            ContentType = contentType
        };
        await _s3.PutObjectAsync(request, cancellationToken);
    }

    public async Task DeleteAsync(string key, CancellationToken cancellationToken)
    {
        await _s3.DeleteObjectAsync(_options.BucketName, key, cancellationToken);
    }

    public string GetPublicUrl(string key) =>
        $"{_options.PublicBaseUrl.TrimEnd('/')}/{key}";
}
