using System.ComponentModel.DataAnnotations;

namespace ApplicationCore.Entities;

public class Trailer
{
    public int Id { get; set; }
    [MaxLength(2084)]
    public string Name { get; set; }
    [MaxLength(2084)]
    public string TrailerUrl { get; set; }

    public int MovieId { get; set; }
    public Movie Movie { get; set; }
}