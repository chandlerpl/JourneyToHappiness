using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.Build.Reporting;
using UnityEngine;

public class BuildManager
{
    [MenuItem("Build/Build WebGL")]
    public static void PerformWebGLBuild()
    {
        BuildPlayerOptions buildPlayerOptions = new BuildPlayerOptions();
        buildPlayerOptions.locationPathName = "build/WebGL";
        buildPlayerOptions.target = BuildTarget.WebGL;
        buildPlayerOptions.options = BuildOptions.None;

        BuildReport report = BuildPipeline.BuildPlayer(buildPlayerOptions);
        CheckResult(report.summary);
    }
    
    [MenuItem("Build/Build Windows")]
    public static void PerformWindowsBuild()
    {
        BuildPlayerOptions buildPlayerOptions = new BuildPlayerOptions();
        buildPlayerOptions.locationPathName = "build/Windows";
        buildPlayerOptions.target = BuildTarget.StandaloneWindows64;
        buildPlayerOptions.options = BuildOptions.None;

        BuildReport report = BuildPipeline.BuildPlayer(buildPlayerOptions);
        CheckResult(report.summary);
    }

    private static void CheckResult(BuildSummary summary)
    {
        switch (summary.result)
        {
            case BuildResult.Succeeded:
                Debug.Log("Build Succeeded: " + summary.totalSize + " bytes - time: " + summary.totalTime.TotalSeconds + " seconds.");
                break;
            case BuildResult.Failed:
                Debug.Log("Build Failed - Errors:" + summary.totalErrors);
                break;
            default:
                Debug.Log("Unknown problem.");
                break;
        }
    }
}

