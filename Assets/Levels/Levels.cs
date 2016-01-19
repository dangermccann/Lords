using System;
using System.Collections.Generic;
using UnityEngine;

namespace Lords {
	public class Levels {
		public static Level Tutorial = new Level() {
			name = "Tutorial",
			description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Maecenas facilisis pharetra odio a aliquet.",
			victoryMessage = "You have achieved a satisfactory rating in the eyes of your contemporaries.",
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
		};

		public static Level PortHenry = new Level() {
			name = "Port Henry",
			description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Maecenas facilisis pharetra odio a aliquet.",
			victoryMessage = "You have achieved a satisfactory rating in the eyes of your contemporaries.",
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
		};

		public static List<Level> All = new List<Level> () {
			Tutorial,
			PortHenry,
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
