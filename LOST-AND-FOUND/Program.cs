using System;
using System.Windows.Forms;
using LostAndFound;

namespace LOST_AND_FOUND
{
    internal static class Program
    {
        [STAThread]
        static void Main()
        {
            try
            {
                Database.Initialize();
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new FormLogin());
            }
            catch (Exception ex)
            {
                MessageBox.Show("Startup Error: " + ex.Message);
            }
        }
    }
}
