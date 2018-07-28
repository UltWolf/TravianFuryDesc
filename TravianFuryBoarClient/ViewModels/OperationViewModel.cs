using AngleSharp.Dom;
using AngleSharp.Dom.Html;
using AngleSharp.Parser.Html;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Policy;
using System.Text;
using System.Web;
using System.Windows;
using System.Windows.Input;
using TravianFuryBoarClient.Models;
using TravianFuryBoarClient.ViewModels.Components;

namespace TravianFuryBoarClient.ViewModels
{
    class OperationViewModel : BaseViewModel
    {
        private System.Net.CookieCollection _token;
        private string _url;
        private VillageOperation village;

        public System.Net.CookieCollection Token { get => _token; set { _token = value; OnPropertyChanged("Token"); } }
        public VillageOperation Village { get => village; set { village = value; OnPropertyChanged("Village"); } }
        public ObservableCollection<VillageOperation> Vil { get; private set; }
        public string URL { get => _url; set { _url = value; OnPropertyChanged("URL"); } }
        public ICommand SendCommand { get; private set; }
        public ICommand GetOperationsCommand { get; private set; }
        public OperationViewModel(System.Net.CookieCollection token)
        {
            _token = token;
            SendCommand = new RelayCommand(Send);
            GetOperationsCommand = new RelayCommand(GetOperation);
        }
        public ObservableCollection<VillageOperation> SyncOperations()
        {
            ObservableCollection<VillageOperation> Villages = new ObservableCollection<VillageOperation>();
            WebClient wc = new WebClient();
            wc.Encoding = Encoding.UTF8;
            wc.Headers.Add(HttpRequestHeader.Cookie,
              Token[0].Name + "=" + Token[0].Value + ";" +
              Token[1].Name + "=" + Token[1].Value + ";" +
              Token[2].Name + "=" + Token[2].Value + ";" +
              Token[3]?.Name + "=" + Token[0]?.Value + ";");
            string html = wc.DownloadString("https://www.gettertools.com/ru5.kingdoms.com.2/try,groups147?groupplan");

            IHtmlDocument angle = new HtmlParser().Parse(html);
            var tables = angle.QuerySelectorAll("table");

            try
            {
                var trs = tables[1].Children[0].Children.GetElementsFromRange(2);
                foreach (var tr in trs)
                {
                    Villages.Add(new VillageOperation() { SourceVillage = tr.Children[0].Children[0].TextContent, DestinationVillage = tr.Children[2].Children[0].TextContent, TypeAttack = tr.Children[3].Children[0].Children[0].TextContent, Time = DateTime.Now } );
                }
            }
            catch (ArgumentOutOfRangeException e)
            {
                MessageBox.Show("You put wrong adress, or you don`t have plan operation", "ERROR", MessageBoxButton.OK);
            }




            return Villages;
        }
        public void GetOperation(object obj)
        {
            Vil = SyncOperations();
            OnPropertyChanged("Vil");
        }
        public void Send(object obj)
        {

        }
    }
    public static class ExtensionIEnumerable
    {
        public static List<IElement> GetElementsFromRange(this IHtmlCollection<IElement> collection, int beginRange, int endRange)
        {

            List<IElement> newCollection = new List<IElement>();
            for (; beginRange < endRange; beginRange++)
            {
                newCollection.Add(collection[beginRange]);
            }
            return newCollection;

        }
        public static List<IElement> GetElementsFromRange(this IHtmlCollection<IElement> collection, int beginRange)
        {
            int endRange = collection.Length-1;
            List<IElement> newCollection = new List<IElement>();
            for (; beginRange <= endRange; beginRange++)
            {
                newCollection.Add(collection[beginRange]);
            }
            return newCollection;

        }


    }
}
