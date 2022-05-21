using System;
using DeskLinkServer.Stores;
using DeskLinkServer.Framework.Base;
using DeskLinkServer.Framework.Components;
using DeskLinkServer.Logic;

namespace DeskLinkServer.ViewModels
{
    public class MainViewModel : ObservableObject
    {
        private readonly NavigationStore navigationStore;

        public MainViewModel(NavigationStore navigationStore, MainLogic mainLogic)
        {
            this.navigationStore = navigationStore;
            navigationStore.CurrentViewModelChanged += new Action(() =>
            {
                RaisePropertyChanged(nameof(CurrentView));
            });
            DeviceNameText = Environment.MachineName;
            mainLogic.Server.ClientConnected += new Action(() =>
            {
                StatusText = "Подключен";
                DisplayStatus = Status.Success;
            });
            mainLogic.Server.ClientDisconnected += new Action(() =>
            {
                StatusText = "Ожидание подключения";
                DisplayStatus = Status.Wait;
            });
            mainLogic.Server.ErrorOccured += new Action<string>((message) =>
            {
                StatusText = "Ошибка";
                DisplayStatus = Status.Error;
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
