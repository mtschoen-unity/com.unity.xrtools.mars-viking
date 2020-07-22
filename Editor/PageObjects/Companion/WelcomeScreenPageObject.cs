using NUnit.Framework;
using UnityEngine;
using UnityEngine.UI;

// ReSharper disable once CheckNamespace
namespace MARSViking.Companion
{
    public static class WelcomeScreenPageObject
    {
        private const string WelcomeScreenPath = "Menus/SafeArea/Welcome Screen";

        public static GameObject WelcomeScreen
        {
            get
            {
                var go = GameObject.Find(WelcomeScreenPath);
                if (go == null)
                    Assert.Inconclusive("WelcomeScreen is missing");
                return go;
            }
        }

        public static Button LinkAccountButton
        {
            get
            {
                var go = GameObject.Find($"{WelcomeScreenPath}/Link Account").GetComponent<Button>();
                if (go == null)
                    Assert.Inconclusive("LinkAccountButton on the WelcomeScreen is missing");
                return go;
            }
        }
    }
}