
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
        private const string GameObjectPath = "GameObject/MARS";
        public const string ProxyMenuPath = GameObjectPath + "/Proxy Object";
    }

    public static class WindowsMenu
    {
        private const string WindowsPath = "Window/MARS";
        public const string MarsPanelPath = WindowsPath + "/MARS Panel";
    }

    public static class AssertionErrorMessages
    {
        public const string NoMarsPanel = "Could not open the MARS panel from the Window MenuItem";
    }
}
