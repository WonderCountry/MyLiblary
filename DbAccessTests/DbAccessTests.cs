using Microsoft.VisualStudio.TestTools.UnitTesting;
using DbAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Configuration;

namespace DbAccess.Tests
{
	[TestClass()]
	public class DbAccessTests
	{
		private readonly string _conStrOleDb;
		private readonly string _conStrSqlServer;

		public DbAccessTests()
		{
			_conStrOleDb = ConfigurationManager.ConnectionStrings["OleDb"].ConnectionString;
			_conStrSqlServer = ConfigurationManager.ConnectionStrings["SqlServer"].ConnectionString;
		}

		[TestMethod()]
		public void OleDb接続テスト()
		{
			using (var db = new DbAccess<OleDbConnection>(_conStrOleDb))
			{
				Assert.IsInstanceOfType(db.DbConnection, typeof(OleDbConnection));
			}
		}

		[TestMethod()]
		public void SqlServer接続テスト()
		{
			using (var db = new DbAccess<SqlConnection>(_conStrSqlServer))
			{
				Assert.IsInstanceOfType(db.DbConnection, typeof(SqlConnection));
			}
		}

//		[TestMethod()]
//		public void ExecuteTest()
//		{
//			string sql = @"
//UPDATE M_UserInfo SET
//	Name = @Name
//WHERE ID = @ID
//";

		//			using (var db = new DbAccess<OleDbConnection>(CON_STR_OLEDB))
		//			{
		//				Assert.IsInstanceOfType(db, typeof(OleDbConnection));
		//			}
		//			using (var db = new DbAccess<SqlConnection>(CON_STR_SQLSERVER))
		//			{
		//				Assert.IsInstanceOfType(db, typeof(SqlConnection));
		//			}
		//			Assert.Fail();
		//		}

		//		[TestMethod()]
		//		public void GetDataReaderTest()
		//		{
		//			Assert.Fail();
		//		}

		//		[TestMethod()]
		//		public void GetDataTableTest()
		//		{
		//			Assert.Fail();
		//		}

		//		[TestMethod()]
		//		public void GetDataScalarTest()
		//		{
		//			Assert.Fail();
		//		}

		//		[TestMethod()]
		//		public void DisposeTest()
		//		{
		//			Assert.Fail();
		//		}
	}
}