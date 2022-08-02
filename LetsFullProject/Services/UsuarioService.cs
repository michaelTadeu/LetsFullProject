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
    public class UsuarioService : IUsuarioService
    {
        public LetsFullProjectContext _context;
        public UsuarioService(LetsFullProjectContext contexto)
        {
            _context = contexto;
        }
        public IList<Usuario> FindAllUsuarios()
        {
            return _context.Usuarios.ToList();
        }

        public Usuario FindByUsername(string username)
        {
            return _context.Usuarios.Where(x => x.Username.Equals(username)).FirstOrDefault();
        }

        public Usuario RemoveById(int idUsuario)
        {
            return _context.Usuarios.Where(x => x.Id.Equals(idUsuario)).FirstOrDefault();
        }

        public Usuario SaveOrUpdate(Usuario usuario)
        {
            try
            {
                var existe = _context.Usuarios
                    .Where(x => x.Id == usuario.Id)
                    .FirstOrDefault();

                if (existe == null)
                    _context.Usuarios.Add(usuario);
                else
                {
                    existe.Email = usuario.Email;
                    existe.Nome = usuario.Nome;
                    existe.Senha = usuario.Senha;
                }
                _context.SaveChanges();

                return usuario;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public bool VerifyPassword(string password, int userId)
        {
            try
            {
                var find = _context.Usuarios.Where(e => e.Id == userId).FirstOrDefault();
                if (find != null)
                {
                    var decrypt = Utils.Utils.DecryptValue(find.Senha);

                    return decrypt.Equals(password);
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
