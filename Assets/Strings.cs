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

		static Dictionary<TileType, string> tileDescriptions = new Dictionary<TileType, string>() {
			{ TileType.Empty, 		"" },
			{ TileType.Sand, 		String.Format("{0} :food: from Farms\n{1} :security: from Forts", ColorizeYield(-2), ColorizeYield(-2)) },
			{ TileType.Dirt, 		"Ideal for all building types" },
			{ TileType.Snow, 		String.Format("{0} :food: from Farms", ColorizeYield(-2)) },
			{ TileType.Grass, 		"Ideal for all building types" },
			{ TileType.Tundra, 		String.Format("{0} :food: from Farms", ColorizeYield(-1)) },
			{ TileType.Mountains,	"Can not build on Mountains" },
			{ TileType.Water, 		"Can not build on Water" },
			{ TileType.Marsh, 		String.Format("{0} :beauty: from Gardens\n{1} :food: from Farms", ColorizeYield(-1), ColorizeYield(-2)) },
			{ TileType.Forest, 		String.Format("{0} :security: from Forts", ColorizeYield(2)) },
		};

		static Dictionary<BuildingType, string> buildingHelp = new Dictionary<BuildingType, string>() {
			{ BuildingType.Villa, 			"Although Villas provide less :housing: Housing than Slums or Cottages, " +
											"they don't suffer from the negative :security: Security output of those building types. \n\n" +
											"Villas output a small amount of \n" +
											":literacy: Literacy and :beauty: Beauty, and placing multiple Villas nearby increases the \n:beauty: Beauty output by 5% per building." },

			{ BuildingType.Slums, 			"Slums provide more :housing: Housing than any other building type, but they also substantially reduce the " +
											"city's \n:security: Security.\n\n" +
											"Placing multiple Slums near each other increases the negative :security: Security " +
											"output by 5% per building." },

			{ BuildingType.Cottages, 		"Cottages provide a good balance between :housing: Housing and :security: Security.  Unlike Slums, " +
											"Cottages do not increase the negative :security: Security output when there are others nearby." },

			{ BuildingType.School, 			"Schools increase the city's :literacy: Literacy output, which is important for improving the " +
											"city's Prosperity score.\n\n" +
											"Placing multiple schools near each other will reduce thier :literacy: Literacy output.\n\n" +
											"Schools work well on all tile types." },

			{ BuildingType.Vegetable_Farm,	"Vegetable Farms output :food: Food, which is necessary for increasing a city's Population.\n\n" +
											"Placing multiple Vegetable Farms nearby increases their :food: Food output by 15%.\n\n" +
											"Vegetable Farms output less :food: Food on Snow, Sand, Tundra and Marsh." },

			{ BuildingType.Wheat_Farm, 		"Wheat Farms output :food: Food, which is necessary for increasing a city's Population.\n\n" +
											"Placing multiple Wheat Farms nearby increases their :food: Food output by 15%.\n\n" +
											"Wheat Farms output less :food: Food on Snow, Sand, Tundra and Marsh." },

			{ BuildingType.Tavern, 			"Taverns increase a city's \n:entertainment: Entertainment, an important component of its Happiness score.\n\n" +
											"Nearby Forts will reduce the Tavern's negative :security: Security output.\n\n" +
											"Placing multiple Taverns near each other reduces their effectiveness." },

			{ BuildingType.Amphitheater, 	"The Amphitheater outputs more \n:entertainment: Entertainment than the Tavern, but also reduces " +
											"the city's :productivity: Productivity.\n\n" +
											"Nearby Taverns increase the Amphitheater's effectiveness, increasing its :entertainment: " +
											"Entertainment output by 20%.\n\n" +
											"Amphitheaters with others nearby are less effective." },

			{ BuildingType.Trading_Post, 	"The Trading Post improves the city's \n:productivity: Productivity output, at the expense of :beauty: Beauty.\n\n" +
											"Nearby Workshops increase the Trading Post's effectiveness by 20%.\n\n" +
											"Trading Posts with others nearby are less effective." },

			{ BuildingType.Fort, 			"Forts provide :security: Security to the city.\n\n" +
											"When placed on Forest tiles, they are 30% more effective.  Sand tiles reduce their effectiveness by 10%.\n\n" +
											"Forts are less effective if other Forts are nearby." },

			{ BuildingType.Hospital, 		"The Hospital is the only building that increases the city's :health: Health output, a component of the city's Happiness score.\n\n" +
											"Hospitals work equally well on any tile, but are less effective if other Hospitals are nearby." },

			{ BuildingType.Garden, 			"Gardens improve the city's :beauty: Beauty, thereby promoting the Culture score of the city.\n\n" +
											"They are 35% less effective if a Workshop is nearby, and 30% less effective if built on Marsh tiles." },

			{ BuildingType.Church, 			"Chrches promote a city's :faith: Faith output, which adds to the Culture score of the city.\n\n" +
											"They are 20% more effective with a Garden nearby, and 30% less effective with another Church nearby." },

			{ BuildingType.Workshop, 		"The Workshop yields the same \n:productivity: Productivity output as the Trading Post, but has different " +
											"negative side effects.\n\n" +
											"A nearby Hospital reduces the negative :health: Health output by 35%, and other Workshops nearby reduce \n" +
											":productivity: Productivity output by 30%." },
		};

		public static string BuildingTitle(BuildingType type) {
			return type.ToString().Replace('_', ' ');
		}

		public static string TileTitle(TileType type) {
			return type.ToString();
		}

		public static string TileDescription(TileType type) {
			return tileDescriptions[type];
		}

		public static string BuildingCost(BuildingType type) {
			return Building.RequiredFunds[type] + " :gold:   " + Building.RequiredMaterials[type] + " :rawmaterials:";
		}

		public static string BuildingYield(BuildingType type) {
			return FormatPrimative(Building.Yields[type]);
		}

		public static string BuildingYieldLong(BuildingType type) {
			return FormatPrimative(Building.Yields[type], true, "\n");
		}

		public static string FormatPrimative(Primatives p, bool longNames = false, string delimiter = "  ") {
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
			string result = "";
			foreach(KeyValuePair<string, float> pair in yields) {
				result += Delimt(result, delimiter) + ColorizeYield(pair.Value) + " " + YieldIcon(pair.Key);
				if(longNames) {
					result += " " + pair.Key;
				}
			}

			return result;
		}

		public static string BuildingModifier(Tile tile) {
			string result = TileTitle(tile.Type);

			if(tile.Building != null) {
				Dictionary<TileType, Primatives> modifiers = Building.Modifiers[tile.Building.Type];
				if(modifiers.ContainsKey(tile.Type)) {
					result += " (" + FormatPrimative(modifiers[tile.Type]) + ")";
				}
			}
			return result;
		}

		public static string BuildingNotes(City city, Building building) {
			if(building.Type == BuildingType.Villa || 
			   building.Type == BuildingType.Cottages || 
			   building.Type == BuildingType.Slums) {
				return "Population: " + Mathf.Floor(city.FoodLevel() * city.BuildingEffectiveness(building) * Building.Yields[building.Type].Housing);
			}
			else {
				// TODO: figure out how to add in modifiers
				return FormatBuildingEffectiveness(city.BuildingEffectiveness(building));
			}


		}

		public static string BuildingHelp(BuildingType type){
			return buildingHelp[type];
		}

		public static string FormatBuildingEffectiveness(float value) {
			return (Mathf.Round(value * 100)) + "% Effective";
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

		public static string VictoryMessage(Level level) {
			return level.victoryMessage;
		}

		public static string VictoryConditions(Level level) {
			return level.victoryConditions.Population + "\n"
					+ level.victoryConditions.Happiness + "\n"
					+ level.victoryConditions.Prosperity + "\n"
					+ level.victoryConditions.Culture;
		}

		public static string LevelDescription(Level level) {
			return level.description;
		}

		static string PlusMinus(float value) {
			if(value > 0) {
				return "+";
			}
			else {
				return "";
			}
		}

		static string Delimt(string str, string delimiter) {
			if(str.Length > 0) {
				return delimiter;
			}
			return "";
		}

	}
}
