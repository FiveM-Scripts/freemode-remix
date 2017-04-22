using CitizenFX.Core;
using CitizenFX.Core.Native;
using Freeroam.Utils;
using System;

namespace Freeroam.Players
{
    class PvP : BaseScript
    {
        public PvP()
        {
            EventHandlers[Events.PLAYERSPAWNED] += new Action<dynamic>(lol =>
            {
                Function.Call(Hash.SET_CAN_ATTACK_FRIENDLY, Game.PlayerPed.Handle, true, false);
                Function.Call(Hash.NETWORK_SET_FRIENDLY_FIRE_OPTION, true);
            });
        }
    }
}
