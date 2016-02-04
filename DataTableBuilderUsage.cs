using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace DataTableBuilder
{
    [TestFixture]
    public class DataTableBuilderUsage
    {
        [Test]
        public void ExampleUsingOldPattern()
        {
            var table = new DataTable();

            table.Columns.Add("ID", typeof(int));
            table.Columns.Add("Name", typeof(string));
            table.Columns.Add("DOB", typeof(DateTime));
            table.Columns.Add("Amount", typeof(double));
            table.Columns.Add("Flag", typeof(bool));

            var row = table.NewRow();

            row.SetField("ID", 1);
            row.SetField("Name", "Matthew");
            row.SetField("DOB", new DateTime(1978, 8, 2));
            row.SetField("Amount", 1.23d);
            row.SetField("Flag", true);

            table.Rows.Add(row);

            AssertColumnNames(table);

            AssertColumnDataTypes(table);

            AssertFirstRow(table);
        }

        [Test]
        public void ExampleUsingNewPattern()
        {
            var table = new DataTableBuilder()
                .WithColumn("ID", 1)
                .WithColumn("Name", "Matthew")
                .WithColumn("DOB", new DateTime(1978, 8, 2))
                .WithColumn("Amount", 1.23d)
                .WithColumn("Flag", true)
                .Build();

            AssertColumnNames(table);

            AssertColumnDataTypes(table);

            AssertFirstRow(table);
        }

        [Test]
        public void ExampleUsingNewPatternWithNoRows()
        {
            var table = new DataTableBuilder()
                .WithColumn<int>("ID")
                .WithColumn<string>("Name")
                .WithColumn<DateTime>("DOB")
                .WithColumn<double>("Amount")
                .WithColumn<bool>("Flag")
                .Build();

            AssertColumnNames(table);

            AssertColumnDataTypes(table);

            Assert.That(table.Rows, Is.Empty);
        }

        [Test]
        public void ExampleUsingNewPatternWithMultipleRows()
        {
            var table = new DataTableBuilder()
                .WithColumn<int>("ID")
                .WithColumn<string>("Name")
                .WithColumn<DateTime>("DOB")
                .WithColumn<double>("Amount")
                .WithColumn<bool>("Flag")
                .WithRow(1, "Matthew", new DateTime(1978, 8, 2), 1.23d, true)
                .WithRow(2, "Renze", new DateTime(2001, 2, 3), 2.34d, false)
                .Build();

            AssertColumnNames(table);

            AssertColumnDataTypes(table);

            AssertFirstRow(table);

            AssertSecondRow(table);
        }

        private void AssertColumnNames(DataTable table)
        {
            AssertColumnName(table, 0, "ID");
            AssertColumnName(table, 1, "Name");
            AssertColumnName(table, 2, "DOB");
            AssertColumnName(table, 3, "Amount");
            AssertColumnName(table, 4, "Flag");
        }

        private static void AssertColumnName(DataTable table, int index, string name)
        {
            Assert.That(table.Columns[index].ColumnName,
                Is.EqualTo(name));
        }

        private void AssertColumnDataTypes(DataTable table)
        {
            AssertColumnDataType(table, 0, typeof(int));
            AssertColumnDataType(table, 1, typeof(string));
            AssertColumnDataType(table, 2, typeof(DateTime));
            AssertColumnDataType(table, 3, typeof(double));
            AssertColumnDataType(table, 4, typeof(bool));
        }

        private static void AssertColumnDataType(DataTable table, int index, Type type)
        {
            Assert.That(table.Columns[index].DataType,
                Is.EqualTo(type));
        }

        private void AssertFirstRow(DataTable table)
        {
            AssertFieldValue(table, 0, "ID", 1);
            AssertFieldValue(table, 0, "Name", "Matthew");
            AssertFieldValue(table, 0, "DOB", new DateTime(1978, 8, 2));
            AssertFieldValue(table, 0, "Amount", 1.23d);
            AssertFieldValue(table, 0, "Flag", true);
        }

        private void AssertSecondRow(DataTable table)
        {
            AssertFieldValue(table, 1, "ID", 2);
            AssertFieldValue(table, 1, "Name", "Renze");
            AssertFieldValue(table, 1, "DOB", new DateTime(2001, 2, 3));
            AssertFieldValue(table, 1, "Amount", 2.34d);
            AssertFieldValue(table, 1, "Flag", false);
        }

        private void AssertFieldValue<T>(DataTable table, int row, string name, T value)
        {
            Assert.That(table.Rows[row].Field<T>(name),
                Is.EqualTo(value));
        }
    }
}
