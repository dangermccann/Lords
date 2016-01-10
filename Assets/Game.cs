using System;
using System.Collections.Generic;

namespace Lords {
	public class Game {
		public static Action<Level> LevelLoaded;

		public static City CurrentCity;

		static Level currentLevel = null;
		public static Level CurrentLevel {
			get { return currentLevel; }
			set {
				currentLevel = value;
				if(LevelLoaded != null) {
					LevelLoaded(currentLevel); 
				}
			}
		}
	}
}
