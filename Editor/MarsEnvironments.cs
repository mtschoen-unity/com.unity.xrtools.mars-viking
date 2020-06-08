using System.Collections;
using System.Collections.Generic;
using System.IO;
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
    
}
