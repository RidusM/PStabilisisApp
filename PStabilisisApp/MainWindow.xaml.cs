using System.Windows;
using System.Windows.Controls;
using System.Net;
using Newtonsoft.Json;
using System.IO;
using Newtonsoft.Json.Linq;
using System.Diagnostics;

namespace PStabilisisApp
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public string title;
        public string newtitle;
        public string location;
        public string latitude;
        public string longitude;
        public string oldtitle;
        public string capacity;

        public MainWindow()
        {
            InitializeComponent();
        }


        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var url = "http://127.0.0.1:8000/objects/?skip=0&limit=100";
            var request = WebRequest.Create(url);
            request.Method = "GET";

            var webResponse = request.GetResponse();
            var webStream = webResponse.GetResponseStream();

            var reader = new StreamReader(webStream);
            var data = reader.ReadToEnd();
            var simp = JsonConvert.DeserializeObject<JArray>(data);
            Info.ItemsSource = simp;
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            title = Title.Text;
            var url = $"http://127.0.0.1:8000/objects/{{name}}?title={title}";

            var request = WebRequest.Create(url);
            request.Method = "DELETE";

            var webResponse = request.GetResponse();
            var webStream = webResponse.GetResponseStream();

            var reader = new StreamReader(webStream);
            var data = reader.ReadToEnd();
            Button_Click(sender, e);
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            title=Title.Text;
            location = Location.Text;
            latitude = Latitude.Text;
            longitude = Longitude.Text;
            capacity = Capacity.Text;
            var url = $"http://127.0.0.1:8000/objects/?title={title}&location={location}&latitude={latitude}&longitude={longitude}&capacity={capacity}";

            var request = WebRequest.Create(url);
            request.Method = "POST";

            var webResponse = request.GetResponse();
            var webStream = webResponse.GetResponseStream();

            var reader = new StreamReader(webStream);
            var data = reader.ReadToEnd();
            Button_Click(sender, e);
        }

        private void bntPatch_Click(object sender, RoutedEventArgs e)
        {
            oldtitle= Oldtitle.Text;
            newtitle= Newtitle.Text;
            var url = $"http://127.0.0.1:8000/objects/{{name}}?title={oldtitle}&newtitle={newtitle}";

            var request = WebRequest.Create(url);
            request.Method = "PATCH";

            var webResponse = request.GetResponse();
            var webStream = webResponse.GetResponseStream();

            var reader = new StreamReader(webStream);
            var data = reader.ReadToEnd();
            Button_Click(sender, e);
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            string url = File.ReadAllText(@"C:\\Users\\Admin\\PycharmProjects\\FastApiWebServicePractice\\itog.txt");
            Process.Start("explorer.exe", $"{url}");
        }
    }
}
