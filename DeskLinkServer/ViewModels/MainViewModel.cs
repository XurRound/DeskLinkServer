using System;
using System.Windows.Input;
using DeskLinkServer.Stores;
using DeskLinkServer.Framework.Base;
using DeskLinkServer.Framework.Components;

namespace DeskLinkServer.ViewModels
{
    public class MainViewModel : ObservableObject
    {
        private readonly NavigationStore navigationStore;

        public MainViewModel(NavigationStore navigationStore)
        {
            this.navigationStore = navigationStore;
            navigationStore.CurrentViewModelChanged += new Action(() =>
            {
                RaisePropertyChanged(nameof(CurrentView));
            });
        }

        #region Fields

        private string statusText = Properties.Resources.DefaultStatusText;
        private string deviceNameText = Properties.Resources.DefaultDeviceName;

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

        public BaseViewModel CurrentView => navigationStore.CurrentViewModel;

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
    }
}
