using LetsFullProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LetsFullProject.Interfaces
{
    public interface IUsuarioLikeProjetoService
    {
        UsuarioLikeProjeto SaveOrUpdate(UsuarioLikeProjeto model);
        UsuarioLikeProjeto VerifyLike(int IdProjeto, int IdUsuario);
    }
}
