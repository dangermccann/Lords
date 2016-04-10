using System;
using System.Collections.Generic;
using UnityEngine;

namespace Lords {
	public class Levels {
		static Dictionary<Exports, float> EXPORT_PRICES_MEDIUM = new Dictionary<Exports, float>() {
			{ Exports.Clothes,            8 },
			{ Exports.Tools,              8 },
			{ Exports.Manufactured_Goods, 8 }
		};
		
		static Dictionary<Exports, float> EXPORT_PRICES_HARD = new Dictionary<Exports, float>() {
			{ Exports.Clothes,            6 },
			{ Exports.Tools,              6 },
			{ Exports.Manufactured_Goods, 6 }
		};


		public static Level Tutorial = new Level() {
			name = "Tutorial",
			description = "My good fellow, for this first task you are to create a town of modest proportion.\nTime limit: {0}",
			victoryMessage = "You have achieved a satisfactory rating in the eyes of The Crown.",
			victoryConditions = new Aggregates() { 
				Population = 1000,
				Happiness = 5,
				Prosperity = 5,
				Culture = 0,
			},
			mapConfiguration = Maps.Tutorial,
			mapLocation = new Vector2(200, 150),
			illustration = LevelIllustration.House,
			maxElapsedTime = 180 / Game.GameSpeed,
			promotesTo = Ranks.Gentleman
		};

		public static Level PortHenry = new Level() {
			name = "Port Henry",
			description = "Your next charge is a somewhat larger town with simple but balanced qualities.\nTime limit: {0}",
			victoryMessage = "Congratulations, your accomplishment has piqued the interest of the King.",
			victoryConditions = new Aggregates() { 
				Population = 2000,
				Happiness = 15,
				Prosperity = 15,
				Culture = 10,
			},
			mapConfiguration = Maps.Standard,
			mapLocation = new Vector2(216, 225),
			illustration = LevelIllustration.Fish,
			maxElapsedTime = 120 / Game.GameSpeed,
			prerequisites = new Level[1] { Levels.Tutorial }
		};

		public static Level Galloway = new Level() {
			name = "Galloway",
			description = "This next undertaking will require careful attention towards developing the city's Prosperity.\nTime limit: {0}",
			victoryMessage = "The King sends his sincere gratitude for your dedicated service.",
			victoryConditions = new Aggregates() { 
				Population = 2000,
				Happiness = 10,
				Prosperity = 30,
				Culture = 10,
			},
			mapConfiguration = Maps.MountainsForest,
			mapLocation = new Vector2(369, 227),
			illustration = LevelIllustration.Scales,
			maxElapsedTime = 120 / Game.GameSpeed,
			promotesTo = Ranks.Esquire,
			prerequisites = new Level[1] { Levels.PortHenry }
		};

		public static Level Greencastle = new Level() {
			name = "Greencastle",
			description = "The Crown requires a moderately sized city that must be established rather quickly.\nTime limit: {0}",
			victoryMessage = "Nicely done.  You appear to be ready for the more difficult challenges ahead.",
			victoryConditions = new Aggregates() { 
				Population = 2500,
				Happiness = 25,
				Prosperity = 20,
				Culture = 20,
			},
			mapConfiguration = Maps.Greencastle,
			mapLocation = new Vector2(50, 306),
			illustration = LevelIllustration.Clock,
			maxElapsedTime = 75 / Game.GameSpeed,
			prerequisites = new Level[1] { Levels.Galloway }
		};

		public static Level CraigBay = new Level() {
			name = "Craig Bay",
			description = "The Crown now requires a large city to be created within a peculiar landscape.\nTime limit: {0}",
			victoryMessage = "Well done, Esquire.  The King sends his appreciation for your diligence.",
			victoryConditions = new Aggregates() { 
				Population = 4000,
				Happiness = 25,
				Prosperity = 25,
				Culture = 35,
			},
			mapConfiguration = Maps.MarshWater,
			mapLocation = new Vector2(283.8f, 35.8f),
			illustration = LevelIllustration.Sun,
			maxElapsedTime = 120 / Game.GameSpeed,
			prerequisites = new Level[1] { Levels.Galloway }
		};

