using UnityEngine;
using UnityEditor;
using System.Collections;

[CustomEditor( typeof( CurvePath ))]
public class CurvePathEditor : Editor 
{
	CurvePath path;
	
	public void OnEnable()
	{
		path = target as CurvePath;
	}
	
	public override void OnInspectorGUI()
	{
		path.a = EditorGUILayout.FloatField( "Horizontal", path.a );
		path.b = EditorGUILayout.FloatField( "Vertical", path.b );
		path.angleStart = EditorGUILayout.FloatField( "Start Angle", path.angleStart );
		path.angleFinish = EditorGUILayout.FloatField( "Finish Angle", path.angleFinish );
		
		if ( GUILayout.Button ( "Generate" ) )
		{
			GenerateTerminalPoints();
			
			EditorUtility.SetDirty( target );
		}
		
		EditorGUILayout.ObjectField("Start", path.start, typeof( Transform ), true);
		EditorGUILayout.ObjectField("Finish", path.finish, typeof( Transform ), true);
		
		if ( GUI.changed )
		{
			EditorUtility.SetDirty( target );
		}
	}
	
	Transform GeneratePathPoint( string name, float pathPct )
	{
		Transform t = path.gameObject.transform.Find ( name );
		
		if ( t != null )
		{
			DestroyImmediate( t.gameObject );
		}
		
		GameObject go = new GameObject( name );
		
		go.transform.position = path.PointAt( pathPct );
		go.transform.rotation = path.gameObject.transform.rotation;
		go.transform.parent = path.gameObject.transform;
		
		return go.transform;
	}
	
	void GenerateTerminalPoints()
	{
		path.start = GeneratePathPoint( "Start", 0f );
		
		Transform finish = GeneratePathPoint( "Finish", 1f );
		
		float angle = -1 * path.angleDelta;
		finish.RotateAround( finish.position, Vector3.up, angle );
		path.finish = finish;
	}
}
