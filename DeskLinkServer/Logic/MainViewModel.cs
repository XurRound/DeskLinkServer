using System.Windows.Input;
using System.Windows.Controls;
using DeskLinkServer.Framework.Base;
using DeskLinkServer.Framework.Pages;
using DeskLinkServer.Framework.Components;

namespace DeskLinkServer.Logic
{
    public class MainViewModel : ObservableObject
    {
        private readonly MainLogic mainLogic;

        public MainViewModel()
        {
            mainLogic = new MainLogic();

            placeholderPage = new PlaceholderPage();
            addDevicePage = new AddDevicePage();
            devicesListPage = new DevicesListPage();

            CurrentPage = (mainLogic.Configuration.KnownDevices.Count == 0) ? placeholderPage : devicesListPage;
        }

        #region Fields

        private readonly PlaceholderPage placeholderPage;
        private readonly AddDevicePage addDevicePage;
        private readonly DevicesListPage devicesListPage;

        private string statusText = Properties.Resources.DefaultStatusText;
        private string deviceNameText = Properties.Resources.DefaultDeviceName;

        private Page currentPage;

        private Status displayStatus = Status.Wait;

        #endregion

        #region Properties

        public string StatusText
        {
            get
            {
                return statusText;
            }
            set
            {
                statusText = value;
                RaisePropertyChanged();
            }
        }

        public string DeviceNameText
        {
            get
            {
                return deviceNameText;
            }
            set
            {
                deviceNameText = value;
                RaisePropertyChanged();
            }
        }

        public Page CurrentPage
        {
            get
            {
                return currentPage;
            }
            set
            {
                currentPage = value;
                RaisePropertyChanged();
            }
        }

        public Status DisplayStatus
        {
            get
            {
                return displayStatus;
            }
            set
            {
                displayStatus = value;
                RaisePropertyChanged();
            }
        }

        #endregion

        #region Commands

        public ICommand OnLoadedCommand
        {
            get => new RelayCommand((o) => mainLogic.Start());
        }

        public ICommand OnClosingCommand
        {
            get => new RelayCommand((o) => mainLogic.Stop());
        }

        #endregion
    }
}
