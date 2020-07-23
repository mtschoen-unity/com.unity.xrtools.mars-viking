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
        public static GameObject WelcomeScreen => FindObject<GameObject>(WelcomeScreenPath);
        public static Button LinkAccountButton => FindObject<Button>($"{WelcomeScreenPath}/Link Account");

    }
}