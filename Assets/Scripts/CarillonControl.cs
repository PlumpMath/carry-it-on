using UnityEngine;

public class CarillonControl : MonoBehaviour {

	public float speed = 0f;
	public float speedIncrement = 10f;
	public float speedDecrement = 0.5f;
	public float maxSpeed = 400f;

	// Update is called once per frame
	void Update () {
		//float handleRotationX = transform.rotation.eulerAngles.x;

		if (Input.GetKeyDown ("up") && speed < maxSpeed) {
			speed = speed + speedIncrement;

		}else if (Input.GetKeyDown ("down") && speed < maxSpeed && speed > 0) {
			speed = speed - speedIncrement;

		} else if(speed > 0) {
			speed = speed - speedDecrement;

		}

		transform.Rotate (Vector3.right, speed * Time.deltaTime);
	}
}
