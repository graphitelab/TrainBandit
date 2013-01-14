using UnityEngine;
using System.Collections;

[RequireComponent( typeof( VehicleMotor ) )]
public class VehicleAIController : MonoBehaviour {

	private VehicleMotor motor;
	
	private float tiltValue;

	void Start () 
	{
		motor = GetComponent< VehicleMotor >();
	}
	
	void Update () 
	{
		motor.inputTilt = tiltValue;
	}
	
	void OnGUI ()
	{
		GUILayout.Space( 50f );
		GUILayout.Label( "AI tilt: " + tiltValue );
	}
	
	void OnReachedNewPath ( TrackPath newPath )
	{
		CalcTiltValue( newPath );
	}
	
	void CalcTiltValue ( TrackPath path )
	{
		tiltValue = 0f;
		
		int numChoices = path.nextPath.Length;
		
		if ( numChoices < 2 )
			return;
		
		foreach ( TrackPath possiblePath in path.nextPath )
		{
			if ( possiblePath.nextPath.Length < 1 )
				continue;
			
			// TODO - add three-way split handling
			
			tiltValue = possiblePath.DirectionVector.normalized.x;
		}
	}
}
