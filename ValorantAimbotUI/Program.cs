using System;
using System.Windows.Forms;

namespace ValorantAimbotUI
{
	internal static class Program
	{
		[STAThread]
		private static void Main()
		{
			Form1.CheckForIllegalCrossThreadCalls = false;
			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);
			Application.Run(new Form1());
			Application.Run(new FormOverlay());

		}
	}
}
