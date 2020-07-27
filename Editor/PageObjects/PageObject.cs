using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using NUnit.Framework;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Debug = System.Diagnostics.Debug;

// ReSharper disable once CheckNamespace
namespace MARSViking.Companion
{
    public class PageObject
    {
        protected static IEnumerator<GameObject> FindGameObject(string path)
        {
            var go = GameObject.Find(path);
            var counter = 0;
            while (go == null && counter++ < 10)
            {
                yield return null;
            }

            yield return go;
        }


        protected static IEnumerator<T> FindObject<T>(string path) where T : Component
        {
            var enumerator = FindGameObject(path);
            while (enumerator.Current == null && enumerator.MoveNext())
            {
                yield return null;
            }

            var go = enumerator.Current;
            if (go != null)
                yield return go.GetComponent<T>();

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
        public static IEnumerator<bool> IsVisible(this IEnumerator<GameObject> go)
        {
            while (go.Current == null && go.MoveNext())
            {
                yield return false;
            }

            Debug.Assert(go.Current != null, "Couldn't find gameObject");
            yield return go.Current.activeSelf;
        }

        public static IEnumerator Click(this IEnumerator<Button> go)
        {
            while (go.Current == null && go.MoveNext())
            {
                yield return null;
            }

            Debug.Assert(go.Current != null, "Couldn't find gameObject");
            go.Current.onClick.Invoke();
        }

        public static IEnumerator SetText(this IEnumerator<TMP_InputField> textField, string text)
        {
            while (textField.Current == null && textField.MoveNext())
            {
                yield return null;
            }

            Debug.Assert(textField.Current != null, "Couldn't find gameObject");
            textField.Current.text = text;
        }

    }
}
