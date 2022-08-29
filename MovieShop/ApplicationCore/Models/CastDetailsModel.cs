using ApplicationCore.Entities;

namespace ApplicationCore.Models;

public class CastDetailsModel
{
    public CastDetailsModel()
    {
        Movies = new List<CastMovieModel>();
    }
    
    public int Id { get; set; }
    public string Name { get; set; }
    public string Gender { get; set; }
    public string ProfilePath { get; set; }
    public string TmdbUrl { get; set; }

    public List<CastMovieModel> Movies { get; set; }
}