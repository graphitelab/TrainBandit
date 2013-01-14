using UnityEngine;
using System.Collections;

public class GameEvent 
{
	public GameEventType type;
	public object data;
	
	public GameEvent ( GameEventType type )
	{
		this.type = type;
	}
	
	public GameEvent ( GameEventType type, object data )
	{
		this.type = type;
		this.data = data;
	}
}
