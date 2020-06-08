using System.Collections;
using System.Linq;
using NUnit.Framework;
using Unity.MARS;
using Unity.MARS.Simulation;
using Unity.XRTools.ModuleLoader;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;
using static UnityEngine.GameObject;
using Object = UnityEngine.Object;

// ReSharper disable once CheckNamespace
namespace MARSViking
{
    public class EditorProxyTests
    {
        [SetUp]
        public void SetUp()
        {
            // Reset view before each test
            //EditorApplication.ExecuteMenuItem("Window/Layouts/Default");
            // Create a new scene before each test
            EditorSceneManager.NewScene(NewSceneSetup.EmptyScene, NewSceneMode.Single);
            
            var environmentManager = ModuleLoaderCore.instance.GetModule<MARSEnvironmentManager>();
            var names = MarsEnvironments.GetEnvironmentNames();
            var envIndex = names.FindIndex(env => env == "Bedroom_12ftx12ft");
            environmentManager.TrySetupEnvironmentAndRestartSimulation(envIndex);
        }

        [TearDown]
        public void TearDown()
        {
            // Remove dirty scene after test
            EditorSceneManager.CloseScene(SceneManager.GetActiveScene(), true);
        }

        [Test]
        [Category("Acceptance")]
        [NUnit.Framework.Property("TestRailId", "C576069")]
        public void CanCreateProxyFromGameObjectMenu()
        {
            
            // Create a Proxy using the GameObject Menu
            EditorApplication.ExecuteMenuItem(Constants.GameObjectMarsMenu.CreateProxy);
            // Find all gameObjects in Scene
            var sceneGameObjects = Object.FindObjectsOfType<GameObject>().ToList();

            // Find my Proxy
            var newProxy = sceneGameObjects.Find(
                x => x.name == Constants.HierarchyPanel.ProxyName
                );
            
            Assert.NotNull(newProxy, Constants.AssertionErrorMessages.NoProxy);
        }

        [Test]
        [Category("Acceptance")]
        [NUnit.Framework.Property("TestRailId", "C576067")]
        public void CanCreateNewSessionAlongWithNewProxy()
        {
            // Create a Proxy using the GameObject Menu
            EditorApplication.ExecuteMenuItem(Constants.GameObjectMarsMenu.CreateProxy);
            // Find all gameObjects in Scene
            var sceneGameObjects = Object.FindObjectsOfType<GameObject>().ToList();

            // Find the Mars Session created when adding a Proxy
            var newMarsSession = sceneGameObjects.Find(
                x => x.name == Constants.HierarchyPanel.MarsSession
                );
            
            Assert.NotNull(newMarsSession, Constants.AssertionErrorMessages.NoMarsSession);
        }
    
        [UnityTest]
        [Category("Acceptance")]
        [NUnit.Framework.Property("TestRailId", "C576447")]
        public IEnumerator CanSeeMissingConditionWarning()
        {
            AutomatedIMElement planeSizeConditionCreateButton;
            AutomatedIMElement planeSizeConditionHelpBox;
            
            // Create a Proxy
            EditorApplication.ExecuteMenuItem(Constants.GameObjectMarsMenu.CreateProxy);
            var sceneGameObjects = Object.FindObjectsOfType<GameObject>().ToList();
            var newProxy = sceneGameObjects.Find(
                x => x.name == Constants.HierarchyPanel.ProxyName
                );
            if(newProxy == null)
                Assert.Inconclusive(Constants.AssertionErrorMessages.NoProxy);

            // Open Inspector Window
            var inspectorWindow = EditorWindow.GetWindow<InspectorWindow>();
            if(inspectorWindow == null)
                Assert.Inconclusive("Could not find Inspector Window during setting up test");
            
            // Grab hold of the Inspector windows
            using (var window = new MarsAutomatedWindow<InspectorWindow>(inspectorWindow))
            {
                // Move forward one step to refresh the Inspector UI
                yield return null;

                // Grab hold of the default add PlaneSize Condition Button in Inspector
                planeSizeConditionCreateButton = window.FindElementsByGUIContent(
                    new GUIContent(
                        "Add PlaneSize Condition",
                        tooltip: null
                    )).ToArray().FirstOrDefault() as AutomatedIMElement;

                // Grab hold of the default add PlaneSize Condition Button in Inspector
                planeSizeConditionHelpBox = window.FindElementsByGUIContent(
                    new GUIContent(
                        Constants.InspectorComponents.MissingPlaneSizeConditionHelpBox,
                        tooltip: null
                    )).ToArray().FirstOrDefault() as AutomatedIMElement;
            }
            
            Assert.NotNull(planeSizeConditionCreateButton, "Cannot find Create PlaneSize Condition button in Inspector");
            Assert.NotNull(planeSizeConditionHelpBox, "Cannot find Missing Condition Help Box in Inspector");
        }

