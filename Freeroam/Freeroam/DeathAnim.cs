using CitizenFX.Core;
using CitizenFX.Core.UI;
using System.Threading.Tasks;

namespace Freeroam
{
    class DeathAnim : BaseScript
    {
        private bool alreadyShown = false;

        public DeathAnim()
        {
            Tick += OnTick;
        }

        private async Task OnTick()
        {
            Ped playerPed = Game.PlayerPed;
            if (playerPed != null)
            {
                if (playerPed.IsDead)
                {
                    if (!alreadyShown)
                    {
                        Screen.Effects.Start(ScreenEffect.DeathFailMpIn);
                        Game.PlaySound("Bed", "WastedSounds");
                        alreadyShown = true;
                    }
                }
                else if (alreadyShown)
                {
                    Screen.Effects.Stop(ScreenEffect.DeathFailMpIn);
                    alreadyShown = false;
                }
            }

            await Task.FromResult(0);
        }
    }
}
