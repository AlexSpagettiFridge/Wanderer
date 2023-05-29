using Godot;

namespace Wanderer.Entities.Enviroment
{
    internal partial class GroundTile: Node2D
    {
        [Export]
        private AnimationPlayer animationPlayer;
        public override void _Ready()
        {
            animationPlayer.Play("Instance");
        }
    }
}