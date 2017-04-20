using CitizenFX.Core;
using CitizenFX.Core.UI;
using Freeroam.Utils;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Freeroam.Missions
{
    interface IMission
    {
        void Start();
        void Stop(bool success);
        Task Tick();
    }

    static class Missions
    {
        public const string ASSASSINATION = "Assassination";
    }

    class Mission : BaseScript
    {
        private static Dictionary<string, Type> missions = new Dictionary<string, Type>()
        {
            [Missions.ASSASSINATION] = typeof(Assassination)
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
                    Type challenge;
                    if (missions.TryGetValue(missionKey, out challenge))
                    {
                        CURRENT_MISSION = (IMission)Activator.CreateInstance(challenge);
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

    internal class MissionPlayerBlip : BaseScript
    {
        private BlipColor prevPlayerBlipColor;

        public MissionPlayerBlip()
        {
            EventHandlers[Events.MISSION_RUNNING] += new Action<int, bool>((clientHandle, state) =>
            {
                Player missionPlayer = new Player(clientHandle);
                if (missionPlayer != Game.Player)
                {
                    Ped playerPed = missionPlayer.Character;
                    if (state)
                    {
                        prevPlayerBlipColor = playerPed.AttachedBlip.Color;
                        playerPed.AttachedBlip.Color = BlipColor.Yellow;
                    }
                    else playerPed.AttachedBlip.Color = prevPlayerBlipColor;
                }
            });
        }
    }
}
