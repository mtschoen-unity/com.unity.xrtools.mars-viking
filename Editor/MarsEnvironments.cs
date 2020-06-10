using System.Collections;
using System.Collections.Generic;
using System.IO;
using Unity.MARS;
using UnityEditor;
using UnityEngine;

public class MarsEnvironments 
{

    public static List<string> GetEnvironmentNames()
    {
        
        var envPrefabs = AssetDatabase.FindAssets("t:prefab l:Environment");
        var EnvPrefabNames = new List<string>();
        foreach (var guid in envPrefabs)
        {
            var path = AssetDatabase.GUIDToAssetPath(guid);
           // m_EnvironmentPrefabPaths.Add(path);
            EnvPrefabNames.Add(Path.GetFileNameWithoutExtension(path));
        }

        return EnvPrefabNames;
    }

    public static GameObject CreateEnvironment()
    {
        var newEnv = new GameObject();
        var settings = newEnv.AddComponent<MARSEnvironmentSettings>();
        var sceneView = SceneView.lastActiveSceneView;
        settings.SetDefaultEnvironmentCamera(sceneView, false);
        
        newEnv.name = "TestEnv";

        Mesh mesh = new Mesh();
        var content = new GameObject();
        content.AddComponent<MeshRenderer>();
        content.AddComponent<MeshFilter>();
        
        Vector3[] vertices = new Vector3[]
        {
            new Vector3(0,0,0),
            new Vector3(0,0,4),
            new Vector3(4,0,0),
            new Vector3(4,0,4)
        };
        int[] triangles = new int[]
        {
            0,1,2,
            1,3,2
        };
        
        mesh.vertices = vertices;
        mesh.triangles = triangles;
        content.GetComponent<MeshFilter>().sharedMesh = mesh;

        mesh.RecalculateNormals();
        
        content.transform.parent = newEnv.transform;
        
        // Set the path as within the Assets folder,
        // and name it as the GameObject's name with the .Prefab format
        string localPath = "Assets/" + newEnv.name + ".prefab";

        // Make sure the file name is unique, in case an existing Prefab has the same name.
        localPath = AssetDatabase.GenerateUniqueAssetPath(localPath);
        

        // Create the new Prefab.
        PrefabUtility.SaveAsPrefabAssetAndConnect(newEnv, localPath, InteractionMode.UserAction);
        var asset = AssetDatabase.LoadMainAssetAtPath(localPath);
        AssetDatabase.SetLabels(asset, new string[]{"Environment"});
        

        return newEnv;
    }
    
}
