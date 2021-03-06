using System.ComponentModel;
using System.IO;

namespace TravianFuryBoarClient.ViewModels.Components
{
    public abstract class BaseViewModel: INotifyPropertyChanged
    {
        public MemoryStream ms = new MemoryStream();
        public string ServerUrl; 
        public string url= "https://localhost:5001/";
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged(string propertyname)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyname));
            }
        }

    }
}