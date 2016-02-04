using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataTableBuilder
{
    public class DataTableBuilder
    {
        private readonly Dictionary<string, Type> _columns = new Dictionary<string, Type>();  
        private readonly Dictionary<string, object> _fields = new Dictionary<string, object>();
        private readonly List<List<object>> _rows = new List<List<object>>();
 
        public DataTableBuilder WithColumn<T>(string name)
        {
            _columns.Add(name, typeof(T));

            return this;
        }

        public DataTableBuilder WithColumn<T>(string name, T value)
        {
            _columns.Add(name, typeof(T));

            _fields.Add(name, value);

            return this;
        }

        public DataTableBuilder WithRow(params object[] fields)
        {
            var row = fields.ToList();

            _rows.Add(row);

            return this;
        }

        public DataTable Build()
        {
            var table = new DataTable();

            AddColumns(table);
            
            if (_fields.Any())
                AddFirstRow(table);

            if (_rows.Any())
                AddRows(table);

            return table;
        }

        private void AddColumns(DataTable table)
        {
            foreach (var pair in _columns)
                table.Columns.Add(pair.Key, pair.Value);
        }

        private void AddFirstRow(DataTable table)
        {
            var row = table.NewRow();

            foreach (var field in _fields)
                row.SetField(field.Key, field.Value);

            table.Rows.Add(row);
        }

        private void AddRows(DataTable table)
        {
            foreach (var fields in _rows)
            {
                var row = table.NewRow();

                for (int i = 0; i < fields.Count; i++)
                    row.SetField(i, fields[i]);

                table.Rows.Add(row);
            }
        }
    }
}
