using CsvHelper;
using CsvHelper.Configuration;
using System.IO;

namespace MyLib.MyCsvHelper.Wrapper
{
	public class CsvReaderWrap : CsvReader
	{
		/// <summary>
		/// コンストラクタ（ICsvParserオブジェクト指定）
		/// </summary>
		/// <param name="parser">読込ファイル</param>
		public CsvReaderWrap(ICsvParser parser) : base(parser)
		{
		}

		/// <summary>
		/// コンストラクタ（TextReaderオブジェクト指定）
		/// </summary>
		/// <param name="reader">読込ファイル</param>
		public CsvReaderWrap(TextReader reader) : base(reader, GetInitCsvConf())
		{
		}

		/// <summary>
		/// コンストラクタ（TextReader、File読込設定情報 指定）
		/// </summary>
		/// <param name="reader">読込ファイル</param>
		/// <param name="configuration">File読込設定情報</param>
		public CsvReaderWrap(TextReader reader, CsvConfiguration configuration) : base(reader, configuration)
		{
		}

		/// <summary>
		/// File読込設定情報 初期値取得
		/// </summary>
		/// <returns></returns>
		public static CsvConfiguration GetInitCsvConf()
		{
			var conf = new CsvConfiguration();
			// 空白行を無視
			conf.SkipEmptyRecords = true;
			// コメント行(#)を無視
			conf.AllowComments = true;
			// 項目の前後にある空白を削除
			conf.TrimFields = true;

			return conf;
		}
	}
}
