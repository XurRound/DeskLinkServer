using DeskLinkServer.Framework.Base;
using DeskLinkServer.Stores;
using System.Windows.Input;

namespace DeskLinkServer.ViewModels
{
    public class AddDeviceViewModel : BaseViewModel
    {
        public ICommand ReturnToDevicesListCommand { get; }

        public AddDeviceViewModel(NavigationStore navigationStore)
        {
            ReturnToDevicesListCommand = new RelayCommand((o) =>
            {
                navigationStore.CurrentViewModel = new DevicesListViewModel(navigationStore);
            });
        }
    }
}
