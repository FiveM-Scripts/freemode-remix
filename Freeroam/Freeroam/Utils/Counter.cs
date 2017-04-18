using NativeUI;

namespace Freeroam.Utils
{
    class Counter
    {
        private TextTimerBar counter;

        public Counter(string label, string text)
        {
            counter = new TextTimerBar(label, text);
        }

        public void Draw()
        {
            counter.Draw(20);
        }
    }
}
