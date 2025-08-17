using Microsoft.AspNetCore.Mvc;
using ScreenSound.API.Requests;
using ScreenSound.API.Response;
using ScreenSound.Banco;
using ScreenSound.Modelos;

namespace ScreenSound.API.Endpoints
{
    public static class MusicaExtensions
    {
        public static void AddEndpontsMusicas(this WebApplication app)
        {

            app.MapGet("/Musicas", ([FromServices] DAL<Musica> dal) =>
            {
                return Results.Ok(dal.Listar());
            });

            app.MapGet("/Musicas/{nome}", ([FromServices] DAL<Musica> dal, string nome) =>
            {
                var musica = dal.RecuperarPor(a => a.Nome.ToUpper().Equals(nome.ToUpper()));
                if (musica is null)
                {
                    return Results.NotFound();
                }
                return Results.Ok(musica);
            });

            app.MapPost("/Musicas", ([FromServices] DAL<Musica> dal, [FromBody] MusicaRequest musicaRequest) =>
            {
                Musica musica = new Musica(musicaRequest.nome, musicaRequest.ArtistaId, musicaRequest.anoLancamento);
                dal.Adicionar(musica);
                return Results.Ok();
            });

            app.MapDelete("/Musicas/{id}", ([FromServices] DAL<Musica> dal, int id) => {
                var musica = dal.RecuperarPor(a => a.Id == id);
                if (musica is null)
                {
                    return Results.NotFound();
                }
                dal.Remover(musica);
                return Results.NoContent();
            });


            app.MapPut("/Musicas", ([FromServices] DAL<Musica> dal, [FromBody] MusicaRequestEdit musicaRequest) =>
            {
                var musicaAtualizar = dal.RecuperarPor(a => a.Id == musicaRequest.id);
                if (musicaAtualizar is null)
                {
                    return Results.NotFound();
                }
                musicaAtualizar.Nome = musicaRequest.nome;
                musicaAtualizar.AnoLancamento = musicaRequest.anoLancamento;

                dal.Atualizar(musicaAtualizar);
                return Results.Ok();
            });
        }

        private static ICollection<MusicaResponse> EntityListToResponseList(IEnumerable<Musica> musicaList)
        { 
            return musicaList.Select(musica => EntityToResponse(musica)).ToList();
        }

        private static MusicaResponse EntityToResponse(Musica musica)
        { 
            return new MusicaResponse(musica.Id, musica.Nome!, musica.Artista!.Id, musica.Artista.Nome);
        }
    }
}
