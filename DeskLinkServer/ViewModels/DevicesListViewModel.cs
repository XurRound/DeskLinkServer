using DeskLinkServer.Stores;
using DeskLinkServer.Framework.Base;
using System.Collections.Generic;
using DeskLinkServer.Logic;
using System.Windows.Input;
using DeskLinkServer.Services;
using System.Windows;
using System.Collections.ObjectModel;

namespace DeskLinkServer.ViewModels
{
    public class DevicesListViewModel : BaseViewModel
    {
        public ICommand AddDeviceCommand { get; }

        public ICommand DeleteDeviceCommand { get; }

        public DevicesListViewModel(NavigationStore navigationStore, MainLogic mainLogic)
        {
            KnownDevices = new ObservableCollection<Device>(mainLogic.Configuration.KnownDevices);

            AddDeviceCommand = new RelayCommand((o) =>
            {
                navigationStore.CurrentViewModel = new AddDeviceViewModel(navigationStore, mainLogic);
            });

            DeleteDeviceCommand = new RelayCommand((o) =>
            {
                MessageBoxResult result = MessageBox.Show("Отключить устройство?",
                    "Отключение устройства",
                    MessageBoxButton.YesNo,
                    MessageBoxImage.Question);
                if (result == MessageBoxResult.Yes)
                {
                    string id = o as string;
                    mainLogic.Configuration.KnownDevices.RemoveAll(d => d.Identifier == id);
                    mainLogic.RefreshKnownDevices();
                    NavigationService.NavigateToDeviceList(navigationStore, mainLogic);
                }
            });
        }

        public ObservableCollection<Device> KnownDevices { get; set; }
    }
}
