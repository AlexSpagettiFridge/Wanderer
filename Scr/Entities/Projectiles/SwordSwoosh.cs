using System;
using Godot;

namespace Wanderer.Entities.Projectiles
{
    [Tool]
    internal partial class SwordSwoosh : Area2D
    {
        #region Head
        [Export]
        private CollisionPolygon2D _collisionPolygon { get => collisionPolygon; set { collisionPolygon = value; UpdateCollisionShape(); } }
        [Export]
        private float _width { get => width; set { width = value; UpdateCollisionShape(); } }
        [Export]
        private float _angle { get => angle; set { angle = value; UpdateCollisionShape(); } }
        [Export]
        private float _startRadius { get => startRadius; set { startRadius = value; UpdateCollisionShape(); } }
        [Export]
        private float _endRadius { get => endRadius; set { endRadius = value; UpdateCollisionShape(); } }

        private CollisionPolygon2D collisionPolygon = null;
        private float width = 1, angle = 0, startRadius = 12, endRadius = 40;
        #endregion

        public SwordSwoosh(float width, float angle, float startRadius, float endRadius)
        {
            this.width = width;
            this.angle = angle;
            this.startRadius = startRadius;
            this.endRadius = endRadius;
        }
        public SwordSwoosh(){}

        public override void _Ready()
        {
            if (collisionPolygon == null && !Engine.IsEditorHint())
            {
                collisionPolygon = new CollisionPolygon2D();
                AddChild(collisionPolygon);
            }
            UpdateCollisionShape();
        }

        private void UpdateCollisionShape()
        {
            QueueRedraw();
            if (collisionPolygon == null) { return; }
            int curvePoints = (int)Math.Floor(Math.Max(width / Mathf.DegToRad(15), 2));
            Vector2[] points = new Vector2[curvePoints + 2];

            for (int i = 0; i <= curvePoints; i++)
            {
                float pointAngle = angle - width / 2 + (float)i / curvePoints * width;
                Vector2 pointVector = new Vector2(Mathf.Cos(pointAngle) * endRadius, Mathf.Sin(pointAngle) * endRadius);
                points[i + 1] = pointVector;
            }
            collisionPolygon.Polygon = points;
            QueueRedraw();
        }

        public override void _Draw()
        {
            DrawArc(Vector2.Zero, endRadius - startRadius / 2, angle - width / 2, angle + width / 2, 14, new Color(1, 1, 1, 1), endRadius - startRadius);
        }
    }
}