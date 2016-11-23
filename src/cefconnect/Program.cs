using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using ServiceStack;
using ServiceStack.Text;

namespace cefconnect
{
	class Program
	{
		static void Main(string[] args)
		{

			/*
			//var baseUrl = "http://www.cefconnect.com/api/v3/PriceHistory/{ticker}/30Y/{MM}-{dd}-{yyyy}";
			var baseUrl = "http://www.cefconnect.com/api/v3";
			IJsonServiceClient client = new JsonServiceClient(baseUrl);

			var request = new GetCefConnect { ticker = "GCH" };

			var response = client.Get(request);
			*/


			Harvest(GetNonUsEquityTickers(), "non-us-equity");
			Harvest(GetReitTickers(), "reit");
			Harvest(GetTaxableIncomeTickers(), "taxableincome");
			Harvest(GetUsEquityTickers(), "us-equity");
			Harvest(GetTaxFreeIncomeTickers(), "tax-free");

		}


		public static void Harvest(List<string> tickers, string filename)
		{
			var targetFile = $@"c:\temp\{filename}.csv";

			var baseUrl = "http://www.cefconnect.com/api/v3";
			IJsonServiceClient client = new JsonServiceClient(baseUrl);


			var results = new List<DataRow>();

			foreach (var ticker in tickers)
			{
				var year = DateTime.Now.Year - 5;

				var resultsFound = true;
				while (resultsFound)
				{
					var request = new GetCefConnect { ticker = ticker, yyyy = year.ToString() };
					var url = request.ToGetUrl();
					var response = client.Get(request);

					resultsFound = (response.Data.IsNullOrEmpty() == false);

					if (resultsFound)
					{
						var dataRows = response.Data.ConvertAll(x => x.ConvertTo<DataRow>());
						dataRows.ForEach(dr => dr.Ticker = ticker);
						results.AddRange(dataRows);

						year = year - 5;
					}
				}

				Console.WriteLine($"Parsed {ticker}");
			}

			
			using (var sw = new StreamWriter(targetFile))
			{
				//var reader = new CsvReader(sr);
				var writer = new CsvHelper.CsvWriter(sw);

				//CSVReader will now read the whole file into an enumerable
				//IEnumerable records = reader.GetRecords<DataRecord>().ToList();

				//Write the entire contents of the CSV file into another
				writer.WriteRecords(results);

				Console.WriteLine($"Wrote out {targetFile}");
			}
			
			
		}


		public static List<string> GetTaxFreeIncomeTickers()
		{
			return new List<string>
			{
				"AKP",
"AFB",
"BJZ",
"BFZ",
"BFO",
"BKN",
"BTA",
"BZM",
"BPK",
"BAF",
"BYM",
"MUI",
"MNE",
"BTT",
"MUA",
"MEN",
"MUC",
"MHD",
"MUH",
"MFL",
"MUJ",
"MHN",
"MUE",
"MUS",
"MVF",
"MVT",
"MYD",
"MZA",
"MYC",
"MCA",
"MYF",
"MIY",
"MYJ",
"MYN",
"MPA",
"MYI",
"MQY",
"MQT",
"BKK",
"BBK",
"BFK",
"BLE",
"BBF",
"MFT",
"BLJ",
"BNJ",
"BLH",
"BSE",
"BQH",
"BFY",
"BNY",
"BSD",
"BHV",
"DTF",
"VFL",
"VMM",
"KTF",
"KSM",
"DMB",
"DMF",
"DSM",
"LEO",
"EIA",
"EVM",
"CEV",
"MAB",
"MMV",
"MIW",
"EMI",
"ETX",
"EIM",
"EIV",
"EVN",
"EMJ",
"EVJ",
"NYH",
"ENX",
"EVY",
"EOT",
"EIO",
"EVO",
"EIP",
"EVP",
"FPT",
"FMN",
"VKI",
"VCV",
"OIA",
"VGM",
"VMO",
"VKQ",
"VPV",
"IQI",
"VTN",
"IIM",
"CCA",
"CXE",
"CMU",
"CXH",
"MFM",
"MMD",
"MZF",
"MHE",
"NBW",
"NBH",
"NBO",
"NUW",
"NEA",
"NAZ",
"NKX",
"NAC",
"NCA",
"NCB",
"NXC",
"NTC",
"NVG",
"NZF",
"NEV",
"NKG",
"NIQ",
"NID",
"NMT",
"NMY",
"NUM",
"NOM",
"NMS",
"NHA",
"NMZ",
"NMI",
"NUV",
"NNC",
"NXJ",
"NJV",
"NRK",
"NAN",
"NNY",
"NYV",
"NXN",
"NUO",
"NQP",
"NPN",
"NAD",
"NIM",
"NXP",
"NXQ",
"NXR",
"NTX",
"NPV",
"PCQ",
"PCK",
"PZC",
"PMF",
"PML",
"PMX",
"PNF",
"PNI",
"PYN",
"MAV",
"MHI",
"PMM",
"PMO",
"SBI",
"MMU",
"MTT",
"MHF",
"MNP",

			};
		}

