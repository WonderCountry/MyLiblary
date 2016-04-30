using CsvHelper;
using CsvHelper.Configuration;
using System.IO;

namespace MyLib.MyCsvHelper.Wrapper
{
	internal class CsvReaderWrap : CsvReader
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
		public CsvReaderWrap(TextReader reader) : base(reader)
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
	}
}
