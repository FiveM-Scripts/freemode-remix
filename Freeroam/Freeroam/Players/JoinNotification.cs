using CitizenFX.Core;
using CitizenFX.Core.UI;
using Freeroam.Utils;
using System;

namespace Freeroam.Players
{
    class JoinNotification : BaseScript
    {
        public JoinNotification()
        {
            EventHandlers["onClientMapStart"] += new Action<dynamic>(ecksdee => TriggerServerEvent(Events.PLAYER_JOINED, Game.Player.Handle));
            EventHandlers[Events.PLAYER_JOINED] += new Action<string>(name => Screen.ShowNotification(string.Format("~b~" + Strings.PLAYER_JOINED, name)));
            EventHandlers[Events.PLAYER_LEFT] += new Action<string>(name => Screen.ShowNotification(string.Format("~r~" + Strings.PLAYER_LEFT, name)));
        }
    }
}
