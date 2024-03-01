using backend.Models;
using backend.Repos.IRepos;
using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers;

[ApiController]
[Route("[controller]")]
public class WeatherForecastController : ControllerBase
{
    private static readonly string[] Summaries = new[]
    {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

    private readonly ILogger<WeatherForecastController> _logger;
    private readonly IProductRepo productRepo;
    private readonly ITicketRepo ticketRepo;

    public WeatherForecastController(ILogger<WeatherForecastController> logger, IProductRepo productRepo, ITicketRepo ticketRepo)
    {
        _logger = logger;
        this.productRepo = productRepo;
        this.ticketRepo = ticketRepo;
    }

    [HttpGet(Name = "GetWeatherForecast")]
    public IEnumerable<WeatherForecast> Get()
    {
        return Enumerable.Range(1, 5).Select(index => new WeatherForecast
        {
            Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
            TemperatureC = Random.Shared.Next(-20, 55),
            Summary = Summaries[Random.Shared.Next(Summaries.Length)]
        })
        .ToArray();
    }


    [HttpGet("/DB")]
    public async Task HidrateDB()
    {
        
        var products = new List<Product>();

        var baseId = 1000;
        for (int i = 0; i < 1000; i++)
        {
            products.Add(new Product
            {
                ProductId = baseId++,
                Nome = "Product " + i,

            });
        }

        await productRepo.createProducts(products);


        var tickets = new List<Ticket>();

        var random = new Random(DateTimeOffset.Now.Millisecond);

        var ticketBaseId = 1000;
        for (int i = 0; i < 1000000; i++)
        {
            tickets.Add(new Ticket
            {
                TicketId = ticketBaseId++,
                Titulo = "Ticket " + i,
                Descricao = "Lorem ipsum dolor sit amet",
                Prioridade = random.Next(1,6),
                ProdutoId = random.Next(1000,2000),
            });
        }

        await ticketRepo.createTickets(tickets);
    }
}
