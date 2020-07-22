﻿using System.Reflection;
using NUnit.Framework;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

// ReSharper disable once CheckNamespace
namespace MARSViking.Companion
{
    public class PageObject
    {
        public static bool IsFound(GameObject go, string userInterfaceElementName, string screenName)
        {
            if (go != null) return true;

            Assert.Inconclusive(
                userInterfaceElementName == screenName
                    ? $"{userInterfaceElementName} is missing"
                    : $"{userInterfaceElementName} on the {screenName} is missing");

            return false;
        }
    }
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