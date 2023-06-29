using Godot;
using System;
using Wanderer.Info;
using Wanderer.Items;
using Wanderer.Items.GameItems;

namespace Wanderer.Entities
{

    internal partial class Hero : CharacterBody2D
    {
        private const float Speed = 120, Acceleration = 1700, Friction = 900;
        public event Action StoppedWalking;
        private double walkTime;

        public Hero()
        {
            StoppedWalking += OnWalkingStopped;
        }

        public override void _Input(InputEvent @event)
        {
            // Handling Ability usage
            int skillPress = -1;
            if (@event.IsAction("gm_skill1")) { skillPress = 0; }
            if (@event.IsAction("gm_skill2")) { skillPress = 1; }
            if (@event.IsAction("gm_skill3")) { skillPress = 2; }
            if (@event.IsAction("gm_skill4")) { skillPress = 3; }
            if (@event.IsAction("gm_skill5")) { skillPress = 4; }
            if (skillPress == -1) { return; }
            if (@event.IsPressed())
            {
                Util.GameData.HeroData.Abilities[skillPress]?.TryInvoke(this);
            }
            else
            {
                Util.GameData.HeroData.Abilities[skillPress]?.Release(this);
            }
        }

        public override void _Process(double delta)
        {
            Vector2 movementInput = new Vector2(
                Input.GetAxis("gm_left", "gm_right"),
                Input.GetAxis("gm_up", "gm_down")
                );
            Velocity += movementInput * (float)(Acceleration * delta);
            if (Velocity.Length() > Speed)
            {
                Velocity = Velocity.Normalized() * Speed;
            }
            Velocity = Velocity.MoveToward(Vector2.Zero, (float)(delta * Friction));
            MoveAndSlide();
            if (Velocity == Vector2.Zero)
            {
                if (walkTime > 0) { StoppedWalking.Invoke(); }
                walkTime = 0;
            }
            else
            {
                walkTime += delta;
            }
        }

        private void OnWalkingStopped()
        {
            //Position = (Position / 4).Round() * 4;
        }
    }
}