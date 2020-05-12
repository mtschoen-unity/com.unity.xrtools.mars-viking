using System;
using System.Collections;
using System.Linq;
using NUnit.Framework;
using Unity.Labs.MARS;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
//using UnityEditor.UIAutomation;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;

// ReSharper disable once CheckNamespace
namespace MARSViking
{
    public class EditorProxyTests
    {
        [SetUp]
        public void SetUp()
        {
            EditorApplication.ExecuteMenuItem("Window/Layouts/Default");
            EditorSceneManager.NewScene(NewSceneSetup.EmptyScene, NewSceneMode.Single);
        }

        [TearDown]
        public void TearDown()
        {
            EditorSceneManager.CloseScene(SceneManager.GetActiveScene(), true);
            /*var goAll = Resources.FindObjectsOfTypeAll(typeof(GameObject)).ToList();
            foreach (var go in goAll)
            {
                if(go.name == "Proxy Object")
                    GameObject.DestroyImmediate(go, true);
            }*/
        }

        
        
        [UnityTest]
        [Category("Acceptance")]
        [NUnit.Framework.Property("TestRailId", "C576069")]
        public IEnumerator CanCreateProxyFromGameObjectMenu()
        {
            EditorApplication.ExecuteMenuItem(Constants.MarsGameObjectMenu.ProxyMenuPath);
            var goAll = Resources.FindObjectsOfTypeAll(typeof(GameObject)).ToList();
            Assert.IsTrue(goAll.Find(x => x.name == Constants.HierarchyPanel.ProxyGameObjectName));
            yield return null;
        }

        [UnityTest]
        [Category("Acceptance")]
        [NUnit.Framework.Property("TestRailId", "C576067")]
        public IEnumerator CanCreateNewSessionAlongWithNewProxy()
        {
            EditorApplication.ExecuteMenuItem(Constants.MarsGameObjectMenu.ProxyMenuPath);
            var goAll = Resources.FindObjectsOfTypeAll(typeof(GameObject)).ToList();
            Assert.IsTrue(goAll.Find(x => x.name == Constants.HierarchyPanel.MarsSessionGameObjectName));
            yield return null;
        }
    
        [UnityTest]
        [Category("Acceptance")]
        [NUnit.Framework.Property("TestRailId", "C576447")]
        public IEnumerator CanSeeMissingConditionWarning()
        {
            EditorApplication.ExecuteMenuItem(Constants.MarsGameObjectMenu.ProxyMenuPath);
            var goAll = Resources.FindObjectsOfTypeAll(typeof(GameObject)).ToList();
            var proxyObj = goAll.Find(x => x.name == Constants.HierarchyPanel.ProxyGameObjectName) as GameObject;
            Assert.IsTrue(proxyObj, "Could not find Proxy Object in scene");

            AutomatedIMElement planeSizeCondButton;
            AutomatedIMElement planeSizeConditionHelpBox;
            var inspectorWindow = Resources.FindObjectsOfTypeAll<InspectorWindow>().FirstOrDefault();
            using (var window = new MarsAutomatedWindow<InspectorWindow>(inspectorWindow))
            {
                
                yield return null;

                planeSizeCondButton = window.FindElementsByGUIContent(
                    new GUIContent(
                        "Add PlaneSize Condition",
                        tooltip: null
                    )).ToArray().FirstOrDefault() as AutomatedIMElement;

                planeSizeConditionHelpBox = window.FindElementsByGUIContent(
                    new GUIContent(
                        Constants.InspectorComponents.MissingPlaneSizeConditionHelpBox,
                        tooltip: null
                    )).ToArray().FirstOrDefault() as AutomatedIMElement;
            }
            Assert.IsTrue(planeSizeCondButton != null);
            Assert.IsTrue(planeSizeConditionHelpBox != null);
        }

        [UnityTest]
        [Category("Acceptance")]
        [NUnit.Framework.Property("TestRailId", "C573635")]
        public IEnumerator CanCreateProxyFromMarsPanel()
        {
            EditorApplication.ExecuteMenuItem(Constants.WindowsMenu.MarsPanelPath);
            EditorApplication.RequestRepaintAllViews();
            var marsPanel = Resources.FindObjectsOfTypeAll<MARSPanel>().FirstOrDefault();
            Assert.That(marsPanel, Is.Not.Null,
                Constants.AssertionErrorMessages.NoMarsPanel);

            using (var window = new MarsAutomatedWindow<MARSPanel>(marsPanel))
            {
                var content = new GUIContent(
                    Constants.MarsPanel.ProxyButtonText,
                    Constants.MarsPanel.ProxyButtonTooltip
                );

                var proxyButton = window.FindElementsByGUIContent(content).ToArray().FirstOrDefault();

                window.Click(proxyButton);
                window.window.RepaintImmediately();
            }
            
            var goAll = Resources.FindObjectsOfTypeAll(typeof(GameObject)).ToList();
            Assert.IsTrue(goAll.Find(x => x.name == Constants.HierarchyPanel.ProxyGameObjectName));
            Assert.IsTrue(goAll.Find(x => x.name == Constants.HierarchyPanel.MarsSessionGameObjectName));
            yield return null;
        }
        
        [UnityTest]
        [Category("Use Case")]
        [NUnit.Framework.Property("TestRailId", "C576677")]
        public IEnumerator CanDisplayPrefabWhenProxyConditionIsMatched()
        {
            EditorApplication.ExecuteMenuItem(Constants.WindowsMenu.MarsSimulationViewPath);
            //EditorApplication.ExecuteMenuItem(Constants.WindowsMenu.MarsPanelPath);
            EditorApplication.RequestRepaintAllViews();
            yield return null;
            yield return null;


            var marsPanel = EditorWindow.GetWindow<MARSPanel>(typeof(InspectorWindow));

            
            EditorApplication.ExecuteMenuItem(Constants.MarsGameObjectMenu.ProxyMenuPath);
            var goAll = Resources.FindObjectsOfTypeAll(typeof(GameObject)).ToList();
            var myProxy =  goAll.Find(x => x.name == Constants.HierarchyPanel.ProxyGameObjectName) as GameObject;
            Assert.IsTrue(myProxy, "Could not find Proxy in Scene");
            
            GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
            cube.transform.parent = myProxy.transform;
            myProxy.AddComponent<PlaneSizeCondition>();

            AutomatedIMElement proxyObject = null;

            Assert.That(marsPanel, Is.Not.Null,
                Constants.AssertionErrorMessages.NoMarsPanel);
            
            yield return null;
            EditorApplication.RequestRepaintAllViews();
            yield return null;

            using (var window = new MarsAutomatedWindow<MARSPanel>(marsPanel))
            {
                var content = new GUIContent(
                    "Content Hierarchy",
                    tooltip: null
                );

                var contentHierarchy = window.FindElementsByGUIContent(content).ToArray().First() as AutomatedIMElement;
                Assert.NotNull(contentHierarchy, "Could not find MARS Panel Content Hierarchy");
                var contentHierarchyList = contentHierarchy.nextSibling as AutomatedIMElement;
                Assert.NotNull(contentHierarchyList, "Could not find MARS Panel Content Hierarchy List of content");
                
                var content2 = new GUIContent(
                    "Proxy Object",
                    "Match found"
                );
                yield return null;
                EditorApplication.RequestRepaintAllViews();
                yield return null;
                
                
                proxyObject = contentHierarchyList.FindElementsByGUIContent(content2).ToArray().First() as AutomatedIMElement;

            }

            Assert.NotNull(proxyObject, "Proxy was not matched");
            yield return null;
        }
        



    }
}