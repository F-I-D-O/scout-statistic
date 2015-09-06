using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TKS
{
    class DataLoader
    {
        public string FilePath { get; set; }


        public DataLoader()
        {
 
        }

        public ObservableCollection<Row> loadData()
        {
            StreamReader reader = new StreamReader(File.OpenRead(FilePath));

            ObservableCollection<Row> rows = new ObservableCollection<Row>();

            reader.ReadLine();
            while (!reader.EndOfStream)
            {
                string line = reader.ReadLine();
                string[] values = line.Split(';');

                Row row = new Row(values[0], values[1], values[2], values[3], values[4], values[5], values[6], values[7]);
                rows.Add(row);
            }

            return rows;
        }
    }
}
