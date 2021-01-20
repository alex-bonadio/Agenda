using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CadastroProdutos.Controller
{
    public class Contato
    {
        private int idcontato;
        private string nome;
        private string telefone;
        private string email;

        public int Idcontato { get => idcontato; set => idcontato = value; }
        public string Nome { get => nome; set => nome = value; }
        public string Telefone { get => telefone; set => telefone = value; }
        public string Email { get => email; set => email = value; }

        public Contato()
        {
        }
    }
}