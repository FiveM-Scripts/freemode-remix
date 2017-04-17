using CitizenFX.Core;
using CitizenFX.Core.Native;
using CitizenFX.Core.UI;
using System.Threading.Tasks;

namespace Freeroam.Players
{
    class PlayerBlips : BaseScript
    {
        public PlayerBlips()
        {
            Tick += OnTick;
        }

        private async Task OnTick()
        {
            foreach (Player player in Players)
            {
                Ped playerPed = player.Character;
                if (player != Game.Player && playerPed != null && playerPed.AttachedBlip == null)
                {
                    Blip playerBlip = playerPed.AttachBlip();
                    playerBlip.Sprite = BlipSprite.Standard;
                    playerBlip.Scale = 0.8f;
                    Function.Call(Hash.SET_BLIP_NAME_TO_PLAYER_NAME, playerBlip.Handle, player.Handle);
                    Function.Call(Hash._SET_BLIP_SHOW_HEADING_INDICATOR, playerBlip.Handle, true);
                }
            }

            await Task.FromResult(0);
        }
    }
}
