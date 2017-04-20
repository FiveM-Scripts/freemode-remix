using CitizenFX.Core;
using CitizenFX.Core.UI;
using Freeroam.Utils;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Freeroam.Missions
{
    interface Mission
    {
        void Start();
        void Stop(bool success);
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

        private bool missionRunning = false;

        public MissionManager()
        {
            EventHandlers[Events.MISSION_START] += new Action<string>(StartMission);
            EventHandlers[Events.MISSION_STOP] += new Action<bool>(StopMission);

            EventHandlers[Events.MISSION_RUNNING] += new Action<string, bool>(ClientStartedMission);

            Tick += OnTick;
        }

        private void StartMission(string missionKey)
        {
            if (CURRENT_MISSION != null || missionRunning) throw new MissionAlreadyRunningException();
            else
            {
                TriggerServerEvent(Events.MISSION_RUNNING, Game.Player.ServerId, true);

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
        }

        private void StopMission(bool success)
        {
            if (CURRENT_MISSION != null)
            {
                if (!success) Screen.ShowNotification(Strings.MISSIONS_FAIL);
                CURRENT_MISSION.Stop(success);
                CURRENT_MISSION = null;

                TriggerServerEvent(Events.MISSION_RUNNING, Game.Player.ServerId, false);
            }
        }

        private void ClientStartedMission(string playerName, bool state)
        {
            missionRunning = state;

            string text = string.Format(state ? Strings.PHONEMENU_MISSIONS_MISSIONSTARTED : Strings.PHONEMENU_MISSIONS_MISSIONSTOPPED, $"~b~{playerName}~w~");
            Screen.ShowNotification(text);
        }

        private async Task OnTick()
        {
            if (CURRENT_MISSION != null) await CURRENT_MISSION.Tick();

            await Task.FromResult(0);
        }

        internal class MissionAlreadyRunningException : Exception { }
    }
}
