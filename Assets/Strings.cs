using System;
using System.Reflection;
using System.Collections.Generic;
using UnityEngine;

namespace Lords {
	public enum Colors {
		PositiveYield,
		NegativeYield,
		Requirement
	}

	public class Strings {

		static Dictionary<Colors, string> colors = new Dictionary<Colors, string>() {
			{ Colors.PositiveYield, "189b0a" },
			{ Colors.NegativeYield, "c02f2f" },
			{ Colors.Requirement, "823111" },
		};

		public static string BuildingTitle(BuildingType type) {
			return type.ToString().Replace('_', ' ');
		}

		public static string BuildingCost(BuildingType type) {
			return Building.RequiredFunds[type] + " :gold:   " + Building.RequiredMaterials[type] + " :rawmaterials:";
		}

		public static string BuildingYield(BuildingType type) {
			string result = "";
			Primatives p = Building.Yields[type];

			// Build a list of all non-zero yields so we can sort them
			List<KeyValuePair<string, float>> yields = new List<KeyValuePair<string, float>>();
			foreach(string property in Primatives.Properties()) {
				float value = p[property];
				if(value != 0) {
					yields.Add(new KeyValuePair<string, float>(property, value));
				}
			}

			// Sort descending 
			yields.Sort((firstPair, nextPair) => {
				return nextPair.Value.CompareTo(firstPair.Value);
			});

			// build the result string using the sorted, non-zero yield values
			foreach(KeyValuePair<string, float> pair in yields) {
				result += Delimt(result) + ColorizeYield(pair.Value) + " " + YieldIcon(pair.Key);
			}

			return result;
		}

		public static string CityInventory(City city) {
			return String.Format ("{0} :gold: {1} :rawmaterials:", Mathf.Floor(city.Funds), Mathf.Floor(city.RawMaterials));
		}

		public static string CanNotBuildMessage(City city, BuildingType type) {
			string message = "";
			if(city.Funds < Building.RequiredFunds[type]) {
				message = String.Format("{0} :gold:", Building.RequiredFunds[type]);;
			}
			if(city.RawMaterials < Building.RequiredMaterials[type]) {
				message = String.Format("{0} :rawmaterials:", Building.RequiredMaterials[type]);;
			}
			if(city.Score.Population < Building.PopulationMinimums[type]) {
				message = String.Format("{0} Population", Building.PopulationMinimums[type]);;
			}

			return Colorize(message, Colors.Requirement);;
		}

		public static string YieldIcon(string property) {
			return ":" + property.ToLower() + ":";
		}

		public static string Colorize(string str, Colors color) {
			return String.Format("[{0}]{1}[-]", colors[color], str);
		}

		public static string ColorizeYield(float value) {
			return String.Format("[{0}]{1}{2}[-]", value > 0 ? colors[Colors.PositiveYield] : colors[Colors.NegativeYield], 
			                     PlusMinus(value), value);
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
