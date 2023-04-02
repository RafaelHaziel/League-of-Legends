namespace LoL.Models;

public class Campeao
{
    //Atributos
    public int Numero { get; set; }
    public string Nome { get; set; }
    public string Descricao { get; set; }
    public List<string> Rota { get; set; }
    public string Funcao { get; set; }
    public string Imagem { get; set; }

    // Método Construtor
    public Campeao()
    {
        Rota = new List<string>();
    }
}
