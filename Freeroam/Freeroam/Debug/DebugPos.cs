using CitizenFX.Core;
using System.Threading.Tasks;

namespace Freeroam.Debug
{
    class DebugPos : BaseScript
    {
        public DebugPos()
        {
#if DEBUG
            Tick += OnTick;
#endif
        }

        private async Task OnTick()
        {
            if (Game.IsControlJustReleased(1, Control.EnterCheatCode))
            {
                Vector3 playerPos = Game.PlayerPed.Position;
                TriggerEvent("chatMessage", "", new[] { 0, 0, 0 }, $"{playerPos.X} {playerPos.Y} {playerPos.Z}");
            }

            await Task.FromResult(0);
        }
    }
}
