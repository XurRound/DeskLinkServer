using System.Windows;
using WinForms = System.Windows.Forms;
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

        private readonly WinForms.NotifyIcon NotifyIcon;

        public App()
        {
            NotifyIcon = new WinForms.NotifyIcon();
            NotifyIcon.Icon = new System.Drawing.Icon(GetResourceStream(
                new System.Uri("/DeskLinkServer;component/icon.ico", System.UriKind.RelativeOrAbsolute)).Stream);

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

            MainWindow.Closing += (s, evt) =>
            {
                evt.Cancel = true;
                MainWindow.WindowState = WindowState.Minimized;
                MainWindow.ShowInTaskbar = false;
            };
            MainWindow.StateChanged += (s, evt) =>
            {
                if (MainWindow.WindowState == WindowState.Minimized)
                    MainWindow.ShowInTaskbar = false;
            };

            MainWindow.Show();

            NotifyIcon.ContextMenu = new WinForms.ContextMenu();

            NotifyIcon.ContextMenu.MenuItems.Add("Open", (s, evt) =>
            {
                MainWindow.WindowState = WindowState.Normal;
                MainWindow.ShowInTaskbar = true;
                MainWindow.Activate();
            });
            NotifyIcon.ContextMenu.MenuItems.Add("Quit", (s, evt) =>
            {
                Current.Shutdown();
            });

            NotifyIcon.DoubleClick += (s, evt) =>
            {
                MainWindow.WindowState = WindowState.Normal;
                MainWindow.ShowInTaskbar = true;
                MainWindow.Activate();
            };

            NotifyIcon.Visible = true;

            base.OnStartup(e);
        }

        protected override void OnExit(ExitEventArgs e)
        {
            mainLogic.Stop();

            NotifyIcon.Visible = false;
            NotifyIcon.Icon.Dispose();
            NotifyIcon.Dispose();

            base.OnExit(e);
        }
    }
}
