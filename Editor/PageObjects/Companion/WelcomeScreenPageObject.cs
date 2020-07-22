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

        public static GameObject WelcomeScreen
        {
            get
            {
                var go = GameObject.Find(WelcomeScreenPath);
                return IsFound(
                    go, 
                    MethodBase.GetCurrentMethod().Name.Replace("get_", ""), 
                    MethodBase.GetCurrentMethod().DeclaringType.Name.Replace("PageObject", "")) ? go : null;
            }
        }

        public static Button LinkAccountButton
        {
            get
            {
                var go = GameObject.Find($"{WelcomeScreenPath}/Link Account");
                return IsFound(
                    go, 
                    MethodBase.GetCurrentMethod().Name.Replace("get_", ""), 
                    MethodBase.GetCurrentMethod().DeclaringType.Name.Replace("PageObject", "")) ? go.GetComponent<Button>() : null;
            }
        }
    }
}