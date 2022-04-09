using System.Windows.Controls;
using System.Windows.Input;
using DeskLinkServer.Framework.Base;
using DeskLinkServer.Framework.Components;

namespace DeskLinkServer.Logic
{
    public class MainViewModel : ObservableObject
    {
        public MainViewModel()
        {

        }

        #region Fields

        private string statusText = "";
        private string btNameText = "";

        private Page currentPage;

        private Status displayStatus = Status.Success;

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

        public string BTNameText
        {
            get
            {
                return btNameText;
            }
            set
            {
                btNameText = value;
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
