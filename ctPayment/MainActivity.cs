using Android.App;
using Android.OS;
using Android.Runtime;
using Android.Support.V7.App;
using Android.Views;
using Android.Widget;
using Newtonsoft.Json.Linq;
using RestSharp;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xamarin.Essentials;
using static Android.App.ActionBar;

namespace ctPayment
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = true)]
    public class MainActivity : AppCompatActivity
    {
        private List<Classes.Customer> mCustomerList;
        private int currentPosition;
        private Button btnPopupCancel;
        private Button btnPopOk;
        private Dialog popupDialog;
        private EditText etEmailAddress;
        private EditText etAmount;

        private Dialog configDialog;


        protected override void OnCreate(Bundle savedInstanceState)
        {

            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.activity_main);
            Button getCustomers = FindViewById<Button>(Resource.Id.getCustomers);
			getCustomers.Click += GetCustomers_Click;
            Button buttonConfig = FindViewById<Button>(Resource.Id.buttonConfig);
			buttonConfig.Click += ButtonConfig_Click;
            if(string.IsNullOrEmpty(Authorization) || string.IsNullOrEmpty(ctAPIURI) || string.IsNullOrEmpty(CpnyID))
			{
                ShowConfigDialog();
			}
        }

		private void ShowConfigDialog()
		{
            configDialog = new Dialog(this);
            configDialog.SetContentView(Resource.Layout.ctAPIInfoPopup);
            configDialog.Window.SetSoftInputMode(SoftInput.AdjustResize);
            configDialog.Show();

            configDialog.Window.SetLayout(LayoutParams.MatchParent, LayoutParams.WrapContent);

            EditText etURI = configDialog.FindViewById<EditText>(Resource.Id.ctAPIURI);
            EditText etAuth = configDialog.FindViewById<EditText>(Resource.Id.ctAPIAuth);
            EditText etCpnyID = configDialog.FindViewById<EditText>(Resource.Id.CpnyID);
            Button btnAuthCancel = configDialog.FindViewById<Button>(Resource.Id.btnAuthCancel);
            Button btnAuthOK = configDialog.FindViewById<Button>(Resource.Id.btnAuthOk);

            btnAuthCancel.Click += BtnAuthCancel_Click;
            btnAuthOK.Click += BtnAuthOK_Click;

            etURI.Text = ctAPIURI;
            etAuth.Text = Authorization;
            etCpnyID.Text = CpnyID;
        }

		private void ButtonConfig_Click(object sender, System.EventArgs e)
		{
            ShowConfigDialog();
        }

		private void BtnAuthOK_Click(object sender, System.EventArgs e)
		{
            EditText etURI = configDialog.FindViewById<EditText>(Resource.Id.ctAPIURI);
            EditText etAuth = configDialog.FindViewById<EditText>(Resource.Id.ctAPIAuth);
            EditText etCpnyID = configDialog.FindViewById<EditText>(Resource.Id.CpnyID);

            Authorization = etAuth.Text;
            ctAPIURI = etURI.Text;
            CpnyID = etCpnyID.Text;

            configDialog.Dismiss();
            configDialog.Hide();
            configDialog.Cancel();
        }

		private void BtnAuthCancel_Click(object sender, System.EventArgs e)
		{
            configDialog.Dismiss();
            configDialog.Hide();
            configDialog.Cancel();
        }

		private void GetCustomers_Click(object sender, System.EventArgs e)
		{
            var client = new RestClient(ctAPIURI + "financial/accountsReceivable/customer/query");
            var request = new RestRequest(Method.POST);
            request.RequestFormat = DataFormat.Json;
            request.AddHeader("Content-Type", "application/json");

            TextView searchEntry = FindViewById<TextView>(Resource.Id.entry);

            var requestObject = new List<Classes.NameValuePairs>
                {
                    new Classes.NameValuePairs{name = "CustID", value = searchEntry.Text.Trim() + "%"}
                };

            string requestBody = Newtonsoft.Json.JsonConvert.SerializeObject(requestObject,
            Newtonsoft.Json.Formatting.Indented,
            new Newtonsoft.Json.JsonSerializerSettings
            {
                NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore
            });

            request.AddParameter("application/json", requestBody, ParameterType.RequestBody);
            request.AddParameter("Authorization", Authorization, ParameterType.HttpHeader);

            IRestResponse response = client.Execute(request);

            mCustomerList = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Classes.Customer>>(response.Content);

            ListView customerList = FindViewById<ListView>(Resource.Id.customerList);
            customerList.Adapter = new Classes.CustomListAdapter(this, mCustomerList);
			customerList.ItemClick += CustomerList_ItemClick;

            searchEntry.ClearFocus();
        }

		private void CustomerList_ItemClick(object sender, AdapterView.ItemClickEventArgs e)
		{
            currentPosition = e.Position;

            var select = mCustomerList[currentPosition].CustId;
            
            popupDialog = new Dialog(this);
            popupDialog.SetContentView(Resource.Layout.Popup);
            popupDialog.Window.SetSoftInputMode(SoftInput.AdjustResize);
            popupDialog.Show();


            // Access Popup layout fields like below  
            btnPopupCancel = popupDialog.FindViewById<Button>(Resource.Id.btnCancel);
            btnPopOk = popupDialog.FindViewById<Button>(Resource.Id.btnOk);

            etEmailAddress = popupDialog.FindViewById<EditText>(Resource.Id.emailAddress);
            etAmount = popupDialog.FindViewById<EditText>(Resource.Id.amountToSend);

            var tvCustName = popupDialog.FindViewById<TextView>(Resource.Id.custName);
            var tvCustID = popupDialog.FindViewById<TextView>(Resource.Id.custID);

            etEmailAddress.Text = mCustomerList[e.Position].EMailAddr;
            etAmount.Text = "0";
            tvCustName.Text = mCustomerList[currentPosition].Name;
            tvCustID.Text = mCustomerList[currentPosition].CustId;

            // Events for that popup layout  
            btnPopupCancel.Click += BtnPopupCancel_Click;
            btnPopOk.Click += BtnPopOk_Click;

        }




        private void BtnPopOk_Click(object sender, System.EventArgs e)
		{
            var client = new RestClient(ctAPIURI + "customSQL/xct_spSLPaddAccountPaymentRequest");
            var request = new RestRequest(Method.POST);
            request.RequestFormat = DataFormat.Json;
            request.AddHeader("Content-Type", "application/json");

            TextView searchEntry = FindViewById<TextView>(Resource.Id.entry);

            var requestObject = new List<Classes.NameValuePairs>
                {
                    new Classes.NameValuePairs{name = "CpnyID", value = CpnyID},
                    new Classes.NameValuePairs{name = "CustID", value =  mCustomerList[currentPosition].CustId.Trim()},
                    new Classes.NameValuePairs{name = "amount", value = etAmount.Text.Trim()},
                    new Classes.NameValuePairs{name = "paymentEmailList", value = etEmailAddress.Text.Trim()},
                };


            JObject rootObject = new JObject();
            rootObject.Add("parameters", JArray.FromObject(requestObject));

            string requestBody = Newtonsoft.Json.JsonConvert.SerializeObject(rootObject,
            Newtonsoft.Json.Formatting.Indented,
            new Newtonsoft.Json.JsonSerializerSettings
            {
                NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore
            });

            

            request.AddParameter("application/json", requestBody, ParameterType.RequestBody);
            request.AddParameter("Authorization", Authorization, ParameterType.HttpHeader);

            IRestResponse response = client.Execute(request);

            string returnData = response.Content;

            popupDialog.Dismiss();
            popupDialog.Hide();
            popupDialog.Cancel();
        }

		private void BtnPopupCancel_Click(object sender, System.EventArgs e)
		{
            popupDialog.Dismiss();
            popupDialog.Hide();
            popupDialog.Cancel();
        }

		public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }

		private string Authorization
        {
            get
			{
                try
                {
                    var authValue = GetSecureInfo("ctAPIAuth");
                    if(authValue == null) { throw new System.Exception("Null ctAPIAuth returned from secure store."); }
                    return authValue.Result.ToString();
                }
                catch
				{
                    return "";
				}
			}
            set
			{
                SetSecureInfo("ctAPIAuth", value);
			}
        }

        private string ctAPIURI
        {
            get
            {
                try
                {
                    var uriValue = GetSecureInfo("ctAPIURI");
                    if(uriValue == null) { throw new System.Exception("Null ctAPIURI returned from secure store."); }
                    string uriString = uriValue.Result.Trim();
                    if(uriString.Length == 0) { throw new System.Exception("ctAPIURI returned zero length string from secure store."); }
                    if (uriString.Substring(uriString.Length -1, 1) != "/") { uriString += "/";}
                    return uriValue.Result.ToString();
                }
                catch
				{
                    return "";
				}
            }
            set
            {
                SetSecureInfo("ctAPIURI", value);
            }
        }

        private string CpnyID
        {
            get
            {
                try
                {
                    var uriValue = GetSecureInfo("CpnyID");
                    if (uriValue == null) { throw new System.Exception("Null CpnyID returned from secure store."); }
                    string uriString = uriValue.Result.Trim();
                    if (uriString.Length == 0) { throw new System.Exception("CpnyID returned zero length string from secure store."); }
                    if (uriString.Substring(uriString.Length - 1, 1) != "/") { uriString += "/"; }
                    return uriValue.Result.ToString();
                }
                catch
                {
                    return "";
                }
            }
            set
            {
                SetSecureInfo("CpnyID", value);
            }
        }

        private async Task<string>GetSecureInfo(string infoName)
		{
            string returnValue = await SecureStorage.GetAsync(infoName);
            return returnValue;
        }

        private async void SetSecureInfo(string infoName, string value)
        {
            await SecureStorage.SetAsync(infoName, value);
        }

    }
}