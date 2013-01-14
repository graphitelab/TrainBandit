using UnityEngine;

public class SwipeDirection
{
	private const float DOWN_THRESHOLD = 0.6f;
	private const float DOT_THRESHOLD = 0.9f;
	
	public string name;
	public Vector3 vector;
	public float dotThreshold;
	
	private SwipeDirection( string name, Vector3 directionVector, float dotThreshold )
	{
		this.name = name;
		this.vector = directionVector;
		this.dotThreshold = dotThreshold;
	}
	
	public static SwipeDirection FindClosestTo( Vector2 swipe )
	{
		Vector2 swipeNorm = swipe.normalized;
		
		float bestDot = 0f;
		SwipeDirection bestDir = SwipeDirection.Invalid;
		
		foreach ( SwipeDirection dir in findOrder )
		{
			float currentDot = Vector2.Dot ( swipeNorm, dir.vector );
			if ( currentDot > bestDot )
			{
				bestDot = currentDot;
				bestDir = dir;
			}
		}
		
		return bestDir;
	}
	
#region static directions
	public static SwipeDirection Invalid = new SwipeDirection( "Invalid", Vector3.zero, 1f );
	public static SwipeDirection Up = new SwipeDirection( "Up", Vector3.up, DOT_THRESHOLD );
	public static SwipeDirection Down = new SwipeDirection( "Down", Vector3.down, DOWN_THRESHOLD );
	public static SwipeDirection Left = new SwipeDirection( "Left", Vector3.left, DOT_THRESHOLD );
	public static SwipeDirection Right = new SwipeDirection( "Right", Vector3.right, DOT_THRESHOLD );
	
	public static SwipeDirection[] findOrder = { SwipeDirection.Down, SwipeDirection.Left, SwipeDirection.Right, SwipeDirection.Up };
#endregion
}
