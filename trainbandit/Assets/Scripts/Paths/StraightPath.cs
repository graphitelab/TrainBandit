using UnityEngine;
using System.Collections;

public class StraightPath : AtomicPath
{
	protected Vector3 deltaVector;
	
	protected override void Awake()
	{
		deltaVector = finish.position - start.position;
		
		base.Awake();
	}
	
	protected override float CalculateLength()
	{
		if ( IsValid() )
		{
			return ( deltaVector.magnitude );
		}
		
		return 0f;
	}
	
	public override Vector3 PointAt( float pct )
	{
		Vector3 point = start.position + ( deltaVector * pct );
		return point;
	}
	
	void OnDrawGizmos()
	{
		if ( !IsValid() )
			return;
		
		Transform start = StartTransform();
		Transform finish = FinishTransform();
		
		Gizmos.color = Color.magenta;
		Vector3 drawStart = start.position + Constants.kPathHeight;
		Vector3 drawFinish = finish.position + Constants.kPathHeight;
		Gizmos.DrawLine( drawStart, drawFinish );
		
		Gizmos.color = Color.green;
		Gizmos.DrawWireCube (drawStart, Constants.kWireSize);
		
		Gizmos.color = Color.red;
		Gizmos.DrawWireCube( drawFinish, Constants.kWireSize );
		Gizmos.DrawLine( drawFinish, drawFinish + finish.forward );
	}
}
