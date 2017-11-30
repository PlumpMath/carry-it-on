using UnityEngine;

public class CarillonControl : MonoBehaviour {
	public Vector3 direction;
	public float speed = 0f;
	public float speedIncrement = 8f;
	public float speedDecrement = 0.5f;
	public float maxSpeed = 400f;
	private float tempSpeed = 0f;
	// Update is called once per frame
	void Update () {
		//float handleRotationX = transform.rotation.eulerAngles.x;

		if (Input.GetKeyDown ("up") && speed < maxSpeed) {
			speed = speed + speedIncrement;

		}else if (Input.GetKeyDown ("down")) {
			tempSpeed = speed - speedIncrement;
			if (tempSpeed > 0) {
				speed = tempSpeed;
			}

		} else if(speed > 0) {
			speed = speed - speedDecrement;

		}

		transform.Rotate (direction, speed * Time.deltaTime);
	}
}
