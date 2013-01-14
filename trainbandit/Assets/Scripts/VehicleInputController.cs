using UnityEngine;
using System.Collections;

[RequireComponent( typeof( TiltReader ) )]
[RequireComponent( typeof( VehicleMotor ) )]
public class VehicleInputController : MonoBehaviour 
{
	private TiltReader tilt; 
	private VehicleMotor motor;

	void Start () 
	{
		tilt = GetComponent< TiltReader >();
		motor = GetComponent< VehicleMotor >();
	}
	
	void Update () 
	{
		motor.inputTilt = tilt.GetRoll();
	}
}
