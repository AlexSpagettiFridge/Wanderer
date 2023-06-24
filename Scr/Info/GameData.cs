using Godot;

namespace Wanderer.Info
{
    internal partial class GameData : Node
    {
        internal HeroData HeroData;

        internal GameData()
        {
            Util.RegisterRoot(GetParent());
        }

        public override void _Ready()
        {
            HeroData = new HeroData();
        }
    }
}