using CitizenFX.Core;
using Freeroam.Utils;
using System.Threading.Tasks;

namespace Freeroam.Missions
{
    class Assassination : Mission
    {
        public async void Start()
        {
            Ped ped = await Util.CreatePed(PedHash.Abner, Game.PlayerPed.Position);
            ped.MarkAsNoLongerNeeded();
        }

        public void Stop()
        {

        }

        public async Task Tick()
        {

        }
    }
}
