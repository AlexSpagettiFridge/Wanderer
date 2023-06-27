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

        public override void _UnhandledInput(InputEvent @event)
        {
            if (@event.IsActionPressed("gm_skill1"))
            {
                Item weapon = Util.GameData.HeroData.Equipment[0].Items[0];
                if (weapon != null)
                {
                    if (weapon.GetHandler() is ItemWeapon weaponHandler)
                    {
                        weaponHandler.Attack(this, weapon);
                    }
                }
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