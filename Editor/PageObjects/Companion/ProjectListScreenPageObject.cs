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

        public static GameObject ProjectListScreen => FindObject<GameObject>(ProjectListScreenPath);

        public static Button LinkAccountButton => FindObject<Button>($"{ProjectListScreenPath}/Header/Link Account Button");

        public static TMP_Text LinkAccountButtonText => FindObject<TMP_Text>($"{ProjectListScreenPath}/Header/Link Account Button/Text");

        public static Button SelectButton => FindObject<Button>($"{ProjectListScreenPath}/Header/Select Button");
        public static Button CreateProjectButton => FindObject<Button>($"{ProjectListScreenPath}/Buttons/New Project Button");
        
    }
}