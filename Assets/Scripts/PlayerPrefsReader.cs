using UnityEngine;
using UnityEngine.UI;

public class PlayerPrefsReader : MonoBehaviour {

	private Text scoreText;

	// Use this for initialization
	void Start () {
		scoreText = GameObject.FindGameObjectWithTag ("Score").GetComponent<Text>();
		scoreText.text = "Latest score: " + PlayerPrefs.GetInt ("highscore").ToString ();

	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
