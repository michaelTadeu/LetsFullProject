using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace LetsFullProject.Models
{
    [Table("PROJETO")]
    public class Projeto
    {
        [Column("ID"), Required]
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Column("NOME"), Required]
        public string Nome { get; set; }
        [Column("URL"), Required]
        public string URL { get; set; }
        [Column("IMAGEM")]
        public string Imagem { get; set; }
        [Column("LIKE_CONTADOR"), Required]
        public int LikeContador { get; set; }
        [Column("ID_USUARIO_CADASTRO"), Required]
        public Usuario UsuarioCadastro { get; set; }
        [ForeignKey("ID_USUARIO_CADASTRO")]
        public int IdUsuarioCadastro { get; set; }
        public virtual ICollection<UsuarioLikeProjeto> ProjetoLikeUsuario { get; set; }

    }
}
