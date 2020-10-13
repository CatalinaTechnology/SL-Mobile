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
using Microsoft.Win32.SafeHandles;
using static Android.Support.V7.Widget.RecyclerView;

namespace ctPayment.Classes
{
	public class CustomListAdapter : BaseAdapter<Classes.Customer>
	{
		List<Classes.Customer> Customers;
		private Context sContext;
		public CustomListAdapter(Context context, List<Customer> customers)
		{
			this.Customers = customers;
			sContext = context;
		}
		public override Customer this[int position]
		{
			get
			{
				return Customers[position];
			}
		}

		public override int Count
		{
			get
			{
				return Customers.Count;
			}
		}

		public override long GetItemId(int position)
		{
			return position;
		}

		public override View GetView(int position, View convertView, ViewGroup parent)
		{
			View row = convertView;
			try
			{
				if (row == null)
				{
					row = LayoutInflater.From(sContext).Inflate(Resource.Layout.CustomerRow, null, false);
				}
				TextView txtCustID = row.FindViewById<TextView>(Resource.Id.CustId);
				TextView txtName = row.FindViewById<TextView>(Resource.Id.Name);
				txtName.Text = Customers[position].Name;
				txtCustID.Text = Customers[position].CustId;
			}
			catch (Exception ex)
			{
				System.Diagnostics.Debug.WriteLine(ex.Message);
			}
			finally { }
			return row;
		}
	}
}