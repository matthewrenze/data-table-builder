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
    public class DataTableBuilderTests
    {
        private DataTableBuilder _builder;

        private const string Name = "ID";
        private const int Value = 1;

        [SetUp]
        public void SetUp()
        {
            _builder = new DataTableBuilder();
        }

        [Test]
        public void TestAddColumnWithValueShouldAddColumnAndValueToDataTable()
        {
            var result = _builder
                .WithColumn(Name, Value)
                .Build();

            Assert.That(result.Columns[0].ColumnName, 
                Is.EqualTo(Name));

            Assert.That(result.Columns[0].DataType,
                Is.EqualTo(typeof(int)));
            
            Assert.That(result.Rows[0].Field<int>(Name), 
                Is.EqualTo(Value));
        }

        [Test]
        public void TestAddColumnShouldAddColumnToDataTable()
        {
            var result = _builder
                .WithColumn<int>(Name)
                .Build();

            Assert.That(result.Columns[0].ColumnName,
                Is.EqualTo(Name));

            Assert.That(result.Columns[0].DataType,
                Is.EqualTo(typeof(int)));

            Assert.That(result.Rows, Is.Empty);
        }

        [Test]
        public void TestAddRowShouldAddRowToTable()
        {
            var result = _builder
                .WithColumn<int>(Name)
                .WithRow(Value)
                .Build();

            Assert.That(result.Rows[0].Field<int>(Name),
                Is.EqualTo(Value));
        }

        [Test]
        public void TestBuildShouldBuildDataTable()
        {
            var result = _builder.Build();

            Assert.That(result, Is.TypeOf<DataTable>());
        }

        
    }
}
