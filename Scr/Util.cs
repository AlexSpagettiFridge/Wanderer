using Godot;

namespace Wanderer
{
    public static class Util
    {
        public static Theme GetInheritedTheme(Control control)
        {
            if (control.Theme!=null)
            {
                return control.Theme;
            }
            return GetInheritedTheme(control);
        }
    }
}