using System.Linq;
using NUnit.Framework;
using Unity.Labs.MARS;
using UnityEditor;
using UnityEngine;
using UnityEditor.UIAutomation;

// ReSharper disable once CheckNamespace
namespace MARSVikingTests
{
    public class ProxyTests
    {
        [SetUp]
        public void SetUp()
        {
            
        }
        
        [TearDown]
        public void TearDown()
        {
            
        }

        [Test]
        [Category("Acceptance")]
        [NUnit.Framework.Property("TestRailId", "C576069")]
        public void CanCreateProxyFromGameObjectMenu()
        {
            EditorApplication.ExecuteMenuItem(Constants.MarsGameObjectMenu.ProxyMenuPath);
            var goAll = Resources.FindObjectsOfTypeAll(typeof(GameObject)).ToList();
            Assert.IsTrue(goAll.Find(x => x.name == Constants.HierarchyPanel.ProxyGameObjectName));
        }
        
        [Test]
        [Category("Acceptance")]
        [NUnit.Framework.Property("TestRailId", "C573635")]
        public void CanCreateProxyFromMarsPanel()
        {

            EditorApplication.ExecuteMenuItem(Constants.WindowsMenu.MarsPanelPath);
            EditorApplication.RequestRepaintAllViews();
            var marsPanel = Resources.FindObjectsOfTypeAll<MARSPanel>().FirstOrDefault();
            Assert.That(marsPanel, Is.Not.Null,
                Constants.AssertionErrorMessages.NoMarsPanel);
            
            using (var window = new MarsAutomatedWindow<MARSPanel>(marsPanel) )
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
        }
    }
}