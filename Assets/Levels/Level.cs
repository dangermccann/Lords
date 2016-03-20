using System;
using System.Collections.Generic;
using UnityEngine;

namespace Lords {
	public class Level {

		public string name;
		public string description;
		public string victoryMessage;
		public Aggregates victoryConditions;
		public MapConfiguration mapConfiguration;
		public float initialFunds, initialRawMaterials;
		public Vector2 mapLocation;
		public LevelIllustration illustration = LevelIllustration.Clock;
		public float maxElapsedTime;
		public Ranks promotesTo = Ranks.None;
		public Level[] prerequisites = new Level[0];

		public Dictionary<Exports, float> exportPrices = new Dictionary<Exports, float>() {
			{ Exports.Clothes,            10 },
			{ Exports.Tools,              10 },
			{ Exports.Manufactured_Goods, 10 }
		};

		public Dictionary<Imports, float> importPrices = new Dictionary<Imports, float>() {
			{ Imports.Books,    75 },
			{ Imports.Incense,  75 },
			{ Imports.Jewlery,  75 },
			{ Imports.Medicine, 75 },
			{ Imports.Spirits,  75 },
			{ Imports.Weapons,  75 },
		};

		public float additionalFundsPerSecond = 12f;
		public float additionalRawMaterialsPerSecond = 24f;

		public Level() {}
	}
}
