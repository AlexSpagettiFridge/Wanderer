using Godot;

namespace Wanderer.Info
{
    internal partial class TimerService : Node
    {
        public Timer AddTimer(float time)
        {
            Timer timer = new Timer();
            AddChild(timer);
            timer.WaitTime = time;
            timer.Start();
            timer.Timeout+=()=>ClearTimer(timer);
            return timer;
        }

        private void ClearTimer(Timer timer) { timer.QueueFree(); }
    }
}