using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LetsFullProject.DTO
{
    public class ProjetoDto
    {
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [StringLength(80, ErrorMessage = "O campo {0} precisa ter entre {2} e {1} caracteres", MinimumLength = 3)]
        public string Nome { get; set; }
        public string URL { get; set; }
        public string Imagem { get; set; }
        public int IdUsuarioCadastro { get; set; }
    }
}
