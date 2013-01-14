using UnityEngine;
using System.Collections;

public abstract class AtomicPath : TrackPath 
{
	public Transform start;
	public Transform finish;
	
	protected float length;
	
	protected abstract float CalculateLength();
	
	protected override void Awake()
	{
		base.Awake();
		
		length = CalculateLength();
	}
	
	public override Transform StartTransform()
	{
		return start;
	}
	
	public override Transform FinishTransform()
	{
		return finish;
	}
	
	public override float Length
	{
		get 
		{
			return length;
		}
	}
	
	protected override bool IsValid()
	{
		return ( start != null && finish != null );
	}
}
