using System.Collections.Generic;

namespace Guadalupe.Conexao.Backoffice.Core
{
    public class PaginatorViewModel<T>
    {
        public PaginatorViewModel()
        {
            Registers = new List<T>();
        }

        public string Search { get; set; }
        public int Total { get; set; }
        public IEnumerable<T> Registers { get; set; }
        public int Index { get; set; }
        public int Size { 
            get 
            {
                return 6;
            } 
        }
        public bool ShowPaginator { 
            get 
            {
                return Total > Size;
            } 
        }
        public bool ShowPrevious { 
            get 
            {
                return Index > 1;
            } 
        }
        public bool ShowNext {
            get 
            {
                return Total > Index * Size;
            }
        }
    }
}
