using System.Windows;
using DeskLinkServer.Logic;
using DeskLinkServer.Services;
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
            navigationStore = new NavigationStore();

            mainLogic = new MainLogic(navigationStore);

            NavigationService.NavigateToDeviceList(navigationStore, mainLogic);

            mainViewModel = new MainViewModel(navigationStore, mainLogic);
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            mainLogic.Start();

            MainWindow = new MainWindow()
            {
                DataContext = mainViewModel
            };

            MainWindow.Show();

            base.OnStartup(e);
        }

        protected override void OnExit(ExitEventArgs e)
        {
            mainLogic.Stop();

            base.OnExit(e);
        }
    }
}
