using AssetRipper.Core.IO.Endian;
using AssetRipper.Core.Parser.Files.BundleFile.Parser;
using AssetRipper.Core.Utils;

namespace AssetRipper.Core.Parser.Files.BundleFile.Header
{
	public sealed class BundleFileStreamHeader
	{
		public BundleFileStreamHeader(EndianReader reader)
		{
			Size = reader.ReadInt64();
			CompressedBlocksInfoSize = reader.ReadInt32();
			UncompressedBlocksInfoSize = reader.ReadInt32();
			Flags = (BundleFlags)reader.ReadInt32();
			if (Flags.IsBlocksDecrypt())
			{
				DercyptID = reader.ReadUInt32();
				DecryptLib.KeyBuff = new byte[32];
				CabKey1 = new byte[32];
				CabKey2 = new byte[32];
				CabKey1 = reader.ReadStringZeroTermBytes();
				CabKey2 = reader.ReadStringZeroTermBytes();
				DecryptLib.InitDecryptor(DecryptLib.KeyBuff, Flags.GetCompression(), DecryptLib.aseKey, CabKey1, CabKey2);
			}
		}

		/// <summary>
		/// Equal to file size, sometimes equal to uncompressed data size without the header
		/// </summary>
		public long Size { get; set; }
		/// <summary>
		/// UnityFS length of the possibly-compressed (LZMA, LZ4) bundle data header
		/// </summary>
		public int CompressedBlocksInfoSize { get; set; }
		public int UncompressedBlocksInfoSize { get; set; }
		public BundleFlags Flags { get; set; }
		public uint DercyptID { get; set; }
		public byte[] CabKey1 { get; set; }
		public byte[] CabKey2 { get; set; }
	}
}
