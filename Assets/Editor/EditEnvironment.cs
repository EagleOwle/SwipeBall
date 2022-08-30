using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(Environment))]
public class EditEnvironment : Editor
{
    public override void OnInspectorGUI()
    {
        base.DrawDefaultInspector();

         if (GUILayout.Button("Find SpawnPoint"))
        {
            (target as Environment).FindSpawnPoint();
        }
    }
}
