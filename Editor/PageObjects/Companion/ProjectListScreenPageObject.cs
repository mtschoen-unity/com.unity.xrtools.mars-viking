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

        public static GameObject ProjectListScreen
        {
            get
            {
                var go = GameObject.Find(ProjectListScreenPath);
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
                var go = GameObject.Find($"{ProjectListScreenPath}/Header/Link Account Button");
                return IsFound(
                    go, 
                    MethodBase.GetCurrentMethod().Name.Replace("get_", ""), 
                    MethodBase.GetCurrentMethod().DeclaringType.Name.Replace("PageObject", "")) ? go.GetComponent<Button>() : null;
            }
        }

        public static TMP_Text LinkAccountButtonText
        {
            get
            {
                var go = GameObject.Find($"{ProjectListScreenPath}/Header/Link Account Button/Text");
                return IsFound(
                    go, 
                    MethodBase.GetCurrentMethod().Name.Replace("get_", ""), 
                    MethodBase.GetCurrentMethod().DeclaringType.Name.Replace("PageObject", "")) ? go.GetComponent<TMP_Text>() : null;
            }
        }

        public static Button SelectButton
        {
            get
            {
                var go = GameObject.Find($"{ProjectListScreenPath}/Header/Select Button");
                return IsFound(
                    go, 
                    MethodBase.GetCurrentMethod().Name.Replace("get_", ""), 
                    MethodBase.GetCurrentMethod().DeclaringType.Name.Replace("PageObject", "")) ? go.GetComponent<Button>() : null;
            }
        }
    }
}