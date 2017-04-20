using CitizenFX.Core;
using CitizenFX.Core.Native;
using Freeroam.Utils;

namespace Freeroam
{
    class Freeroam : BaseScript
    {
        public Freeroam()
        {
            TriggerServerEvent(Events.FIRSTTIME, Game.Player.ServerId);
            SetPauseMenuTitle();
        }

        private void SetPauseMenuTitle()
        {
            Function.Call((Hash)Util.GetHashKey("ADD_TEXT_ENTRY"), "FE_THDR_GTAO", "FiveM: Freeroam");
        }
    }
}