		public static List<string> GetTaxableIncomeTickers()
		{
			return new List<string>
			{
				"NCV",
"NCZ",
"AGC",
"AVK",
"AWF",
"ACV",
"AFT",
"AIF",
"ARDC",
"ACP",
"BCV",
"MCI",
"BGH",
"MPV",
"BHK",
"HYT",
"BTZ",
"DSU",
"BHL",
"EGF",
"FRA",
"BGT",
"BKT",
"BLW",
"BIT",
"BBN",
"BSL",
"BGX",
"BGB",
"HHY",
"BOI",
"HTR",
"CHY",
"CHI",
"CCD",
"LDP",
"PSF",
"CIK",
"DHY",
"CSI",
"DHG",
"DSL",
"DBL",
"DHF",
"DUC",
"EFT",
"EFF",
"EHT",
"EVV",
"EFR",
"EVF",
"EVG",
"EXD",
"ECC",
"ECF",
"FSD",
"FPF",
"FMY",
"FCT",
"FHY",
"DFP",
"PFO",
"FFC",
"PFD",
"FLC",
"FTF",
"FT",
"GGM",
"GBAB",
"VBF",
"VLT",
"VVR",
"VTA",
"IVH",
"JHS",
"JHI",
"HPI",
"HPF",
"HPS",
"PDT",
"KIO",
"MGF",
"CIF",
"ICB",
"NHS",
"HYB",
"NBB",
"NBD",
"JQC",
"JRO",
"JFR",
"JHB",
"JHY",
"JHA",
"JHD",
"JMT",
"JLS",
"JMM",
"JPC",
"JPS",
"JPI",
"NSL",
"JSD",
"OXLC",
"PCM",
"PTY",
"PCN",
"PCI",
"PDI",
"PGP",
"PHK",
"PKO",
"PFL",
"PFN",
"PHF",
"HNW",
"PHD",
"PHT",
"GHY",
"ISD",
"PCF",
"PIM",
"OPP",
"TSI",
"TSLF",
"VGI",
"PPR",
"EAD",
"ERC",
"TLI",
"GDO",
"HIX",
"HIO",
"HYI",
"IGI",
"PAI",
"DMO",
"WEA",
"GFY",
"WIW",
"WIA",

			};
		}


		public static List<string> GetNonUsEquityTickers()
		{
			return new List<string>
			{
				"FAX",
				"IAF",
				"CH",
				"ABE",
				"FCO",
				"GCH",
				"IF",
				"ISL",
				"JEQ",
				"LAQ",
				"SGF",
				"LCM",
				"AGD",
				"AOD",
				"APB",
				"GRR",
				"BST",
				"INF",
				"CHW",
				"CGO",
				"CEE",
				"CHN",
				"GLV",
				"GLQ",
				"GLO",
				"DEX",
				"VCF",
				"LBF",
				"KMM",
				"KST",
				"EGIF",
				"EEA",
				"FDEU",
				"FEO",
				"FAM",
				"GDL",
				"GGZ",
				"GGT",
				"CUBA",
				"IFN",
				"HEQ",
				"JFC",
				"JOF",
				"KEF",
				"KF",
				"SCD",
				"LDF",
				"LGI",
				"LOR",
				"BWG",
				"MCR",
				"MIN",
				"MMT",
				"APF",
				"CAF",
				"MSF",
				"MSD",
				"EDD",
				"IIF",
				"MXE",
				"MXF",
				"GF",
				"IRL",
				"JDD",
				"JPW",
				"JGH",
				"RCS",
				"PPT",
				"RGT",
				"EDF",
				"EDI",
				"SWZ",
				"TWN",
				"TDF",
				"EMF",
				"TEI",
				"GIM",
				"TTF",
				"TKF",
				"DCA",
				"IAE",
				"IHD",
				"IDE",
				"IID",
				"EOD",
				"ESD",
				"EMD",
				"EHI",
				"SBW",

			};
		}


