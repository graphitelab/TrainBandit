using UnityEngine;
using System.Collections;

public class SwipeReader : MonoBehaviour 
{
#region editor vars
	
	public float minSwipePercent = 0.05f;
	
#endregion
	
	private const int MAX_TOUCHES = 5;
	private Vector2[] touchStart;
	
	void Start ()
	{
		touchStart = new Vector2[MAX_TOUCHES];
	}
	
	void Update () 
	{
		foreach (Touch touch in Input.touches)
		{
			int touchIndex = touch.fingerId;
			
			if ( touch.phase == TouchPhase.Began )
			{
				touchStart[touchIndex] = touch.position;
			}
			else if ( touch.phase == TouchPhase.Ended )
			{
				Vector2 swipe = touch.position - touchStart[touchIndex];
				
				// ignore zero-length swipes
				if ( swipe.magnitude == 0f )
					continue;
				
				SwipeDirection dir = SwipeDirection.FindClosestTo( swipe );
				if ( dir != SwipeDirection.Invalid )
				{
					SendMessage( "OnSwipe", dir );
				}
				else
				{
					Debug.LogWarning( "Could not find matching direction" );
				}
			}
		}
	}
}