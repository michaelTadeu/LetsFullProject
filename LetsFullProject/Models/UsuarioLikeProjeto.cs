using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace LetsFullProject.Models
{
    [Table("USUARIO_LIKE_PROJETO")]
    public class UsuarioLikeProjeto
    {
        [Column("ID"), Required]
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Column("ID_USUARIO_LIKE"), Required]
        public Usuario UsuarioLike { get; set; }
        [ForeignKey("ID_USUARIO_LIKE")]
        public int IdUsuarioLike { get; set; }
        [Column("ID_PROJETO_LIKE"), Required]
        public Projeto ProjetoLike { get; set; }
        [ForeignKey("ID_PROJETO_LIKE")]
        public int IdProjetoLike { get; set; }
    }
}
