using UnityEngine;
using System.Collections;

public class WinCondition : MonoBehaviour {
	
	void OnHitPlayer( Player player )
	{
		GameEventHandler.Instance.SendEvent( new GameEvent( GameEventType.WinCondition ) );
	}
}
