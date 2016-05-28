using CsvHelper.Configuration;
using MyLib.MyCsvHelper.Wrapper;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace MyLib.MyCsvHelper
{
	// MEMO : 遅延実行を活かしたWrapperを作ろうとする場合の考察
	// IDisposableを実装し、Dispose()内でCsvReaderクラスの破棄を行う。
	// 好きになれない点・・・読込ファイルの数だけ、当クラスのインスタンスを生成する必要がある所
	// 理想
	//	＞何本ファイルを読もうが、外側から生成するインスタンスは１つ
	//	＞読込ファイルが遅延実行状態で外側から操作できる（IEnumerable<T>のまま返せる）
	// 課題
	//	＞CsvReaderインスタンスが読込ファイル１つにつき１つ必要であること

	/// <summary>
	/// 自作CSVReader（CsvHelper使用）
	/// </summary>
	/// <remarks>
	/// ただ単にCSVファイルの中身をメモリ上に落としたい場合に使用。
	/// ある程度の指定をやってくれる代わりに、CsvReaderの1つの利点「遅延実行」を無きものとしている点には注意。
	/// どうせ遅延実行が無いならってことで、Dictionary，Lookupで返すメソッドも用意あり
	/// </remarks>
	public class MyCsvReader
	{
		/// <summary>CSVを読込む際のデータ情報</summary>
		public CsvConfiguration CsvConf { get; set; }

		/// <summary>
		/// コンストラクタ（File読込設定情報は、MyCsvReaderの初期値を設定）
		/// </summary>
		public MyCsvReader()
		{
			CsvConf = CsvReaderWrap.GetInitCsvConf();
		}

		/// <summary>
		/// コンストラクタ　File読込設定情報を指定）
		/// </summary>
		/// <param name="fileReadConf">File読込設定情報</param>
		public MyCsvReader(CsvConfiguration fileReadConf)
		{
			CsvConf = fileReadConf;
		}

		/// <summary>
		/// ファイルデータ取得
		/// </summary>
		/// <typeparam name="TEntity">CSVのデータModel</typeparam>
		/// <param name="filePath">ファイルFullPath</param>
		/// <returns>ファイルデータ</returns>
		public virtual IEnumerable<TEntity> GetFileData<TEntity>(string filePath)
			where TEntity : class, new()
		{
			using (var reader = new CsvReaderWrap(new StreamReader(filePath), CsvConf))
			{
				// CsvReader.GetRecordsのIEnumerable<T>は遅延実行。
				// Using外で利用する為に、ToArray()で実体化している。
				return reader.GetRecords<TEntity>().ToArray();
			}
		}
		/// <summary>
		/// ファイルデータ取得（ファイルとEntityのMapping指定）
		/// </summary>
		/// <typeparam name="TEntity">CSVのデータModel</typeparam>
		/// <typeparam name="TMap">ファイルとEntityのMappingクラス</typeparam>
		/// <param name="filePath">ファイルFullPath</param>
		/// <returns>ファイルデータ</returns>
		public virtual IEnumerable<TEntity> GetFileData<TEntity, TMap>(string filePath)
			where TEntity : class, new()
			where TMap : CsvClassMap<TEntity>, new()
		{
			this.CsvConf.RegisterClassMap<TMap>();
			return GetFileData<TEntity>(filePath);
		}

		/// <summary>
		/// ファイルデータDictionary取得
		/// </summary>
		/// <typeparam name="TKey">DictionaryのKey型</typeparam>
		/// <typeparam name="TEntity">CSVのデータModel</typeparam>
		/// <param name="filePath">ファイルFullPath</param>
		/// <param name="keySelector">DictionaryのKey項目Selector</param>
		/// <returns>ファイルデータDictionary</returns>
		public virtual IDictionary<TKey, TEntity> GetFileDataToDictionary<TKey, TEntity>(string filePath, Func<TEntity, TKey> keySelector)
			where TEntity : class, new()
		{
			using (var reader = new CsvReaderWrap(new StreamReader(filePath), CsvConf))
			{
				return reader.GetRecords<TEntity>().ToDictionary(keySelector);
			}
		}
		/// <summary>
		/// ファイルデータDictionary取得（ファイルとEntityのMapping指定）
		/// </summary>
		/// <typeparam name="TKey">DictionaryのKey型</typeparam>
		/// <typeparam name="TEntity">CSVのデータModel</typeparam>
		/// <typeparam name="TMap">ファイルとEntityのMappingクラス</typeparam>
		/// <param name="filePath">ファイルFullPath</param>
		/// <param name="keySelector">DictionaryのKey項目Selector</param>
		/// <returns>ファイルデータDictionary</returns>
		public virtual IDictionary<TKey, TEntity> GetFileDataToDictionary<TKey, TEntity, TMap>(string filePath, Func<TEntity, TKey> keySelector)
			where TEntity : class, new()
			where TMap : CsvClassMap<TEntity>, new()
		{
			this.CsvConf.RegisterClassMap<TMap>();
			return GetFileDataToDictionary<TKey, TEntity>(filePath, keySelector);
		}

		/// <summary>
		/// ファイルデータLookup取得
		/// </summary>
		/// <typeparam name="TKey">LookupのKey型</typeparam>
		/// <typeparam name="TEntity">CSVのデータModel</typeparam>
		/// <param name="filePath">ファイルFullPath</param>
		/// <param name="keySelector">LookupのKey項目Selecter</param>
		/// <returns>ファイルデータLookup</returns>
		public virtual ILookup<TKey, TEntity> GetFileDataToLookup<TKey, TEntity>(string filePath, Func<TEntity, TKey> keySelector)
			where TEntity : class, new()
		{
			using (var reader = new CsvReaderWrap(new StreamReader(filePath), CsvConf))
			{
				return reader.GetRecords<TEntity>().ToLookup(keySelector);
			}
		}
		/// <summary>
		/// ファイルデータLookup取得（ファイルとEntityのMapping指定）
		/// </summary>
		/// <typeparam name="TKey">LookupのKey型</typeparam>
		/// <typeparam name="TEntity">CSVのデータModel</typeparam>
		/// <typeparam name="TMap">ファイルとEntityのMappingクラス</typeparam>
		/// <param name="filePath">ファイルFullPath</param>
		/// <param name="keySelector">LookupのKey項目Selecter</param>
		/// <returns>ファイルデータLookup</returns>
		public virtual ILookup<TKey, TEntity> GetFileDataToLookup<TKey, TEntity, TMap>(string filePath, Func<TEntity, TKey> keySelector)
			where TEntity : class, new()
			where TMap : CsvClassMap<TEntity>, new()
		{
			CsvConf.RegisterClassMap<TMap>();
			return GetFileDataToLookup<TKey, TEntity>(filePath, keySelector);
		}
	}
}
