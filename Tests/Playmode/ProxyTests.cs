using System;
using System.Collections;
using System.Collections.Generic;
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
        [SetUp]
        public void SetUp()
        {
           EditorApplication.ExecuteMenuItem("Window/Layouts/Default");

           try
           {
               EditorSceneManager.NewScene(NewSceneSetup.EmptyScene, NewSceneMode.Single);
           }
           catch (InvalidOperationException e)
           {
               //ignore stupid error
           }
           
        }

        [TearDown]
        public void TearDown()
        {
           
           try
           {
               EditorSceneManager.CloseScene(SceneManager.GetActiveScene(), true);
           }
           catch (InvalidOperationException e)
           {
               //ignore stupid error
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
                var marsCamera = GameObject.FindObjectsOfType<Camera>().ToArray().FirstOrDefault();
                var proxyObj = GameObject.Find("MyTestCube");
                var counter = 0;
                while(proxyObj == null && counter < 60)
                {
                    marsCamera.transform.position += marsCamera.transform.forward * 0.05f;
                    proxyObj = GameObject.Find("MyTestCube");
                    yield return null;
                    System.Threading.Thread.Sleep(200);
                    counter++;
                }
                Assert.IsTrue(proxyObj);
                yield return new ExitPlayMode();
            }
        }
    }
}
