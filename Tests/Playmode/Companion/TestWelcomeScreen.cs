using System;
using System.Collections;
using NUnit.Framework;
using UnityEditor;
using UnityEditor.TestTools;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;

// ReSharper disable once CheckNamespace
namespace MARSViking.Companion.Playmode
{
    [TestFixture]
    [RequirePlatformSupport(BuildTarget.Android)]
    public class TestWelcomeScreen
    {
        [SetUp]
        public void SetUp()
        {
            try
            {
                SceneManager.LoadScene($"Packages/com.unity.mars-companion-mobile/Scenes/Main.unity");
            }
            // Ignore Exception thrown because we are in Editor mode running Playmode and not a Playmode test
            catch (InvalidOperationException e)
            {
                if (!e.Message.Contains("This can only be used during play mode"))
                {
                    throw;
                }
            }
        }

        [UnityTest]
        [Category("Use Case")]
        [NUnit.Framework.Property("TestRailId", "649482")]
        public IEnumerator TestCanLinkCloudAccount()
        {
            WelcomeScreenPageObject.WelcomeScreen.IsVisible();
            WelcomeScreenPageObject.LinkAccountButton.Click();
            
            LinkAccountScreenPageObject.LinkAccountScreen.IsVisible();

            LinkAccountScreenPageObject.ApiKeyInputText.SetText("4a430aa47c5b65a3e60596d5edc91889");
            LinkAccountScreenPageObject.LinkAccountButton.Click();

            Assert.IsTrue(ProjectListScreenPageObject.ProjectListScreen.IsVisible());
            yield return null;
        }
    }
}
