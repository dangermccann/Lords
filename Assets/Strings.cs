using System;

namespace Lords {
	public class Strings {

		public static string BuildingTitle(BuildingType type) {
			return type.ToString().Replace('_', ' ');
		}

		public static string BuildingCost(BuildingType type) {
			return Building.RequiredFunds[type] + " :gold:   " + Building.RequiredMaterials[type] + " :rawmaterials:";
		}

		public static string BuildingYield(BuildingType type) {
			string result = "";
			Primatives p = Building.Yields[type];

			if(p.Beauty != 0) result += Delimt(result) + PlusMinus(p.Beauty) + p.Beauty + " :beauty:";
			if(p.Entertainment != 0) result += Delimt(result) + PlusMinus(p.Entertainment) + p.Entertainment + " :entertainment:";
			if(p.Faith != 0) result += Delimt(result) + PlusMinus(p.Faith) + p.Faith + " :faith:";
			if(p.Food != 0) result += Delimt(result) + PlusMinus(p.Food) + p.Food + " :food:";
			if(p.Health != 0) result += Delimt(result) + PlusMinus(p.Health) + p.Health + " :health:";
			if(p.Housing != 0) result += Delimt(result) + PlusMinus(p.Housing) + p.Housing + " :housing:";
			if(p.Literacy != 0) result += Delimt(result) + PlusMinus(p.Literacy) + p.Literacy + " :literacy:";
			if(p.Productivity != 0) result += Delimt(result) + PlusMinus(p.Productivity) + p.Productivity + " :productivity:";
			if(p.Security != 0) result += Delimt(result) + PlusMinus(p.Security) + p.Security + " :security:";

			return result;
		}

		static string PlusMinus(float value) {
			if(value > 0) {
				return "+";
			}
			else {
				return "";
			}
		}

		static string Delimt(string str) {
			if(str.Length > 0) {
				return "  ";
			}
			return "";
		}

	}
}