		public static Level Aberdeen = new Level() {
			name = "Aberdeen",
			description = "You must next build a prosperous city in the mountainous regions of Aberdeen.\nTime limit: {0}",
			victoryMessage = "Very impressive indeed.  Your upcoming promotion is well deserved.",
			victoryConditions = new Aggregates() { 
				Population = 2800,
				Happiness = 35,
				Prosperity = 45,
				Culture = 30,
			},
			mapConfiguration = Maps.Mountains,
			mapLocation = new Vector2(364, 26),
			illustration = LevelIllustration.Grapes,
			maxElapsedTime = 120 / Game.GameSpeed,
			promotesTo = Ranks.Knight,
			prerequisites = new Level[2] { Levels.CraigBay, Levels.Greencastle },
			exportPrices = EXPORT_PRICES_MEDIUM
		};

		public static Level Crosswell = new Level() {
			name = "Crosswell",
			description = "This next city must be assembled in short order.  You must act quickly if you wish to meet your objective.\nTime limit: {0}",
			victoryMessage = "Excellent work, Sir.  You've met this challenge with prestige and valor.",
			victoryConditions = new Aggregates() { 
				Population = 3000,
				Happiness = 35,
				Prosperity = 25,
				Culture = 35,
			},
			mapConfiguration = Maps.SmallerSand,
			mapLocation = new Vector2(295, 331),
			illustration = LevelIllustration.Hourglass,
			maxElapsedTime = 65 / Game.GameSpeed,
			prerequisites = new Level[1] { Levels.Aberdeen },
			exportPrices = EXPORT_PRICES_MEDIUM
		};

		public static Level Southgate = new Level() {
			name = "Southgate",
			description = "My good Sir, His Majesty requires that Southgate be a culturally advanced city of moderate size.\nTime limit: {0}",
			victoryMessage = "Your achievements in cultural progress have been acknowledged by the Crown.",
			victoryConditions = new Aggregates() { 
				Population = 3500,
				Happiness = 35,
				Prosperity = 35,
				Culture = 50,
			},
			mapConfiguration = Maps.Tundra,
			mapLocation = new Vector2(432, 105),
			illustration = LevelIllustration.Flowers,
			maxElapsedTime = 75 / Game.GameSpeed,
			prerequisites = new Level[1] { Levels.Crosswell },
			exportPrices = EXPORT_PRICES_MEDIUM
		};

		public static Level Mayfield = new Level() {
			name = "Mayfield",
			description = "The challenge at Mayfield involves careful planning to arrive at a large population in a small area.\nTime limit: {0}",
			victoryMessage = "Outstanding accomplishment, Sir.  The Crown sends its appreciation.",
			victoryConditions = new Aggregates() { 
				Population = 4000,
				Happiness = 45,
				Prosperity = 45,
				Culture = 45,
			},
			mapConfiguration = Maps.Smaller,
			mapLocation = new Vector2(497, 143),
			illustration = LevelIllustration.Drinker,
			maxElapsedTime = 90 / Game.GameSpeed,
			prerequisites = new Level[1] { Levels.Crosswell },
			exportPrices = EXPORT_PRICES_MEDIUM
		};

		public static Level LittleAsh = new Level() {
			name = "Little Ash",
			description = "Trade will become much more difficult when building this small but illustrious city.\nTime limit: {0}",
			victoryMessage = "A job well done.  You will be receiving another well deserved promotion.",
			victoryConditions = new Aggregates() { 
				Population = 1800,
				Happiness = 60,
				Prosperity = 55,
				Culture = 65,
			},
			mapConfiguration = Maps.SmallLake,
			mapLocation = new Vector2(65, 106),
			illustration = LevelIllustration.Pear,
			maxElapsedTime = 120 / Game.GameSpeed,
			promotesTo = Ranks.Baronet,
			prerequisites = new Level[2] { Levels.Mayfield, Levels.Southgate },
			exportPrices = EXPORT_PRICES_HARD
		};

