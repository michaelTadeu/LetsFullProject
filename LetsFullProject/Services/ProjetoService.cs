using LetsFullProject.Data;
using LetsFullProject.Interfaces;
using LetsFullProject.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace LetsFullProject.Services
{
    public class ProjetoService : IProjetoService
    {
        private readonly LetsFullProjectContext _context;
        private readonly IUsuarioLikeProjetoService _usaurioProjetoService;
        public ProjetoService(LetsFullProjectContext contexto, IUsuarioLikeProjetoService usaurioProjetoService)
        {
            _context = contexto;
            _usaurioProjetoService = usaurioProjetoService;
        }

        public IList<Projeto> GetAll()
        {
            return _context.Projetos.ToList();
        }

        public IList<Projeto> GetByUsuario(int idUsuario)
        {
            return _context.Projetos.Where(x => x.Id.Equals(idUsuario)).ToList();
        }

        public int LikeProjeto(UsuarioLikeProjeto model)
        {
            try
            {
              
                var projeto = _context.Projetos.Where(x => x.Id.Equals(model.IdProjetoLike)).FirstOrDefault();
                var usuario = _context.Usuarios.Where(x => x.Id.Equals(model.IdUsuarioLike)).FirstOrDefault();
                

                if (projeto == null)
                {
                    throw new FileNotFoundException(message: "Não foi encontrado projeto com o valor inserido");
                }
                else if (usuario == null)
                {
                    throw new FileNotFoundException(message: "Não foi encontrado usuário com o valor inserido");
                }
                else
                {
                    var verifyProjetoUsuario = _usaurioProjetoService.VerifyLike(projeto.Id, usuario.Id);
                    if(verifyProjetoUsuario == null)
                    {
                        _usaurioProjetoService.SaveOrUpdate(model);
                        projeto.LikeContador = projeto.LikeContador + 1;
                        SaveOrUpdate(projeto);
                        return projeto.LikeContador;
                    }
                    else
                    {
                        throw new Exception(message: "Usuário já deu like no projeto");
                    }
                   
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }

        public Projeto SaveOrUpdate(Projeto projeto)
        {
            try
            {
                if (_context.Usuarios.Any(e => e.Id == projeto.IdUsuarioCadastro))
                {
                    var state = projeto.Id == 0 ? EntityState.Added : EntityState.Modified;
                    _context.Entry(projeto).State = state;
                    _context.SaveChanges();
                    return projeto;
                }
                else
                {
                    throw new FileNotFoundException(message: "Não foi encontrado usuário com o valor inserido");
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }
    }
}
