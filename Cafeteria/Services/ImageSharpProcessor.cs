using Cafeteria.Services.IServices;
using SixLabors.ImageSharp.Formats.Webp;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Formats.Png;
using SixLabors.ImageSharp.Formats;
using SixLabors.ImageSharp.Formats.Jpeg;
using SixLabors.ImageSharp.Processing;

namespace Cafeteria.Services;

public class ImageSharpProcessor : IImageProcessor
{
    private const int MaxSide = 1200;
    private const int MaxInputSide = 10_000;

    public async Task<ProcessedImage> ProcessAsync(Stream input, CancellationToken cancellationToken)
    {
        // Se copia a memoria para poder leer el stream varias veces (máx. 5 MB, ya validado)
        using var buffer = new MemoryStream();
        await input.CopyToAsync(buffer, cancellationToken);

        // 1. Validar el formato REAL por los bytes
        buffer.Position = 0;
        var format = await Image.DetectFormatAsync(buffer, cancellationToken);
        if (format is not (JpegFormat or PngFormat or WebpFormat))
        {
            throw new UnknownImageFormatException("Solo se permiten imágenes JPEG, PNG o WEBP");
        }

        // 2. Revisar dimensiones antes de cargar la imagen completa
        buffer.Position = 0;
        var info = await Image.IdentifyAsync(buffer, cancellationToken);
        if (info.Width > MaxInputSide || info.Height > MaxInputSide)
        {
            throw new InvalidImageContentException("La imagen es demasiado grande");
        }

        // 3. Cargar solo el primer cuadro
        buffer.Position = 0;
        using var image = await Image.LoadAsync(new DecoderOptions { MaxFrames = 1 }, buffer, cancellationToken);

        // 4. Corregir orientación y después eliminar metadatos
        image.Mutate(x => x.AutoOrient());
        image.Metadata.ExifProfile = null;
        image.Metadata.XmpProfile = null;
        image.Metadata.IptcProfile = null;

        // 5. Redimensionar solo si excede 1200 px
        if (image.Width > MaxSide || image.Height > MaxSide)
        {
            image.Mutate(x => x.Resize(new ResizeOptions
            {
                Mode = ResizeMode.Max,
                Size = new Size(MaxSide, MaxSide)
            }));
        }

        // 6. Concertir a WebP calidad 80
        var output = new MemoryStream();
        await image.SaveAsWebpAsync(output, new WebpEncoder { Quality = 80 }, cancellationToken);
        output.Position = 0;

        return new ProcessedImage(output, image.Width, image.Height);
    }
}
