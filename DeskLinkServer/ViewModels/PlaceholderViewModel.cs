using System.Windows.Input;
using DeskLinkServer.Stores;
using DeskLinkServer.Framework.Base;
using DeskLinkServer.Logic;

namespace DeskLinkServer.ViewModels
{
    public class PlaceholderViewModel : BaseViewModel
    {
        public ICommand AddDeviceCommand { get; }

        public PlaceholderViewModel(NavigationStore navigationStore, MainLogic mainLogic)
        {
            AddDeviceCommand = new RelayCommand((o) =>
            {
                navigationStore.CurrentViewModel = new AddDeviceViewModel(navigationStore, mainLogic);
            });
        }
    }
}
