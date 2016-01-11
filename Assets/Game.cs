using System;
using System.Collections.Generic;
using UnityEngine;

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

		public static void Pause() {
			Time.timeScale = 0;
		}

		public static void Resume() {
			Time.timeScale = 1;
		}

		public static bool IsPaused() {
			return Time.timeScale == 0;
		}
	}
}
