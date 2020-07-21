using TMPro;
using UnityEngine;
using UnityEngine.UI;

// ReSharper disable once CheckNamespace
namespace MARSViking.Companion
{
    public static class PageObjectExtensions
    {
        public static bool IsVisible(this GameObject go)
        {
            return go.activeSelf;
        }

        public static void Click(this Button go)
        {
            go.onClick.Invoke();
        }

        public static void SetText(this TMP_InputField textField, string text)
        {
            textField.text = text;
        }
    }
}