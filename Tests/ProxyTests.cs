using System.Collections;
using System.Collections.Generic;
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
                //var commonAncestorElement = window.FindElementsByGUIStyle(new GUIStyle()).First().nextSibling;



                var content = new GUIContent();
                //content.image = new Texture2D();
                content.text = "Proxy Object";
                content.tooltip = "A GameObject representing a proxy for an object in the real world.";
                var primitivesSection = window.FindElementsByGUIContent(content).ToArray();
                // var a = window.FindElementsByNamedControl("Proxy Object").ToArray();
                // var m_style = new GUIStyle();
                // m_style.name = "button";
                // var b = window.FindElementsByGUIStyle(m_style).ToArray();
                

                //window.Click(primitivesSection);
                window.window.RepaintImmediately();
                //var proxy = window.FindElementsByGUIContent(new GUIContent("Proxy Object")).ToArray().FirstOrDefault();
                var asdf = "";
                //var marsSession = window.FindElementsByGUIContent(new GUIContent("MARS Session")).ToArray().FirstOrDefault();
                //Assert.NotNull(marsSession, "No Mars Session was found");
            }
            
            
            //var goAll = Resources.FindObjectsOfTypeAll(typeof(GameObject)).ToList();
            //Assert.IsTrue(goAll.Find(x => x.name == "Proxy Object"));
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
