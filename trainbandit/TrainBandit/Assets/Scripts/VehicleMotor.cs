using UnityEngine;
using System.Collections;

public class VehicleMotor : MonoBehaviour 
{
#region inputs
	
	[System.NonSerialized]
	public float inputTilt;
	
#endregion
	
#region editor vars
	
	public TrackPiece startPiece;
	public float accelRate;
	public float secondsBackToStraight = 3.0f;		// How many seconds before an unused swipe returns to straight;
	private float tiltBackToDefault = 0.0f;	 		// The timer for switching back to straight.
	
#endregion
	
	private bool Stop = false;
	
	public float initialSpeedInMPH = 50f;
	private float currentSpeed;		// @ 2.0 = 20 mph (cops catch you), @ 14.0 = 120 (max speed)
	
	private float deceleration;
	public float convertedSpeed;
	
	private TrackPath currentPath;
	private TrackPiece currentPiece;
	private float progress;
	
	private Player playerGameObject;
	
	private string currentPathNameHolder = "nothing";
	
	// Use this for initialization
	void Start () 
	{
		playerGameObject = GetComponent<Player>();
		
		currentPath = startPiece.GetFirstPath();
		
		currentSpeed = ConvertFromMPH(initialSpeedInMPH);
		deceleration = (ConvertFromMPH(20f) - currentSpeed) / 10f;
	}
	
	// Update is called once per frame
	void Update ()
	{
		if(!Stop)
		{
			float deltaTime = Time.deltaTime;
			
			ChangeTiltValue( deltaTime );		
			
			Traverse( deltaTime );
			
			ChangeSpeed( deltaTime );
			
			DecreaseAcceleration( deltaTime );	
		}
		if(Input.GetKeyUp(KeyCode.O))
			Stop = false;
	}
	
	/// <summary>
	/// Increases the speed.
	/// </summary>
	/// <param name='deltaTime'>
	/// Delta time passed in from Update funciton.
	/// </param>
	void ChangeSpeed(float deltaTime)
	{
		currentSpeed = currentSpeed + (accelRate * deltaTime);
		
		if(currentSpeed > 20.0f)
			currentSpeed = 20.0f;
		if(currentSpeed < 7.0f)
		{
			playerGameObject.FormOfDeath("speed");
			playerGameObject.Kill();
		}
		
		convertedSpeed = ConvertToMPH(currentSpeed);
		
		GameEventHandler.Instance.SendEvent( new GameEvent( GameEventType.UpdateSpeed, convertedSpeed / 120f ) );
	}
	
	/// <summary>
	/// Decreases the acceleration.
	/// </summary>
	/// <param name='deltaTime'>
	/// Delta time passed in through Update function.
	/// </param>
	void DecreaseAcceleration( float deltaTime )
	{		
		if(convertedSpeed <= 35f)
		{
			accelRate -= deceleration * 0.5f * deltaTime;
			if( accelRate < -1f )
				accelRate = -1f;
		}
		else if(convertedSpeed == 120f && accelRate >= 1f)
		{
			accelRate = 0.2f;
		}
		else if(accelRate > -1.5f)
		{
			accelRate += deceleration * deltaTime;
			if( accelRate < -1.5f )
				accelRate = -1.5f;
		}
	}
	
	/// <summary>
	/// Increases the acceleration.
	/// </summary>
	void IncreaseAcceleration()
	{
		if(playerGameObject.GetWood() > 0)
		{
			accelRate += (/*-(1f/120f) * */ convertedSpeed) + (7f/6f);
			if(accelRate >= 1.5f)
				accelRate = 1.5f;
			if(convertedSpeed == 120f)
				accelRate = 0.2f;
			
			playerGameObject.SubtractWood();
		}
	}
	
	/// <summary>
	/// Converts the speed to MPH representation.
	/// Reminder: currentSpeed goes from 7.0 to 20.0.
	/// </summary>
	/// <returns>
	/// The currentSpeed to MPH.
	/// </returns>
	/// <param name='speedNow'>
	/// Should be 'currentSpeed' variable.
	/// </param>
	float ConvertToMPH( float speedNow )
	{
		return ((100f/13f) * speedNow) - (440f/13f);
	}
	
