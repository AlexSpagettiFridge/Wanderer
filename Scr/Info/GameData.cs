using Godot;

namespace Wanderer.Info
{
    internal partial class GameData: Node
    {
        HeroData HeroData;

        public override void _Ready()
        {
            HeroData = new HeroData();
        }
    }
}