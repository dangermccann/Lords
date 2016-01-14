using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
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

		public static SavedGame Load() {
			if(File.Exists(SaveLocation())) {
				BinaryFormatter bf = new BinaryFormatter();
				FileStream file = File.Open(SaveLocation(), FileMode.Open);
				SavedGame saved = (SavedGame) bf.Deserialize(file);
				file.Close();
				return saved;
			}
			return null;
		}

		public static void Save() {
			BinaryFormatter bf = new BinaryFormatter();
			FileStream file = File.Create(SaveLocation());
			bf.Serialize(file, GenerateSavedGame());
			file.Close();
		}

		public static SavedGame GenerateSavedGame() {
			SavedGame saved = Load();
			if(saved == null) {
				saved = new SavedGame();
				saved.cities.Add(CurrentCity.SaveCity());
			}
			else {
				int idx = saved.cities.FindIndex(x => x.level == CurrentCity.Level.name);
				SavedCity savedCity = CurrentCity.SaveCity();
				if(idx > -1) {
					saved.cities[idx] = savedCity;
				}
				else {
					saved.cities.Add(savedCity);
				}
			}
			
			if(CurrentCity.MeetsVictoryConditions() && !saved.completedLevels.Contains(CurrentCity.Level.name)) {
				saved.completedLevels.Add(CurrentCity.Level.name);
			}

			return saved;
		}

		static string SaveLocation() { 
			return Application.persistentDataPath + "/save.lm";
		}
	}
	
	[System.Serializable] 
	public class SavedGame {
		public List<SavedCity> cities = new List<SavedCity>();
		public List<string> completedLevels = new List<string>();
	}

	[System.Serializable] 
	public class SavedCity {
		public float rawMaterials;
		public float funds;
		public string level;
		public List<SavedBuilding> buildings = new List<SavedBuilding>();
	}

	[System.Serializable] 
	public class SavedBuilding {
		public Hex position;
		public BuildingType type;
	}
}
