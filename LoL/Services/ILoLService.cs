using LoL.Models;
namespace LoL.Services;
public interface ILoLService
{
    List<Campeao> GetCampeoes();
    List<Rota> GetRotas();
    Campeao GetCampeao(int Numero);
    LoLDto GetLoLDto();
    DetailsDto GetDetailedCampeao(int Numero);
    Rota GetRota(string Nome);
}
