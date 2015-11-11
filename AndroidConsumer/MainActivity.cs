using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using PortableClient;
using PortableModels;

namespace AndroidConsumer
{
    [Activity(Label = "AndroidConsumer", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity
    {
        private const string ServiceMachineIp = "192.168.0.101";

        private readonly ApiClient<Car> _client = new ApiClient<Car>
            ($"http://{ServiceMachineIp}/webservice/api/mycars/");

        private ArrayAdapter _adapter;

        protected override async void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            if (!IsConnectedToService())
            {
                throw new Exception("cant connect to service");
            }

            base.OnCreate(bundle);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);

            ListView carListView = FindViewById<ListView>(Resource.Id.carlistView);

            List<string> cars = (await _client.GetAllAsync()).Select(x=>x.Model).ToList();

            _adapter = new ArrayAdapter(
                this,
                Android.Resource.Layout.SimpleListItem1,
                cars);

            carListView.Adapter = _adapter;
        }

        private static bool IsConnectedToService()
        {
            var ping = new Ping();
             
            var pingreply =  ping.Send(ServiceMachineIp, 1000);

            return pingreply.Status == IPStatus.Success;
        }
    }
}

