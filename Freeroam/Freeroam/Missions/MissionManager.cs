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
        Task Tick();
    }

    static class Missions
    {
        public const string ASSASSINATION = "Assassination";
    }

    class MissionManager : BaseScript
    {
        private static Dictionary<string, Type> missions = new Dictionary<string, Type>()
        {
            [Missions.ASSASSINATION] = typeof(Assassination)
        };

        public static Mission CURRENT_MISSION { get; private set; }

        public MissionManager()
        {
            EventHandlers[Events.CHALLENGE_START] += new Action<string>(StartMission);
            EventHandlers[Events.CHALLENGE_STOP] += new Action(StopMission);

            Tick += OnTick;
        }

        public static void StartMission(string missionKey)
        {
            if (CURRENT_MISSION != null) throw new MissionAlreadyRunningException();

            if (Game.PlayerPed != null)
            {
                Type challenge;
                if (missions.TryGetValue(missionKey, out challenge))
                {
                    CURRENT_MISSION = (Mission)Activator.CreateInstance(challenge);
                    CURRENT_MISSION.Start();
                }
            }
        }

        public static void StopMission()
        {
            if (CURRENT_MISSION != null)
            {
                CURRENT_MISSION.Stop();
                CURRENT_MISSION = null;
            }
        }

        private async Task OnTick()
        {
            if (CURRENT_MISSION != null) await CURRENT_MISSION.Tick();

            await Task.FromResult(0);
        }

        private class MissionAlreadyRunningException : Exception { }
    }
}
