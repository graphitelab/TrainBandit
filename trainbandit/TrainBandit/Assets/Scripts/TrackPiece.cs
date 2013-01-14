using UnityEngine;
using System.Collections;

public class TrackPiece : MonoBehaviour 
{
	public TrackPath[] paths;
	
	void Awake()
	{
		//
		// instruct each child path to generate a coupler at its start point
		//
		// having this inside CompositePath or AtomicPath would generate far more couplers than we actually need,
		// whereas here we produce only n coupler points, where n is the number of child paths defined for this piece
		//
		foreach ( TrackPath path in paths )
		{
			path.SpawnCouplerPoint( path.StartTransform() );
		}
	}
	
	void Start()
	{
		foreach ( TrackPath path in paths )
		{
			path.CalcLocalDirectionVector();
		}
	}
	
	public TrackPath GetFirstPath()
	{
		return paths[ 0 ];
	}
	
#region editor functions
	
	public void AttachTo ( TrackPath otherPath )
	{
		Transform otherTransform = otherPath.FinishTransform();
		
		transform.rotation = otherTransform.rotation;
		
		if ( paths.Length > 0 )
		{
			TrackPath firstPath = GetFirstPath();
			
			Vector3 toOther = otherTransform.position - firstPath.StartTransform().position;
			transform.position += toOther;
		}
		else
		{
			transform.position = otherTransform.position;
		}
	}
	
#endregion
}
