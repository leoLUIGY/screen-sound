namespace ScreenSound.Modelos;

public class Musica
{

    public Musica(){ }
    public Musica(string nome)
    {
        Nome = nome;
    }
    public Musica(string nome, int id, int ano)
    {
        Nome = nome;
        Id = id;
        AnoLancamento = ano;
    }

    public string Nome { get; set; }
    public int Id { get; set; }
    public int? AnoLancamento { get; set; }
    public virtual Artista? Artista { get; set; }

    public void ExibirFichaTecnica()
    {
        Console.WriteLine($"Nome: {Nome}");
      
    }

    public override string ToString()
    {
        return @$"Id: {Id}
        Nome: {Nome}";
    }
}