namespace AB12.Application.Base.Common;

public interface IImageEntity
{
    public byte[]? ImageData { get; set; }
    public string? ImageMimeType { get; set; }
}
