﻿using Microsoft.AspNetCore.Mvc;
using ScreenSound.API.Requests;
using ScreenSound.API.Response;
using ScreenSound.Banco;
using ScreenSound.Modelos;
using ScreenSound.Shared.Modelos.Modelos;

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

            app.MapPost("/Musicas", ([FromServices] DAL<Musica> dal, [FromServices] DAL<Genero> dalGenero,[FromBody] MusicaRequest musicaRequest) =>
            {
                Musica musica = new Musica(musicaRequest.nome){
                    ArtistaId =  musicaRequest.ArtistaId,
                    AnoLancamento =  musicaRequest.anoLancamento,
                    Generos =  musicaRequest.Generos is not null?GeneroRequestConverter(musicaRequest.Generos, dalGenero): new List<Genero>()

                };
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

        private static ICollection<Genero> GeneroRequestConverter(ICollection<GeneroRequest> generos, DAL<Genero> dalGenero)
        {
            var listaDeGeneros = new List<Genero>();
            foreach (var item in generos)
            {
                var entity = RequestToEntity(item);
                var genero = dalGenero.RecuperarPor(a => a.Nome.ToUpper().Equals(entity.Nome.ToUpper()));
                if (genero is not null)
                {
                    listaDeGeneros.Add(genero);
                }
                else
                { 
                    listaDeGeneros.Add(entity);
                }
            }
            return listaDeGeneros;
        }

        private static Genero RequestToEntity(GeneroRequest genero)
        {
            return new Genero() { Nome = genero.Nome, Descricao = genero.Descricao };
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
