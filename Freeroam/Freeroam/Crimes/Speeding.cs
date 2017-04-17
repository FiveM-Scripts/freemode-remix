using CitizenFX.Core;
using CitizenFX.Core.UI;
using Freeroam.Utils;
using System;
using System.Threading.Tasks;

namespace Freeroam.Crimes
{
    class Speeding : BaseScript
    {
        private const int MONEY_TIMEUNTILPAYOUT = 5000;
        private const int MINSPEED = 150;

        private int moneyCountdown = MONEY_TIMEUNTILPAYOUT;

        public Speeding()
        {
            Tick += OnTick;
        }

        private async Task OnTick()
        {
            Ped playerPed = Game.PlayerPed;
            if (playerPed != null && playerPed.IsInVehicle() && playerPed.CurrentVehicle.Driver == playerPed && !playerPed.IsInHeli
                && !playerPed.IsInPlane && !playerPed.IsInBoat)
            {
                Vehicle playerVeh = playerPed.CurrentVehicle;
                if (Util.GetVehKMHSpeed(playerVeh) > MINSPEED)
                {
                    WantedLevelChance();

                    if (moneyCountdown <= 0)
                    {
                        Screen.ShowNotification(Strings.CRIME_SPEEDING_PAYOUT);
                        TriggerEvent(Events.MONEY_ADD, 100);
                        TriggerEvent(Events.XP_ADD, 7);
                        moneyCountdown = MONEY_TIMEUNTILPAYOUT;
                    }

                    moneyCountdown--;
                }
                else moneyCountdown = MONEY_TIMEUNTILPAYOUT;
            }

            await Task.FromResult(0);
        }

        private void WantedLevelChance()
        {
            Vehicle veh = Util.GetClosestVeh(Game.PlayerPed.Position, 20f);
            if (veh != null && veh.Driver != null)
            {
                int chance = new Random().Next(1000);
                if (chance == 50 && Game.Player.WantedLevel == 0) Game.Player.WantedLevel = 1;
            }
        }
    }
}
