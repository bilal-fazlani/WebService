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
using PortableClient;
using PortableModels;

namespace AndroidConsumer
{
    [Activity(Label = "AndroidConsumer", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity
    {
        private readonly ApiClient<Car> _client = new ApiClient<Car>
            ("http://BILALMUSTAF3107/webservice/api/mycars/");

        private ArrayAdapter _adapter;

        protected override async void OnCreate(Bundle bundle)
        {
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
                

            //// Get our button from the layout resource,
            //// and attach an event to it
            //Button button = FindViewById<Button>(Resource.Id.MyButton);

            //button.Click += delegate { button.Text = string.Format("{0} clicks!", count++); };
        }
    }
}

