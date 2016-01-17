using System;
using UnityEngine;

namespace Lords {
	public class Level {

		public string name;
		public Aggregates victoryConditions;
		public MapConfiguration mapConfiguration;
		public float initialFunds, initialRawMaterials;
		public Vector2 mapLocation;
		public LevelIllustration illustration = LevelIllustration.Clock;

		public Level() {}
	}
}
