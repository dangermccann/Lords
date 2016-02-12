using UnityEngine;
using System;
using System.Collections.Generic;

namespace Lords {
	[ExecuteInEditMode]
	public class LevelDesigner : MonoBehaviour {
		GameObject mapRoot;
		MapGenerator generator = new MapGenerator();
		Map map;

		public MapConfiguration config;

		void Awake() {
			mapRoot = GameObject.Find("Map");
			config = new MapConfiguration() {
				Radius = 10,
				Seed = 10,
				PerlinOctive = 5,
				TileConfiguration = new List<TileType>() {
					TileType.Snow,
					TileType.Snow,
					TileType.Dirt,
					TileType.Grass,
					TileType.Sand,
					TileType.Grass,
					TileType.Dirt,
					TileType.Snow,
					TileType.Snow,
				}
			};
		}

		public void Redraw() {

			while (mapRoot.transform.childCount > 0) {
				GameObject.DestroyImmediate(mapRoot.transform.GetChild(0).gameObject);
			}

			Level level = new Level();
			level.name = "Level Designer";
			level.mapConfiguration = config;
			City city = new City(level);
			map = generator.GenerateMap(config.Radius, config.TileConfiguration, config.Seed, config.PerlinOctive);
			city.SetMap(map);

			foreach(Tile tile in city.Tiles.Values) {
				GameAssets.MakeTile(mapRoot.transform, tile);
			}
		}
	}
}
