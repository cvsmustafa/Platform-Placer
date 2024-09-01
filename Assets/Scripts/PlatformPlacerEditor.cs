using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(PlatformPlacer))]
public class PlatformPlacerEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        PlatformPlacer placer = (PlatformPlacer)target;

        if (GUILayout.Button("Create Platform Ends"))
        {
            placer.CreatePlatformEnds();
        }
    }
}
