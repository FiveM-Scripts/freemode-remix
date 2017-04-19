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

    enum Missions
    {
        ASSASSINATION
    }

    class MissionManager : BaseScript
    {
        private static Dictionary<Missions, Type> missions = new Dictionary<Missions, Type>()
        {
            [Missions.ASSASSINATION] = typeof(Assassination)
        };

        public static Mission CURRENT_MISSION { get; private set; }

        public MissionManager()
        {
            EventHandlers[Events.CHALLENGE_START] += new Action<Missions>(StartMission);
            EventHandlers[Events.CHALLENGE_STOP] += new Action(StopMission);

            Tick += OnTick;
        }

        public static void StartMission(Missions missionKey)
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
