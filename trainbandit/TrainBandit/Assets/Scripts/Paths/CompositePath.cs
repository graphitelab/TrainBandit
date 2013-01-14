using UnityEngine;
using System.Collections;

public class CompositePath : TrackPath 
{
	public TrackPath[] paths;
	
	private int numPaths;
	private float[] pathPcts;
	
	private float m_totalLength;
	
	protected override void Awake()
	{
		base.Awake();
		
		numPaths = paths.Length;
		
		pathPcts = new float[ numPaths ];
	}
	
	protected override void Start()
	{
		base.Start ();
		
		CalcPathPercents();
	}
	
	void CalcPathPercents()
	{
		for ( int i = 0; i < numPaths; i++ )
		{
			float length = paths[ i ].Length;
			pathPcts[ i ] = length;
			m_totalLength += length;
		}
		
		for ( int i = 0; i < numPaths; i++ )
		{
			pathPcts[ i ] /= m_totalLength;
//			Debug.Log ( "Subpath " + i + " has length " + paths[ i ].Length + " (" + pathPcts[ i ] + ")");
		}
		
//		Debug.Log ( "Composite path " + this + " has length " + m_totalLength );
	}
	
	public override Transform StartTransform()
	{
		return ( paths[0].StartTransform() );
	}
	
	public override Transform FinishTransform()
	{
		int lastIndex = (paths.Length - 1);
		return ( paths[ lastIndex ].FinishTransform() );
	}
	
	protected override bool IsValid()
	{
		return ( paths.Length > 0 );
	}
	
	public override float Length
	{
		get 
		{
			if ( !IsValid() )
				return 0f;
			
			return m_totalLength;
		}
	}
	
	public override Vector3 PointAt( float pct )
	{
		for ( int i = 0; i < numPaths; i++ )
		{
			float subpathPct = pathPcts[ i ];
			
			if ( pct <= subpathPct || i == ( numPaths - 1 ) )
			{
				float relativePct = pct / pathPcts[ i ];
				return paths[ i ].PointAt( relativePct );
			}
			
			pct -= subpathPct;
		}
		
		return Vector3.zero;
	}
}
