using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using LoL.Models;
using LoL.Services;
namespace LoL.Controllers;
public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly ILoLService _lolService;
    public HomeController(ILogger<HomeController> logger, ILoLService lolService)
    {
        _logger = logger;
        _lolService = lolService;
    }
    public IActionResult Index(string rota)
    {
        var camps = _lolService.GetLoLDto();
        ViewData["filter"] = string.IsNullOrEmpty(rota) ? "all" : rota;
        return View(camps);
    }
    public IActionResult Details(int Numero)
    {
        var campeao = _lolService.GetDetailedCampeao(Numero);
        return View(campeao);
    }
    public IActionResult Privacy()
    {
        return View();
    }
    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id
                    ?? HttpContext.TraceIdentifier });
    }
}