using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System;
using System.Diagnostics;
using AssetRipper.Core.Parser.Files.BundleFile;
using System.Runtime.InteropServices;
using System.Threading;

namespace AssetRipper.Core.Utils
{
	public class DecryptLib
	{
		public static bool InitDecryptor(byte[] newKey, CompressionType type, string key, byte[] CABkey1, byte[] CABkey2)
		{
			if (hModule == IntPtr.Zero)
			{
				Load();
			}
			return ((_InitDecryptor)Marshal.GetDelegateForFunctionPointer(moduleBase + 0x81CE20, typeof(_InitDecryptor))).Invoke(newKey, type, key, CABkey1, CABkey2);
		}

		public static bool DecryptDataSequence(byte[] key, CompressionType type, byte[] compressedBuffer, uint compressBytes, int m_cachedBlockIndex)
		{
			if (hModule == IntPtr.Zero)
			{
				Load();
			}
			return ((_DecryptDataSequence)Marshal.GetDelegateForFunctionPointer(moduleBase + 0x81C2C5, typeof(_DecryptDataSequence))).Invoke(key, type, compressedBuffer, compressBytes, m_cachedBlockIndex);
		}

		[DllImport("kernel32.dll", CharSet = CharSet.Ansi, SetLastError = true)]
		public static extern IntPtr LoadLibrary(string path);

		[DllImport("kernel32.dll")]
		public static extern bool FreeLibrary(IntPtr hModule);
		
		public static void Load()
		{
			hModule = LoadLibrary("UnityPlayer.dll");
			moduleBase = Process.GetCurrentProcess().Modules.Cast<ProcessModule>().First((ProcessModule m) => m.ModuleName == "UnityPlayer.dll").BaseAddress;
		}
		public static IntPtr hModule;
		public static IntPtr moduleBase;
		public static string aseKey = "y5XPvqLOrCokWRIa";
		public static byte[] KeyBuff { get; set; }

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate bool _InitDecryptor(byte[] ASE_Struct, CompressionType type, string key, byte[] CABkey1, byte[] CABkey2);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate bool _DecryptDataSequence(byte[] ASE_Struct, CompressionType type, byte[] compressedBuffer, uint compressBytes, int m_cachedBlockIndex);
	}
}
