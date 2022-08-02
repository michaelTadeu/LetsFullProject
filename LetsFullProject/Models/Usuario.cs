using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace LetsFullProject.Models
{
    [Table("USUARIO")]
    public class Usuario
    {
        [Column("ID"), Required]
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Column("NOME"), Required]
        public string Nome { get; set; }

        [Column("USERNAME"), Required]
        public string Username { get; set; }

        [Column("EMAIL"), Required]
        public string Email { get; set; }
        [Column("SENHA"), Required]
        public string Senha { get; set; }
        public virtual ICollection<Projeto> Projeto { get; set; }
        public virtual ICollection<UsuarioLikeProjeto> UsuarioLikeProjeto { get; set; }
    }
}
