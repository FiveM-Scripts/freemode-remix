using CitizenFX.Core;
using CitizenFX.Core.UI;
using Freeroam.Utils;
using NativeUI;
using System;
using System.Threading.Tasks;

namespace Freeroam.Menus
{
    class InteractionMenu : BaseScript
    {
        private MenuPool menuPool;
        private UIMenu interactionMenu;
        private bool drawMenu = true;

        public InteractionMenu()
        {
            menuPool = new MenuPool();

            interactionMenu = new UIMenu(Strings.INTERACTIONMENU_MAIN_TITLE, "~b~" + Strings.INTERACTIONMENU_MAIN_SUBTITLE);
            menuPool.Add(interactionMenu);

            AddPlayerSkinSubMenu();
            AddAboutSubMenu();

            Tick += OnTick;
        }

        private void AddPlayerSkinSubMenu()
        {
            UIMenu skinMenu = menuPool.AddSubMenu(interactionMenu, Strings.INTERACTIONMENU_PLAYERSKIN_SUBTITLE);

            UIMenuItem skinSearchItem = new UIMenuItem(Strings.INTERACTIONMENU_PLAYERSKIN_SEARCH);
            skinMenu.AddItem(skinSearchItem);

            string[] pedModelNames = Enum.GetNames(typeof(PedHash));
            Array.Sort(pedModelNames, (x, y) => string.Compare(x, y));
            foreach (string pedHashName in pedModelNames)
            {
                UIMenuItem skinItem = new UIMenuItem(pedHashName);
                skinMenu.AddItem(skinItem);
            }

            skinMenu.RefreshIndex();

            skinMenu.OnItemSelect += async (sender, item, index) =>
            {
                drawMenu = false;

                string pedModelName = item.Text;
                if (item.Text == Strings.INTERACTIONMENU_PLAYERSKIN_SEARCH) pedModelName = await Util.GetUserInput();

                PedHash selectedPedModel;
                if (!Enum.TryParse(pedModelName, out selectedPedModel)) Screen.ShowNotification("~r~" + Strings.INTERACTIONMENU_PLAYERSKIN_INVALID);
                else
                {
                    Screen.ShowNotification("~g~" + Strings.INTERACTIONMENU_PLAYERSKIN_CHANGING);

                    Vehicle currentVeh = Game.PlayerPed.CurrentVehicle;
                    TriggerEvent(Events.PLAYERSKIN_CHANGE, (int)selectedPedModel, currentVeh == null ? 0 : currentVeh.Handle);
                }

                drawMenu = true;
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

        private async Task OnTick()
        {
            if (drawMenu)
            {
                menuPool.ProcessMenus();
                if (Game.IsControlJustReleased(1, Control.InteractionMenu))
                {
                    interactionMenu.Visible = !interactionMenu.Visible;
                }
            }

            await Task.FromResult(0);
        }
    }
}
