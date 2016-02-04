using System;
using System.Collections.Generic;
using UnityEngine;

namespace Lords {
	public class Levels {
		public static Level Tutorial = new Level() {
			name = "Tutorial",
			description = "My Lord, for this first task you are to create a town of modest proportion.\nTime limit: {0}",
			victoryMessage = "You have achieved a satisfactory rating in the eyes of The Crown.",
			victoryConditions = new Aggregates() { 
				Population = 1000,
				Happiness = 5,
				Prosperity = 5,
				Culture = 0,
			},
			mapConfiguration = Maps.Basic,
			initialFunds = 1000,
			initialRawMaterials = 1000,
			mapLocation = new Vector2(200, 150),
			illustration = LevelIllustration.House,
			maxElapsedTime = 2 * 365 / Game.GameSpeed,
		};

		public static Level PortHenry = new Level() {
			name = "Port Henry",
			description = "Your Lordship's next charge is a moderately larger town with simple but balanced qualities.\nTime limit: {0}",
			victoryMessage = "You have achieved a satisfactory rating in the eyes of The Crown.",
			victoryConditions = new Aggregates() { 
				Population = 2000,
				Happiness = 15,
				Prosperity = 15,
				Culture = 10,
			},
			mapConfiguration = Maps.Wet,
			initialFunds = 1000,
			initialRawMaterials = 1000,
			mapLocation = new Vector2(216, 225),
			illustration = LevelIllustration.Fish,
			maxElapsedTime = 4 * 365 / Game.GameSpeed,
		};

		public static Level Lewes = new Level() {
			name = "Lewes",
			description = "This next undertaking will require careful attention towards developing the city's Prosperity.\nTime limit: {0}",
			victoryMessage = "You have achieved a satisfactory rating in the eyes of The Crown.",
			victoryConditions = new Aggregates() { 
				Population = 4000,
				Happiness = 10,
				Prosperity = 25,
				Culture = 15,
			},
			mapConfiguration = Maps.Mountains,
			initialFunds = 1000,
			initialRawMaterials = 1000,
			mapLocation = new Vector2(369, 227),
			illustration = LevelIllustration.Scales,
			maxElapsedTime = 4 * 365 / Game.GameSpeed,
		};

		public static List<Level> All = new List<Level> () {
			Tutorial,
			PortHenry,
			Lewes
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
