using UnityEngine;
using Lords;

public class StatsPanel : MonoBehaviour {
	
	UILabel population; 
	Lords.Slider happiness, prosperity, culture;

	void Start() {
		population = GetUIText("Table/PopulationValue");
		happiness = GetSlider("Table/HappinessSlider");
		prosperity = GetSlider("Table/ProsperitySlider");
		culture = GetSlider("Table/CultureSlider");
	}

	UILabel GetUIText(string name) {
		return transform.FindChild(name).gameObject.GetComponent<UILabel>();
	}

	Lords.Slider GetSlider(string name) {
		return transform.FindChild(name).gameObject.GetComponent<Lords.Slider>();
	}
	
	void FixedUpdate() {
		population.text = Mathf.Floor(Game.CurrentCity.Score.Population).ToString();
		happiness.Value = SliderValue(Game.CurrentLevel.victoryConditions.Happiness, Game.CurrentCity.Score.Happiness);
		prosperity.Value = SliderValue(Game.CurrentLevel.victoryConditions.Prosperity, Game.CurrentCity.Score.Prosperity);
		culture.Value = SliderValue(Game.CurrentLevel.victoryConditions.Culture, Game.CurrentCity.Score.Culture);
	}

	float SliderValue(float target, float actual) {
		if(target == 0) 
			return 1 + (actual / 10f);

		return actual / target * 0.5f + 0.5f;
	}
}
