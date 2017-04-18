using CitizenFX.Core;
using Freeroam.Utils;
using System;
using System.Threading.Tasks;

namespace Freeroam.Challenges
{
    class DriveDistanceChallenge : BaseScript
    {
        private bool challengeStarted = false;
        //private int 
        private int drivenDistance;

        public DriveDistanceChallenge()
        {
            EventHandlers[Events.CHALLENGE_DRIVEDISTANCE_START] += new Action(StartChallenge);
            EventHandlers[Events.CHALLENGE_DRIVEDISTANCE_STOP] += new Action(StopChallenge);

            Tick += OnTick;
        }

        private void StartChallenge()
        {

        }

        private void StopChallenge()
        {

        }

        private async Task OnTick()
        {
            Ped playerPed = Game.PlayerPed;
            if (playerPed != null && playerPed.IsInVehicle() && playerPed.CurrentVehicle.Driver == playerPed && !playerPed.IsInHeli
                && !playerPed.IsInPlane && !playerPed.IsInBoat)
            {
                Vehicle playerVeh = playerPed.CurrentVehicle;
            }

            await Task.FromResult(0);
        }

        private void CheckDistance()
        {
            Vehicle playerVeh = Game.PlayerPed.CurrentVehicle;
        }
    }
}
