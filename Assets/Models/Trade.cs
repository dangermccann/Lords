using System;
using System.Collections.Generic;

namespace Lords {
	public enum Exports {
		Clothes,
		Tools, 
		Manufactured_Goods
	}

	public enum Imports {
		Medicine,
		Spirits,
		Books,
		Incense,
		Jewlery,
		Weapons
	}

	public class Trade {
		public const float ImportItemCount = 6;

		public static Dictionary<Imports, string> ImportLookupTable = new Dictionary<Imports, string> {
			{ Imports.Medicine, PrimativeValues.Health },
			{ Imports.Spirits,  PrimativeValues.Entertainment },
			{ Imports.Books,    PrimativeValues.Literacy },
			{ Imports.Incense,  PrimativeValues.Faith },
			{ Imports.Jewlery,  PrimativeValues.Beauty },
			{ Imports.Weapons,  PrimativeValues.Security },
		};

		public static Dictionary<Exports, BuildingType> ExportLookupTable = new Dictionary<Exports, BuildingType> {
			{ Exports.Clothes, BuildingType.Workshop },
			{ Exports.Tools, BuildingType.Blacksmith },
			{ Exports.Manufactured_Goods, BuildingType.Factory }
		};

		public static float EXPORT_QTY_PER_BUILDING = 100;
	}


}
