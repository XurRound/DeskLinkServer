using System.Windows;
using DeskLinkServer.Logic;

namespace DeskLinkServer
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            DataContext = new MainViewModel();
        }
    }
}
