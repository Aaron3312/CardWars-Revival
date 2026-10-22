using System.Collections;
using UnityEngine;

// Lets plain (non-MonoBehaviour) classes run coroutines, e.g. to wait on a web request without blocking the main thread.
public class WebCoroutineRunner : MonoBehaviour
{
	private static WebCoroutineRunner instance;

	public static void Run(IEnumerator routine)
	{
		if (instance == null)
		{
			GameObject gameObject = new GameObject("WebCoroutineRunner");
			Object.DontDestroyOnLoad(gameObject);
			instance = gameObject.AddComponent<WebCoroutineRunner>();
		}
		instance.StartCoroutine(routine);
	}
}
