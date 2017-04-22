using CitizenFX.Core;
using CitizenFX.Core.UI;
using Freeroam.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Freeroam.Missions
{
    interface IMission
    {
        void Start();
        void Stop(bool success);
        Task Tick();
    }

    internal struct MissionItem
    {
        public string NAME { get; private set; }
        public string DESC { get; private set; }
        public Type MISSION { get; private set; }

        public MissionItem(string name, string desc, Type mission)
        {
            NAME = name;
            DESC = desc;
            MISSION = mission;
        }
    }

    class Mission : BaseScript
    {
        public static MissionItem[] MISSIONS { get; } = new MissionItem[]
        {
            new MissionItem(Strings.MISSIONS_ASSASSINATION_NAME, Strings.MISSIONS_ASSASSINATION_DESC, typeof(Assassination))
        };

        public static IMission CURRENT_MISSION { get; private set; }

        private bool missionRunning = false;

        public Mission()
        {
            EventHandlers[Events.MISSION_START] += new Action<string>(StartMission);
            EventHandlers[Events.MISSION_STOP] += new Action<bool>(StopMission);

            EventHandlers[Events.MISSION_RUNNING] += new Action<int, bool>(ClientStartedMission);

            Tick += OnTick;
        }

        private void StartMission(string missionKey)
        {
            if (CURRENT_MISSION != null || missionRunning) throw new MissionAlreadyRunningException();
            else
            {
                TriggerServerEvent(Events.MISSION_RUNNING, Game.Player.ServerId, Game.Player.Handle, true);

                if (Game.PlayerPed != null)
                {
                    MissionItem missionItem = MISSIONS.Where(i => i.NAME == missionKey).First();
                    CURRENT_MISSION = (IMission)Activator.CreateInstance(missionItem.MISSION);
                    CURRENT_MISSION.Start();
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

                TriggerServerEvent(Events.MISSION_RUNNING, Game.Player.ServerId, Game.Player.Handle, false);
            }
        }

        private void ClientStartedMission(int clientHandle, bool state)
        {
            missionRunning = state;

            string playerName = new Player(clientHandle).Name;
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
