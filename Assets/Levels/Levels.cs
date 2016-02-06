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
			mapConfiguration = Maps.Tutorial,
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
			mapConfiguration = Maps.PortHenry,
			initialFunds = 1000,
			initialRawMaterials = 1000,
			mapLocation = new Vector2(216, 225),
			illustration = LevelIllustration.Fish,
			maxElapsedTime = 4 * 365 / Game.GameSpeed,
		};

		public static Level Galloway = new Level() {
			name = "Galloway",
			description = "This next undertaking will require careful attention towards developing the city's Prosperity.\nTime limit: {0}",
			victoryMessage = "You have achieved a satisfactory rating in the eyes of The Crown.",
			victoryConditions = new Aggregates() { 
				Population = 2000,
				Happiness = 10,
				Prosperity = 25,
				Culture = 15,
			},
			mapConfiguration = Maps.Galloway,
			initialFunds = 1000,
			initialRawMaterials = 1000,
			mapLocation = new Vector2(369, 227),
			illustration = LevelIllustration.Scales,
			maxElapsedTime = 4 * 365 / Game.GameSpeed,
		};

		// HERE
		public static Level Greencastle = new Level() {
			name = "Greencastle",
			description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Curabitur magna sem, condimentum in ornare ac.\nTime limit: {0}",
			victoryMessage = "Suspendisse potenti. Etiam non augue sed massa scelerisque auctor.",
			victoryConditions = new Aggregates() { 
				Population = 2000,
				Happiness = 10,
				Prosperity = 25,
				Culture = 15,
			},
			mapConfiguration = Maps.Greencastle,
			initialFunds = 1000,
			initialRawMaterials = 1000,
			mapLocation = new Vector2(50, 306),
			illustration = LevelIllustration.Scales,
			maxElapsedTime = 4 * 365 / Game.GameSpeed,
		};
		
		public static Level CraigBay = new Level() {
			name = "Craig Bay",
			description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Curabitur magna sem, condimentum in ornare ac.\nTime limit: {0}",
			victoryMessage = "Suspendisse potenti. Etiam non augue sed massa scelerisque auctor.",
			victoryConditions = new Aggregates() { 
				Population = 2000,
				Happiness = 10,
				Prosperity = 25,
				Culture = 15,
			},
			mapConfiguration = Maps.Tundra,
			initialFunds = 1000,
			initialRawMaterials = 1000,
			mapLocation = new Vector2(283.8f, 35.8f),
			illustration = LevelIllustration.Scales,
			maxElapsedTime = 4 * 365 / Game.GameSpeed,
		};

		public static Level Aberdeen = new Level() {
			name = "Aberdeen",
			description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Curabitur magna sem, condimentum in ornare ac.\nTime limit: {0}",
			victoryMessage = "Suspendisse potenti. Etiam non augue sed massa scelerisque auctor.",
			victoryConditions = new Aggregates() { 
				Population = 2000,
				Happiness = 10,
				Prosperity = 25,
				Culture = 15,
			},
			mapConfiguration = Maps.Tundra,
			initialFunds = 1000,
			initialRawMaterials = 1000,
			mapLocation = new Vector2(364, 26),
			illustration = LevelIllustration.Scales,
			maxElapsedTime = 4 * 365 / Game.GameSpeed,
		};

		public static Level Crosswell = new Level() {
			name = "Crosswell",
			description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Curabitur magna sem, condimentum in ornare ac.\nTime limit: {0}",
			victoryMessage = "Suspendisse potenti. Etiam non augue sed massa scelerisque auctor.",
			victoryConditions = new Aggregates() { 
				Population = 2000,
				Happiness = 10,
				Prosperity = 25,
				Culture = 15,
			},
			mapConfiguration = Maps.Tundra,
			initialFunds = 1000,
			initialRawMaterials = 1000,
			mapLocation = new Vector2(295, 331),
			illustration = LevelIllustration.Scales,
			maxElapsedTime = 4 * 365 / Game.GameSpeed,
		};

		public static Level Southgate = new Level() {
			name = "Southgate",
			description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Curabitur magna sem, condimentum in ornare ac.\nTime limit: {0}",
			victoryMessage = "Suspendisse potenti. Etiam non augue sed massa scelerisque auctor.",
			victoryConditions = new Aggregates() { 
				Population = 2000,
				Happiness = 10,
				Prosperity = 25,
				Culture = 15,
			},
			mapConfiguration = Maps.Tundra,
			initialFunds = 1000,
			initialRawMaterials = 1000,
			mapLocation = new Vector2(432, 105),
			illustration = LevelIllustration.Scales,
			maxElapsedTime = 4 * 365 / Game.GameSpeed,
		};

		public static Level Mayfield = new Level() {
			name = "Mayfield",
			description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Curabitur magna sem, condimentum in ornare ac.\nTime limit: {0}",
			victoryMessage = "Suspendisse potenti. Etiam non augue sed massa scelerisque auctor.",
			victoryConditions = new Aggregates() { 
				Population = 2000,
				Happiness = 10,
				Prosperity = 25,
				Culture = 15,
			},
			mapConfiguration = Maps.Tundra,
			initialFunds = 1000,
			initialRawMaterials = 1000,
			mapLocation = new Vector2(497, 143),
			illustration = LevelIllustration.Scales,
			maxElapsedTime = 4 * 365 / Game.GameSpeed,
		};

		public static Level LittleAsh = new Level() {
			name = "Little Ash",
			description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Curabitur magna sem, condimentum in ornare ac.\nTime limit: {0}",
			victoryMessage = "Suspendisse potenti. Etiam non augue sed massa scelerisque auctor.",
			victoryConditions = new Aggregates() { 
				Population = 2000,
				Happiness = 10,
				Prosperity = 25,
				Culture = 15,
			},
			mapConfiguration = Maps.Tundra,
			initialFunds = 1000,
			initialRawMaterials = 1000,
			mapLocation = new Vector2(65, 106),
			illustration = LevelIllustration.Scales,
			maxElapsedTime = 4 * 365 / Game.GameSpeed,
		};

		public static Level Easton = new Level() {
			name = "Easton",
			description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Curabitur magna sem, condimentum in ornare ac.\nTime limit: {0}",
			victoryMessage = "Suspendisse potenti. Etiam non augue sed massa scelerisque auctor.",
			victoryConditions = new Aggregates() { 
				Population = 2000,
				Happiness = 10,
				Prosperity = 25,
				Culture = 15,
			},
			mapConfiguration = Maps.Tundra,
			initialFunds = 1000,
			initialRawMaterials = 1000,
			mapLocation = new Vector2(681, 271),
			illustration = LevelIllustration.Scales,
			maxElapsedTime = 4 * 365 / Game.GameSpeed,
		};

		public static Level Welburn = new Level() {
			name = "Welburn",
			description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Curabitur magna sem, condimentum in ornare ac.\nTime limit: {0}",
			victoryMessage = "Suspendisse potenti. Etiam non augue sed massa scelerisque auctor.",
			victoryConditions = new Aggregates() { 
				Population = 2000,
				Happiness = 10,
				Prosperity = 25,
				Culture = 15,
			},
			mapConfiguration = Maps.Tundra,
			initialFunds = 1000,
			initialRawMaterials = 1000,
			mapLocation = new Vector2(497, 269),
			illustration = LevelIllustration.Scales,
			maxElapsedTime = 4 * 365 / Game.GameSpeed,
		};

		public static Level Dunham = new Level() {
			name = "Dunham",
			description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Curabitur magna sem, condimentum in ornare ac.\nTime limit: {0}",
			victoryMessage = "Suspendisse potenti. Etiam non augue sed massa scelerisque auctor.",
			victoryConditions = new Aggregates() { 
				Population = 2000,
				Happiness = 10,
				Prosperity = 25,
				Culture = 15,
			},
			mapConfiguration = Maps.Tundra,
			initialFunds = 1000,
			initialRawMaterials = 1000,
			mapLocation = new Vector2(638, 143),
			illustration = LevelIllustration.Scales,
			maxElapsedTime = 4 * 365 / Game.GameSpeed,
		};

		public static Level OldMilddleton = new Level() {
			name = "Old Milddleton",
			description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Curabitur magna sem, condimentum in ornare ac.\nTime limit: {0}",
			victoryMessage = "Suspendisse potenti. Etiam non augue sed massa scelerisque auctor.",
			victoryConditions = new Aggregates() { 
				Population = 2000,
				Happiness = 10,
				Prosperity = 25,
				Culture = 15,
			},
			mapConfiguration = Maps.Tundra,
			initialFunds = 1000,
			initialRawMaterials = 1000,
			mapLocation = new Vector2(562, 20),
			illustration = LevelIllustration.Scales,
			maxElapsedTime = 4 * 365 / Game.GameSpeed,
		};

		public static Level Normantown = new Level() {
			name = "Normantown",
			description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Curabitur magna sem, condimentum in ornare ac.\nTime limit: {0}",
			victoryMessage = "Suspendisse potenti. Etiam non augue sed massa scelerisque auctor.",
			victoryConditions = new Aggregates() { 
				Population = 2000,
				Happiness = 10,
				Prosperity = 25,
				Culture = 15,
			},
			mapConfiguration = Maps.Tundra,
			initialFunds = 1000,
			initialRawMaterials = 1000,
			mapLocation = new Vector2(602, 216),
			illustration = LevelIllustration.Scales,
			maxElapsedTime = 4 * 365 / Game.GameSpeed,
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
