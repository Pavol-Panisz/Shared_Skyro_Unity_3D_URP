using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(MapGenerator))]
public class MapGeneratorEditorScript : Editor
{
    public override void OnInspectorGUI()
    {
        MapGenerator mapGenerator = (MapGenerator)target;

        if (DrawDefaultInspector())
        {
            if (mapGenerator.autoUpdate)
            {
                mapGenerator.DrawMapInEditor();
            }
        }

        if (GUILayout.Button("Generate"))
        {
            mapGenerator.DrawMapInEditor();
        }
    }
}
