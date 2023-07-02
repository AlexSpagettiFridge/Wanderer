using Godot;
using Wanderer.Info;

namespace Wanderer.Ui.Gui
{
    [Tool]
    internal partial class HeroResourceBar : ProgressBar
    {
        private HeroResource presentedResource;
        private double subMax = 100;
        [Export]
        public double SubMax
        {
            get => subMax;
            set
            {
                subMax = value;
                QueueRedraw();
            }
        }

        public HeroResource PresentedResource
        {
            get => presentedResource;
            set
            {
                presentedResource = value;
                MaxValue = presentedResource.AbsoluteMax;
                SubMax = presentedResource.Max;
                Value = presentedResource.Value;
                presentedResource.ValueChanged += OnResourceUpdated;
            }
        }

        public override void _Ready()
        {
            Draw += DrawMaxBar;
        }

        private void DrawMaxBar()
        {
            Theme theme = Util.GetInheritedTheme(this);
            if (theme == null) { return; }
            if (!theme.HasStylebox("maxBar", "ProgressBar")) { return; }
            StyleBox styleBox = theme.GetStylebox("maxBar", "ProgressBar");
            float barLenght = (float)((MaxValue - SubMax) / MaxValue * Size.X);
            DrawStyleBox(styleBox, new Rect2(Size.X - barLenght, 0, barLenght, Size.Y));
        }

        private void OnResourceUpdated(object sender, HeroResourceChangedArgs args)
        {
            switch (args.Type)
            {
                case HeroResourceChangedArgs.ChangeType.Value:
                    Tween tween = GetTree().CreateTween();
                    Value = args.OldValue;
                    tween.TweenProperty(this,"value",presentedResource.Value,0.2f);
                    break;
                case HeroResourceChangedArgs.ChangeType.Max:
                    SubMax = presentedResource.Max;
                    break;
                case HeroResourceChangedArgs.ChangeType.Absolute:
                    MaxValue = presentedResource.AbsoluteMax;
                    return;
            }
            QueueRedraw();
        }
    }
}