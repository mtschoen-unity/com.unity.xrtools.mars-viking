using System;
using System.Collections;
using System.Linq;
using NUnit.Framework;
using Unity.Labs.MARS;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;

namespace MARSViking
{
    public class PlaymodeProxyTests
    {
        private int QueryThreshold = 60;
        
        [SetUp]
        public void SetUp()
        {
            // Reset Layout to make sure no Views are open that shouldn't be
           EditorApplication.ExecuteMenuItem("Window/Layouts/Default");
           
           // Make sure Playmode is exited
           // if (EditorApplication.isPlaying)
           // {
           //     EditorApplication.ExitPlaymode();
           //     //EditorApplication.ExecuteMenuItem("Edit/Play");
           // }

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

        }

        [TearDown]
        public void TearDown()
        {
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
            var marsPanel = EditorWindow.GetWindow<MARSPanel>(typeof(InspectorWindow));
            
            EditorApplication.ExecuteMenuItem(Constants.MarsGameObjectMenu.ProxyMenuPath);
            var goAll = Resources.FindObjectsOfTypeAll(typeof(GameObject)).ToList();
            var myProxy =  goAll.Find(x => x.name == Constants.HierarchyPanel.ProxyGameObjectName) as GameObject;
            Assert.IsTrue(myProxy, "Could not find Proxy in Scene");
            
            GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
            cube.name = "MyTestCube";
            cube.transform.parent = myProxy.transform;
            myProxy.AddComponent<PlaneSizeCondition>();
            yield return new EnterPlayMode();
            var gameWindow = Resources.FindObjectsOfTypeAll<GameView>().FirstOrDefault();
            using (var window = new MarsAutomatedWindow<GameView>(gameWindow))
            {
                var marsCamera = GameObject.Find("MARS Session");
                var empty = new GameObject();
                marsCamera.transform.parent = empty.transform;
                yield return null;
                
                var proxyObj = GameObject.Find("MyTestCube");
                var myProxy2 =  GameObject.FindObjectsOfType<Proxy>().ToArray().FirstOrDefault();
                var counter = 0;
                while(counter < QueryThreshold && myProxy2.queryState != QueryState.Tracking)
                {
                    empty.transform.position += empty.transform.forward * 0.05f;
                    
                    yield return null;
                    System.Threading.Thread.Sleep(200);
                    counter++;
                }
                proxyObj = GameObject.Find("MyTestCube");
                Assert.IsTrue(proxyObj);
                yield return new ExitPlayMode();
            }
        }
    }
}
