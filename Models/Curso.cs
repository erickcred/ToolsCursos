using System.Collections.Generic;

namespace Tools.Models
{
    public class Curso
    {
        public Curso()
        {
            Instrutores = new List<Instrutor>();
        }
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Categoria { get; set; }
        public bool Situacao { get; set; }
        public string CodigoCRC { get; set; }
        public string TurmaCRC { get; set; }
        
        public List<Instrutor> Instrutores { get; set; }
    }
}