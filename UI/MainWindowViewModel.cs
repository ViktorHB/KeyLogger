using Microsoft.Win32;
using MWVMLib;
using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;


namespace UI
{
    class MainWindowViewModel : ViewModelBase
    {
        private String _searchTExt;
        private DateTime _selectedDate;
        private ObservableCollection<String> _users;
        private ObservableCollection<String> _textMessages;
        private String _selectedUser;
        private ServiceReferenceUI.UIServiceClient serviceUI;
        private ICommand _textChangedCommand;
        private ICommand _selectedUserCommand;
        private ICommand _selectedDateCommand;


        public MainWindowViewModel()
        {
            serviceUI = new ServiceReferenceUI.UIServiceClient();
        }


        private void SelectedDateCommandExecute(object obj)
        {
            //почему вылетает если берем дату из будущего на 
            if (SelectedDate != null)
            {
                var res = serviceUI.GetMessegesOnDate(SelectedUser, SelectedDate);
                if (res != null)
                    TextMessages = new ObservableCollection<string>(res);
                else
                {
                    TextMessages.Clear();
                    TextMessages.Add("no data");
                }
            }
        }

        private void SelectedUserCommandExecute(object obj)
        {
            TextMessages = new ObservableCollection<string>(serviceUI.GetAllMessages(SelectedUser));
        }

        private void UsersNameFilter(object obj)
        {
            if (!String.IsNullOrEmpty(SearchTExt) && !String.IsNullOrWhiteSpace(SearchTExt))
            {
                Users = new ObservableCollection<string>(serviceUI.LiveSearch(SearchTExt));
                // await LiveSearchAsync();
            }
            else
            {
                Users = new ObservableCollection<string>(serviceUI.GetAllUsers());
            }

            //Users = new ObservableCollection<string>(
            //    (!String.IsNullOrEmpty(SearchTExt) && !String.IsNullOrWhiteSpace(SearchTExt))?
            //    (serviceUI.LiveSearch(SearchTExt)):
            //    (serviceUI.GetAllUsers()));
        }

        //private Task LiveSearchAsync()
        //{
        //    return Task.Run(() => { Users = new ObservableCollection<string>(serviceUI.LiveSearch(SearchTExt))});
        //}


        #region prop

        public ICommand SelectedDateCommand
        {
            get
            {
                if (_selectedDateCommand == null)
                    _selectedDateCommand = new RelayCommand(SelectedDateCommandExecute);

                return _selectedDateCommand;
            }
            set
            {
                _selectedDateCommand = value;
                OnPropertyChanged(nameof(SelectedDateCommand));
            }
        }


        public ICommand SelectedUserCommand
        {
            get
            {
                if (_selectedUserCommand == null)
                    _selectedUserCommand = new RelayCommand(SelectedUserCommandExecute);
                return _selectedUserCommand;
            }
            set
            {
                _selectedUserCommand = value;
                OnPropertyChanged(nameof(SelectedUserCommand));
            }
        }
        public ICommand TextChangedCommand
        {
            get
            {
                if (_textChangedCommand == null)
                    _textChangedCommand = new RelayCommand(UsersNameFilter);
                return _textChangedCommand;
            }
            set
            {
                _textChangedCommand = value;
                OnPropertyChanged(nameof(_textChangedCommand));
            }
        }
        public String SelectedUser
        {
            get { return _selectedUser; }
            set
            {
                if (_selectedUser != value)
                {
                    _selectedUser = value;
                    OnPropertyChanged(nameof(SelectedUser));
                }
            }
        }

        public ObservableCollection<String> TextMessages
        {
            get
            {
                if (_textMessages == null)
                    return new ObservableCollection<string>();
                return _textMessages;
            }
            set
            {
                if (_textMessages != value)
                {
                    _textMessages = value;
                    OnPropertyChanged(nameof(TextMessages));
                }
            }
        }

        public ObservableCollection<String> Users
        {
            get
            {
                if (_users == null)
                    return new ObservableCollection<string>(serviceUI.GetAllUsers());
                return _users;
            }
            set
            {
                if (_users != value)
                {
                    _users = value;
                    OnPropertyChanged(nameof(Users));
                }
            }
        }


        public DateTime SelectedDate
        {
            get
            {
                //if (_selectedDate == null)
                //    return DateTime.Now;
                return _selectedDate;
            }
            set
            {
                if (_selectedDate != value)
                {
                    _selectedDate = value;
                    OnPropertyChanged(nameof(SelectedDate));
                }
            }
        }

        public String SearchTExt
        {
            get { return _searchTExt; }
            set
            {
                if (_searchTExt != value)
                {
                    _searchTExt = value;
                    OnPropertyChanged(nameof(SearchTExt));
                }
            }
        }
        #endregion prop
    }
}
