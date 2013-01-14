using UnityEngine;
using System.Collections;

public class Obstacle : MonoBehaviour 
{
	void OnHitPlayer( Player player )
	{
		if(gameObject.tag == "Rock")
		{
			player.ReduceHealth(20);
			if( player.GetHealth() != 0 )
				GameObject.Destroy(gameObject);
		}
		else if(gameObject.tag == "Coin")
		{
			player.AddCoins();
			GameObject.Destroy(gameObject);
		}
		else if(gameObject.tag == "Dynamite")
		{
			player.AddDynamite();
			GameObject.Destroy(gameObject);
		}
		else if(gameObject.tag == "Wood")
		{
			player.AddWood();
			GameObject.Destroy(gameObject);
		}
	}
}
