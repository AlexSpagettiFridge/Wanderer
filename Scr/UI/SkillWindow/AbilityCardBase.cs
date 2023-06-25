using Godot;
using Wanderer.Abilities;

namespace Wanderer.Ui.SkillWindow
{
    /// <summary>
    /// Common code for drawing Ability Cards
    /// </summary>
    internal partial class AbilityCardBase : Control
    {
        private Ability _shownAbility = null;
        /// <summary>
        /// The Abilitycard that will be drawn. Will QueueRedraw when changed.
        /// </summary>
        /// <value></value>
        protected Ability shownAbility
        {
            get => _shownAbility;
            set
            {
                _shownAbility = value;
                QueueRedraw();
            }
        }
        private Texture2D backgroundTexture = ResourceLoader.Load<Texture2D>("res://Gfx/Skills/Background.png");

        public AbilityCardBase()
        {
            CustomMinimumSize = new Vector2(32, 48);
        }
        public override void _Draw()
        {
            DrawTextureRect(backgroundTexture, new Rect2(Vector2.Zero, Size), false);
        }
    }
}