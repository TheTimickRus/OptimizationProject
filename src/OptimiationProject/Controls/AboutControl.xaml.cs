using System;
using System.IO;
using System.Windows;

namespace OptimiationProject.Controls
{
    public partial class AboutControl
    {
        public AboutControl()
        {
            InitializeComponent();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                LicenseRtb.Selection.Load(new MemoryStream(File.ReadAllBytes("LICENSE.rtf")), DataFormats.Rtf);
            }
            catch (Exception ex)
            {
                LicenseRtb.Selection.Text = ex.Message;
            }
        }
    }
}
