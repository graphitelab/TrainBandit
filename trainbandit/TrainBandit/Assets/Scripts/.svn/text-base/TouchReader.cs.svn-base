using UnityEngine;
using System.Collections;

public class TouchReader : MonoBehaviour 
{
	private const int MAX_TOUCHES = 5;
	
	Vector2[] startPosition;
	
	public Camera rayCam;
	
	private int layerMask;
	
	void Start () 
	{
		startPosition = new Vector2[MAX_TOUCHES];
		
		layerMask = 1 << Constants.LAYER_TOUCHABLE;
	}
	
	void Update () 
	{
		foreach (Touch touch in Input.touches)
		{
			int touchIndex = touch.fingerId;
			
			if (touch.phase == TouchPhase.Began)
			{
				startPosition[touchIndex] = touch.position;
			}
			else if (touch.phase == TouchPhase.Ended)
			{
				Ray ray = rayCam.ScreenPointToRay( touch.position );
				
				RaycastHit hit;
				if (Physics.Raycast(ray, out hit, Mathf.Infinity, layerMask))
				{
					OnObjectTouch( hit.collider.gameObject );
				}
			}
		}
	}
	
	void OnObjectTouch( GameObject go )
	{
		if(go.tag == "Rock")
		{
			go.GetComponent<RockExplosion>().BlowupRock();
		}
	}
}