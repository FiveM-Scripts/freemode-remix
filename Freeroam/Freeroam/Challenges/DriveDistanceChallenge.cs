using System;
using CitizenFX.Core;

namespace Freeroam.Challenges
{
    class DriveDistanceChallenge : Challenge
    {
        private bool challengeStarted = false;
        private float drivenDistance;
        private float bestDrivenDistance;
        private Vector3 lastPos;

        void Challenge.Start()
        {
            throw new NotImplementedException();
        }

        void Challenge.Stop()
        {
            throw new NotImplementedException();
        }

        void Challenge.Tick()
        {
            Ped playerPed = Game.PlayerPed;
            if (playerPed.IsInVehicle() && playerPed.CurrentVehicle.Driver == playerPed && !playerPed.IsInHeli
                && !playerPed.IsInPlane && !playerPed.IsInBoat)
            {
                CheckDistance();
            }
            else drivenDistance = 0;
        }

        private void CheckDistance()
        {
            Vector3 vehPos = Game.PlayerPed.CurrentVehicle.Position;
            if (lastPos != null)
            {
                drivenDistance += World.GetDistance(vehPos, lastPos);
            }

            lastPos = vehPos;
        }
    }
}
