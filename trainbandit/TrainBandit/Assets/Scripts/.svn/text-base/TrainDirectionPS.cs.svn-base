using UnityEngine;
using System.Collections;

public class TrainDirectionPS : MonoBehaviour {
	
	public int resolution = 10;
	
	private float increment;
	private ParticleSystem.Particle[] points;
	private GameObject locomotiveObject;

	// Use this for initialization
	void Start () 
	{
		increment = 1f/resolution;
		locomotiveObject = GameObject.Find("Locomotive");
		CreatePoints();
	}
	
	private void CreatePoints()
	{
		points = new ParticleSystem.Particle[resolution];
		for(int i = 0; i < resolution; i++)
		{
			Vector3 desiredPosition = locomotiveObject.GetComponent<VehicleMotor>().GetPathForParticles(i * increment);
			points[i].position = new Vector3(desiredPosition.x, desiredPosition.y, desiredPosition.z);
			points[i].color = Color.red;
			points[i].size = 0.25f;
		}
	}
	
	private void UpdatePoints()
	{
		for(int i = 0; i < resolution; i++)
		{
			Vector3 desiredPosition = locomotiveObject.GetComponent<VehicleMotor>().GetPathForParticles(i * increment);
			points[i].position = new Vector3((desiredPosition.x)
											, desiredPosition.y
											, (desiredPosition.z));
		}
	}
	
	// Update is called once per frame
	void Update () 
	{
		UpdatePoints();
		particleSystem.SetParticles(points, points.Length);
	}
}
