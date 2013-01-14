using UnityEngine;
using System.Collections;

// thanks to http://stackoverflow.com/a/4491859

public abstract class Singleton< ClassType >  where ClassType : new()
{
	static Singleton() {}
	
	private static readonly ClassType instance = new ClassType();
	
	public static ClassType Instance
	{
		get
		{
			return instance;
		}
	}
}
