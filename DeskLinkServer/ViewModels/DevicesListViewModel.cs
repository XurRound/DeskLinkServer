using DeskLinkServer.Stores;
using DeskLinkServer.Framework.Base;
using System.Collections.Generic;
using DeskLinkServer.Logic;
using System.Windows.Input;

namespace DeskLinkServer.ViewModels
{
    public class DevicesListViewModel : BaseViewModel
    {
        private List<Device> knownDevices;

        public ICommand AddDeviceCommand { get; }

        public DevicesListViewModel(NavigationStore navigationStore, MainLogic mainLogic)
        {
            knownDevices = mainLogic.Configuration.KnownDevices;

            AddDeviceCommand = new RelayCommand((o) =>
            {
                navigationStore.CurrentViewModel = new AddDeviceViewModel(navigationStore, mainLogic);
            });
        }

        public List<Device> KnownDevices
        {
            get { return knownDevices; }
            set
            {
                knownDevices = value;
                RaisePropertyChanged();
            }
        }
    }
}
