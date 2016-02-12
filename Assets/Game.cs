using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using UnityEngine;

namespace Lords {
	public class Game {
		public const float GameSpeed = 1 / 30f; // game days per second

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
				try {
					SavedGame saved = (SavedGame) bf.Deserialize(file);
					Debug.Log("Loaded " + SaveLocation());
					return saved;
				}
				catch(IOException ioe) {
					Debug.LogException(ioe);
				}
				finally {
					file.Close();
				}
			}
			else {
				Debug.Log("File not found: " + SaveLocation());
			}
			return null;
		}

		public static void Save() {
			Persist(GenerateSavedGame());
		}

		public static void ResetCurrentCity() {
			SavedGame saved = GenerateSavedGame();
			int idx = saved.cities.FindIndex(x => x.level == CurrentCity.Level.name);
			saved.cities[idx].Reset();
			Persist(saved);
		}

		static void Persist(SavedGame saved) {
			BinaryFormatter bf = new BinaryFormatter();
			FileStream file = File.Create(SaveLocation());
			
			try {
				bf.Serialize(file, saved);
				Debug.Log("successfully saved game to " + SaveLocation());
			}
			catch(IOException ioe) {
				Debug.LogException(ioe);
			}
			finally {
				file.Close();
			}
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
			
			if(CurrentCity.MeetsVictoryConditions()) {
				if(!saved.completedLevels.Contains(CurrentCity.Level.name)) {
					saved.completedLevels.Add(CurrentCity.Level.name);
				}

				if(CurrentCity.Level.promotesTo > saved.rank) {
					saved.rank = CurrentCity.Level.promotesTo;
				}
			}

			return saved;
		}

		static string SaveLocation() { 
			return Application.persistentDataPath + "/save-v1.lm";
		}

		public static bool IsLevelUnlocked(SavedGame saved, Level level) {
			bool unlocked = true;
			foreach(Level prereq in level.prerequisites) {
				if(!saved.completedLevels.Contains(prereq.name)) {
					unlocked = false;
				}
			}

			return unlocked;
		}
	}
	
	[System.Serializable] 
	public class SavedGame {
		public List<SavedCity> cities = new List<SavedCity>();
		public List<string> completedLevels = new List<string>();
		public Ranks rank;

		public SavedCity FindCity(string level) {
			foreach(SavedCity city in cities) {
				if(city.level == level) {
					return city;
				}
			}
			return null;
		}
	}

	[System.Serializable] 
	public class SavedCity {
		public float rawMaterials;
		public float funds;
		public string level;
		public float elapsedTime;
		public List<SavedBuilding> buildings = new List<SavedBuilding>();

		public void Reset() {
			rawMaterials = Levels.GetLevel(level).initialRawMaterials;
			funds = Levels.GetLevel(level).initialFunds;
			elapsedTime = 0;
			buildings.Clear();
		}
	}

	[System.Serializable] 
	public class SavedBuilding {
		public Hex position;
		public BuildingType type;
	}
}
