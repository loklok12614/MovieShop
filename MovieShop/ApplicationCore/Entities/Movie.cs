using System.ComponentModel.DataAnnotations;

namespace ApplicationCore.Entities;

public class Movie
{
    public int Id { get; set; }
    [MaxLength(256)]
    public string Title { get; set; } = null!;
    public string Overview { get; set; } = null!;
    public string Tagline { get; set; } = null!;
    public decimal? Budget { get; set; }
    public decimal? Revenue { get; set; }
    public string ImdbUrl { get; set; } = null!;
    public string TmdbUrl { get; set; } = null!;
    public string PosterUrl { get; set; } = null!;
    public string BackdropUrl { get; set; } = null!;
    [MaxLength(64)]
    public string OriginalLanguage { get; set; } = null!;
    public DateTime? ReleaseDate { get; set; }
    public int? RunTime { get; set; }
    public decimal? Price { get; set; }
    public DateTime? CreatedDate { get; set; }
    public DateTime? UpdatedDate { get; set; }
    public string? UpdatedBy { get; set; }
    public string? CreatedBy { get; set; }

    public decimal? Rating => UsersReviewed.Count > 0 ? UsersReviewed.Average(r => r.Rating) : 0;

    public ICollection<MovieGenre> GenresOfMovie { get; set; } // One Movie has many Genre
    public ICollection<MovieCast> CastsOfMovie { get; set; }
    public ICollection<Trailer> Trailers { get; set; } //One Movie has many Trailers
    public ICollection<Favorite> UsersFavorited { get; set; } 
    public ICollection<Review> UsersReviewed { get; set; }
    public ICollection<Purchase> UsersPurchased { get; set; }

}