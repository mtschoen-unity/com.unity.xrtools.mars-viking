// ReSharper disable once CheckNamespace
namespace MARSViking.MARS
{
    public static class Constants
    {
        public static class TestControls
        {
            public const int QueryThreshold = 90;
        }
        public static class MarsPanel
        {
            public const string ProxyButtonText = "Proxy Object";
            public const string ProxyButtonTooltip =
                "A GameObject representing a proxy for an object in the real world.";
        }

        public static class HierarchyPanel
        {
            public const string MarsSession = "MARS Session";
            public const string ProxyName = "Proxy Object";
            public const string MarsCamera = "Main Camera";
        }

        public static class GameObjectMarsMenu
        {
            private const string GameObjectPath = "GameObject";
            public const string CreateProxy = GameObjectPath + "/MARS/Proxy Object";
        }

        public static class WindowsMenu
        {
            private const string WindowsPath = "Window";
            public const string OpenMarsPanel = WindowsPath + "/MARS/MARS Panel";
            public const string InspectorPath = WindowsPath + "/General/Inspector";
            public const string OpenSimulationView = WindowsPath + "/MARS/Simulation View";
        }

        public static class AssertionErrorMessages
        {
            public const string NoMarsSession = "Cound not find the MARS Session object in the Hierarchy View";
            public const string NoMarsPanel = "Could not open the MARS panel from the Window MenuItem";
            public const string NoSimulationView = "Could not open the Simulation View from the Window MenuItem";
            public const string NoProxy = "Cound not find the Proxy object in the Hierarchy View";
            public const string NoProxyMatch = "Cannot find a Match for the Proxy";
            public const string NoContentHierarchyList = "Could not find Content Hierarchy List in the Mars Panel";
            public const string NoContentHierarchy = "Cannot find Hierarchy Content in the Mars panel";
            public const string NoProxyInMarsPanel = "Cannot find Create Proxy button in Mars Panel";
        }

        public static class InspectorComponents
        {
            public const string MissingPlaneSizeConditionHelpBox =
                "No conditions have been assigned to this entity, so it won't match any world data.\nUse 'Add MARS Component -> Condition' below, or click the button to add a PlaneSize condition to make this Entity match a surface with a minimum size.";
        }
    }
}