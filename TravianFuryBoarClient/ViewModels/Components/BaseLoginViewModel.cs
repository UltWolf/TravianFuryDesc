namespace TravianFuryBoarClient.ViewModels.Components
{
    public class BaseLoginViewModel:BaseViewModel
    {
        private string _username;
        private string _password;
        public string Username { get => _username; set{_username = value; OnPropertyChanged("Username");} }
        public string Password{ get=> _password;set{_password=value;OnPropertyChanged("Password");}}
    }
}