using UnityEngine;
using System;

namespace Lords {
	public class EffectsController : MonoBehaviour {
		AudioClip click, click2, click3, open, place;

		void Start() {
			click = Resources.Load("Audio/Blip_Select") as AudioClip;
			click2 = Resources.Load("Audio/Blip_Select2") as AudioClip;
			click3 = Resources.Load("Audio/Blip_Select4") as AudioClip;
			open = Resources.Load("Audio/Open") as AudioClip;
			place = Resources.Load("Audio/Randomize4") as AudioClip;
		}

		public void Click() {
			Play(click);
		}

		public void Click2() {
			Play(click2);
		}

		public void Click3() {
			Play(click3);
		}

		public void Open() {
			Play(open);
		}

		public void Place() {
			Play(place);
		}

		void Play(AudioClip clip) {
			AudioSource.PlayClipAtPoint(clip, Camera.main.transform.position);
		}
	}
}
