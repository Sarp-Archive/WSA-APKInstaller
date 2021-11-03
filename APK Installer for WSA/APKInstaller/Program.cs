using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Windows.Forms;
using System.Net;

namespace APKInstaller
{
	static class Program
	{
		static string workpath = Path.GetDirectoryName(System.Reflection.Assembly.GetEntryAssembly().Location);
		static string adbpath = Path.Combine(workpath, "platform-tools/adb.exe");
		static string zippath = Path.Combine(workpath, "adb.zip");
		static string ziplink = "https://dl.google.com/android/repository/platform-tools_r31.0.3-windows.zip";

		static string path = "";

		static string conaddr = "127.0.0.1:58526";

		[STAThread]
		static void Main()
		{
			if (!(File.Exists(adbpath)))
			{
				getADB();
			}

			string[] args = Environment.GetCommandLineArgs();
			if (args.Length == 2)
			{
				path = args[1];
				if (File.Exists(path) && path.EndsWith(".apk"))
				{
					installAPK();
				}
				else
				{
					MessageBox.Show("File is not available or invalid a Android app package. Please check the file again.", "APKInstaller for WSA");
					Environment.Exit(1);
				}

			}
		}
		static void getADB()
		{
			if (File.Exists(zippath))
				File.Delete(zippath);

			using (var w = new WebClient())
			{
				w.DownloadFile(ziplink, zippath);
			}

			ZipFile.ExtractToDirectory(zippath, workpath);
			File.Delete(zippath);
			
		}
	
		static void installAPK()
		{
			Process installproc = new Process();
			ProcessStartInfo si = new ProcessStartInfo
			{
				FileName = adbpath,
				Arguments = "install " + path,
				UseShellExecute = false,
				RedirectStandardOutput = true,
				CreateNoWindow = true
			};

			installproc.StartInfo = si;
			installproc.Start();
			installproc.WaitForExit();

			string cmdout = installproc.StandardOutput.ReadToEnd();
			if (cmdout.Contains("no devices/emulators found"))
			{
				connectDev();
			}
			else if (cmdout.Contains("Success"))
			{
				MessageBox.Show("Installed app successfully.", "APKInstaller for WSA");
				Environment.Exit(0);
			}
			else
			{
				MessageBox.Show("An error occured while installing the app.", "APKInstaller for WSA");
				Environment.Exit(1);
			}
		}

		static void connectDev()
		{
			Process installproc = new Process();
			ProcessStartInfo si = new ProcessStartInfo
			{
				FileName = adbpath,
				Arguments = conaddr,
				UseShellExecute = false,
				RedirectStandardOutput = true,
				CreateNoWindow = true
			};

			installproc.StartInfo = si;
			installproc.Start();
			installproc.WaitForExit();

			string cmdout = installproc.StandardOutput.ReadToEnd();
			if (cmdout.Contains("connected to " + conaddr))
			{
				installAPK();
			}
			else if (cmdout.Contains("refused"))
			{
				MessageBox.Show("Cannot connect to WSA.", "APKInstaller for WSA");
				Environment.Exit(1);
			}
			else
			{
				MessageBox.Show("An error occured while installing the app.", "APKInstaller for WSA");
				Environment.Exit(1);
			}
		}
	}
}
