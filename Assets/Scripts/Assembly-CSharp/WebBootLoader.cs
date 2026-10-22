using System.Collections;
using System.IO;
using System.Text;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

public class WebBootLoader : MonoBehaviour
{
	public string nextScene = "StartupScene";

	private string status = "Loading...";

	private IEnumerator Start()
	{
#if UNITY_WEBGL && !UNITY_EDITOR
		string url = Application.streamingAssetsPath + "/" + WebStreamingAssets.BundleName;
		using (UnityWebRequest request = UnityWebRequest.Get(url))
		{
			request.SendWebRequest();
			while (!request.isDone)
			{
				status = "Loading game data " + Mathf.RoundToInt(request.downloadProgress * 100f) + "%";
				yield return null;
			}
			if (request.result != UnityWebRequest.Result.Success)
			{
				status = "Failed to load game data: " + request.error;
				Debug.LogError("[WebBoot] " + status + " (" + url + ")");
				yield break;
			}
			Unpack(request.downloadHandler.data, WebStreamingAssets.Root);
		}
#endif
		Debug.Log("[WebBoot] StreamingAssets ready at " + WebStreamingAssets.Root + ", loading " + nextScene);
		SceneManager.LoadScene(nextScene);
		yield break;
	}

	private void OnGUI()
	{
		GUI.Label(new Rect(20f, 20f, 600f, 30f), status);
	}

	private static void Unpack(byte[] data, string root)
	{
		int count = 0;
		using (BinaryReader reader = new BinaryReader(new MemoryStream(data), Encoding.UTF8))
		{
			int files = reader.ReadInt32();
			for (int i = 0; i < files; i++)
			{
				string relativePath = reader.ReadString();
				int length = reader.ReadInt32();
				byte[] bytes = reader.ReadBytes(length);
				string path = Path.Combine(root, relativePath);
				Directory.CreateDirectory(Path.GetDirectoryName(path));
				File.WriteAllBytes(path, bytes);
				count++;
			}
		}
		Debug.Log("[WebBoot] unpacked " + count + " files to " + root);
	}
}
