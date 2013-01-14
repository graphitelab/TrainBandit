using UnityEngine;
using System.Collections;

public class CurvePath : AtomicPath 
{
	public float angleStart;
	public float angleFinish;
	
	public float angleDelta	{ get { return angleFinish - angleStart; } }
	
	public float a;
	public float b;
	
	private const int CURVE_RESOLUTION = 8;
	
	protected override void Awake()
	{
		base.Awake();
	}
	
	protected override float CalculateLength()
	{
		if ( IsValid() )
		{
			// calculate circumference using Ramanujan's approximation from http://en.wikipedia.org/wiki/Ellipse#Circumference
			float circumference = Mathf.PI * ( 3 * ( a + b ) - Mathf.Sqrt( 10 * a * b + 3 * ( Mathf.Pow( a, 2 ) + Mathf.Pow ( b, 2 ) ) ) );
			
			float angleDelta = Mathf.Abs(angleFinish - angleStart);
			float percent = (angleDelta / 360f);
			float length = circumference * percent;
			return length;
		}
		
		return 0f;
	}
	
	private Vector3 PointOnCurve( float angleInDeg )
	{
		float angleInRad = angleInDeg * Mathf.Deg2Rad;
		
		float x = Mathf.Cos( angleInRad ) * a;
		float z = Mathf.Sin( angleInRad ) * b;
		
		Vector3 point = new Vector3( x, 0.0f, z );
		Vector3 absolutePoint = transform.TransformPoint( point );
		
		return absolutePoint;
	}
	
	public override Vector3 PointAt( float pct )
	{
		if ( pct > 1f )
		{
			return finish.position + finish.forward * 10f;
		}
		
		float angle = angleStart + ( angleDelta * pct );
		Vector3 point = PointOnCurve( angle );
		return point;
	}
	
	public bool IsReversed()
	{
		return ( angleStart > angleFinish );
	}
	
#region gizmos
	
	void OnDrawGizmos()
	{
		DrawPointCube( transform, Color.blue, true );
		
		Gizmos.color = Color.magenta;
		DrawPath( CURVE_RESOLUTION );
		
		DrawPointCube( start, Color.green, true );
		DrawPointCube( finish, Color.red, true );
	}
	
	void DrawPath( int numPoints )
	{
		float pctStep = 1f / (float)numPoints;
		
		for ( int i = 0; i < numPoints; i++ )
		{
			Vector3 start = PointAt ( pctStep * i ) + Constants.kPathHeight;
			Vector3 finish = PointAt ( pctStep * (i + 1) ) + Constants.kPathHeight;
			Gizmos.DrawLine( start, finish );
		}
	}
	
	void DrawPointCube( Transform point, Color color, bool drawFowardVector )
	{
		if ( point == null )
			return;
		
		Gizmos.color = color;
		Vector3 pos = point.position + Constants.kPathHeight;
		Gizmos.DrawWireCube( pos, Constants.kWireSize );
		
		if ( drawFowardVector )
		{
			Gizmos.DrawLine( pos, pos + point.forward );
		}
	}
	
#endregion
}
