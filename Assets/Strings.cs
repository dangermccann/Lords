using System;

namespace Lords {
	public class Strings {

		public static string BuildingTitle(BuildingType type) {
			return type.ToString().Replace('_', ' ');
		}

		public static string BuildingCost(BuildingType type) {
			return "Cost: " + Building.RequiredFunds[type] + ", " + Building.RequiredMaterials[type] + " mat";
		}

		public static string BuildingYield(BuildingType type) {
			string result = "";
			Primatives p = Building.Yields[type];

			if(p.Beauty != 0) result += Delimt(result) + PlusMinus(p.Beauty) + p.Beauty + " Beauty";
			if(p.Entertainment != 0) result += Delimt(result) + PlusMinus(p.Entertainment) + p.Entertainment + " Entertainment";
			if(p.Faith != 0) result += Delimt(result) + PlusMinus(p.Faith) + p.Faith + " Faith";
			if(p.Food != 0) result += Delimt(result) + PlusMinus(p.Food) + p.Food + " Food";
			if(p.Health != 0) result += Delimt(result) + PlusMinus(p.Health) + p.Health + " Health";
			if(p.Housing != 0) result += Delimt(result) + PlusMinus(p.Housing) + p.Housing + " Housing";
			if(p.Literacy != 0) result += Delimt(result) + PlusMinus(p.Literacy) + p.Literacy + " Literacy";
			if(p.Productivity != 0) result += Delimt(result) + PlusMinus(p.Productivity) + p.Productivity + " Productivity";
			if(p.Security != 0) result += Delimt(result) + PlusMinus(p.Security) + p.Security + " Security";

			return result;
		}

		static string PlusMinus(float value) {
			if(value > 0) {
				return "+";
			}
			else {
				return "-";
			}
		}

		static string Delimt(string str) {
			if(str.Length > 0) {
				return ", ";
			}
			return "";
		}

	}
}
