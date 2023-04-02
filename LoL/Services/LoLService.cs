using System.Text.Json;
using LoL.Models;
namespace LoL.Services;
public class LoLService : ILoLService
{
    private readonly IHttpContextAccessor _session;
    private readonly string campeaoFile = @"Data\campeao.json";
    private readonly string rotaFile = @"Data\rota.json";
    public LoLService(IHttpContextAccessor session)
    {
        _session = session;
        PopularSessao();
    }
    public List<Campeao> GetCampeoes()
    {
        PopularSessao();
        var campeoes = JsonSerializer.Deserialize<List<Campeao>>
        (_session.HttpContext.Session.GetString("Campeoes"));
        return campeoes;
    }
    public List<Rota> GetRotas()
    {
        PopularSessao();
        var rotas = JsonSerializer.Deserialize<List<Rota>>
        (_session.HttpContext.Session.GetString("Rotas"));
        return rotas;
    }
    public Campeao GetCampeao(int Numero)
    {
        var campeoes = GetCampeoes();
        return campeoes.Where(p => p.Numero == Numero).FirstOrDefault();
    }
    public LoLDto GetLoLDto()
    {
        var camps = new LoLDto()
        {
            Campeoes = GetCampeoes(),
            Rotas = GetRotas()
        };
        return camps;
    }
    public DetailsDto GetDetailedCampeao(int Numero)
    {
        var campeoes = GetCampeoes();
        var camps = new DetailsDto()
        {
        Current = campeoes.Where(p => p.Numero == Numero)
            .FirstOrDefault(),
        Prior = campeoes.OrderByDescending(p => p.Numero)
            .FirstOrDefault(p => p.Numero < Numero),
        Next = campeoes.OrderBy(p => p.Numero)
            .FirstOrDefault(p => p.Numero > Numero),
        };
        return camps;   
    }
    public Rota GetRota(string Nome)
    {
        var rotas = GetRotas();
        return rotas.Where(t => t.Nome == Nome).FirstOrDefault();
    }
    private void PopularSessao()
    {
        if (string.IsNullOrEmpty(_session.HttpContext.Session.GetString("Rotas")))
        {
            _session.HttpContext.Session
                .SetString("Campeoes", LerArquivo(campeaoFile));
            _session.HttpContext.Session
                .SetString("Rotas", LerArquivo(rotaFile));
        }
    }
    private string LerArquivo(string fileName)
    {
        using (StreamReader leitor = new StreamReader(fileName))
        {
            string dados = leitor.ReadToEnd();
            return dados;
        }
    }
}
