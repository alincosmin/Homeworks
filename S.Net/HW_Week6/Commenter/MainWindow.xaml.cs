using System;
using System.ServiceModel;
using System.Windows;

namespace Commenter
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private ServiceHost _host;

        public MainWindow()
        {
            InitializeComponent();
            _host = new ServiceHost(typeof (Poster));
        }


        private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            AppendTextToBox("Starting server...\n");
            StartServer();
        }

        private void StartServer()
        {
            _host.Open();
            AppendTextToBox("Server is alive.\n");
            foreach (var add in _host.BaseAddresses)
            {
                AppendTextToBox("Address: " + add);
            }
        }

        private void AppendTextToBox(string text)
        {
            mainTxtBox.AppendText(text);
            mainTxtBox.ScrollToEnd();
        }
    }
}
