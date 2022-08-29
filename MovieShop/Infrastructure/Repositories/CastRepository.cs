using ApplicationCore.Contracts.Repositories;
using ApplicationCore.Entities;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class CastRepository : ICastRepository
{
    private readonly MovieShopDbContext _movieShopDbContext;

    public CastRepository(MovieShopDbContext movieShopDbContext)
    {
        _movieShopDbContext = movieShopDbContext;
    }
    public Cast GetById(int id)
    {
        var castDetails = _movieShopDbContext.Casts
            .Include(c => c.MoviesOfCast).ThenInclude(mc => mc.Movie)
            .FirstOrDefault(c => c.Id == id);

        return castDetails;
    }
}