using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace APKInstaller
{
	static class Program
	{
		[STAThread]
		static void Main()
		{
			string[] args = Environment.GetCommandLineArgs();
			if (args.Length == 2)
			{
				string path = args[1];
				if (File.Exists(args[1]) && path.EndsWith(".apk"))
				{
					Process installproc = new Process();
					ProcessStartInfo si = new ProcessStartInfo
					{
						FileName = "adb",
						Arguments = "install " + path,
						UseShellExecute = false,
						RedirectStandardOutput = true,
						CreateNoWindow = true
					};

					installproc.StartInfo = si;
					installproc.Start();
					installproc.WaitForExit();

					string cmdout = installproc.StandardOutput.ReadToEnd();
					if (cmdout.Contains("adb: error: failed to get feature set: no devices/emulators found"))
					{
						MessageBox.Show("Device not connected. Please connect your device with ADB.", "APKInstaller for WSA");
						Console.WriteLine("Device not connected. Please connect your device with ADB.");
						Environment.Exit(1);
					}
					else if (cmdout.Contains("Success"))
					{
						MessageBox.Show("Installed app successfully.", "APKInstaller for WSA");
						Console.WriteLine("Installed app successfully.");
						Environment.Exit(0);
					}
					else
					{
						MessageBox.Show("An error occured while installing the app.", "APKInstaller for WSA");
						Console.WriteLine("An error occured while installing the app.");
						Environment.Exit(1);
					}
				}
				else
				{
					MessageBox.Show("File is not available or not a Android app package. Please check the file again.", "APKInstaller for WSA");
					Console.WriteLine("File is not available or not a Android app package. Please check the file again.");
					Environment.Exit(1);
				}

			}
		}
	}
}
