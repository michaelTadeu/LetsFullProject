
using LetsFullProject.Data;
using LetsFullProject.Interfaces;
using LetsFullProject.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LetsFullProject.Services
{
    public class UsuarioLikeProjetoService : IUsuarioLikeProjetoService
    {
        public readonly LetsFullProjectContext _context;
        public UsuarioLikeProjetoService(LetsFullProjectContext contexto)
        {
            _context = contexto;
        }
        public UsuarioLikeProjeto SaveOrUpdate(UsuarioLikeProjeto model)
        {
            try
            {
                var state = model.Id == 0 ? EntityState.Added : EntityState.Modified;
                _context.Entry(model).State = state;
                _context.SaveChanges();

                return model;
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }  
        }

        public UsuarioLikeProjeto VerifyLike(int IdProjeto, int IdUsuario)
        {
            var response = _context.UsuariosLikeProjetos.Where(x => x.IdProjetoLike.Equals(IdProjeto)
            && x.IdUsuarioLike.Equals(IdUsuario)).FirstOrDefault();
            return response;
        }
    }
}
