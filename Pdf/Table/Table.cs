using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuotationExport.Pdf.Table
{
    public class Table
    {
        public IList<Tr<TCell>> Rows { get; set; }


        public int Count => Rows.Count;
        public bool IsReadOnly => Rows.IsReadOnly;
        public Tr<TCell> this[int index] { get => Rows[index]; set => Rows[index] = value; }

        public Table(IList<Tr<TCell>> rows)
        {
            Rows = rows;
        }

        public void AddRow(Tr<TCell> row) 
        {
            Rows.Add(row);
        }
    }
}
