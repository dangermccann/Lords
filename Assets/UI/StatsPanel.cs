using UnityEngine;
using UnityEngine.UI;
using Lords;

public class StatsPanel : MonoBehaviour {
	
	Text population; 
	Lords.Slider happiness, prosperity, culture;

	void Start() {
		population = GetUIText("PopulationValue");
		happiness = GetSlider("HappinessSlider");
		prosperity = GetSlider("ProsperitySlider");
		culture = GetSlider("CultureSlider");
	}

	Text GetUIText(string name) {
		return transform.FindChild(name).gameObject.GetComponent<Text>();
	}

	Lords.Slider GetSlider(string name) {
		return transform.FindChild(name).gameObject.GetComponent<Lords.Slider>();
	}
	
	void FixedUpdate() {
		population.text = Mathf.Floor(Game.CurrentCity.Score.Population).ToString();
		happiness.Value = Game.CurrentCity.Score.Happiness;
		prosperity.Value = Game.CurrentCity.Score.Prosperity;
		culture.Value = Game.CurrentCity.Score.Culture;
	}
}
