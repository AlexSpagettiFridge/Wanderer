using Godot;
using Wanderer.Info;

namespace Wanderer
{
    internal static class Util
    {
        public static Theme GetInheritedTheme(Control control)
        {
            if (control.Theme!=null)
            {
                return control.Theme;
            }
            return GetInheritedTheme(control.GetParent<Control>());
        }
    }
}