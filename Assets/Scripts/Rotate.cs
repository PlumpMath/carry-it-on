using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Rotate : MonoBehaviour {
	public Vector3 direction;
	public float speed = 0f;
	public float dragSpeed = 0.3f;
	public float pitchMin = 0.6f;
	public float pitchMax = 2f;

	private float diff; 
	private float pitchVal;
	private float speedRatio;
	private GameObject handleGameObject;
	private CarillonControl handleScript;
	private GameObject zParticleGameObject;
	private ParticleSystem zParticle;

	private AudioSource lullaby;

	private GameObject arrowUpGameObject;
	private GameObject arrowDownGameObject;
	private Text scoreText;
	private Text failText;

	private int score;
	private bool timed;
	private float currentTimer;
	private float failCounter;
	private bool fail;
	private float failMax = 2f;

	void Awake(){
		handleGameObject = GameObject.FindGameObjectWithTag ("Handle");
		handleScript = handleGameObject.GetComponent<CarillonControl> ();
		lullaby = GetComponent<AudioSource> ();

		zParticleGameObject = GameObject.FindGameObjectWithTag ("ZParticle");
		zParticle = zParticleGameObject.GetComponent<ParticleSystem> ();

		arrowUpGameObject = GameObject.FindGameObjectWithTag ("ArrowUp");
		arrowDownGameObject = GameObject.FindGameObjectWithTag ("ArrowDown");
		arrowUpGameObject.SetActive (false);
		arrowDownGameObject.SetActive (false);

		scoreText = GameObject.FindGameObjectWithTag ("Score").GetComponent<Text>();
		failText = GameObject.FindGameObjectWithTag ("Fails").GetComponent<Text>();

		score = 0;
		currentTimer = 1f;
		timed = false;
		failCounter = failMax;
		fail = false;
	}
	
	// Update is called once per frame
	void Update () {
		if (handleScript.speed > 0) {
			if (!lullaby.isPlaying) {
				lullaby.Play ();
			}

			speed = handleScript.speed - (handleScript.speed * dragSpeed);
			speedRatio = (speed / handleScript.maxSpeed);
			pitchVal = (pitchMax - pitchMin) * speedRatio + pitchMin;

			if (pitchVal > 0.9f && pitchVal < 1.1f) {
				lullaby.pitch = 1f;
				if(!zParticle.isPlaying){
					zParticle.Play ();
				}
				Scoring ();
				FailReset ();
			} else {
				zParticle.Stop();
				lullaby.pitch = pitchVal;
				FailDecrease ();
			}

			transform.Rotate(direction, speed * Time.deltaTime);

		}

		ManageArrows (pitchVal);

		if (handleScript.speed < 1 && lullaby.isPlaying) {
			lullaby.Pause ();
		}

		Fail ();
	}

	void FailReset(){
		fail = true;
		failCounter = failMax;
		failText.text = "";
	}

	void FailDecrease(){
		if (fail) {
			failCounter -= Time.deltaTime;
			if (failCounter < 0f) {
				failCounter = 0f;
			}
			failText.text = failCounter.ToString ("N2");
		}
	}

	void Fail(){
		if (failCounter == 0) {
			PlayerPrefs.SetInt ("highscore", score);
			SceneManager.LoadScene(3);
		}
	}

	void ManageArrows(float val){
		if (val < 0.9f) {
			arrowUpGameObject.SetActive (true);
		} else if(val > 1.1f){
			arrowDownGameObject.SetActive (true);
		} else {
			arrowUpGameObject.SetActive (false);
			arrowDownGameObject.SetActive (false);
		}
	}

	void Scoring(){
		if (timed) {
			currentTimer -= Time.deltaTime;
		}

		if (currentTimer < 0) {
			currentTimer = 1f;
			timed = false;
			score++;
			scoreText.text = score.ToString("0");
		} else {
			timed = true;
		}
	}
}
