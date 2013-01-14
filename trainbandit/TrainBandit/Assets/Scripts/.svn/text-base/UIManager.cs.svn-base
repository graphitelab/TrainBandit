using UnityEngine;
using System.Collections;

public class UIManager : MonoBehaviour {
	
	public UILabel scoreWidget;
	public UISprite steamWidget;
	public UILabel coinWidget;
	public UILabel healthWidget;
	public UILabel woodWidget;
	public UILabel dynamiteWidget;
	public GameObject speedNeedleWidget;
	
	private UIWidget steamColorWidget;
	
	private bool winCondition = false;
	
	public UIPanelAlpha deathPanel;
	public UILabel deathText;
	
	void Awake()
	{
		GameEventHandler.Instance.AddObserver( OnScoreUpdated, GameEventType.UpdateScore );
		GameEventHandler.Instance.AddObserver( OnSteamUpdated, GameEventType.UpdateSteam );
		GameEventHandler.Instance.AddObserver( OnCoinUpdated, GameEventType.UpdateCoin );
		GameEventHandler.Instance.AddObserver( OnHealthUpdated, GameEventType.UpdateHealth );
		GameEventHandler.Instance.AddObserver( OnWoodUpdated, GameEventType.UpdateWood );
		GameEventHandler.Instance.AddObserver( OnDynamiteUpdated, GameEventType.UpdateDynamite );
		GameEventHandler.Instance.AddObserver( OnSpeedUpdated, GameEventType.UpdateSpeed );
		
		GameEventHandler.Instance.AddObserver( OnWinCondition, GameEventType.WinCondition );
		
		GameEventHandler.Instance.AddObserver( OnPlayerDied, GameEventType.PlayerDied );
	}
	
	// Called when player dies.
	// Cleans up the observers and handles other events that happen at player's death.
	void OnPlayerDied( GameEvent e )
	{
		CleanupSubscriptions();
		
		string tempString;
		
		// TODO other end-of-game UI stuff
		if(winCondition)
			tempString = "You made it! Now do it again, but better.";
		else
		{
			switch((string)e.data)
			{
				case "deadend":
					tempString = "This isn't a magic train. It needs rails. There are no rails that way.";
					break;
				case "speed":
					tempString = "The coppers caught you. Next time, keep your speed up.";
					break;
				case "steam":
					tempString = "The boiler can't handle that much pressure. Try letting off some steam.";
					break;
				case "health":
					tempString = "Those rocks really cause some damage. Dodge them or blow them up.";			
					break;
				default:
					tempString = "You are dead. I wasn't paying attention to how you died, though.";
					break;
			}
		}
		
		deathText.text = tempString;
		deathPanel.alpha = 1;
		
	}
	
	// Called from inside OnPlayerDied().
	// Should contain the same subscriptions that are in Start().
	void CleanupSubscriptions()
	{
		GameEventHandler.Instance.RemoveObserver( OnScoreUpdated, GameEventType.UpdateScore );
		GameEventHandler.Instance.RemoveObserver( OnSteamUpdated, GameEventType.UpdateSteam );
		GameEventHandler.Instance.RemoveObserver( OnCoinUpdated, GameEventType.UpdateCoin );
		GameEventHandler.Instance.RemoveObserver( OnHealthUpdated, GameEventType.UpdateHealth );
		GameEventHandler.Instance.RemoveObserver( OnWoodUpdated, GameEventType.UpdateWood );
		GameEventHandler.Instance.RemoveObserver( OnDynamiteUpdated, GameEventType.UpdateDynamite );
		GameEventHandler.Instance.RemoveObserver( OnSpeedUpdated, GameEventType.UpdateSpeed );
		
		GameEventHandler.Instance.RemoveObserver( OnWinCondition, GameEventType.WinCondition );
		
		GameEventHandler.Instance.RemoveObserver( OnPlayerDied, GameEventType.PlayerDied );
	}
	
	// When Score is updated
	void OnScoreUpdated( GameEvent e )
	{
		int scoreValue = (int)e.data;
		
		if ( scoreWidget != null )
		{
			scoreWidget.text = scoreValue.ToString();
		}
		else
		{
			Debug.LogError ("Cannot update score widget because it has not been defined!");
		}
	}
	
	// When Steam is updated
	void OnSteamUpdated( GameEvent e )
	{		
		Vector3 steamValue = new Vector3(-0.62969f,(0.7933f * (float)e.data) -100.9138f,0f);
		
		//Debug.Log(steamWidget.transform.position.y);
		if (steamWidget != null)
		{
			steamWidget.transform.position = steamValue;
			
			if((float)e.data > 0.9f)
				steamWidget.color = Color.red;
			else
				steamWidget.color = Color.white;
		}
		else
			Debug.LogError("Cannot update steam widget because it has not been defined!");
	}
	
	// When Coins are updated
	void OnCoinUpdated( GameEvent e )
	{		
		int coinValue = (int)e.data;
		
		if (coinWidget != null)
			coinWidget.text = "Coins : " + coinValue;
		else
			Debug.LogError("Cannot update coin widget because it has not been defined!");
	}
	
	// When Health is updated
	void OnHealthUpdated( GameEvent e )
	{		
		int healthValue = (int)e.data;
		
		if (healthWidget != null)
			healthWidget.text = "Health : " + healthValue;
		else
			Debug.LogError("Cannot update health widget because it has not been defined!");
	}
	
	// When Wood is updated
	void OnWoodUpdated( GameEvent e )
	{		
		int woodValue = (int)e.data;
		
		if (woodWidget != null)
			woodWidget.text = "Wood : " + woodValue;
		else
			Debug.LogError("Cannot update wood widget because it has not been defined!");
	}
	
	// When Dynamite is updated
	void OnDynamiteUpdated( GameEvent e )
	{		
		int dynamiteValue = (int)e.data;
		
		if (dynamiteWidget != null)
			dynamiteWidget.text = "Dynamite : " + dynamiteValue;
		else
			Debug.LogError("Cannot update dynamite widget because it has not been defined!");
	}
	
	void OnSpeedUpdated( GameEvent e )
	{
		float speedValue = (-270f * (float)e.data) + 180f;
		
		if(speedNeedleWidget != null)
			speedNeedleWidget.transform.rotation = Quaternion.Euler(0f, 0f, speedValue);
		else
			Debug.LogError("Cannot update speed widget because it has not been defined!");
	}
	
	void OnWinCondition( GameEvent e )
	{
		winCondition = true;
	}
}
