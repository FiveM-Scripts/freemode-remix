using CitizenFX.Core;
using CitizenFX.Core.UI;
using Freeroam.Utils;
using NativeUI;
using System.Drawing;
using System.Threading.Tasks;

namespace Freeroam
{
    class PhoneMenu : BaseScript
    {
        private MenuPool menuPool;
        private UIMenu interactionMenu;

        public PhoneMenu()
        {
            menuPool = new MenuPool();

            interactionMenu = new UIMenu(Strings.PHONEMENU_MAIN_TITLE, "~g~" + Strings.PHONEMENU_MAIN_SUBTITLE);
            interactionMenu.SetBannerType(new UIResRectangle(new PointF(), new SizeF(100f, 100f), Color.FromArgb(255, 0, 156, 0)));
            menuPool.Add(interactionMenu);

            Tick += OnTick;
        }

        private void AddSomeMenuLater()
        {
            // TODO: Change this (copied over)

            UIMenu aboutMenu = menuPool.AddSubMenu(interactionMenu, Strings.INTERACTIONMENU_ABOUT);

            UIMenuItem versionItem = new UIMenuItem(Strings.INTERACTIONMENU_ABOUT_VERSION);
            aboutMenu.AddItem(versionItem);

            UIMenuItem authorItem = new UIMenuItem(Strings.INTERACTIONMENU_ABOUT_AUTHOR, Strings.INTERACTIONMENU_ABOUT_AUTHOR_DESC);
            aboutMenu.AddItem(authorItem);
        }

        private async Task OnTick()
        {
            menuPool.ProcessMenus();
            if (Game.IsControlJustReleased(1, Control.Phone))
            {
                interactionMenu.Visible = !interactionMenu.Visible;
            }

            await Task.FromResult(0);
         }
    }
}
