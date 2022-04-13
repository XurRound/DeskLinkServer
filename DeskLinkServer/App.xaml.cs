using System.Windows;
using DeskLinkServer.Logic;
using DeskLinkServer.Stores;
using DeskLinkServer.ViewModels;

namespace DeskLinkServer
{
    public partial class App : Application
    {
        private readonly MainLogic mainLogic;

        private readonly MainViewModel mainViewModel;

        private readonly NavigationStore navigationStore;

        public App()
        {
            mainLogic = new MainLogic();
            
            navigationStore = new NavigationStore();
            navigationStore.CurrentViewModel = (mainLogic.Configuration.KnownDevices.Count == 0) ?
                new PlaceholderViewModel(navigationStore) : new DevicesListViewModel(navigationStore);

            mainViewModel = new MainViewModel(navigationStore);
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            MainWindow = new MainWindow()
            {
                DataContext = mainViewModel
            };

            MainWindow.Show();

            base.OnStartup(e);
        }
    }
}
