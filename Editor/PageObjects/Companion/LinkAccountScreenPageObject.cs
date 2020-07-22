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

        public static GameObject LinkAccountScreen
        {
            get
            {
                var go = GameObject.Find($"{LinkAccountScreenPath}");
                return IsFound(
                    go, 
                    MethodBase.GetCurrentMethod().Name.Replace("get_", ""), 
                    MethodBase.GetCurrentMethod().DeclaringType.Name.Replace("PageObject", "")) ? go : null;
            }
        }

        public static TMP_InputField ApiKeyInputText
        {
            get
            {
                var go = GameObject.Find($"{LinkAccountScreenPath}/Scan View/Bottom ThreeRows/Row Middle/API Key Field/Input Field");
                return IsFound(
                    go, 
                    MethodBase.GetCurrentMethod().Name.Replace("get_", ""), 
                    MethodBase.GetCurrentMethod().DeclaringType.Name.Replace("PageObject", "")) ? go.GetComponent<TMP_InputField>() : null;
            }
        }

        public static Button LinkAccountButton
        {
            get
            {
                var go = GameObject.Find($"{LinkAccountScreenPath}/Scan View/Bottom ThreeRows/Row Buttons/Button Rectangle");
                return IsFound(
                    go, 
                    MethodBase.GetCurrentMethod().Name.Replace("get_", ""), 
                    MethodBase.GetCurrentMethod().DeclaringType.Name.Replace("PageObject", "")) ? go.GetComponent<Button>() : null;
            }
        }
    }
}
