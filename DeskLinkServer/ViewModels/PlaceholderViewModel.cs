using System.Windows.Input;
using DeskLinkServer.Stores;
using DeskLinkServer.Framework.Base;

namespace DeskLinkServer.ViewModels
{
    public class PlaceholderViewModel : BaseViewModel
    {
        public ICommand AddDeviceCommand { get; }

        public PlaceholderViewModel(NavigationStore navigationStore)
        {
            AddDeviceCommand = new RelayCommand((o) =>
            {
                navigationStore.CurrentViewModel = new AddDeviceViewModel(navigationStore);
            });
        }
    }
}
