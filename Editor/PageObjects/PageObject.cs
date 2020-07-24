using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;
using NUnit.Framework;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.Analytics;
using UnityEngine.UI;

// ReSharper disable once CheckNamespace
namespace MARSViking.Companion
{
    public class PageObject
    {
        protected static T FindObject<T>(string path)
        {
            object go = null;
            var counter = 0;
            while (go == null && counter < 10)
            {
                go = GameObject.Find(path);
                EditorApplication.Step();
                System.Threading.Thread.Sleep(1000);
                counter++;
            }

            if (go != null)
            {
                if (typeof(T) == typeof(GameObject))
                    return (T) go;
                
                var tempGo = (GameObject) go; 
                var component = tempGo.GetComponent<T>();
                if (component != null)
                    return component;
            }
            
            var stackTrace = new StackTrace();
            var userInterfaceElementName = stackTrace.GetFrame(1).GetMethod().Name.Replace("get_", "");
            var screenName = stackTrace.GetFrame(1).GetMethod().DeclaringType.Name.Replace("PageObject", "");
            throw new InconclusiveException(
                userInterfaceElementName == screenName
                    ? $"{userInterfaceElementName} is missing"
                    : $"{userInterfaceElementName} on the {screenName} is missing");
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