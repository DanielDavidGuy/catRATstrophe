using UnityEngine;
using UnityEngine.UI;

public class ResourceUI : MonoBehaviour {
	private Text displayText;

    void Start() {
		displayText = GetComponent<Text>();
    }

    void Update() {
        displayText.text = "Yarn: " + Inventory.inventory["yarn"].ToString();
    }
}
