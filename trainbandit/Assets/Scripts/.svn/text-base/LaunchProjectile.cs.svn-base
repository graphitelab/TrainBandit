using UnityEngine;
using System.Collections;

public class LaunchProjectile : MonoBehaviour {
	
	public GameObject projectileDynamitePrefab;
	public float speed;
	
	// Use this for initialization
	void Start () 
	{
		if( projectileDynamitePrefab == null )
			Debug.LogError("Projectile Dynamite Prefab has not been defined!");
	}
	
	// Update is called once per frame
	void Update () 
	{
	
	}
	
	public void Launch( Vector3 targetPos )
	{
		Vector3 myPosition = transform.position;
		
		float initialDistance = Vector3.Distance(myPosition, targetPos);
	}
}
