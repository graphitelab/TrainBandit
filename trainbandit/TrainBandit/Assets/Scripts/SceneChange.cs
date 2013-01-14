using UnityEngine;
using System.Collections;

public class SceneChange : MonoBehaviour {
	
	public UIPanelAlpha loadingPanel;
	
	private int currentScene;
	
	void Start()
	{
		currentScene = Application.loadedLevel;
	}

	private void ChangeScene()
	{
		if(currentScene == 0)
		{
			loadingPanel.alpha = 1;
		}
		
		if(currentScene == 0)
			currentScene = 1;
		else
			currentScene = 0;
		
		Application.LoadLevel(currentScene);
	}
	
	private void ReloadLevel()
	{
		Application.LoadLevel( Application.loadedLevel );
	}
}
