using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Reto4.Services;

namespace Reto4
{
    [Activity(Label = "Registrar datos")]
    public class RegistroActivity : Activity
    {
        TextView txtView;
        Button btnRegistrar;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.Registro);
            FindViewById<Button>(Resource.Id.btnConsulta).Click += OnBtnConsultaClick;

            btnRegistrar = FindViewById<Button>(Resource.Id.btnRegistrar);
            btnRegistrar.Click += OnBtnRegistrarClick;

            txtView = FindViewById<TextView>(Resource.Id.textView);
        }

        private async void OnBtnRegistrarClick(object sender, EventArgs e)
        {
            try
            {
                ServiceHelper serviceHelper = new ServiceHelper();
                // Retrieve the values the user entered into the UI
                string AndroidId = Android.Provider.Settings.Secure.GetString(ContentResolver, Android.Provider.Settings.Secure.AndroidId);

                string email = FindViewById<EditText>(Resource.Id.editTextEmail).Text;

                await serviceHelper.InsertarEntidad(email, "reto4+" + serviceHelper.items.Count, AndroidId);

                Toast.MakeText(this, "Gracias por registrarte en Reto 4", ToastLength.Long).Show();

                SetResult(Result.Ok, Intent);
            }
            catch (Exception exc)
            {
                Toast.MakeText(this, exc.Message, ToastLength.Long).Show();
                SetResult(Result.Canceled, Intent);
            }

            Finish();
        }

        async void OnBtnConsultaClick(object sender, EventArgs e)
        {
            try
            {
                ServiceHelper serviceHelper = new ServiceHelper();
                string email = FindViewById<EditText>(Resource.Id.editTextEmail).Text;
                await serviceHelper.BuscarRegistros(email);

                foreach (var item in serviceHelper.items)
                {
                    txtView.Text += String.Format("{0} {1} {2} \n", item.DeviceId, item.Email, item.Reto);
                }
                btnRegistrar.Visibility = ViewStates.Visible;
            }
            catch (Exception exc)
            {
                Toast.MakeText(this, exc.Message, ToastLength.Long).Show();
                SetResult(Result.Canceled, Intent);
            }
        }
    }
}