using System.Collections.Generic;
using System.Linq;
using Dapper;
using System.Data;

namespace DbAccess
{
	/// <summary>
	/// DapperのDB接続クラス
	/// </summary>
	/// <typeparam name="TConnection">DbConnectionクラス</typeparam>
	public class DapperDbAccess<TConnection> : DbAccess<TConnection>
		where TConnection : IDbConnection, new()
	{
		private readonly bool _isAnsi;

		/// <summary>
		/// コンストラクタ（接続）
		/// </summary>
		/// <param name="connectionString">接続文字列</param>
		/// <param name="isAnsi">isAnsi</param>
		public DapperDbAccess(string connectionString, bool isAnsi = true) : base(connectionString)
		{
			// TODO : 未実装
			_isAnsi = isAnsi;
		}

		/// <summary>
		/// Dapper.Exeuteのラッパー
		/// </summary>
		/// <param name="sql">sqlQuery</param>
		/// <param name="sqlParams">パラメータ</param>
		/// <param name="transaction">トランザクション</param>
		/// <returns>影響を受けた行の件数</returns>
		public int Execute(string sql, IEnumerable<IDbDataParameter> sqlParams = null, IDbTransaction transaction = null)
		{
			return DbConnection.Execute(sql, sqlParams, transaction);
		}

		/// <summary>
		/// Dapper.Queryのラッパー
		/// </summary>
		/// <typeparam name="T">取得するデータ型</typeparam>
		/// <param name="sql">SqlQuery</param>
		/// <param name="sqlParams">パラメータ</param>
		/// <param name="transaction">トランザクション</param>
		/// <returns>実行結果</returns>
		public IEnumerable<T> Query<T>(string sql, IEnumerable<IDbDataParameter> sqlParams = null, IDbTransaction transaction = null)
		{
			return DbConnection.Query<T>(sql, sqlParams, transaction);
		}

		/// <summary>
		/// Dapper.QuerytのFirstOrDefault
		/// </summary>
		/// <typeparam name="T">取得するデータ型</typeparam>
		/// <param name="sql">SqlQuery</param>
		/// <param name="sqlParams">パラメータ</param>
		/// <param name="transaction">トランザクション</param>
		/// <returns>実行結果の先頭１件</returns>
		public T QueryFirst<T>(string sql, IEnumerable<IDbDataParameter> sqlParams = null, IDbTransaction transaction = null)
		{
			return DbConnection.Query<T>(sql, sqlParams, transaction).FirstOrDefault();
		}

		/// <summary>
		/// Dapper.ExecuteSalarのラッパー
		/// </summary>
		/// <param name="sql">SqlQuery</param>
		/// <param name="sqlParams">パラメータ</param>
		/// <param name="transaction">トランザクション</param>
		/// <returns>クエリが返す最初の行の最初の列の値</returns>
		public object ExecuteScalar(string sql, IEnumerable<IDbDataParameter> sqlParams = null, IDbTransaction transaction = null)
		{
			return DbConnection.ExecuteScalar(sql, sqlParams, transaction);
		}
	}
}
