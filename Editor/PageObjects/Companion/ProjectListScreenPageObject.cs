using System.Collections.Generic;
using System.Reflection;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

// ReSharper disable once CheckNamespace
namespace MARSViking.Companion
{
    public class ProjectListScreenPageObject: PageObject
    {
        private const string ProjectListScreenPath = "Menus/SafeArea/ProjectListMenu";

        public static IEnumerator<GameObject> ProjectListScreen => FindGameObject(ProjectListScreenPath);

        public static IEnumerator<Button> LinkAccountButton => FindObject<Button>($"{ProjectListScreenPath}/Header/Link Account Button");

        public static IEnumerator<TMP_Text> LinkAccountButtonText => FindObject<TMP_Text>($"{ProjectListScreenPath}/Header/Link Account Button/Text");

        public static IEnumerator<Button> SelectButton => FindObject<Button>($"{ProjectListScreenPath}/Header/Select Button");
        public static IEnumerator<Button> CreateProjectButton => FindObject<Button>($"{ProjectListScreenPath}/Buttons/New Project Button");

    }
}
