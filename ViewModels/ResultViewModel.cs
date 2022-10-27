using System.Collections.Generic;

namespace Tools.ViewModels
{
    public class ResultViewModel<Model>
    {
        public ResultViewModel(Model data)
        {
            Data = data;
        }
        
        public ResultViewModel(List<string> errors)
        {
            Errors = errors;
        }

        public ResultViewModel(string error)
        {
            Errors.Add(error);
        }

        public ResultViewModel(Model data, List<string> erros)
        {
            Data = data;
            Errors = erros;
        }

        public Model Data { get; private set; }
        public List<string> Errors { get; private set; } = new();
    }
}