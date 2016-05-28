using CsvHelper.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MyLib.MyCsvHelper;
using MyLib.MyCsvHelper.Wrapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyLib.MyCsvHelper.Tests
{
	[TestClass()]
	public class MyCsvReaderTests
	{
		//[TestMethod()]
		public void GetFileDataTest()
		{
			var testClass = new MyCsvReader();
			var conf = CsvReaderWrap.GetInitCsvConf();

			foreach (var pro in typeof(CsvConfiguration).GetProperties())
			{
				Assert.AreEqual(pro.GetValue(testClass.CsvConf), pro.GetValue(conf), pro.Name);
			}
			
			
		}
	}
}