using System;
using System.Collections;
using System.Linq;
using NUnit.Framework;
using Unity.MARS;
using Unity.XRTools.ModuleLoader;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;
using Object = UnityEngine.Object;

// ReSharper disable once CheckNamespace
namespace MARSViking
{
    public class PlaymodeProxyTests
    {

        [SetUp]
        public void SetUp()
        {
            // Reset Layout to make sure no Views are open that shouldn't be
           EditorApplication.ExecuteMenuItem("Window/Layouts/Default");
           MarsEnvironments.CreateEnvironment();

           // Create a clean scene to test with
           try
           {
               EditorSceneManager.NewScene(NewSceneSetup.EmptyScene, NewSceneMode.Single);
           }
           // Ignore Exception thrown because we are in Editor mode running Playmode and not a Playmode test
           catch (InvalidOperationException e)
           {
               if (!e.Message.Contains("This cannot be used during play mode"))
               {
                   throw;
               }
           }
           
           // var environmentManager = ModuleLoaderCore.instance.GetModule<MARSEnvironmentManager>();
           // var names = MarsEnvironments.GetEnvironmentNames();
           // var envIndex = names.FindIndex(env => env == "Bedroom_12ftx12ft");
           // environmentManager.TrySetupEnvironmentAndRestartSimulation(envIndex);
        }

        [TearDown]
        public void TearDown()
        {
            MarsEnvironments.RemoveTestEnvAssets();
            // Remove used Test Scene
            try
            {
                EditorSceneManager.CloseScene(SceneManager.GetActiveScene(), true);
            }
            // Ignore Exception thrown because we are in Editor mode running Playmode and not a Playmode test
            catch (InvalidOperationException e)
            {
                if (!e.Message.Contains("This cannot be used during play mode"))
                {
                    throw;
                }
            }
        }
        
        [UnityTest]
        [Category("Use Case")]
        [NUnit.Framework.Property("TestRailId", "C576677")]
        public IEnumerator CanDisplayPrefabWhenProxyConditionIsMatched()
        {

            EditorApplication.ExecuteMenuItem(Constants.WindowsMenu.OpenSimulationView);
            
            var environmentManager = ModuleLoaderCore.instance.GetModule<MARSEnvironmentManager>();
            MARSEnvironmentManager.CurrentEnvironmentModifiedBehavior = EnvironmentModifiedBehavior.AutoSave;
            var names = MarsEnvironments.GetEnvironmentNames();
            var envIndex = names.FindIndex(env => env == "TestEnv");
            environmentManager.TrySetupEnvironmentAndRestartSimulation(envIndex);
            
            // Open Mars Panel next to Inspector Window
            // MARS Panel must be placed in position where it is fully extended so we can grab hold of UI elements
            var marsPanel = EditorWindow.GetWindow<MARSPanel>(typeof(InspectorWindow));
            if(marsPanel == null)
                Assert.Inconclusive(Constants.AssertionErrorMessages.NoMarsPanel);    
            
            // Create a new Proxy
            EditorApplication.ExecuteMenuItem(Constants.GameObjectMarsMenu.CreateProxy);
            var sceneGameObjects = Object.FindObjectsOfType<GameObject>().ToList();
            var myProxy =  sceneGameObjects.Find(
                x => x.name == Constants.HierarchyPanel.ProxyName
                );
            if(myProxy == null)
                Assert.Inconclusive(Constants.AssertionErrorMessages.NoProxy);
            
            // Add a Cube as a child of the proxy
            var cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
            cube.name = "MyTestCube";
            cube.transform.parent = myProxy.transform;
            myProxy.AddComponent<PlaneSizeCondition>();
            
            yield return new EnterPlayMode();

            // Find the Mars Camera and add an Empty gameObject as a parent so we can script the movement of the camera
            var marsCamera = GameObject.Find(Constants.HierarchyPanel.MarsCamera);
            var marsSession = GameObject.Find(Constants.HierarchyPanel.MarsSession);
            var emptyGameObject = new GameObject();
            marsCamera.transform.parent = emptyGameObject.transform;
            emptyGameObject.transform.parent = marsSession.transform;

            // Find our hidden Proxy in the GameView
            var myProxyInGame =  Object.FindObjectsOfType<Proxy>().ToArray().FirstOrDefault();
            if(myProxyInGame == null)
                Assert.Inconclusive(Constants.AssertionErrorMessages.NoProxyMatch);
            
            // Move camera in the room until the Proxy appears
            var counter = 0;
            while(counter < Constants.TestControls.QueryThreshold && myProxyInGame.queryState != QueryState.Tracking)
            {
                emptyGameObject.transform.position -= emptyGameObject.transform.up * 0.05f;
                yield return null;
                System.Threading.Thread.Sleep(500);
                counter++;
            }
            
            // Validate that the Proxy's gameObject appeared
            var cubeInGame = GameObject.Find("MyTestCube");
            Assert.IsTrue(cubeInGame);
            
            yield return new ExitPlayMode();
        }
    }
}
