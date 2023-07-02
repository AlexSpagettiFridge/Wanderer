using System;

namespace Wanderer.Info
{
    internal class HeroResource
    {
        public event EventHandler<HeroResourceChangedArgs> ValueChanged;
        private float value = 0;
        private int max = 0, absoluteMax = 0;

        public HeroResource(int max)
        {
            value = max;
            this.max = max;
            absoluteMax = max;
        }

        public void Exhaust(int amount, float exhaustion = .025f)
        {
            float oldValue = value;
            value = Math.Max(0, value - amount);
            ValueChanged?.Invoke(this, new HeroResourceChangedArgs(HeroResourceChangedArgs.ChangeType.Value, oldValue));
            Max = (int)Math.Ceiling(Math.Max(0, Max - amount * exhaustion));
        }
        public void Restore(float amount) { value = Math.Min(max, value + amount); }

        #region Value getter/setter
        public int Value
        {
            get => (int)Math.Floor(value);
            set
            {
                float oldValue = this.value;
                this.value = Math.Clamp((float)value, 0, max);
                ValueChanged?.Invoke(this, new HeroResourceChangedArgs(HeroResourceChangedArgs.ChangeType.Value, oldValue));
            }
        }
        public int Max
        {
            get => max;
            set
            {
                float oldValue = max;
                max = Math.Clamp(value, 0, absoluteMax);
                ValueChanged?.Invoke(this, new HeroResourceChangedArgs(HeroResourceChangedArgs.ChangeType.Max, oldValue));
                if (this.value > max) { Value = max; }
            }
        }
        public int AbsoluteMax
        {
            get => absoluteMax;
            set
            {
                float oldValue = absoluteMax;
                absoluteMax = Math.Max(value, 0);
                ValueChanged?.Invoke(this, new HeroResourceChangedArgs(HeroResourceChangedArgs.ChangeType.Absolute, oldValue));
                if (max > absoluteMax)
                {
                    Max = absoluteMax;
                }
            }
        }
        #endregion
    }
}