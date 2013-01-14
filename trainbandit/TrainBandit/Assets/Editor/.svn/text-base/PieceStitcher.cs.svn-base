using UnityEngine;
using UnityEditor;
using System.Collections;

[ExecuteInEditMode]
public class PieceStitcher : EditorWindow 
{
	Object fromPath;
	Object attachObject;
	Object fromObject;
	Object lastFromObject;
	
	const string WINDOW_TITLE = "Connect Pieces";
	
	bool advanceToAttach;
	
	[MenuItem( "Window/" + WINDOW_TITLE )]
	static void ShowWindow() 
	{
		EditorWindow window = EditorWindow.GetWindow( typeof( PieceStitcher ), false, WINDOW_TITLE );
		window.Show ();
	}
	
	void OnGUI()
	{
		GUILayout.Label( "Connect Pieces", EditorStyles.boldLabel );
		
		fromObject = EditorGUILayout.ObjectField( "From", fromObject, typeof( TrackPiece ), true );
		fromPath = EditorGUILayout.ObjectField( "Path", fromPath, typeof( TrackPath ), true );
		
		attachObject = EditorGUILayout.ObjectField( "Attach", attachObject, typeof( TrackPiece ), true );
		
		advanceToAttach = EditorGUILayout.Toggle( "Advance", advanceToAttach );
		
		if ( GUILayout.Button ( "Connect" ) )
		{
			TrackPiece fromPiece = fromObject as TrackPiece;
			TrackPiece attachPiece = attachObject as TrackPiece;
			
			if ( attachPiece != null && fromPiece != null )
			{
				attachPiece.AttachTo( fromPath as TrackPath );
				
				if ( advanceToAttach )
				{
					fromObject = attachObject;
					RefreshFromObject();
					attachObject = null;
				}
			}
		}
		
		if ( GUI.changed )
		{
			RefreshFromObject();
		}
	}
	
	void RefreshFromObject()
	{
		if ( fromObject != lastFromObject )
		{
			fromPath = null;
			lastFromObject = fromObject;
		}
		
		if ( fromObject != null && fromPath == null )
		{
			fromPath = (fromObject as TrackPiece).GetFirstPath();
		}
	}
}
