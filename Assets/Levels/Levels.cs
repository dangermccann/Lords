using System;
using System.Collections.Generic;
using UnityEngine;

namespace Lords {
	public class Levels {
		public static Level Tutorial = new Level() {
			name = "Tutorial",
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
		};

		public static List<Level> All = new List<Level> () {
			Tutorial,
		};
	}
}
