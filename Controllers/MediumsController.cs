using Microsoft.AspNetCore.Mvc;
using CardsGeneratorBackend.Models;
using CardsGeneratorBackend.Services;

namespace CardsGeneratorBackend.Controllers;

[ApiController]

// [Route("[controller]")] // --> '/Mediums'
[Route("/bulk")]
public class MediumsController : ControllerBase
{
    private readonly ILogger<MediumsController> _logger;
    private static string NowDateTime() => $"{DateTime.UtcNow:yyyy-MM-dd_HH:mm:ss}";
    private void LogNow() => Console.WriteLine(NowDateTime());
    private async void Write(HashSet<string> cardsGenerated)
    {
        Console.WriteLine($"Generated: {cardsGenerated.Count}");
        string logDir = Path.Combine(Directory.GetCurrentDirectory(), "Logs");
        Directory.CreateDirectory(logDir);
        string logFileName = $"log_{NowDateTime()}.txt";
        string logFilePath = Path.Combine(logDir, logFileName);
        Console.WriteLine($"{logFileName} {logFilePath}");
        await System.IO.File.WriteAllLinesAsync(logFilePath, cardsGenerated);
    }

    public MediumsController(ILogger<MediumsController> logger) => _logger = logger;

    /// <summary>
    /// Generación de tarjetas por lote (mínimo 15)
    /// </summary>
    /// <remarks>
    /// Sample request:
    /// 
    ///     POST /generate
    ///     {
    ///         "count": 15,
    ///         "prefix": "914"
    ///     }
    ///     
    /// </remarks>
    /// 
    // /// <param name="item"></param>
    // /// <returns>A newly created Todo<returns>
    [HttpPost(Name = "bulk")]
    public void Post(CardsGeneratorPayload payload)
    {
        // CardGenerator cardGenerator = new();
        CardGeneratorC cardGenerator = new();
        (int Count, string Prefix) = payload;
        LogNow();
        HashSet<string> generated = cardGenerator.Generate(Count, Prefix);
        LogNow();
        Write(generated);
    }
}