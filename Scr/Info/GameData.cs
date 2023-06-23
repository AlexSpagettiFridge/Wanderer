using Godot;

namespace Wanderer.Info
{
    internal partial class GameData : Node
    {
        public static GameData Current;

        internal HeroData HeroData;

        internal GameData()
        {
            Current=this;
        }

        public override void _Ready()
        {
            HeroData = new HeroData();
        }
    }
}