using UnityEngine;
using UnityEngine.UI;
using Lords;

public class StatsPanel : MonoBehaviour {


	Text population; 
	void Start() {
		population = GetUIText("PopulationValue");
	}

	Text GetUIText(string name) {
		return transform.FindChild(name).gameObject.GetComponent<Text>();
	}

	void Update () {
		population.text = Mathf.Floor(Game.CurrentCity.Score.Population).ToString();
	}
}
