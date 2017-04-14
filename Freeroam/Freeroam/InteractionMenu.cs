﻿using CitizenFX.Core;
using CitizenFX.Core.UI;
using Freeroam.Utils;
using NativeUI;
using System;
using System.Threading.Tasks;

namespace Freeroam
{
    class InteractionMenu : BaseScript
    {
        private MenuPool menuPool;
        private UIMenu interactionMenu;

        public InteractionMenu()
        {
            menuPool = new MenuPool();

            interactionMenu = new UIMenu(Strings.INTERACTIONMENU_MAIN_TITLE, "~b~" + Strings.INTERACTIONMENU_MAIN_SUBTITLE);
            menuPool.Add(interactionMenu);

            AddPlayerSkinSubMenu();
            AddAboutSubMenu();

            Tick += OnTick;
        }

        private async Task OnTick()
        {
            menuPool.ProcessMenus();
            if (Game.IsControlJustReleased(1, Control.InteractionMenu))
            {
                interactionMenu.Visible = !interactionMenu.Visible;
            }

            await Task.FromResult(0);
        }

        private void AddPlayerSkinSubMenu()
        {
            UIMenu skinMenu = menuPool.AddSubMenu(interactionMenu, Strings.INTERACTIONMENU_PLAYERSKIN_SUBTITLE);

            string[] pedModelNames = Enum.GetNames(typeof(PedHash));
            Array.Sort(pedModelNames, (x, y) => string.Compare(x, y));
            foreach (string pedHashName in pedModelNames)
            {
                UIMenuItem skinItem = new UIMenuItem(pedHashName);
                skinMenu.AddItem(skinItem);
            }

            skinMenu.RefreshIndex();

            skinMenu.OnItemSelect += (sender, item, index) =>
            {
                PedHash selectedPedModel;
                Enum.TryParse(item.Text, out selectedPedModel);
                Screen.ShowNotification("~g~" + Strings.INTERACTIONMENU_PLAYERSKIN_CHANGED);
                TriggerEvent(Events.PLAYERSKIN_CHANGE, (int) selectedPedModel);
            };
        }

        private void AddAboutSubMenu()
        {
            UIMenu aboutMenu = menuPool.AddSubMenu(interactionMenu, Strings.INTERACTIONMENU_ABOUT);

            UIMenuItem versionItem = new UIMenuItem(Strings.INTERACTIONMENU_ABOUT_VERSION);
            aboutMenu.AddItem(versionItem);

            UIMenuItem authorItem = new UIMenuItem(Strings.INTERACTIONMENU_ABOUT_AUTHOR, Strings.INTERACTIONMENU_ABOUT_AUTHOR_DESC);
            aboutMenu.AddItem(authorItem);
        }
    }
}
