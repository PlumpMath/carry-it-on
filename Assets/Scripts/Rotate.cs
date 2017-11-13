using UnityEngine;

public class Rotate : MonoBehaviour {
	public Vector3 direction;
	public float speed = 0f;
	public float dragSpeed = 40f;

	public float pitchMin = 0.6f;
	public float pitchMax = 2f;

	private float diff; 
	private float pitchVal;
	private float speedRatio;

	private GameObject handleGameObject;
	private CarillonControl handleScript;

	private AudioSource lullaby;

	void Start(){
		handleGameObject = GameObject.FindGameObjectWithTag ("Handle");
		handleScript = handleGameObject.GetComponent<CarillonControl> ();

		lullaby = GetComponent<AudioSource> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (handleScript.speed > 0) {
			if (!lullaby.isPlaying) {
				lullaby.Play ();
			}

			diff = handleScript.speed - dragSpeed;
			if (diff > 0) {
				speed = diff;
			} else {
				speed = handleScript.speed;
			}


			speedRatio = (speed / handleScript.maxSpeed);
			pitchVal = (pitchMax - pitchMin) * speedRatio + pitchMin;

			if (pitchVal > 0.95f && pitchVal < 1.05f) {
				lullaby.pitch = 1f;
			} else {
				lullaby.pitch = pitchVal;
			}

			transform.Rotate(direction, speed * Time.deltaTime);

		}

		if (handleScript.speed < 1 && lullaby.isPlaying) {
			lullaby.Pause ();
		}
	}
}
