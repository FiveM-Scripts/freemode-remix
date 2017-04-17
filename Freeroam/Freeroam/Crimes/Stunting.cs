using CitizenFX.Core;
using CitizenFX.Core.UI;
using Freeroam.Utils;
using System;
using System.Threading.Tasks;

namespace Freeroam.Crimes
{
    class Stunting : BaseScript
    {
        private const int MONEY_CAR_MINAIRTIME = 150;
        private const int MONEY_BOAT_MINAIRTIME = 100;

        private int airTime;
        private double payout;

        public Stunting()
        {
            Tick += OnTick;
        }

        private async Task OnTick()
        {
            Ped playerPed = Game.PlayerPed;
            if (playerPed != null && playerPed.IsInVehicle() && playerPed.CurrentVehicle.Driver == playerPed && !playerPed.IsInHeli
                && !playerPed.IsInPlane)
            {
                Vehicle playerVeh = playerPed.CurrentVehicle;
                if (playerPed.IsInBoat) HandleBoatStunting();
                else HandleCarStunting();
            }

            await Task.FromResult(0);
        }

        private async void HandleBoatStunting()
        {
            Vehicle playerVeh = Game.PlayerPed.CurrentVehicle;
            if (!playerVeh.IsInWater && playerVeh.HeightAboveGround > 2f)
            {
                if (airTime >= MONEY_BOAT_MINAIRTIME)
                {
                    payout += 0.2;
                }

                airTime++;
            }
            else
            {
                await Delay(100);
                CheckPayout();
            }
        }

        private async void HandleCarStunting()
        {
            Vehicle playerVeh = Game.PlayerPed.CurrentVehicle;
            if (playerVeh.HeightAboveGround > 2f)
            {
                if (airTime >= MONEY_CAR_MINAIRTIME)
                {
                    payout += 0.2;
                }

                airTime++;
            }
            else
            {
                await Delay(100);
                CheckPayout();
            }
        }

        private void CheckPayout()
        {
            if (payout > 0)
            {
                // Check if player didn't die or fell off the vehicle (bike)
                if (!Game.PlayerPed.IsDead && Game.PlayerPed.CurrentVehicle != null)
                {
                    Screen.ShowNotification(Strings.CRIME_STUNTING_PAYOUT);

                    int realPayout = (int)Math.Ceiling(payout);
                    TriggerEvent(Events.MONEY_ADD, realPayout);
                    TriggerEvent(Events.XP_ADD, realPayout / 15);
                }

                payout = 0;
            }

            airTime = 0;
        }
    }
}
