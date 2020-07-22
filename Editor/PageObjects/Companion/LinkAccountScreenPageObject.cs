using System.Reflection;
using NUnit.Framework;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

// ReSharper disable once CheckNamespace
namespace MARSViking.Companion
{
    public class LinkAccountScreenPageObject: PageObject
    {
        private const string LinkAccountScreenPath = "Menus/SafeArea/Link Account";

        public static GameObject LinkAccountScreen => FindObject<GameObject>($"{LinkAccountScreenPath}");

        public static TMP_InputField ApiKeyInputText =>
            FindObject<TMP_InputField>(
                $"{LinkAccountScreenPath}/Scan View/Bottom ThreeRows/Row Middle/API Key Field/Input Field");

        public static Button LinkAccountButton => FindObject<Button>($"{LinkAccountScreenPath}/Scan View/Bottom ThreeRows/Row Buttons/Button Rectangle");

    }
}
