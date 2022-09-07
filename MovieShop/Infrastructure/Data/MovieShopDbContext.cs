using ApplicationCore.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data;

public class MovieShopDbContext : DbContext
{
    public MovieShopDbContext(DbContextOptions<MovieShopDbContext> options) : base(options)
    {
        
    }
    
    //Dbsets are properties of DbContext class
    
    //Movies Table access
    public DbSet<Movie> Movies { get; set; }
    
    // Genre
    public DbSet<Genre> Genres { get; set; }
    public DbSet<MovieGenre> MovieGenres { get; set; }
    
    // Cast
    public DbSet<Cast> Casts { get; set; }
    public DbSet<MovieCast> MovieCasts { get; set; }

    // Trailer
    public DbSet<Trailer> Trailers { get; set; }
    
    // User
    public DbSet<User> Users { get; set; }
    public DbSet<Favorite> Favorites { get; set; }
    public DbSet<Review> Reviews { get; set; }
    public DbSet<Purchase> Purchases { get; set; }
    public DbSet<UserRole> UserRoles { get; set; }
    
    // Role
    public DbSet<Role> Roles { get; set; }
    
    // override the method called OnModelCreating for Fluent API
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Movie>(ConfigureMovie);
        modelBuilder.Entity<MovieGenre>(ConfigureMovieGenre);
        modelBuilder.Entity<MovieCast>(ConfigureMovieCast);

        modelBuilder.Entity<User>(ConfigureUser);
        modelBuilder.Entity<Favorite>(ConfigureFavorite);
        modelBuilder.Entity<Review>(ConfigureReview);
        modelBuilder.Entity<Purchase>(ConfigurePurchase);
        modelBuilder.Entity<UserRole>(ConfigureUserRole);
    }

    private void ConfigureMovieGenre(EntityTypeBuilder<MovieGenre> builder)
    {
        builder.ToTable("MovieGenres");
        builder.HasKey(mg => new { mg.MovieId, mg.GenreId });
    }

    private void ConfigureMovieCast(EntityTypeBuilder<MovieCast> builder)
    {
        builder.ToTable("MovieCasts");
        builder.HasKey(mc => new { mc.CastId, mc.MovieId, mc.Character });
        builder.Property(mc => mc.Character).HasMaxLength(450);
    }

    private void ConfigureUserRole(EntityTypeBuilder<UserRole> builder)
    {
        builder.ToTable("UserRoles");
        builder.HasKey(ur => new { ur.UserId, ur.RoleId });
    }

    private void ConfigureFavorite(EntityTypeBuilder<Favorite> builder)
    {
        builder.ToTable("Favorites");
        builder.HasKey(f => new { f.MovieId, f.UserId });
    }
    
    private void ConfigureReview(EntityTypeBuilder<Review> builder)
    {
        builder.ToTable("Reviews");
        builder.HasKey(r => new { r.MovieId, r.UserId });

        builder.Property(r => r.ReviewText).HasMaxLength(20000);
        builder.Property(r => r.Rating).HasColumnType("decimal(3, 2)");
        builder.Property(r => r.CreatedDate).HasDefaultValueSql("getdate()");
    }

    private void ConfigurePurchase(EntityTypeBuilder<Purchase> builder)
    {
        builder.ToTable("Purchases");
        builder.HasKey(p => new { p.MovieId, p.UserId });

        builder.Property(p => p.TotalPrice).HasColumnType("decimal(5, 2)").HasDefaultValue(0.0m);

        builder.Property(p => p.PurchaseDateTime).HasDefaultValueSql("getdate()");
    }

    private void ConfigureMovie(EntityTypeBuilder<Movie> builder)
    {
        // Specify all the Fluent API rules
        builder.HasKey(m => m.Id);
        builder.Property(m => m.Title).HasMaxLength(512);
        builder.Property(m => m.Overview).HasMaxLength(4096);
        builder.Property(m => m.Tagline).HasMaxLength(512);
        builder.Property(m => m.ImdbUrl).HasMaxLength(2084);
        builder.Property(m => m.TmdbUrl).HasMaxLength(2084);
        builder.Property(m => m.PosterUrl).HasMaxLength(2084);
        builder.Property(m => m.BackdropUrl).HasMaxLength(2084);
        builder.Property(m => m.OriginalLanguage).HasMaxLength(64);
        builder.Property(m => m.UpdatedBy).HasMaxLength(512);
        builder.Property(m => m.CreatedBy).HasMaxLength(512);

        builder.Property(m => m.Price).HasColumnType("decimal(5, 2)").HasDefaultValue(9.9m);
        builder.Property(m => m.Budget).HasColumnType("decimal(18, 4)").HasDefaultValue(9.9m);
        builder.Property(m => m.Revenue).HasColumnType("decimal(18, 4)").HasDefaultValue(9.9m);
        builder.Property(m => m.CreatedDate).HasDefaultValueSql("getdate()");
        
        // You want the property in your C# for your business logic, but not as column in database
        builder.Ignore(m => m.Rating);

        builder.HasIndex(m => m.Title);
        builder.HasIndex(m => m.Price);
        builder.HasIndex(m => m.Revenue);
        builder.HasIndex(m => m.Budget);
    }

    private void ConfigureUser(EntityTypeBuilder<User> builder)
    {
        builder.HasKey(u => u.Id);

        builder.Property(u => u.Email).HasMaxLength(256);
        builder.Property(u => u.FirstName).HasMaxLength(128);
        builder.Property(u => u.LastName).HasMaxLength(128);
        builder.Property(u => u.PhoneNumber).HasMaxLength(16);
        builder.Property(u => u.HashedPassword).HasMaxLength(1024);
        builder.Property(u => u.Salt).HasMaxLength(1024);

        builder.Property(u => u.IsLocked).HasDefaultValue(false);

        builder.HasIndex(u => u.Email).IsUnique();
    }
}