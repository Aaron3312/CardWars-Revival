using System.IO;
using System.Text;
using UnityEditor;
using UnityEditor.Build;
using UnityEditor.Build.Reporting;
using UnityEditor.SceneManagement;
using UnityEngine;
using System.Linq;

// Packs Assets/StreamingAssets into a single file for WebBootLoader, so the web build fetches it with one request.
public class StreamingAssetsBundler : IPreprocessBuildWithReport
{
	public int callbackOrder { get { return 0; } }

	public void OnPreprocessBuild(BuildReport report)
	{
		if (report.summary.platform == BuildTarget.WebGL)
		{
			Pack();
		}
	}

	[MenuItem("Tools/Web/Pack StreamingAssets")]
	public static void Pack()
	{
		string root = Path.Combine(Application.dataPath, "StreamingAssets");
		string bundlePath = Path.Combine(root, WebStreamingAssets.BundleName);
		if (File.Exists(bundlePath))
		{
			File.Delete(bundlePath);
		}
		string[] files = Directory.GetFiles(root, "*", SearchOption.AllDirectories)
			.Where(f => !f.EndsWith(".meta") && !f.EndsWith(".DS_Store") && !f.StartsWith(bundlePath))
			.OrderBy(f => f, System.StringComparer.Ordinal)
			.ToArray();
		using (BinaryWriter writer = new BinaryWriter(File.Create(bundlePath), Encoding.UTF8))
		{
			writer.Write(files.Length);
			foreach (string file in files)
			{
				byte[] bytes = File.ReadAllBytes(file);
				writer.Write(file.Substring(root.Length + 1).Replace('\\', '/'));
				writer.Write(bytes.Length);
				writer.Write(bytes);
			}
		}
		Debug.Log("[WebBoot] packed " + files.Length + " StreamingAssets files into " + WebStreamingAssets.BundleName);
	}

	// Creates Assets/Scene/Scenes/BootScene.unity and makes it the first scene in the build.
	public static void CreateBootScene()
	{
		const string path = "Assets/Scene/Scenes/BootScene.unity";
		var scene = EditorSceneManager.NewScene(NewSceneSetup.EmptyScene, NewSceneMode.Single);
		var cameraObject = new GameObject("Camera");
		var camera = cameraObject.AddComponent<Camera>();
		camera.clearFlags = CameraClearFlags.SolidColor;
		camera.backgroundColor = Color.black;
		new GameObject("WebBootLoader").AddComponent<WebBootLoader>();
		EditorSceneManager.SaveScene(scene, path);
		var scenes = EditorBuildSettings.scenes.Where(s => s.path != path).ToList();
		scenes.Insert(0, new EditorBuildSettingsScene(path, true));
		EditorBuildSettings.scenes = scenes.ToArray();
		AssetDatabase.SaveAssets();
	}
}
