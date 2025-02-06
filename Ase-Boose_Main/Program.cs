using System.Diagnostics;
using BOOSE;

namespace Ase_Boose
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            Debug.WriteLine(AboutBOOSE.about());
            ApplicationConfiguration.Initialize();
            Application.Run(new Canvas());
        }
    }
}