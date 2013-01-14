using UnityEngine;
using System.Collections;

public abstract class TrackPath : MonoBehaviour 
{
	public abstract Transform StartTransform();
	public abstract Transform FinishTransform();
	public abstract Vector3 PointAt( float pct );
	public abstract float Length { get; }
	protected abstract bool IsValid();
	
	private Vector3 localDirVector;
	public Vector3 DirectionVector { get { return localDirVector; } }
	
	protected TrackPiece m_parentPiece;
	public TrackPiece ParentPiece { get { return m_parentPiece; } }
	
	public TrackPath[] nextPath;
	
	protected virtual void Awake()
	{
		m_parentPiece = FindParentPiece();
	}
	
	protected virtual void Start ()
	{
		// pieces spawn coupler points during Awake, so this step must wait until Start
		FindNextPaths();
	}
	
	public void CalcLocalDirectionVector()
	{
		// transform start and finish points from world space into local space
		Vector3 invStart = transform.InverseTransformPoint( StartTransform().position );
		Vector3 invFinish = transform.InverseTransformPoint( FinishTransform().position );
		
		localDirVector = invFinish - invStart;
	}
	
	public void SpawnCouplerPoint( Transform point )
	{
		GameObject go = point.gameObject;
		
		// ensure point belongs to the coupler layer
		go.layer = Constants.LAYER_COUPLER;
		
		// add a trigger collider to facilitate coupling via raycasts
		if ( go.GetComponent< BoxCollider >() == null )
		{
			BoxCollider bc = go.AddComponent( typeof( BoxCollider ) ) as BoxCollider;
			bc.isTrigger = true;
		}
		
		// create a PathPoint component for quick resolution of topmost parent path when raycasting
		if ( go.GetComponent< PathPoint >() == null )
		{
			PathPoint pp = go.AddComponent( typeof( PathPoint ) ) as PathPoint;
			pp.parentPath = this;
		}
	}
	
#region relational methods
	
	public void FindNextPaths()
	{
		Transform finish = FinishTransform();
		Vector3 direction = finish.forward;
		Vector3 origin = finish.position - direction;
		
		RaycastHit[] hits = Physics.RaycastAll( origin, direction, Constants.NEXT_PIECE_CAST_DISTANCE, Constants.LAYER_MASK_COUPLER );
		
		nextPath = new TrackPath[ hits.Length ];
		int i = 0;
		
		foreach ( RaycastHit hitInfo in hits )
		{
			GameObject go = hitInfo.collider.gameObject;
			PathPoint pp = go.GetComponent< PathPoint >();
			TrackPath path = pp.parentPath;
			
			if ( path == null )
				continue;
			
			nextPath[ i ] = path;
			i++;
		}
	}
	
	public TrackPath SelectNextPath( float tiltValue )
	{
		Vector3 tiltVector = new Vector3( tiltValue, 0, 1f );
		TrackPath bestPath = null;
		float bestDot = 0f;
		
		foreach ( TrackPath path in nextPath )
		{
			Vector3 pathVector = path.DirectionVector.normalized;
			float dot = Vector3.Dot ( tiltVector, pathVector );
			if ( dot > bestDot || bestPath == null )
			{
				bestPath = path;
				bestDot = dot;
			}
		}
		
		return bestPath;
	}
	
	public TrackPiece FindParentPiece()
	{
		Transform parent = transform.parent;
		
		TrackPath path = parent.GetComponent< TrackPath >();
		if ( path != null )
		{
			return path.FindParentPiece();
		}
		
		TrackPiece piece = parent.GetComponent< TrackPiece >();
		if ( piece != null )
		{
			return piece;
		}
		
		Debug.LogWarning ( "Path " + this + " could not find a parent piece." );
		return null;
	}
	
#endregion
}

