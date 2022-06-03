namespace AssetRipper.Core.Parser.Files.BundleFile.Parser
{
	[Flags]
	public enum StorageBlockFlags
	{
		CompressionTypeMask = 0x3F,

		Streamed = 0x40,

		DecryptBlock = 0x100,
	}

	public static class StorageBlockFlagsExtensions
	{
		public static CompressionType GetCompression(this StorageBlockFlags _this)
		{
			return (CompressionType)(_this & StorageBlockFlags.CompressionTypeMask);
		}

		public static bool IsStreamed(this StorageBlockFlags _this)
		{
			return (_this & StorageBlockFlags.Streamed) != 0;
		}

		public static bool IsDecryptBlock(this StorageBlockFlags _this)
		{
			return (_this & StorageBlockFlags.DecryptBlock) != 0;
		}
	}
}
