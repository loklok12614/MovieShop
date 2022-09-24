using ApplicationCore.Contracts.Repositories;
using ApplicationCore.Entities;
using ApplicationCore.Models;
using Infrastructure.Repositories;
using Infrastructure.Services;
using Moq;

namespace MovieShop.UnitTests;

[TestClass]
public class MovieShopUnitTest
{
    private MovieService _sut;
    private static List<Movie> _movies;

    private Mock<IMovieRepository> _mockMovieRepository;
    private Mock<IReviewRepository> _mockReviewRepository;

    [TestInitialize] // run on each test
    // [OneTimeSetup] in NUnit
    public void OneTimeSetup()
    {
        _mockMovieRepository = new Mock<IMovieRepository>();
        _mockMovieRepository.Setup(expression: m => m.GetTop30GrossingMovies()).ReturnsAsync(_movies);

        _mockReviewRepository = new Mock<IReviewRepository>();

        // Arrange
        _sut = new MovieService(_mockMovieRepository.Object, _mockReviewRepository.Object);
    }

    [ClassInitialize] // run only once before tests start
    public static void SetUp(TestContext context)
    {
        _movies = new List<Movie>
        {
            new Movie { Id = 1, Title = "Avengers", Budget = 1200000 },
            new Movie { Id = 2, Title = "Avengers1", Budget = 1200000 },
            new Movie { Id = 3, Title = "Avengers2", Budget = 1200000 },
            new Movie { Id = 4, Title = "Avengers3", Budget = 1200000 },
            new Movie { Id = 5, Title = "Avengers4", Budget = 1200000 },
            new Movie { Id = 6, Title = "Avengers5", Budget = 1200000 },
            new Movie { Id = 7, Title = "Avengers6", Budget = 1200000 }
        };
    }
    
    [TestMethod]
    public async Task TestListOfTopGrossingMoviesFromFakeData()
    {
        // SUT system under test MovieService => GetTopGrossing
        //Arrange, Act, Assert **
        
        // Arrange
        // Mock objects, data, methods etc

        // Act
        var movies = await _sut.GetTop30GrossingMovies();
        
        // Assert
        Assert.IsNotNull(movies);
    }

    [TestMethod]
    public async Task TestListOfTopGrossingMoviesReturnsType()
    {
        var movies = await _sut.GetTop30GrossingMovies();
        Assert.IsInstanceOfType(movies, typeof(List<MovieCardModel>));
    }
    
    [TestMethod]
    public async Task TestListOfTopGrossingMoviesReturns7Movies()
    {
        var movies = await _sut.GetTop30GrossingMovies();
        Assert.AreEqual(7, movies.Count());
    }
}