using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Lords {
	public class MusicController : MonoBehaviour {
		private static MusicController instance = null;
		public static MusicController Instance {
			get { return instance; }
		}

		static List<string> Songs = new List<string>() { 
			"Audio/Agnus Dei X",
			"Audio/Danse Macabre - No Violin",
			"Audio/Divertimento K131",
			"Audio/Ghost Dance",
			"Audio/Ranz des Vaches",
			"Audio/Sheep May Safely Graze - BWV 208",
			"Audio/Sonatina",
			"Audio/Trio for Piano Violin and Viola",
		};

		static System.Random random;

		AudioClip currentClip;
		int currentIndex;
		AudioSource audioSource;

		void Awake() {
			if (instance != null && instance != this) {
				Destroy(this.gameObject);
				return;
			} else {
				instance = this;
			}
			DontDestroyOnLoad(this.gameObject);
		}
		
		void Start() {
			random = new System.Random((int)System.DateTime.Now.Ticks);
			Shuffle(Songs);

			currentIndex = 0;
			audioSource = GetComponent<AudioSource>();
			audioSource.volume = Game.Options.musicVolume;
			StartCoroutine(PlayBackgroundMusic());
		}

		IEnumerator PlayBackgroundMusic() {
			while(true) {
				currentClip = Resources.Load(Songs[currentIndex]) as AudioClip;
				audioSource.clip = currentClip;
				audioSource.Play();

				Debug.Log("Playing " + currentClip.name);

				while(audioSource.isPlaying) {
					yield return new WaitForSeconds(1);
				}

				currentIndex += 1;
				currentIndex %= Songs.Count;
			}
		}

		public float Volume {
			get { return audioSource.volume; }
			set { audioSource.volume = value; }
		}

		public static void Shuffle<T>(IList<T> list) {  
			int n = list.Count;  
			while (n > 1) {  
				n--;
				int k = random.Next(n + 1);
				T value = list[k];
				list[k] = list[n];
				list[n] = value;
			}  
		}
	}
}
