using UnityEngine;
using UnityEngine.SceneManagement;

// Logs scene loads and uncaught errors so a stuck web build shows where it stopped.
public static class WebDiagnostics
{
	[RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
	private static void Init()
	{
		SceneManager.sceneLoaded += delegate(Scene scene, LoadSceneMode mode)
		{
			UnityEngine.Debug.Log("[WebDiag] scene loaded: " + scene.name + " (" + mode + ") at t=" + Time.realtimeSinceStartup);
		};
	}
}
