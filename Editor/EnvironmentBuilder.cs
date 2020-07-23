using System.Collections.Generic;
using System.IO;
using Unity.MARS;
using Unity.XRTools.ModuleLoader;
using UnityEditor;
using UnityEngine;

namespace MARSViking
{
    public static class EnvironmentBuilder
    {

        private static List<string> GetEnvironmentNames()
        {
            var envPrefabs = AssetDatabase.FindAssets("t:prefab l:Environment");
            var envPrefabNames = new List<string>();
            foreach (var guid in envPrefabs)
            {
                var path = AssetDatabase.GUIDToAssetPath(guid);
                envPrefabNames.Add(Path.GetFileNameWithoutExtension(path));
            }

            return envPrefabNames;
        }

        //TODO: this needs to be generalized to be a kind of factory for environments
        public static void CreateEnvironment()
        {
            // NOTES:
            // env must have a ceiling
            // env must have simView and playmode camera position def
            // this function needs to access MARS internals to set the env manager to autosave prefab to avoid save overrides dialog

            var newEnv = new GameObject();
            var settings = newEnv.AddComponent<MARSEnvironmentSettings>();
            var mySceneView = SceneView.lastActiveSceneView;
            
            settings.EnvironmentInfo.EnvironmentBounds = new Bounds(new Vector3(0.99f, 5f, 1f), new Vector3(1f, 5f, 1f));

            mySceneView.camera.transform.position = new Vector3(-0.164f, 0.834f, -0.350f);
            mySceneView.camera.transform.rotation = new Quaternion(0.298f, 0.339f, -0.113f, 0.884f);
            // Simulation View camera 
            settings.SetDefaultEnvironmentCamera(mySceneView, true);
            // PlayMode camera
            settings.SetSimulationStartingPose(
                new Pose(new Vector3(2f, 2.6f, 2f), new Quaternion(-0.164f, 0.834f, -0.350f, -0.390f)), false);

            //TODO: below waiting on https://github.com/UnityLabs/com.unity.labs.mars/pull/1136
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
            //planeExtractionSettings.VoxelGenerationParams = vox;
            //planeExtractionSettings.PlaneFindingParams = voxFind;

            newEnv.name = "TestEnv";

            Mesh mesh = new Mesh();
            var content = new GameObject();
            content.name = "Floor";
            var meshRend = content.AddComponent<MeshRenderer>();
            meshRend.material = Material.GetDefaultMaterial();
            content.AddComponent<MeshFilter>();

            Vector3[] vertices = new Vector3[]
            {
                new Vector3(0, 0, 0),
                new Vector3(0, 0, 2),
                new Vector3(2, 0, 0),
                new Vector3(2, 0, 2)
            };
            int[] triangles = new int[]
            {
                0, 1, 2,
                1, 3, 2
            };


            mesh.vertices = vertices;
            mesh.triangles = triangles;
            mesh.RecalculateNormals();
            mesh.name = "Meshy";


            content.GetComponent<MeshFilter>().sharedMesh = mesh;
            AssetDatabase.CreateAsset(mesh, "Assets/floorMesh");
            AssetDatabase.SaveAssets();

            Mesh mesh2 = new Mesh();
            var content2 = new GameObject();
            content2.name = "Ceiling";
            content2.AddComponent<MeshRenderer>();
            content2.AddComponent<MeshFilter>();

            Vector3[] vertices2 = new Vector3[]
            {
                new Vector3(0, 10, 0),
                new Vector3(0, 10, 2),
                new Vector3(2, 10, 0),
                new Vector3(2, 10, 2)
            };
            int[] triangles2 = new int[]
            {
                0, 1, 2,
                1, 3, 2
            };


            mesh2.vertices = vertices2;
            mesh2.triangles = triangles2;
            mesh2.RecalculateNormals();
            mesh2.name = "Meshy";

            content2.GetComponent<MeshFilter>().sharedMesh = mesh2;
            AssetDatabase.CreateAsset(mesh2, "Assets/ceilingMesh");
            AssetDatabase.SaveAssets();

            content.transform.parent = newEnv.transform;
            content.layer = 3;
            content2.transform.parent = newEnv.transform;
            content2.layer = 3;

            string envlocalPath = "Assets/" + newEnv.name + ".prefab";

            envlocalPath = AssetDatabase.GenerateUniqueAssetPath(envlocalPath);
            PlaneGenerationModule.ExtractPlanes(planeExtractionSettings);

            PrefabUtility.SaveAsPrefabAsset(newEnv, envlocalPath);
            var asset = AssetDatabase.LoadMainAssetAtPath(envlocalPath);
            AssetDatabase.SetLabels(asset, new string[] {"Environment"});
            AssetDatabase.SaveAssets();

            Object.DestroyImmediate(newEnv, false);
        }

        //TODO: This needs to be generalized to delete any assets I created
        public static void RemoveTestEnvAssets()
        {
            if (AssetDatabase.FindAssets("floorMesh", new[] {"Assets"}).Length > 0)
            {
                AssetDatabase.DeleteAsset("Assets/floorMesh");
            }

            if (AssetDatabase.FindAssets("ceilingMesh", new[] {"Assets"}).Length > 0)
            {
                AssetDatabase.DeleteAsset("Assets/ceilingMesh");
            }

            if (AssetDatabase.FindAssets("testEnv", new[] {"Assets"}).Length > 0)
            {
                AssetDatabase.DeleteAsset("Assets/testEnv.prefab");
            }
        }

        public static void UseEnvironment(string testenv)
        {
            var environmentManager = ModuleLoaderCore.instance.GetModule<MARSEnvironmentManager>();
            //TODO: below line waiting on https://github.com/UnityLabs/com.unity.labs.mars/pull/1139
            //MARSEnvironmentManager.CurrentEnvironmentModifiedBehavior = EnvironmentModifiedBehavior.AutoSave;
            var names = EnvironmentBuilder.GetEnvironmentNames();
            var envIndex = names.FindIndex(env => env == testenv);
            environmentManager.TrySetupEnvironmentAndRestartSimulation(envIndex);
        }
    }
}
