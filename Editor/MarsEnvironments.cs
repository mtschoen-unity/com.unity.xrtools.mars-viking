using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Unity.MARS;
using UnityEditor;
using UnityEditor.VersionControl;
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
    
    public static void CreateEnvironment()
    {
        //TODO: PlanesExtractionManager.cs must be made public
        //TODO: PlaneExtractionSettings.cs must have setters for it's properties
        //TODO: SimEnv must have a ceiling or some depth to extract planes
        var newEnv = new GameObject();
        var settings = newEnv.AddComponent<MARSEnvironmentSettings>();
        var sceneView = SceneView.lastActiveSceneView;
        settings.SetDefaultEnvironmentCamera(sceneView, false);
        
        var planeExtractionSettings = newEnv.GetComponent<PlaneExtractionSettings>();
        
        var vox = new PlaneVoxelGenerationParams()
        {
            raycastSeed = 0,
            raycastCount = 304800,
            maxHitDistance = 10,
            normalToleranceAngle = 15,
            voxelSize = 0.1f,
            outerPointsThreshold = 0.25f
        };

        var voxFind = new VoxelPlaneFindingParams()
        {
            minPointsPerSqMeter = 500,
            minSideLength = 0.15f,
            inLayerMergeDistance = 0.4f,
            crossLayerMergeDistance = 0.05f,
            checkEmptyArea = true,
            allowedEmptyAreaCurve = new AnimationCurve(new Keyframe(0, 0), new Keyframe(1, 1))
        };
        planeExtractionSettings.VoxelGenerationParams = vox;
        planeExtractionSettings.PlaneFindingParams = voxFind;

        newEnv.name = "TestEnv";

        Mesh mesh = new Mesh();
        var content = new GameObject();
        content.name = "Floor";
        content.AddComponent<MeshRenderer>();
        content.AddComponent<MeshFilter>();
        
        Vector3[] vertices = new Vector3[]
        {
            new Vector3(0,0,0),
            new Vector3(0,0,2),
            new Vector3(2,0,0),
            new Vector3(2,0,2)
        };
        int[] triangles = new int[]
        {
            0,1,2,
            1,3,2
        };
        
        
        mesh.vertices = vertices;
        mesh.triangles = triangles;
        mesh.RecalculateNormals();
        mesh.name = "Meshy";
        
        content.GetComponent<MeshFilter>().sharedMesh = mesh;
        AssetDatabase.CreateAsset(mesh,"Assets/floorMesh");
        AssetDatabase.SaveAssets();
        
        Mesh mesh2 = new Mesh();
        var content2 = new GameObject();
        content2.name = "Ceiling";
        content2.AddComponent<MeshRenderer>();
        content2.AddComponent<MeshFilter>();
        
        Vector3[] vertices2 = new Vector3[]
        {
            new Vector3(0,10,0),
            new Vector3(0,10,2),
            new Vector3(2,10,0),
            new Vector3(2,10,2)
        };
        int[] triangles2 = new int[]
        {
            0,1,2,
            1,3,2
        };
        
        
        mesh2.vertices = vertices2;
        mesh2.triangles = triangles2;
        mesh2.RecalculateNormals();
        mesh2.name = "Meshy";
        
        content2.GetComponent<MeshFilter>().sharedMesh = mesh2;
        AssetDatabase.CreateAsset(mesh2,"Assets/ceilingMesh");
        AssetDatabase.SaveAssets();

        content.transform.parent = newEnv.transform;
        content2.transform.parent = newEnv.transform;

        string envlocalPath = "Assets/" + newEnv.name + ".prefab";

        envlocalPath = AssetDatabase.GenerateUniqueAssetPath(envlocalPath);
        PlanesExtractionManager.ExtractPlanes(planeExtractionSettings);
        
        PrefabUtility.SaveAsPrefabAsset(newEnv, envlocalPath);
        var asset = AssetDatabase.LoadMainAssetAtPath(envlocalPath);
        AssetDatabase.SetLabels(asset, new string[]{"Environment"});
        
    }

    public static void RemoveTestEnvAssets()
    {
        AssetDatabase.DeleteAsset("Assets/floorMesh");
        AssetDatabase.DeleteAsset("Assets/ceilingMesh");
        AssetDatabase.DeleteAsset("Assets/testEnv.prefab");
    }

    [MenuItem("jason/save")]
    public static void ApplyOverrides()
    {
        var asset = PrefabUtility.LoadPrefabContents("Assets/TestEnv.prefab");
        PrefabUtility.SaveAsPrefabAsset(asset, "Assets/TestEnv.prefab");
        
    }

    [MenuItem("jason/create")]
    public static void Create()
    {
        CreateEnvironment();
    }
}
