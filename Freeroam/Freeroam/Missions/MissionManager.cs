using CitizenFX.Core;
using Freeroam.Utils;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Freeroam.Missions
{
    interface Mission
    {
        void Start();
        void Stop();
        void Tick();
    }

    internal static class Missions
    {
    }

    class MissionManager : BaseScript
    {
        private static Dictionary<string, Type> challenges = new Dictionary<string, Type>()
        {

        };

        public static Mission CURRENT_MISSION { get; private set; }

        public MissionManager()
        {
            EventHandlers[Events.CHALLENGE_START] += new Action<string>(StartMission);
            EventHandlers[Events.CHALLENGE_STOP] += new Action(StopMission);

            Tick += OnTick;
        }

        private void StartMission(string challengeKey)
        {
            if (CURRENT_MISSION != null) throw new ChallengeAlreadyRunningException();

            if (Game.PlayerPed != null)
            {
                Type challenge;
                if (challenges.TryGetValue(challengeKey, out challenge))
                {
                    CURRENT_MISSION = (Mission)Activator.CreateInstance(challenge);
                }
            }
        }

        private void StopMission()
        {
            if (CURRENT_MISSION != null)
            {
                CURRENT_MISSION.Stop();
                CURRENT_MISSION = null;
            }
        }

        private async Task OnTick()
        {
            // Hi

            await Task.FromResult(0);
        }

        private class ChallengeAlreadyRunningException : Exception { }
    }
}
