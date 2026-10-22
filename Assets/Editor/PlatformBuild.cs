using System;
using System.Linq;
using UnityEditor;
using UnityEditor.Build;
using UnityEditor.Build.Reporting;
using UnityEngine;

// Command-line player builds for the non-web platforms.
// Add -simulator for an iOS Simulator build, -development for a development build.
// Usage: Unity -batchmode -quit -projectPath . -buildTarget StandaloneOSX -executeMethod PlatformBuild.Build -buildPath Build/Mac/CardWars.app [-development]
public static class PlatformBuild
{
	public static void Build()
	{
		string path = GetArg("-buildPath");
		if (string.IsNullOrEmpty(path))
		{
			Debug.LogError("PlatformBuild: -buildPath is required");
			EditorApplication.Exit(2);
			return;
		}
		ConfigureMobile(EditorUserBuildSettings.activeBuildTarget);
		var options = new BuildPlayerOptions
		{
			scenes = EditorBuildSettings.scenes.Where(s => s.enabled).Select(s => s.path).ToArray(),
			locationPathName = path,
			target = EditorUserBuildSettings.activeBuildTarget,
			options = Environment.GetCommandLineArgs().Contains("-development") ? BuildOptions.Development : BuildOptions.None
		};
		BuildReport report = BuildPipeline.BuildPlayer(options);
		Debug.Log("PlatformBuild result: " + report.summary.result + " (" + options.target + "), errors: " + report.summary.totalErrors + ", size: " + report.summary.totalSize);
		EditorApplication.Exit(report.summary.result == BuildResult.Succeeded ? 0 : 1);
	}

	// Bundle identifier and Android backend; settings the project did not have. Change before publishing.
	private const string BundleId = "com.shishkabob.cardwars";

	private static void ConfigureMobile(BuildTarget target)
	{
		if (target == BuildTarget.Android)
		{
			PlayerSettings.SetApplicationIdentifier(NamedBuildTarget.Android, BundleId);
			PlayerSettings.SetScriptingBackend(NamedBuildTarget.Android, ScriptingImplementation.IL2CPP);
			PlayerSettings.Android.targetArchitectures = AndroidArchitecture.ARM64;
		}
		else if (target == BuildTarget.iOS)
		{
			PlayerSettings.SetApplicationIdentifier(NamedBuildTarget.iOS, BundleId);
			PlayerSettings.iOS.targetOSVersionString = "15.0";
			PlayerSettings.iOS.sdkVersion = Environment.GetCommandLineArgs().Contains("-simulator") ? iOSSdkVersion.SimulatorSDK : iOSSdkVersion.DeviceSDK;
		}
	}

	private static string GetArg(string name)
	{
		string[] args = Environment.GetCommandLineArgs();
		int i = Array.IndexOf(args, name);
		return (i >= 0 && i + 1 < args.Length) ? args[i + 1] : null;
	}
}