		public static List<string> GetReitTickers()
		{
			return new List<string>
			{
				"AWP",
"IGR",
"RQI",
"RNP",
"RFI",
"DRA",
"NRO",
"JRI",
"JRS",
"PGZ",
"RIF",

			};
		}

		public static List<string> GetUsEquityTickers()
		{
			return new List<string>
			{
				"NIE",
"ASA",
"ADX",
"PEO",
"NFJ",
"BGR",
"CII",
"BDJ",
"BOE",
"BME",
"BGY",
"BCX",
"BUI",
"BIF",
"CSQ",
"CEN",
"CEF",
"CET",
"CBA",
"CEM",
"EMO",
"CTR",
"FOF",
"INB",
"UTF",
"MIE",
"STK",
"CLM",
"CRF",
"SRF",
"SRV",
"SZC",
"DNP",
"DDF",
"DNI",
"DPG",
"DSE",
"EOI",
"EOS",
"ETJ",
"ETO",
"ETG",
"EVT",
"ETB",
"ETV",
"ETY",
"ETW",
"EXG",
"GRF",
"FMO",
"FEN",
"FIF",
"FFA",
"FEI",
"FPL",
"FGB",
"FXBY",
"GGN",
"GNT",
"GCV",
"GDV",
"GAB",
"GLU",
"GGO",
"GRX",
"GUT",
"GAM",
"GMZ",
"GER",
"GEQ",
"GGE",
"GPM",
"GOF",
"BTO",
"HTY",
"HTD",
"KED",
"KYE",
"KYN",
"KMF",
"USA",
"ASG",
"MFV",
"MFD",
"MGU",
"MCN",
"MSP",
"HIE",
"NML",
"NHF",
"JMLP",
"JCE",
"DIAX",
"JMF",
"QQQX",
"BXMX",
"SPXX",
"JTD",
"JTA",
"RCG",
"UTG",
"RIV",
"RMT",
"RVT",
"SMM",
"SOR",
"SPE",
"FUND",
"PHYS",
"SPPP",
"PSLV",
"HQH",
"THQ",
"HQL",
"THW",
"NDP",
"TYG",
"NTG",
"TTP",
"TPZ",
"TY",
"ZTR",
"IGD",
"IGA",
"IRR",
"ERH",
"ZF",

			};
		}
	}


	

	[Route("/PriceHistory/{ticker}/5Y/{stem}", "GET")]
	public class GetCefConnect : IReturn<GetCefConnectResponse>
	{
		public string ticker { get; set; }

		public string yyyy { get; set; }

		public string stem { get { return "10-31-" + yyyy; } }

		//public string Month => DateTime.Now.AddMonths(-1).ToString("MM");
	}


	public class GetCefConnectResponse
	{
		public List<DataPoint> Data { get; set; } = new List<DataPoint>();
	}

	public class DataRow : DataPoint
	{
		public string Ticker { get; set; }
	}

	public class DataPoint
	{
		/*
		 * "NAVData":6.82000,
         "DiscountData":-18.62,
         "Data":5.55000,
         "DataDate":"2016-04-15T00:00:00",
         "DataDateJs":"2016/04/15",
         "DataDateDisplay":"4/15/2016"
		 */

		public float NAVData { get; set; }

		public float DiscountData { get; set; }

		public float Data { get; set; }
		
		public DateTime DataDateJs { get; set; } 
	}



}
