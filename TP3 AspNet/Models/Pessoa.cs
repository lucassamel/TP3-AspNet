using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TP3_AspNet.Models
{
    public class Pessoa
    {
        public int PessoaId { get; set; }
        public string Nome { get; set; }
        public string Sobrenome { get; set; }
        public int Idade { get; set; }
    }
}
