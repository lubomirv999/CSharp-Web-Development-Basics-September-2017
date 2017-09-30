using System.Collections.Generic;

public class Tag
{
    public int Id { get; set; }

    [Tag]
    public string TagValue { get; set; }

    public List<AlbumTag> Albums { get; set; } = new List<AlbumTag>();
}