using System;

namespace XamFormsCardIO
{
		/// <summary>
		/// Class holding configuration options for the Card.IO plugin
		/// </summary>
		public class CardIOConfig
		{
			public bool RequireExpiry { get; set; }
			public bool RequireCvv { get; set; }
			public bool RequirePostalCode { get; set; }
			public bool ShowPaypalLogo { get; set; }
			public bool HideCardIOLogo { get; set; }
    		public bool CollectCardholderName { get; set; }
			public string ScanInstructions { get; set; }
			public string Localization { get; set; }

			public CardIOConfig()
			{
				this.ShowPaypalLogo = false;
				this.RequireExpiry = true;
				this.RequireCvv = true;
				this.RequirePostalCode = false;
			this.HideCardIOLogo = true;
			this.CollectCardholderName = true;
					}

		}
}

