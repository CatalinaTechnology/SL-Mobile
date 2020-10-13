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

namespace ctPayment.Classes
{
	public class Customer
	{
		public int counter { get; set; }
		public int totalEntries { get; set; }
		public int totalPages { get; set; }
		public string errorMessage { get; set; }
		public string notes { get; set; }
		public int resultCode { get; set; }
		public string AccrRevAcct { get; set; }
		public string AccrRevSub { get; set; }
		public string AcctNbr { get; set; }
		public string Addr1 { get; set; }
		public string Addr2 { get; set; }
		public string AgentID { get; set; }
		public int ApplFinChrg { get; set; }
		public string ArAcct { get; set; }
		public string ArSub { get; set; }
		public string Attn { get; set; }
		public int AutoApply { get; set; }
		public string BankID { get; set; }
		public string BillAddr1 { get; set; }
		public string BillAddr2 { get; set; }
		public string BillAttn { get; set; }
		public string BillCity { get; set; }
		public string BillCountry { get; set; }
		public string BillFax { get; set; }
		public string BillName { get; set; }
		public string BillPhone { get; set; }
		public string BillSalut { get; set; }
		public string BillState { get; set; }
		public int BillThruProject { get; set; }
		public string BillZip { get; set; }
		public DateTime CardExpDate { get; set; }
		public string CardHldrName { get; set; }
		public string CardNbr { get; set; }
		public string CardType { get; set; }
		public string City { get; set; }
		public string ClassId { get; set; }
		public int ConsolInv { get; set; }
		public string Country { get; set; }
		public float CrLmt { get; set; }
		public DateTime Crtd_DateTime { get; set; }
		public string Crtd_Prog { get; set; }
		public string Crtd_User { get; set; }
		public string CuryId { get; set; }
		public string CuryPrcLvlRtTp { get; set; }
		public string CuryRateType { get; set; }
		public int CustFillPriority { get; set; }
		public string CustId { get; set; }
		public string DfltShipToId { get; set; }
		public string DocPublishingFlag { get; set; }
		public int DunMsg { get; set; }
		public string EMailAddr { get; set; }
		public string Fax { get; set; }
		public int InvtSubst { get; set; }
		public string LanguageID { get; set; }
		public DateTime LUpd_DateTime { get; set; }
		public string LUpd_Prog { get; set; }
		public string LUpd_User { get; set; }
		public string Name { get; set; }
		public int NoteId { get; set; }
		public int OneDraft { get; set; }
		public string PerNbr { get; set; }
		public string Phone { get; set; }
		public string PmtMethod { get; set; }
		public string PrcLvlId { get; set; }
		public string PrePayAcct { get; set; }
		public string PrePaySub { get; set; }
		public string PriceClassID { get; set; }
		public int PrtMCStmt { get; set; }
		public int PrtStmt { get; set; }
		public string S4Future01 { get; set; }
		public string S4Future02 { get; set; }
		public float S4Future03 { get; set; }
		public float S4Future04 { get; set; }
		public float S4Future05 { get; set; }
		public float S4Future06 { get; set; }
		public DateTime S4Future07 { get; set; }
		public DateTime S4Future08 { get; set; }
		public int S4Future09 { get; set; }
		public int S4Future10 { get; set; }
		public string S4Future11 { get; set; }
		public string S4Future12 { get; set; }
		public string Salut { get; set; }
		public DateTime SetupDate { get; set; }
		public int ShipCmplt { get; set; }
		public string ShipPctAct { get; set; }
		public float ShipPctMax { get; set; }
		public string SICCode1 { get; set; }
		public string SICCode2 { get; set; }
		public int SingleInvoice { get; set; }
		public string SlsAcct { get; set; }
		public string SlsperId { get; set; }
		public string SlsSub { get; set; }
		public string State { get; set; }
		public string Status { get; set; }
		public string StmtCycleId { get; set; }
		public string StmtType { get; set; }
		public string TaxDflt { get; set; }
		public string TaxExemptNbr { get; set; }
		public string TaxID00 { get; set; }
		public string TaxID01 { get; set; }
		public string TaxID02 { get; set; }
		public string TaxID03 { get; set; }
		public string TaxLocId { get; set; }
		public string TaxRegNbr { get; set; }
		public string Terms { get; set; }
		public string Territory { get; set; }
		public float TradeDisc { get; set; }
		public string User1 { get; set; }
		public string User2 { get; set; }
		public float User3 { get; set; }
		public float User4 { get; set; }
		public string User5 { get; set; }
		public string User6 { get; set; }
		public DateTime User7 { get; set; }
		public DateTime User8 { get; set; }
		public string Zip { get; set; }
	}

}