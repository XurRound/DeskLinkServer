using DeskLinkServer.Logic;
using DeskLinkServer.Stores;
using DeskLinkServer.ViewModels;

namespace DeskLinkServer.Services
{
    public static class NavigationService
    {
        public static void NavigateToDeviceList(NavigationStore navigationStore, MainLogic mainLogic)
        {
            if (mainLogic.Configuration.KnownDevices.Count == 0)
                navigationStore.CurrentViewModel = new PlaceholderViewModel(navigationStore, mainLogic);
            else
                navigationStore.CurrentViewModel = new DevicesListViewModel(navigationStore, mainLogic);
        }
    }
}
