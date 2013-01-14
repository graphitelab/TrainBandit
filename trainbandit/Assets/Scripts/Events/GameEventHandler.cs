using UnityEngine;
using System.Collections;

public delegate void GameEventDelegate( GameEvent evt );

public class GameEventHandler : Singleton< GameEventHandler >
{
	public ArrayList[] observers;
	
	public GameEventHandler ()
	{
		observers = new ArrayList[ ( int ) GameEventType.TotalEventTypes ];
	}
	
	public void AddObserver ( GameEventDelegate addDelegate, GameEventType type )
	{
		int typeInt = type.ToInt();
		
		if ( observers[ typeInt ] == null )
		{
			observers[ typeInt ] = new ArrayList();
		}
		
		observers[ typeInt ].Add( addDelegate );
	}
	
	public void RemoveObserver ( GameEventDelegate removeDelegate, GameEventType type )
	{
		int typeInt = type.ToInt();
		
		if ( observers[ typeInt ] == null )
			return;
		
		if ( observers[ typeInt ].Contains( removeDelegate ) )
		{
			observers[ typeInt ].Remove( removeDelegate );
		}
		
		if ( observers[ typeInt ].Count == 0 )
		{
			observers[ typeInt ] = null;
		}
	}
	
	public void SendEvent ( GameEvent evt )
	{
		int typeInt = evt.type.ToInt();
		
		if ( observers[ typeInt ] == null )
			return;
		
		ArrayList safeObservers = new ArrayList( observers[ typeInt ] );
		
		foreach ( GameEventDelegate observer in safeObservers ) 
		{
			observer( evt );
		}
	}
}
