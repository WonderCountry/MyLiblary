using System;
using System.Collections.Generic;
using System.Data;

namespace DbAccess
{
	/// <summary>
	/// データ接続クラス
	/// </summary>
	/// <typeparam name="TConnection">DbConnecionクラス</typeparam>
	public class DbAccess<TConnection> : IDisposable
		where TConnection : IDbConnection, new()
    {
		/// <summary>接続</summary>
		public IDbConnection DbConnection { get; protected set; }
		/// <summary>コマンド</summary>
		protected IDbCommand DbCommand { get; set; }

		/// <summary>
		/// コンストラクタ（接続）
		/// </summary>
		/// <param name="connectionString">接続文字列</param>
		public DbAccess(string connectionString)
		{
			DbConnection = new TConnection();
			DbConnection.ConnectionString = connectionString;

			DbConnection.Open();
			DbCommand = DbConnection.CreateCommand();
		}

		/// <summary>
		/// SQL実行
		/// </summary>
		/// <param name="sql">SqlQuery</param>
		/// <param name="sqlParams">Sqlパラメータ</param>
		/// <returns>影響を受けた行の件数</returns>
		public virtual int Execute(string sql, IEnumerable<IDbDataParameter> sqlParams = null)
		{
			CreateCommand(sql, sqlParams);
			return DbCommand.ExecuteNonQuery();
		}

		/// <summary>
		/// クエリが返すデータ取得
		/// </summary>
		/// <param name="sql">SqlQuery</param>
		/// <param name="sqlParams">Sqlパラメータ</param>
		/// <returns>iDataReader</returns>
		public virtual IDataReader GetDataReader(string sql, IEnumerable<IDbDataParameter> sqlParams = null)
		{
			CreateCommand(sql, sqlParams);
			return DbCommand.ExecuteReader();
		}

		/// <summary>
		/// クエリが返すデータをDataTableで取得
		/// </summary>
		/// <param name="sql">SqlQuery</param>
		/// <param name="sqlParams">Sqlパラメータ</param>
		/// <param name="dataTableName">返すDataTableのテーブル名</param>
		/// <returns>DataTable</returns>
		public virtual DataTable GetDataTable(string sql, IEnumerable<IDbDataParameter> sqlParams = null, string dataTableName = "")
		{
			CreateCommand(sql, sqlParams);
			IDataReader reader = DbCommand.ExecuteReader();

			DataTable dtb = reader.GetSchemaTable();
			dtb.TableName = dataTableName;
			return dtb;
		}

		/// <summary>
		/// クエリが返す最初の行の最初の列の値を取得
		/// </summary>
		/// <param name="sql">SqlQuery</param>
		/// <param name="sqlParams">Sqlパラメータ</param>
		/// <returns>Object</returns>
		public virtual object GetDataScalar(string sql, IEnumerable<IDbDataParameter> sqlParams = null)
		{
			CreateCommand(sql, sqlParams);
			return DbCommand.ExecuteScalar();
		}
		
		/// <summary>
		/// DbCommand作成
		/// </summary>
		/// <param name="sql">SqlQuery</param>
		/// <param name="sqlParams">Sqlパラメータ</param>
		protected virtual void CreateCommand(string sql, IEnumerable<IDbDataParameter> sqlParams)
		{
			DbCommand.CommandText = sql;
			DbCommand.Parameters.Clear();
			if (sqlParams != null)
			{
				foreach (var prm in sqlParams)
				{
					DbCommand.Parameters.Add(prm);
				}
			}
		}

		#region IDisposable Support
		private bool disposedValue = false; // 重複する呼び出しを検出するには

		protected virtual void Dispose(bool disposing)
		{
			if (!disposedValue)
			{
				if (disposing)
				{
					// TODO: マネージ状態を破棄します (マネージ オブジェクト)。
					if (DbCommand != null)
					{
						DbCommand.Dispose();
						DbCommand = null;
					}

					if (DbConnection != null)
					{
						DbConnection.Dispose();
						DbConnection = null;
                    }
				}

				// TODO: アンマネージ リソース (アンマネージ オブジェクト) を解放し、下のファイナライザーをオーバーライドします。
				// TODO: 大きなフィールドを null に設定します。

				disposedValue = true;
			}
		}

		// TODO: 上の Dispose(bool disposing) にアンマネージ リソースを解放するコードが含まれる場合にのみ、ファイナライザーをオーバーライドします。
		// ~DbAccess() {
		//   // このコードを変更しないでください。クリーンアップ コードを上の Dispose(bool disposing) に記述します。
		//   Dispose(false);
		// }

		// このコードは、破棄可能なパターンを正しく実装できるように追加されました。
		public void Dispose()
		{
			// このコードを変更しないでください。クリーンアップ コードを上の Dispose(bool disposing) に記述します。
			Dispose(true);
			// TODO: 上のファイナライザーがオーバーライドされる場合は、次の行のコメントを解除してください。
			// GC.SuppressFinalize(this);
		}
		#endregion
	}
}
