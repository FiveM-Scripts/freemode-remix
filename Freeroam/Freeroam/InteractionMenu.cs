using CitizenFX.Core;
using Freeroam.Utils;
using NativeUI;
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

            interactionMenu = new UIMenu(Game.Player.Name, "~b~" + Strings.INTERACTIONMENU_MAIN_SUBTITLE);
            menuPool.Add(interactionMenu);

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
