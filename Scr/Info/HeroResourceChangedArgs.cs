using System;

namespace Wanderer.Info
{
    internal class HeroResourceChangedArgs : EventArgs
    {
        public ChangeType Type;
        public float OldValue;

        public HeroResourceChangedArgs(ChangeType type,float oldValue)
        {
            Type = type;
            OldValue = oldValue;
        }

        public enum ChangeType
        {
            Value, Max, Absolute
        }
    }
}