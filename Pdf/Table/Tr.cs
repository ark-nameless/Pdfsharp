using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuotationExport.Pdf.Table
{
    public class Tr<T> where T : TCell
    {
        public IList<T> Cells { get; set; }

        public T this[int index] { get => Cells[index]; set => Cells[index] = (T)value; }

        public Tr() { Cells = new List<T>(); }
        public Tr(IList<T> cells) {  Cells = cells; }

        public void Add(T cell) { Cells.Add(cell); }
        public void Remove(T cell) { Cells.Remove(cell); }
    }
}
