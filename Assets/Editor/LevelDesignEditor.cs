using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;

namespace Lords {

	/// <summary>
	/// There is somethign about the Unity serialization system that I completely don't understand, so this 
	/// rubbish is a bit of a sledge hammer to work around it.  It's only for level design, so I'm not really 
	/// worried about it.  
	/// </summary>

	[CustomEditor(typeof(LevelDesigner))]
	public class LevelDesignEditor : Editor {

		//SerializedProperty configProp, seed, radius, perlinOctive, tiles;
		SerializedProperty configProp, tiles;
		LevelDesigner designer;

		void OnEnable () {
			designer = (LevelDesigner) target;

			configProp = serializedObject.FindProperty("config");
			//seed = configProp.FindPropertyRelative("Seed");
			//perlinOctive = configProp.FindPropertyRelative("PerlinOctive");
			//radius = configProp.FindPropertyRelative("Radius");
			tiles = configProp.FindPropertyRelative("TileConfiguration");
		}

		public override void OnInspectorGUI() {

			//UnityEditorInternal.ReorderableList
			//EditorGUILayout.PropertyField(configProp, new GUIContent("Configuration"), true);
			//EditorGUILayout.PropertyField(radius, new GUIContent("Radius"), true);
			//EditorGUILayout.PropertyField(perlinOctive, new GUIContent("Perlin Octive"), true);

			designer.config.Radius = EditorGUILayout.IntField("Radius", designer.config.Radius);
			designer.config.Seed = EditorGUILayout.IntField("Seed", designer.config.Seed);
			designer.config.PerlinOctive = EditorGUILayout.IntField("PerlinOctive", designer.config.PerlinOctive);
			EditorGUILayout.PropertyField(tiles, new GUIContent("Tile Configuration"), true);




			if(GUILayout.Button("Redraw")) {
				TileType[] types = new TileType[tiles.arraySize];
				for(int i = 0; i < types.Length; i++) {
					types[i] = (TileType) tiles.GetArrayElementAtIndex(i).enumValueIndex;
				}

				List<TileType> newList = new List<TileType>();
				newList.AddRange(types);
				designer.config.TileConfiguration = newList;


				designer.Redraw();
			}
		}
	}
}