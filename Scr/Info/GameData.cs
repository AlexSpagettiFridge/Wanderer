using Godot;
using Wanderer.Abilities;
using Wanderer.Items;

namespace Wanderer.Info
{
    internal partial class GameData : Node
    {
        internal HeroData HeroData;

        internal GameData()
        {
            ItemBank.Init();
            AbilityBank.Init();
        }

        public override void _Ready()
        {
            Util.RegisterRoot(GetParent());
            HeroData = new HeroData();
        }
    }
}