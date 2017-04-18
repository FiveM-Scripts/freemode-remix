using CitizenFX.Core;
using Freeroam.Utils;
using NativeUI;
using System.Drawing;
using System.Threading.Tasks;

namespace Freeroam.Menus
{
    class PhoneMenu : BaseScript
    {
        private static UIResRectangle phoneTitle = new UIResRectangle(new PointF(), new SizeF(100f, 100f), Color.FromArgb(255, 0, 156, 0));

        private MenuPool menuPool;
        private UIMenu interactionMenu;

        public PhoneMenu()
        {
            menuPool = new MenuPool();

            interactionMenu = new UIMenu(Strings.PHONEMENU_MAIN_TITLE, "~g~" + Strings.PHONEMENU_MAIN_SUBTITLE);
            interactionMenu.SetBannerType(phoneTitle);
            menuPool.Add(interactionMenu);

            AddMissionsMenu();

            Tick += OnTick;
        }

        private void AddMissionsMenu()
        {
            UIMenu missionsMenu = menuPool.AddSubMenu(interactionMenu, Strings.PHONEMENU_MISSIONS_MENU);
            missionsMenu.SetBannerType(phoneTitle);
        }

        private async Task OnTick()
        {
            menuPool.ProcessMenus();
            if (Game.IsControlJustReleased(1, Control.Phone) && !interactionMenu.Visible)
            {
                interactionMenu.Visible = !interactionMenu.Visible;
            }

            await Task.FromResult(0);
         }
    }
}
