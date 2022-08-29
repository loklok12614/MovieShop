namespace ApplicationCore.Models;

public class CastMovieModel
{
    public int Id { get; set; }
    public string Title { get; set; } = null!;
    public DateTime? ReleaseDate { get; set; }
    public string Character { get; set; }
}