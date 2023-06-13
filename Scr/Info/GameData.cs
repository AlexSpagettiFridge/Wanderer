using Godot;

namespace Wanderer.Info
{
    internal partial class GameData: Node
    {
        internal HeroData HeroData;

        public override void _Ready()
        {
            HeroData = new HeroData();
        }
    }
}