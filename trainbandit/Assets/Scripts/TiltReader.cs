using UnityEngine;
using System.Collections;

public class TiltReader : MonoBehaviour
{
	private int GetTiltIndex()
	{
		switch ( Application.platform )
		{
			case RuntimePlatform.Android:
				switch ( Screen.orientation )
				{
					default:
					case ScreenOrientation.Portrait:
						return 1;
					
					case ScreenOrientation.LandscapeLeft:
						return 0;
				}
			
			case RuntimePlatform.IPhonePlayer:
				switch ( Screen.orientation )
				{
					default:
					case ScreenOrientation.Portrait:
						return 0;
					
					case ScreenOrientation.LandscapeLeft:
						return 1;
				}
		}
		
		return 0;
	}
	
	public float GetRoll()
	{
		// HACK - development only
		if ( Application.isEditor )
		{
			if ( Input.GetKey( KeyCode.Q ) )
			{
				return -1f;
			}
			else if ( Input.GetKey( KeyCode.E ) )
			{
				return 1f;
			}
		}
		
		int tiltIndex = GetTiltIndex();
		float axisValue = Input.acceleration[ tiltIndex ];
		
		if ( Application.platform == RuntimePlatform.Android )
		{
			axisValue *= -1f;
		}
		
		return axisValue;
	}
}
