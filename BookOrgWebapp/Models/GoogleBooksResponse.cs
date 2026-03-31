public class GoogleBooksResponse
{
    public List<Item>? Items { get; set; }
}

public class Item
{
    public VolumeInfo? VolumeInfo { get; set; }
}

public class VolumeInfo
{
    public string? Title { get; set; }
    public string? Subtitle { get; set; }
    public List<string>? Authors { get; set; }
    public string? Description { get; set; }
    public string? Publisher { get; set; }
    public string? PublishedDate { get; set; }
    public List<string>? Categories { get; set; }
    public string? Language { get; set; }
    public int? PageCount { get; set; }

    public ImageLinks? ImageLinks { get; set; } 
}

public class ImageLinks
{
    public string? SmallThumbnail { get; set; }
    public string? thumbnail { get; set; }
}