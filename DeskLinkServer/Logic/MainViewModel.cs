using System.Windows.Controls;
using System.Windows.Input;
using DeskLinkServer.Framework.Base;
using DeskLinkServer.Framework.Components;
using DeskLinkServer.Framework.Pages;

namespace DeskLinkServer.Logic
{
    public class MainViewModel : ObservableObject
    {
        public MainViewModel()
        {
            currentPage = new PlaceholderPage();
        }

        #region Fields

        private string statusText = "StatusText";
        private string deviceNameText = "DeviceNameText";

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
            get => new RelayCommand((o) => { });
        }

        public ICommand OnClosingCommand
        {
            get => new RelayCommand((o) => { });
        }

        #endregion
    }
}
