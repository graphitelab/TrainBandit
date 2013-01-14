using UnityEngine;
using System.Collections;

public class RockExplosion : MonoBehaviour {
	
	private Player myPlayer;
	
	// This is just a test
	
	void Start()
	{
		myPlayer = GameObject.Find("Locomotive").GetComponent<Player>();
	}
	
	public void BlowupRock()
	{
		if( myPlayer.GetDynamite() > 0 )
		{
			myPlayer.SubtractDynamite();
			GameObject.Destroy(gameObject);
		}
	}
}
