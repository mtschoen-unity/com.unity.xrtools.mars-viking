
public static class Constants
{
    public static class MarsPanel
    {
        public const string ProxyButtonText = "Proxy Object";
        public const string ProxyButtonTooltip = "A GameObject representing a proxy for an object in the real world.";
    }

    public static class HierarchyPanel
    {
        public const string MarsSessionGameObjectName = "MARS Session";
        public const string ProxyGameObjectName = "Proxy Object";
    }

    public static class MarsGameObjectMenu
    {
        private const string GameObjectPath = "GameObject";
        public const string ProxyMenuPath = GameObjectPath + "/MARS/Proxy Object";
    }

    public static class WindowsMenu
    {
        private const string WindowsPath = "Window";
        public const string MarsPanelPath = WindowsPath + "/MARS/MARS Panel";
        public const string InspectorPath = WindowsPath + "/General/Inspector";
    }

    public static class AssertionErrorMessages
    {
        public const string NoMarsPanel = "Could not open the MARS panel from the Window MenuItem";
    }

    public static class InspectorComponents
    {
        public const string MissingPlaneSizeConditionHelpBox =
            "No conditions have been assigned to this entity, so it won't match any world data.\nUse 'Add MARS Component -> Condition' below, or click the button to add a PlaneSize condition to make this Entity match a surface with a minimum size.";
    }
}
