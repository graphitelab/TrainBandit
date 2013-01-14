using UnityEngine;
using System.Collections;

public class DevSwipeReader : MonoBehaviour 
{	
	void Update () 
	{
		// HACK - debug only
		if ( Application.isEditor )
		{
			if ( Input.GetAxisRaw( "Horizontal" ) >= 1f )
			{
				SendMessage( "OnSwipe", SwipeDirection.Right );
			}
			else if ( Input.GetAxisRaw( "Horizontal" ) <= -1f )
			{
				SendMessage( "OnSwipe", SwipeDirection.Left );
			}
			else if ( Input.GetButtonDown ( "Release Pressure" ) )
			{
				SendMessage( "OnSwipe", SwipeDirection.Down );
			}
			else if ( Input.GetButtonDown ( "Increase Speed" ) )
			{
				SendMessage( "OnSwipe", SwipeDirection.Up );
			}
//			else if ( Input.GetAxisRaw( "Vertical" ) >= 1f )
//			{
//				SendMessage( "OnSwipe", SwipeDirection.Up );
//			}
//			else if ( Input.GetAxisRaw( "Vertical" ) <= -1f )
//			{
//				SendMessage( "OnSwipe", SwipeDirection.Down );
//			}
		}
	}
}