	/// <summary>
	/// Converts from MPH to Unity Speed.
	/// Reminder: convertedSpeed goes from 20.0 mph to 120.0 mph.
	/// </summary>
	/// <returns>
	/// The convertedSpeed from MPH.
	/// </returns>
	/// <param name='speedNow'>
	/// Should be 'convertedSpeed' variable.
	/// </param>
	float ConvertFromMPH( float speedNow )
	{
		return (speedNow + (440f/13f)) / (100f/13f);
	}
	
	float GetTiltValue()
	{
		return inputTilt;
	}
	
	private void ChangeTiltValue( float deltaTime )
	{
		tiltBackToDefault += deltaTime;
		if(tiltBackToDefault >= secondsBackToStraight)
		{
			inputTilt = 0.0f;
		}
	}
	
	private void ChangeTiltValueAtPathChange()
	{	
		if(currentPathNameHolder != "nothing")
		{
			inputTilt = 0.0f;
			
			currentPathNameHolder = "nothing";
		}
		
		if( ( currentPath.name.Contains("Curve") && !currentPath.name.Contains("Death") ) || currentPath.name == "Straight")
			return;
		else
			currentPathNameHolder = currentPath.name;
	}
	
	public void Traverse ( float deltaTime )
	{
		if ( currentPath == null )
			return;
		
		float moveDelta = currentSpeed * deltaTime;
		
		float lengthRemaining = ( 1 - progress ) * currentPath.Length;
		
		// TODO cleanup, make while loop?
		if ( moveDelta > lengthRemaining )
		{
			moveDelta -= lengthRemaining;
			
			currentPath = currentPath.SelectNextPath( GetTiltValue() );
			
			if ( currentPath == null )
			{
				playerGameObject.FormOfDeath("deadend");
				playerGameObject.Kill();
				return;
			}
			else
			{
				ChangeTiltValueAtPathChange();
				SendMessage( "OnReachedNewPath", currentPath, SendMessageOptions.DontRequireReceiver );
			}
			
			progress = 0;
			lengthRemaining = ( 1 - progress ) * currentPath.Length;
		}
		
		float progressDelta = moveDelta / currentPath.Length;
		
		progress += progressDelta;
		
		transform.position = currentPath.PointAt( progress );
		
		LookAhead();
	}
	
	
	public void OnSwipe(SwipeDirection sd)
	{
		if(sd == SwipeDirection.Up)
		{
			IncreaseAcceleration();
		}
		if(sd == SwipeDirection.Left)
		{
			inputTilt = -1f;
			tiltBackToDefault = 0.0f;
		}
		if(sd == SwipeDirection.Right)
		{
			inputTilt = 1f;
			tiltBackToDefault = 0.0f;
		}
	}
	
	void LookAhead ()
	{
		float lookProgress = progress + Constants.LOOK_AHEAD_PCT;
		
		Vector3 lookPoint = currentPath.PointAt( lookProgress );
		
		transform.LookAt ( lookPoint );
	}
	
	/// <summary>
	/// Used by TrainDirectionPS.cs, Gets the position for particle.
	/// </summary>
	/// <returns>
	/// The exact position for where a particle should be placed based on the percentage parameter passed in.
	/// </returns>
	/// <param name='percentAhead'>
	/// The percent the particle should be along the section of track. If great than one, it move on to next piece.
	/// </param>
	public Vector3 GetPathForParticles( float percentAhead )
	{
		float temp = progress + percentAhead;
		if(currentPath == null)
			return gameObject.transform.position;
		
		if( temp > 1 )
		{
			float nextPercentAhead = temp - 1.0f;
			
			try
			{
				return currentPath.SelectNextPath( GetTiltValue() ).PointAt(nextPercentAhead);
			}catch
			{
				return gameObject.transform.position;
			}
			
		}
		return currentPath.PointAt( temp );
	}
	
	public void StopLocomotion()
	{
		Stop = true;
		currentSpeed = 0.0f;
		accelRate = 0.0f;
	}
}
