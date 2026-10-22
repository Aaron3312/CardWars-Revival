using System.IO;
using UnityEngine;

// WebGL has no real StreamingAssets folder, and synchronous file reads cannot wait on a download.
// WebBootLoader unpacks one bundle (built by StreamingAssetsBundler) into the in-memory filesystem
// before the game starts, so the existing File/Directory code works on it unchanged.
public static class WebStreamingAssets
{
	public const string BundleName = "web_streaming_bundle.bin";

	public static string Root
	{
		get
		{
#if UNITY_WEBGL && !UNITY_EDITOR
			return Path.Combine(Application.temporaryCachePath, "StreamingAssets");
#else
			return Application.streamingAssetsPath;
#endif
		}
	}
}
