using CitizenFX.Core;
using Freeroam.Utils;
using System;
using System.Threading.Tasks;

namespace Freeroam.Holders
{
    class PlayerSkin : BaseScript
    {
        private Model persistantPlayerSkin = VehicleHash.Adder;
        private Vehicle setIntoVeh;

        public PlayerSkin()
        {
            EventHandlers[Events.PLAYERSKIN_CHANGE] += new Action<int, int>(ChangeSkin);

            Tick += OnTick;
        }

        private void ChangeSkin(int newSkinHash, int currentVehHandle)
        {
            persistantPlayerSkin = new Model(newSkinHash);
        }

        private async Task OnTick()
        {
            if (persistantPlayerSkin != VehicleHash.Adder && Game.PlayerPed.Model != persistantPlayerSkin)
            {
                await Util.ChangePlayerSkin(persistantPlayerSkin);
                if (setIntoVeh != null)
                {
                    Game.PlayerPed.SetIntoVehicle(setIntoVeh, VehicleSeat.Any);
                    setIntoVeh = null;
                }
            } 

            await Task.FromResult(0);
        }
    }
}
