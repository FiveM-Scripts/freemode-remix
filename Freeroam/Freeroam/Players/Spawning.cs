using CitizenFX.Core;
using CitizenFX.Core.UI;
using Freeroam.Utils;
using System;
using System.Threading.Tasks;

namespace Freeroam.Players
{
    class Spawning : BaseScript
    {
        /**
              __________  ____  ____ 
             /_  __/ __ \/ __ \/ __ \
              / / / / / / / / / / / /
             / / / /_/ / /_/ / /_/ / 
            /_/  \____/_____/\____/  

            Make this non-hacky and override spawnmanager
                         
         */


        private Vector3 prevPos = Vector3.Zero;
        private bool posAlreadyFetched = false;

        public Spawning()
        {
            EventHandlers["playerSpawned"] += new Action<dynamic>(SpawnNearby);

            Tick += OnTick;
        }

        private async void SpawnNearby(dynamic hi)
        {
            if (prevPos != Vector3.Zero)
            {
                await Delay(200);

                int nth = new Random().Next(50, 100);
                Vector3 spawnPos; float heading;
                Util.GetClosestVehNode(prevPos, out spawnPos, out heading, nth);

                Game.PlayerPed.Position = spawnPos;
                prevPos = Vector3.Zero;
                posAlreadyFetched = false;
            }
        }

        private async Task OnTick()
        {
            if (Game.PlayerPed != null && Game.PlayerPed.IsDead)
            {
                if (!posAlreadyFetched)
                {
                    prevPos = Game.PlayerPed.Position;
                    posAlreadyFetched = true;

                    await Delay(2000);
                    for (int i = 0; i < 230; i++)
                    {
                        if (!Screen.Fading.IsFadedOut && !Screen.Fading.IsFadingOut) Screen.Fading.FadeOut(500);
                        await Delay(1);
                    }
                    Screen.Fading.FadeIn(500);
                }
            }

            await Task.FromResult(0);
        }
    }
}
