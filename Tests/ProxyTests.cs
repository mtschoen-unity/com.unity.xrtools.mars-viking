using System.Collections;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using Unity.Labs.MARS;
using UnityEditor;
using UnityEngine;
using UnityEditor.UIAutomation;
using UnityEngine.Assertions.Must;
using UnityEngine.TestTools;
using UnityEngine.UIElements;

// ReSharper disable once CheckNamespace
namespace MARSVikingTests
{
    public class ProxyTests
    {
        [SetUp]
        public void SetUp()
        {
            EditorApplication.ExecuteMenuItem("Window/Layouts/Revert Factory Settings...");
        }

        [TearDown]
        public void TearDown()
        {
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

            IAutomatedUIElement planeSizeCondButton;
            IAutomatedUIElement planeSizeConditionHelpBox;
            var inspectorWindow = Resources.FindObjectsOfTypeAll<InspectorWindow>().FirstOrDefault();
            using (var window = new MarsAutomatedWindow<InspectorWindow>(inspectorWindow))
            {
                yield return null;

                planeSizeCondButton = window.FindElementsByGUIContent(
                    new GUIContent(
                        "Add PlaneSize Condition",
                        tooltip: null
                    )).ToArray().FirstOrDefault();

                planeSizeConditionHelpBox = window.FindElementsByGUIContent(
                    new GUIContent(
                        Constants.InspectorComponents.MissingPlaneSizeConditionHelpBox,
                        tooltip: null
                    )).ToArray().FirstOrDefault();
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
    }
}