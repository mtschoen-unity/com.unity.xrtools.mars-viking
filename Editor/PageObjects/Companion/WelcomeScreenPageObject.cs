using System.Collections.Generic;
using System.Reflection;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.UI;

// ReSharper disable once CheckNamespace
namespace MARSViking.Companion
{
    public class WelcomeScreenPageObject: PageObject
    {
        private const string WelcomeScreenPath = "Menus/SafeArea/Welcome Screen";
        public static IEnumerator<GameObject> WelcomeScreen => FindGameObject(WelcomeScreenPath);
        public static IEnumerator<Button> LinkAccountButton => FindObject<Button>($"{WelcomeScreenPath}/Link Account");
        public static IEnumerator<Button> SkipButton => FindObject<Button>($"{WelcomeScreenPath}/Skip");
    }
}
