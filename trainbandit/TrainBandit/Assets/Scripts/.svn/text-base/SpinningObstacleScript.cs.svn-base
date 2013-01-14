using UnityEngine;
using System.Collections;

public class SpinningObstacleScript : MonoBehaviour {
	
	public float RotationInterval = 60.0f;
	
	// Update is called once per frame
	void Update () 
	{
		if(tag == "Wood")
			transform.Rotate(Vector3.right * Time.deltaTime * RotationInterval);
		else
			transform.Rotate(Vector3.up * Time.deltaTime * RotationInterval);		
	}
	
	public void WhenObstacleIsHit()
	{
		GameObject.Destroy(gameObject);
	}
}
