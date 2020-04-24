using System.Collections;
using System.Linq;
using NUnit.Framework;
using Unity.Labs.MARS;
using UnityEditor;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEditor.UIAutomation;

namespace Tests
{
    public class ProxyTests
    {
        [SetUp]
        public void SetUp()
        {
            //EditorApplication.ExecuteMenuItem("Window/Layouts/Default");
        }
        
        [TearDown]
        public void TearDown()
        {
            
        }

        [Test]
        public void CanCreateProxyFromGameObjectMenu()
        {
            EditorApplication.ExecuteMenuItem("GameObject/MARS/Proxy Object");
            var goAll = Resources.FindObjectsOfTypeAll(typeof(GameObject)).ToList();
            Assert.IsTrue(goAll.Find(x => x.name == "Proxy Object"));
        }
        
        [Test]
        public void CanCreateProxyFromMarsPanelMenu()
        {

            EditorApplication.ExecuteMenuItem("Window/MARS/MARS Panel");
            EditorApplication.RequestRepaintAllViews();
            var marsPanel = Resources.FindObjectsOfTypeAll<MARSPanel>().FirstOrDefault();
            Assert.That(marsPanel, Is.Not.Null,
                "Could not open the MARS panel from the Window MenuItem");
            
            using (var window = new MarsAutomatedWindow<MARSPanel>(marsPanel) )
            {

                var content = new GUIContent(
                    "Proxy Object",
                "A GameObject representing a proxy for an object in the real world."
                    );

                var proxyButton = window.FindElementsByGUIContent(content).ToArray().FirstOrDefault();

                window.Click(proxyButton);
                window.window.RepaintImmediately();

            }
            
            
            var goAll = Resources.FindObjectsOfTypeAll(typeof(GameObject)).ToList();
            Assert.IsTrue(goAll.Find(x => x.name == "Proxy Object"));
            Assert.IsTrue(goAll.Find(x => x.name == "MARS Session"));
        }

        // A UnityTest behaves like a coroutine in Play Mode. In Edit Mode you can use
        // `yield return null;` to skip a frame.
        [UnityTest]
        public IEnumerator ATest()
        {
            
            yield return null;
        }
    }
}