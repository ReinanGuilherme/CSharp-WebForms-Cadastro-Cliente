using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Cadastro_Cliente_WebForms.Entidades
{
    public class Cliente
    {
        public int Codigo { get; set; }
        public string Nome { get; set; }
        public DateTime Data_Nascimento { get; set; }
        public string Logradouro { get; set; }
        public int Numero { get; set; }
        public string Bairro { get; set; }
        public string Cidade { get; set; }
        public string UF { get; set; }
        public string CEP { get; set; }
        public string Telefone { get; set; }
        public double Limite_Credito { get; set; }

        public Cliente()
        {

        }

        public Cliente(int codigo, string nome, DateTime data_Nascimento, string logradouro, int numero, string bairro, string cidade, string uf, string cep, string telefone, double limite_Credito)
        {
            Codigo = codigo;
            Nome = nome;
            Data_Nascimento = data_Nascimento;
            Logradouro = logradouro;
            Numero = numero;
            Bairro = bairro;
            Cidade = cidade;
            UF = uf;
            CEP = cep;
            Telefone = telefone;
            Limite_Credito = limite_Credito;
        }
    }
}