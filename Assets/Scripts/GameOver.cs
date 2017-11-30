using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour {
	private float timer = 2f;
	
	// Update is called once per frame
	void Update () {
		timer -= Time.deltaTime;
		if (timer < 0) {
			SceneManager.LoadScene(0);
		}
	}
}
