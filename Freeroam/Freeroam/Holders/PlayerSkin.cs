using CitizenFX.Core;
using Freeroam.Utils;
using System;
using System.Threading.Tasks;

namespace Freeroam.Holders
{
    class PlayerSkin : BaseScript
    {
        private Model persistantPlayerSkin = VehicleHash.Adder;

        public PlayerSkin()
        {
            EventHandlers[Events.PLAYERSKIN_CHANGE] += new Action<int>(ChangeSkin);

            Tick += OnTick;
        }

        private void ChangeSkin(int newSkinHash)
        {
            persistantPlayerSkin = new Model(newSkinHash);
        }

        private async Task OnTick()
        {
            if (persistantPlayerSkin != VehicleHash.Adder && Game.PlayerPed.Model != persistantPlayerSkin)
            {
                await Util.ChangePlayerSkin(persistantPlayerSkin);
            } 

            await Task.FromResult(0);
        }
    }
}
