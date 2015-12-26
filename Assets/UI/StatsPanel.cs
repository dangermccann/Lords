using UnityEngine;
using UnityEngine.UI;
using Lords;

public class StatsPanel : MonoBehaviour {


	Text population, funds, materials, happiness, propsperity, culture; 
	void Start() {
		population = GetUIText("PopulationValue");
		funds = GetUIText("FundsValue");
		materials = GetUIText("MaterialsValue");
		happiness = GetUIText("HappinessValue");
		propsperity = GetUIText("ProsperityValue");
		culture = GetUIText("CultureValue");
	}

	Text GetUIText(string name) {
		return transform.FindChild(name).gameObject.GetComponent<Text>();
	}

	void Update () {
		population.text = Mathf.Floor(Game.CurrentCity.Score.Population).ToString();
		funds.text = Mathf.Floor(Game.CurrentCity.Funds).ToString();
		materials.text = Mathf.Floor(Game.CurrentCity.RawMaterials).ToString();
		happiness.text = Mathf.Floor(Game.CurrentCity.Score.Happiness).ToString();
		propsperity.text = Mathf.Floor(Game.CurrentCity.Score.Prosperity).ToString();
		culture.text = Mathf.Floor(Game.CurrentCity.Score.Culture).ToString();
	}
}