		public static Level Easton = new Level() {
			name = "Easton",
			description = "The King now requires an enormous city, perhaps your most challenging charge thus far.\nTime limit: {0}",
			victoryMessage = "This splendid achievement is yet another example of your undying dedication.",
			victoryConditions = new Aggregates() { 
				Population = 8000,
				Happiness = 50,
				Prosperity = 50,
				Culture = 50,
			},
			mapConfiguration = Maps.LargeEasy,
			mapLocation = new Vector2(681, 271),
			illustration = LevelIllustration.Factory,
			maxElapsedTime = 190 / Game.GameSpeed,
			prerequisites = new Level[1] { Levels.LittleAsh },
			exportPrices = EXPORT_PRICES_HARD
		};

		public static Level Welburn = new Level() {
			name = "Welburn",
			description = "With limited terrain available for building, you must establish a city that reaches a high Happiness score.\nTime limit: {0}",
			victoryMessage = "You have demonstrated yet again that you are a wise and capable mayor.",
			victoryConditions = new Aggregates() { 
				Population = 1500,
				Happiness = 65,
				Prosperity = 45,
				Culture = 45,
			},
			mapConfiguration = Maps.TinyRiver,
			mapLocation = new Vector2(497, 269),
			illustration = LevelIllustration.Drinks,
			maxElapsedTime = 90 / Game.GameSpeed,
			prerequisites = new Level[1] { Levels.LittleAsh },
			exportPrices = EXPORT_PRICES_HARD
		};

		public static Level Dunham = new Level() {
			name = "Dunham",
			description = "To reach the rank of Baron you must erect an impressive city on the most difficult of terrain.\nTime limit: {0}",
			victoryMessage = "Congratulations on proving your worthiness to The Crown once again.",
			victoryConditions = new Aggregates() { 
				Population = 2000,
				Happiness = 50,
				Prosperity = 50,
				Culture = 55,
			},
			mapConfiguration = Maps.TinyHard,
			mapLocation = new Vector2(638, 143),
			illustration = LevelIllustration.Paddles,
			maxElapsedTime = 100 / Game.GameSpeed,
			promotesTo = Ranks.Baron,
			prerequisites = new Level[2] { Levels.Welburn, Levels.Easton },
			exportPrices = EXPORT_PRICES_HARD
		};

		public static Level OldMilddleton = new Level() {
			name = "Old Milddleton",
			description = "The King requires a city of epic proportions in the desert of Old Middleton.\nTime limit: {0}",
			victoryMessage = "Tremendous accopmlishment, my Lord.  You are well prepared for your final challenge.",
			victoryConditions = new Aggregates() { 
				Population = 7000,
				Happiness = 60,
				Prosperity = 60,
				Culture = 60,
			},
			mapConfiguration = Maps.HugeSand,
			mapLocation = new Vector2(562, 20),
			illustration = LevelIllustration.Sun,
			maxElapsedTime = 150 / Game.GameSpeed,
			prerequisites = new Level[1] { Levels.Dunham },
			exportPrices = EXPORT_PRICES_HARD
		};

		public static Level Normantown = new Level() {
			name = "Normantown",
			description = "You must complete this final city in frigid Normantown to earn the title of Lord Mayor.\nTime limit: {0}",
			victoryMessage = "You have done it!  Congratulations Lord Mayor!",
			victoryConditions = new Aggregates() { 
				Population = 5000,
				Happiness = 70,
				Prosperity = 70,
				Culture = 70,
			},
			mapConfiguration = Maps.MediumCold,
			mapLocation = new Vector2(602, 216),
			illustration = LevelIllustration.CoatOfArms,
			maxElapsedTime = 150 / Game.GameSpeed,
			promotesTo = Ranks.Lord_Mayor,
			prerequisites = new Level[1] { Levels.OldMilddleton },
			exportPrices = EXPORT_PRICES_HARD
		};

		public static List<Level> All = new List<Level> () {
			Tutorial,
			PortHenry,
			Galloway,
			Greencastle,
			CraigBay,
			Aberdeen,
			Crosswell,
			Southgate,
			Mayfield,
			LittleAsh,
			Easton,
			Welburn,
			Dunham,
			OldMilddleton,
			Normantown
		};

		public static Level GetLevel(string name) {
			foreach(Level level in All) {
				if(level.name == name) {
					return level;
				}
			}

			return null;
		}
	}
}
