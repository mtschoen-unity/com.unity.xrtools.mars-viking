using System;
using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;

namespace MARSViking.Companion.Playmode
{
    public class TestProxy
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
        [NUnit.Framework.Property("TestRailId", "")]
        public IEnumerator TestCanSelectProxyFlow()
        {
            WelcomeScreenPageObject.WelcomeScreen.IsVisible();

            WelcomeScreenPageObject.SkipButton.Click();

            ProjectListScreenPageObject.ProjectListScreen.IsVisible();

            ProjectListScreenPageObject.CreateProjectButton.Click();
            HomeScreenPageObject.HomeScreen.IsVisible();
            HomeScreenPageObject.Content.IsVisible();
            yield return new WaitForSeconds(1f);
            HomeScreenPageObject.ProxyScanStartButton.Click();
            yield return new WaitForSeconds(1f);
            CameraMan.MoveTo();
            yield return new WaitForSeconds(1f);

            yield return null;
        }
    }
}
