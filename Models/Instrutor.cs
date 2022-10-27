using System.Collections.Generic;

namespace Tools.Models
{
    public class Instrutor
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public string CPF { get; set; }
        public bool Situacao { get; set; }
        public List<Curso> Cursos { get; set; }

    }
}