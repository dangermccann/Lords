using UnityEngine;
using System;
using System.Collections.Generic;

namespace Lords {
	public enum LevelIllustration {
		Drinks 		= 0,
		Paddles 	= 2,
		Pear 		= 3,
		House 		= 4,
		Fish 		= 5,
		Clock 		= 8,
		Drinker 	= 11,
		Hourglass	= 12,
		Sun 		= 13,
		Scales 		= 14,
		Grapes 		= 17,
		Flowers 	= 18,
		CoatOfArms	= 19,
		Factory		= 20,
	}

	public class GameAssets {

		const string SHEET = "Sprite-Sheet";
		const string BUILDING_TAG = "Building";
		const string TILE_TAG = "Tile";
		const string INTERSTITIAL = "Interstitial-Sprite-Sheet";

		public static Dictionary<TileType, int> TileIndexes = new Dictionary<TileType, int> {
			{ TileType.Empty, 		16 },
			{ TileType.Sand, 		14 },
			{ TileType.Dirt, 		1 },
			{ TileType.Snow, 		0 },
			{ TileType.Grass,		15 },
			{ TileType.Tundra,		16 },
			{ TileType.Water,		19 },
			{ TileType.Forest,		20 },
			{ TileType.Marsh,		21 },
			{ TileType.Mountains,	22 },
		};

		public static Dictionary<BuildingType, int> BuildingIndexes = new Dictionary<BuildingType, int> {
			{ BuildingType.Villa, 			2 },
			{ BuildingType.Slums, 			3 },
			{ BuildingType.Cottages,		4 },
			{ BuildingType.School, 			5 },
			{ BuildingType.Vegetable_Farm, 	6 },
			{ BuildingType.Wheat_Farm, 		7 },
			{ BuildingType.Tavern, 			8 },
			{ BuildingType.Amphitheater,	9 },
			{ BuildingType.Trading_Post, 	10 },
			{ BuildingType.Fort, 			11 },
			{ BuildingType.Hospital, 		12 },
			{ BuildingType.Garden,			13 },
			{ BuildingType.Church,			17 },
			{ BuildingType.Workshop,		18 },
		};

		public static Sprite GetSprite(TileType type) {
			return Resources.LoadAll<Sprite>(SHEET)[TileIndexes[type]];
		}

		public static Sprite GetSprite(BuildingType type) {
			return Resources.LoadAll<Sprite>(SHEET)[BuildingIndexes[type]];
		}

		public static Sprite GetDeleteTile() {
			return Resources.LoadAll<Sprite>(SHEET)[23];
		}

		public static void RemoveBuilding(GameObject go) {
			GameObject buildingGO = GetBuilding(go);
			if(buildingGO != null) {
				GameObject.Destroy(buildingGO);
			}
		}

		public static GameObject MakeBuilding(GameObject go, Building building) {
			if(building == null) {
				RemoveBuilding(go);
				return null;
			}

			Sprite buildingSprite = GetSprite(building.Type);

			GameObject buildingGO = GetBuilding(go);
			if(buildingGO == null) {
				buildingGO = (GameObject) GameObject.Instantiate(Resources.Load("empty-hex"), go.transform.position, Quaternion.identity);
				buildingGO.transform.parent = go.transform;
				buildingGO.GetComponent<SpriteRenderer>().sortingLayerID = 1;
			}

			buildingGO.name = building.Type.ToString();
			buildingGO.tag = BUILDING_TAG;
			buildingGO.GetComponent<SpriteRenderer>().sprite = buildingSprite;

			return buildingGO;
		}

		public static GameObject MakeTile(Transform parent, Tile tile) {
			GameObject go = (GameObject) GameObject.Instantiate(Resources.Load("empty-hex"), tile.Position.ToWorld(), Quaternion.identity);
			go.transform.parent = parent;
			go.name = String.Format("Tile ({0}, {1})", tile.Position.q, tile.Position.r);
			go.tag = TILE_TAG;
			go.GetComponent<SpriteRenderer>().sprite = GetSprite(tile.Type);
			return go;
		}

		public static void RedrawTile(GameObject go, Tile tile) {
			go.GetComponent<SpriteRenderer>().sprite = GetSprite(tile.Type);
		}

		static GameObject GetBuilding(GameObject go) {
			foreach(Transform child in go.transform) {
				if(child.gameObject.CompareTag(BUILDING_TAG)) {
					return child.gameObject;
				}
			}
			return null;
		}

		public static Sprite LevelIllustration(LevelIllustration illustration) {
			return Resources.LoadAll<Sprite>(INTERSTITIAL)[(int) illustration];
		}
	}
}

