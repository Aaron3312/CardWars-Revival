using System.Linq;
using UnityEditor;
using UnityEditor.Build.Reporting;
using UnityEngine;

public static class WebGLBuild
{
	// Usage: Unity -batchmode -quit -buildTarget WebGL -executeMethod WebGLBuild.Build
	public static void BuildDev()
	{
		PlayerSettings.WebGL.exceptionSupport = WebGLExceptionSupport.FullWithStacktrace;
		UnityEditor.Compilation.CompilationPipeline.codeOptimization = UnityEditor.Compilation.CodeOptimization.Release;
		Run("Build/WebGLDev", BuildOptions.Development);
	}

	public static void Build()
	{
		Run("Build/WebGL", BuildOptions.None);
	}

	private static void Run(string path, BuildOptions buildOptions)
	{
		string[] scenes = EditorBuildSettings.scenes.Where(s => s.enabled).Select(s => s.path).ToArray();
		var options = new BuildPlayerOptions
		{
			scenes = scenes,
			locationPathName = path,
			target = BuildTarget.WebGL,
			options = buildOptions
		};
		BuildReport report = BuildPipeline.BuildPlayer(options);
		Debug.Log("WebGL build result: " + report.summary.result + ", errors: " + report.summary.totalErrors + ", size: " + report.summary.totalSize);
		EditorApplication.Exit(report.summary.result == BuildResult.Succeeded ? 0 : 1);
	}
}
