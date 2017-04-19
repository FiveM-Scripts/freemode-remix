﻿using CitizenFX.Core;
using CitizenFX.Core.UI;
using Freeroam.Utils;
using System.Threading.Tasks;

namespace Freeroam.Missions
{
    class Assassination : Mission
    {
        public async void Start()
        {
            Ped ped = await Util.CreatePed(PedHash.Abner, Game.PlayerPed.Position);
            ped.MarkAsNoLongerNeeded();

            MissionManager.StopMission();
        }

        public void Stop()
        {
            Screen.ShowNotification("End!");
        }

        public async Task Tick()
        {


            await Task.FromResult(0);
        }
    }
}