        [Test]
        [Category("Acceptance")]
        [NUnit.Framework.Property("TestRailId", "C573635")]
        public void CanCreateProxyFromMarsPanel()
        {
            // Open the MARS Panel
            EditorApplication.ExecuteMenuItem(Constants.WindowsMenu.OpenMarsPanel);
            var marsPanel = EditorWindow.GetWindow<MARSPanel>();
            if(marsPanel == null)
                Assert.Inconclusive(Constants.AssertionErrorMessages.NoMarsPanel);

            // Grab hold of the Mars Panel
            using (var window = new MarsAutomatedWindow<MARSPanel>(marsPanel))
            {
                var content = new GUIContent(
                    Constants.MarsPanel.ProxyButtonText,
                    Constants.MarsPanel.ProxyButtonTooltip
                );
                
                // Find the Create Proxy button and click it
                var proxyButton = window.FindElementsByGUIContent(content).ToArray().FirstOrDefault();
                
                if(proxyButton == null)
                    Assert.Inconclusive(Constants.AssertionErrorMessages.NoProxyInMarsPanel);
                window.Click(proxyButton);
            }
            
            // Find the Proxy among all the game objects in Scene
            var sceneGameObjects = Object.FindObjectsOfType<GameObject>().ToList();
            var newProxy = sceneGameObjects.Find(
                x => x.name == Constants.HierarchyPanel.ProxyName
                );

            Assert.NotNull(newProxy, Constants.AssertionErrorMessages.NoProxy);

        }
        
        [UnityTest]
        [Category("Use Case")]
        [NUnit.Framework.Property("TestRailId", "C576677")]
        public IEnumerator CanDisplayPrefabWhenProxyConditionIsMatched()
        {
            AutomatedIMElement proxyObject;
            
            // Open Simulation View and Open the MarsPanel next to Inspector 
            // MARS Panel must be placed in position where it is fully extended so we can grab hold of UI elements
            EditorApplication.ExecuteMenuItem(Constants.WindowsMenu.OpenSimulationView);
            var marsPanel = EditorWindow.GetWindow<MARSPanel>(typeof(InspectorWindow));
            var simView = EditorWindow.GetWindow<SimulationView>();
            if(marsPanel == null)
                Assert.Inconclusive(Constants.AssertionErrorMessages.NoMarsPanel);
            if(simView == null)
                Assert.Inconclusive(Constants.AssertionErrorMessages.NoSimulationView);

            // Create a Proxy
            EditorApplication.ExecuteMenuItem(Constants.GameObjectMarsMenu.CreateProxy);
            var myProxy = Object.FindObjectsOfType<Proxy>().ToArray().FirstOrDefault();
            if(myProxy == null)
                Assert.Inconclusive(Constants.AssertionErrorMessages.NoProxy);
            
            // Add a Cube as a child to the Proxy
            var cube = CreatePrimitive(PrimitiveType.Cube);
            cube.transform.parent = myProxy.transform;
            myProxy.gameObject.AddComponent<PlaneSizeCondition>();

            // Get the SimulationManager and wait until it is in sync before looking in the Content Hierarchy of the MARS Panel
            var simManager = ModuleLoaderCore.instance.GetModule<SimulatedObjectsManager>();
            var counter = 0;
            while (counter < Constants.TestControls.QueryThreshold && simManager.SimulationSyncedWithScene == false)
            {
                yield return null;
                System.Threading.Thread.Sleep(500);
                counter++;
            }

            // Grab hold of the MARS Panel and find the Content Hierarchy
            using (var window = new MarsAutomatedWindow<MARSPanel>(marsPanel))
            {
                var contentHierarchyGuiContent = new GUIContent(
                    "Content Hierarchy",
                    tooltip: null
                );

                var contentHierarchy = window.FindElementsByGUIContent(contentHierarchyGuiContent).ToArray().First() as AutomatedIMElement;
                if(contentHierarchy == null)
                    Assert.Inconclusive(Constants.AssertionErrorMessages.NoContentHierarchy);

                var contentHierarchyList = contentHierarchy.nextSibling as AutomatedIMElement;
                if(contentHierarchyList == null)
                    Assert.Inconclusive(Constants.AssertionErrorMessages.NoContentHierarchyList);

                // Search for our Proxy and make sure it is matched
                var proxyGuiContent = new GUIContent(
                    Constants.HierarchyPanel.ProxyName,
                    "Match found"
                );
                proxyObject = contentHierarchyList.FindElementsByGUIContent(proxyGuiContent).ToArray().FirstOrDefault() as AutomatedIMElement;
            }
            Assert.NotNull(proxyObject, Constants.AssertionErrorMessages.NoProxyMatch);
        }
    }
}