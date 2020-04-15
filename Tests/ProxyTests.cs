using System.Collections;
using System.Collections.Generic;
using System.IO;
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
            EditorApplication.ExecuteMenuItem("Window/Layouts/Default");
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
            
            using (var window = new AutomatedWindow<MARSPanel>(marsPanel) )
            {


                var tex = new Texture2D(2, 2, TextureFormat.DXT5, false);
                byte[] file;
                file = File.ReadAllBytes("Packages/com.unity.labs.mars/Editor/Icons/Create/Dark/Proxy.png");
                tex.LoadImage(file);
                tex.name = "Proxy";

                var content = new GUIContent(
                    "Proxy Object",
                    tex,
                    "A GameObject representing a proxy for an object in the real world."
                    );

                var proxyButton = window.FindElementsByGUIContent(content).ToArray().FirstOrDefault();
                window.Click(proxyButton);
                window.window.RepaintImmediately();

            }
            
            
            var goAll = Resources.FindObjectsOfTypeAll(typeof(GameObject)).ToList();
            Assert.IsTrue(goAll.Find(x => x.name == "Proxy Object"));
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
