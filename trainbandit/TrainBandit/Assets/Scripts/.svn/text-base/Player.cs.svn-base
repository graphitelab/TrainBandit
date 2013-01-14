using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour 
{
	private Transform anchor;
	
	public int initialWood = 5;
	public int woodGivenPerUnit = 3;
	public int initialDynamite = 3;
	public int dynamiteGivenPerUnit = 2;
	
	private int score;
	private int health;
	private int coins;
	private int dynamite;
	private int wood;
	
	private string formOfDeath = "";
	
	private bool alive = true;
	
	void Start () 
	{
		Reset();
	}
	
	void Update () 
	{
		if(alive)
		{
			SnapToAnchor();
			
			AddScore( );
		}
				
	}
	
	void Reset ()
	{
		InitializeScore();
		
		InitializeHealth();
		
		InitializeCoins();
		InitializeDynamite();
		InitializeWood();
		
		alive = true;
	}
	
	void OnTriggerEnter( Collider other )
	{
		GameObject go = other.gameObject;
		
		if ( go == null )
			return;
		
		go.SendMessage( "OnHitPlayer", this, SendMessageOptions.DontRequireReceiver );
	}
	
	void OnCollisionEnter( Collision collision )
	{
		Kill ();
	}
	
	public void FormOfDeath( string coroner )
	{
		formOfDeath = coroner;
	}
	
#region Score Accessors
	// Initialize Score to Zero.
	public void InitializeScore()
	{
		score = 0;
		
		GameEventHandler.Instance.SendEvent( new GameEvent( GameEventType.UpdateScore, score ) );
	}
	
	public void AddScore()
	{
		score++;
		
		GameEventHandler.Instance.SendEvent( new GameEvent( GameEventType.UpdateScore, score ) );
	}
	
	public void AddScore( int delta )
	{
		score += delta;
		
		GameEventHandler.Instance.SendEvent( new GameEvent( GameEventType.UpdateScore, score ) );
	}
	
	public int GetScore()
	{
		return score;
	}
#endregion
	
#region Health Accessors
	// Initialize Health to 100.
	public void InitializeHealth()
	{
		health = 100;
		
		GameEventHandler.Instance.SendEvent( new GameEvent( GameEventType.UpdateHealth, health ) );
	}
	
	// Subtracts 20 points from health
	public void ReduceHealth()
	{
		health -= 20;
		if(health <= 0)
		{
			formOfDeath = "health";
			Kill();
		}
		
		GameEventHandler.Instance.SendEvent( new GameEvent( GameEventType.UpdateHealth, health ) );
	}
	
	// Subtracts health equal to the parameter value
	public void ReduceHealth( int damage )
	{
		health -= damage;
		if(health <= 0)
		{
			formOfDeath = "health";
			Kill ();
		}
		
		GameEventHandler.Instance.SendEvent( new GameEvent( GameEventType.UpdateHealth, health ) );
	}
	
	// Returns current Health value.
	public int GetHealth()
	{
		return health;
	}
#endregion
	
#region Coin Accessors
	// Initialize Coins to Zero.
	public void InitializeCoins()
	{
		coins = 0;
		
		GameEventHandler.Instance.SendEvent( new GameEvent( GameEventType.UpdateCoin, coins ) );
	}
	
	// Adds one coin
	public void AddCoins()
	{
		coins++;	
		
		GameEventHandler.Instance.SendEvent( new GameEvent( GameEventType.UpdateCoin, coins ) );
	}
	// Adds coins equal to parameter value
	public void AddCoins( int money )
	{
		coins += money;	
		
		GameEventHandler.Instance.SendEvent( new GameEvent( GameEventType.UpdateCoin, coins ) );
	}
	public int GetCoins()
	{
		return coins;	
	}
	#endregion
	
#region Dynamite Accessors
	// Initialize Dynamite to initialDynamite.
	public void InitializeDynamite()
	{
		dynamite = initialDynamite;
		
		GameEventHandler.Instance.SendEvent( new GameEvent( GameEventType.UpdateDynamite, dynamite ) );
	}
	
	/// <summary>
	/// Adds a unit of dynamite.
	/// </summary>
	public void AddDynamite()
	{
		dynamite += dynamiteGivenPerUnit;
		
		GameEventHandler.Instance.SendEvent( new GameEvent( GameEventType.UpdateDynamite, dynamite ) );
	}
	/// <summary>
	/// Subtracts a unit of dynamite.
	/// </summary>
	public void SubtractDynamite()
	{
		if(dynamite >= 1)
			dynamite--;
		
		GameEventHandler.Instance.SendEvent( new GameEvent( GameEventType.UpdateDynamite, dynamite ) );
	}
	/// <summary>
	/// Gets the number of dynamite.
	/// </summary>
	/// <returns>
	/// The number of dynamite.
	/// </returns>
	public int GetDynamite()
	{
		return dynamite;	
	}
#endregion
	
#region Wood Accessors
	// Initialize Wood to initialWood.
	public void InitializeWood()
	{
		wood = initialWood;
		
		GameEventHandler.Instance.SendEvent( new GameEvent( GameEventType.UpdateWood, wood ) );
	}
	
	
	/// <summary>
	/// Adds a unit of wood.
	/// </summary>
	public void AddWood()
	{
		wood += woodGivenPerUnit;	
		
		GameEventHandler.Instance.SendEvent( new GameEvent( GameEventType.UpdateWood, wood ) );
	}
	/// <summary>
	/// Subtracts a unit of wood.
	/// </summary>
	public void SubtractWood()
	{
		if(wood >= 1)
			wood--;
		
		GameEventHandler.Instance.SendEvent( new GameEvent( GameEventType.UpdateWood, wood ) );
	}
	/// <summary>
	/// Gets the number of wood.
	/// </summary>
	/// <returns>
	/// The number of wood.
	/// </returns>
	public int GetWood()
	{
		return wood;	
	}
#endregion
	
	public void AttachTo( Transform anchor )
	{
		this.anchor = anchor;
		
		SnapToAnchor();
		
		transform.gameObject.SetActiveRecursively( true );
	}
	
	public void Hide ()
	{
		transform.gameObject.SetActiveRecursively( false );
	}
	
	void SnapToAnchor ()
	{
		if ( anchor == null )
			return;
	
		transform.position = anchor.position;
		transform.rotation = anchor.rotation;
	}
	
	public void Kill()
	{
		// get busy living...
		if ( !alive )
			return;
		
		// or get busy dying
		alive = false;
		
		gameObject.GetComponent<VehicleMotor>().StopLocomotion();
		gameObject.GetComponent<SteamPressure>().StopSteam();
		
		if ( anchor != null )
		{
			transform.position += anchor.up * 0.5f;
			this.anchor = null;
		}
		
		transform.parent = null;
		
		GameEventHandler.Instance.SendEvent( new GameEvent( GameEventType.PlayerDied, formOfDeath ) );
	}
	
	public void Restart()
	{
		if(alive)
			return;
		else
		{
			alive = true;
			Application.LoadLevel( Application.loadedLevel );
		}
	}
	
	public bool IsAlive()
	{
		return alive;
	}
}
