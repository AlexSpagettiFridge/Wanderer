using Godot;
using Wanderer.Info;

namespace Wanderer
{
    internal static class Util
    {
        private static Node root;

        internal static void RegisterRoot(Node root) { Util.root = root; }
        public static GameData GameData=> root.GetNode<GameData>("GameData");
        public static TimerService TimerService=> root.GetNode<TimerService>("TimerService");

        public static Theme GetInheritedTheme(Control control)
        {
            if (control.Theme != null)
            {
                return control.Theme;
            }
            return GetInheritedTheme(control.GetParent<Control>());
        }
    }
}