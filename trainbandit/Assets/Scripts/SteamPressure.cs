using UnityEngine;
using System.Collections;

public class SteamPressure : MonoBehaviour {
	
	private float pressure;
	public float increaseAmount = 5.0f;
	public float releaseAmount = 15.0f;
	private float maxPressure = 120.0f;
	private float deltatime;
	
	// Use this for initialization
	void Start () {
		Reset();
	}
	
	void Reset()
	{
		pressure = 0;	
	}
	
	// Update is called once per frame
	void Update () 
	{		
		deltatime = Time.deltaTime * 25.0f;
		
		// This 'if' block increase steam slower as it gets to fuller capacity
		if(pressure <= 100.0f)
			pressure += increaseAmount * deltatime;
		else if(pressure <= 110.0f)
			pressure += (increaseAmount * 0.8f) * deltatime;
		else if(pressure <= 117.5f)
			pressure += (increaseAmount * 0.6f) * deltatime;
		else
			pressure += (increaseAmount * 0.4f) * deltatime;
		
		if(pressure > maxPressure)
		{
			pressure = maxPressure;
			gameObject.GetComponent<Player>().FormOfDeath("steam");
			gameObject.GetComponent<Player>().Kill();
		}
		
		GameEventHandler.Instance.SendEvent( new GameEvent( GameEventType.UpdateSteam, pressure/maxPressure ) );
	}
	
	public void ReleasePressure( float release )
	{
		pressure -= release;
		if(pressure < 0)
			pressure = 0;
	}
	
	public void OnSwipe(SwipeDirection sd)
	{
		if( sd == SwipeDirection.Down)
			ReleasePressure(releaseAmount);
	}
	
	public void StopSteam()
	{
		pressure = 0.0f;
		increaseAmount = 0.0f;
		releaseAmount = 0.0f;
	}
}
