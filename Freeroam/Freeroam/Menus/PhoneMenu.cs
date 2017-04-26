using CitizenFX.Core;
using CitizenFX.Core.UI;
using Freeroam.Missions;
using Freeroam.Utils;
using NativeUI;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;

namespace Freeroam.Menus
{
    class PhoneMenu : BaseScript
    {
        private static UIResRectangle phoneTitle = new UIResRectangle(new PointF(), new SizeF(100f, 100f), Color.FromArgb(255, 0, 156, 0));
        private const int MISSION_COOLDOWN_SECS = 120;

        private MenuPool menuPool;
        private UIMenu interactionMenu;
        private UIMenu playerListMenu;
        private UIMenu missionsMenu;

        private bool missionRunning = false;
        private static int missionCooldown;

        public PhoneMenu()
        {
            menuPool = new MenuPool();

            interactionMenu = new UIMenu(Strings.PHONEMENU_MAIN_TITLE, "~g~" + Strings.PHONEMENU_MAIN_SUBTITLE);
            interactionMenu.SetBannerType(phoneTitle);
            menuPool.Add(interactionMenu);

            AddPlayerListMenu();
            AddMissionsMenu();

            EventHandlers[Events.MISSION_STOP] += new Action<bool>(success => missionCooldown = MISSION_COOLDOWN_SECS);
            EventHandlers[Events.MISSION_RUNNING] += new Action<int, bool>(ClientStartedMission);

            Tick += OnTick;
        }

        private void AddPlayerListMenu()
        {
            playerListMenu = AddSubMenu(Strings.PHONEMENU_PLAYERLIST_MENU);
        }

        private void UpdatePlayerListMenu()
        {
            if (playerListMenu.MenuItems.Count() != Players.Count())
            {
                playerListMenu.Clear();
                foreach (Player player in Players)
                {
                    UIMenuItem playerItem = new UIMenuItem(player.Name);
                    playerListMenu.AddItem(playerItem);
                }
            }
        }

        private void AddMissionsMenu()
        {
            missionsMenu = AddSubMenu(Strings.PHONEMENU_MISSIONS_MENU);

            foreach (MissionItem mission in Mission.MISSIONS)
            {
                UIMenuItem missionItem = new UIMenuItem(mission.NAME);
                missionItem.Description = mission.DESC;
                missionsMenu.AddItem(missionItem);
            }

            missionsMenu.OnItemSelect += (sender, item, index) =>
            {
                if (missionCooldown > 0) Screen.ShowNotification(string.Format("~r~" + Strings.PHONEMENU_MISSIONS_MISSIONCOOLDOWN, missionCooldown));
                else if (missionRunning) Screen.ShowNotification("~r~" + Strings.PHONEMENU_MISSIONS_MISSIONRUNNING);
                else
                {
                    missionsMenu.Visible = false;

                    string missionName = item.Text;
                    TriggerEvent(Events.MISSION_START, missionName);
                }
            };
        }

        private UIMenu AddSubMenu(string name)
        {
            UIMenu menu = menuPool.AddSubMenu(interactionMenu, name);
            menu.SetBannerType(phoneTitle);
            return menu;
        }

        private void ClientStartedMission(int clientHandle, bool state)
        {
            missionRunning = state;
        }

        private async Task OnTick()
        {
            menuPool.ProcessMenus();
            if (Game.IsControlJustReleased(1, Control.ReplayStartpoint) && !interactionMenu.Visible)
            {
                interactionMenu.Visible = !interactionMenu.Visible;
            }

            if (playerListMenu.Visible) UpdatePlayerListMenu();

            await Task.FromResult(0);
         }

        internal class MissionCooldownCounter : BaseScript
        {
            public MissionCooldownCounter()
            {
                Tick += OnTick;
            }

            private async Task OnTick()
            {
                if (missionCooldown > 0)
                {
                    await Delay(1000);
                    missionCooldown--;
                }

                await Task.FromResult(0);
            }
        }
    }
}
