using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
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
            var isVisible = WelcomeScreenPageObject.WelcomeScreen.IsVisible();
            yield return isVisible;
            Debug.Log($"Welcome screen is visible: {isVisible.Current}");

            yield return WelcomeScreenPageObject.SkipButton.Click();

            isVisible = ProjectListScreenPageObject.ProjectListScreen.IsVisible();
            yield return isVisible;
            Debug.Log($"Project list is visible: {isVisible.Current}");

            yield return ProjectListScreenPageObject.CreateProjectButton.Click();

            isVisible = HomeScreenPageObject.HomeScreen.IsVisible();
            yield return isVisible;
            Debug.Log($"Home screen is visible: {isVisible.Current}");

            isVisible = HomeScreenPageObject.Content.IsVisible();
            yield return isVisible;
            Debug.Log($"Content is visible: {isVisible.Current}");

            isVisible = HomeScreenPageObject.ProxyScanCard.IsVisible();
            yield return isVisible;
            Debug.Log($"Proxy Scan Card is visible: {isVisible.Current}");

            //yield return new WaitForSeconds(1f);
            //HomeScreenPageObject.ProxyScanStartButton.Click();
            //yield return new WaitForSeconds(1f);
            //yield return CameraMan.MoveTo();
        }
    }
}
