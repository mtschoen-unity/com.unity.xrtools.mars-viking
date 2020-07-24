using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using MARSViking.Companion;
using NUnit.Framework;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

namespace MARSViking.Companion
{
    public class HomeScreenPageObject : PageObject
    {
        private const string HomeScreenPath = "Menus/SafeArea/Home";

        public static GameObject HomeScreen => FindObject<GameObject>(HomeScreenPath);

        public static GameObject ModeSwitcher => FindObject<GameObject>($"{HomeScreenPath}/Common/Mode Switcher");

        public static GameObject Content =>
            FindObject<GameObject>($"{HomeScreenPath}/Common/Mode Switcher/Viewport/Content");

        public static GameObject ProxyScanCard => FindObject<GameObject>($"{HomeScreenPath}/Common/Mode Switcher/Viewport/Content/Proxy Scan Card");
        //public static List<GameObject> Cards => FindObjects<GameObject>($"{HomeScreenPath}/Common/Mode Switcher/Viewport/Content/Home View Card(Clone)");

        //public static Button ProxyScanStartButton => ProxyScanCard.GetComponentInChildren<Button>();
        
    }

    public static class HomeScreenExtensions
    {
        public static GameObject GetCarouselCard(this GameObject go, string cardName)
        {
            var childCount = go.transform.childCount;

            for (int i = 0; i < childCount-1; i++)
            {
                var child = go.transform.GetChild(i).gameObject;
                if (child.GetComponentInChildren<TMP_Text>().text == cardName)
                {
                    return child;
                }
            }

            throw new InconclusiveException($"{cardName} Card in Carousel is missing");
        }
    }
}