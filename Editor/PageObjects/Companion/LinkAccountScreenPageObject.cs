using NUnit.Framework;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

// ReSharper disable once CheckNamespace
namespace MARSViking.Companion
{
    public static class LinkAccountScreenPageObject
    {
        private const string LinkAccountScreenPath = "Menus/SafeArea/Link Account";

        public static GameObject LinkAccountScreen
        {
            get
            {
                var go = GameObject.Find($"{LinkAccountScreenPath}/Scan View/Bottom ThreeRows/Row Middle/API Key Field/Input Field");
                if (go == null)
                    Assert.Inconclusive($"LinkAccountScreen is missing");
                return go;
            }
        }

        public static TMP_InputField ApiKeyInputText
        {
            get
            {
                var go = GameObject.Find($"{LinkAccountScreenPath}/Scan View/Bottom ThreeRows/Row Middle/API Key Field/Input Field").GetComponent<TMP_InputField>();
                if (go == null)
                    Assert.Inconclusive("ApiKeyInputText on the LinkAccountScreen is missing");
                return go;
            }
        }

        public static Button LinkAccountButton
        {
            get
            {
                var go = GameObject.Find($"{LinkAccountScreenPath}/Scan View/Bottom ThreeRows/Row Buttons/Button Rectangle").GetComponent<Button>();
                if (go == null)
                    Assert.Inconclusive("LinkAccountButton on the LinkAccountScreen is missing");
                return go;
            }
        }
    }
}
