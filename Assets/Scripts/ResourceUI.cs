using UnityEngine;
using UnityEngine.UI;

public class ResourceUI : MonoBehaviour {
	public static int resourceDisplay;
	private Text displayText;

    void Start() {
		displayText = GetComponent<Text>();
    }

    void Update() {
        displayText.text = "Yarn: " + resourceDisplay.ToString();
    }
}
