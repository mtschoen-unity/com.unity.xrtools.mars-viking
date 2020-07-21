using NUnit.Framework;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

// ReSharper disable once CheckNamespace
namespace MARSViking.Companion
{
    public static class ProjectListScreenPo
    {
        private const string ProjectListScreenPath = "Menus/SafeArea/ProjectListMenu";

        public static GameObject ProjectListScreen
        {
            get
            {
                var go = GameObject.Find(ProjectListScreenPath);
                if (go == null)
                    Assert.Inconclusive("ProjectListScreen is missing");
                return go;
            }
        }

        public static Button LinkAccountButton
        {
            get
            {
                var go = GameObject.Find($"{ProjectListScreenPath}/Header/Link Account Button").GetComponent<Button>();
                if (go == null)
                    Assert.Inconclusive("LinkAccountButton on the ProjectListScreen is missing");
                return go;
            }
        }

        public static TMP_Text LinkAccountButtonText
        {
            get
            {
                var go = GameObject.Find($"{ProjectListScreenPath}/Header/Link Account Button/Text")
                    .GetComponent<TMP_Text>();
                if (go == null)
                    Assert.Inconclusive("LinkAccountButtonText on the ProjectListScreen is missing");
                return go;
            }
        }

        public static Button SelectButton
        {
            get
            {
                var go = GameObject.Find($"{ProjectListScreenPath}/Header/Select Button").GetComponent<Button>();
                if (go == null)
                    Assert.Inconclusive("SelectButton on the ProjectListScreen is missing");
                return go;
            }
        }
    }
}