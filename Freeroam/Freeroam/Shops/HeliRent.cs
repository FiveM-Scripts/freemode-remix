using CitizenFX.Core;
using CitizenFX.Core.UI;
using System.Drawing;
using System.Threading.Tasks;

namespace Freeroam.Shops.HeliRent
{
    class HeliRent
    {
        private static float MARKER_SCALE { get { return 2f;  } }

        private Vector3 pos;
        public Vector3 POS { get { return pos; }}

        private Vehicle currentHeli;

        public HeliRent(Vector3 pos)
        {
            this.pos = pos;

            Blip blip = World.CreateBlip(pos);
            blip.Sprite = BlipSprite.Helicopter;
            blip.Name = "Heli Rent";
            blip.Scale = 0.8f;
            blip.IsShortRange = true;
        }

        public void Tick()
        {
            World.DrawMarker(MarkerType.VerticalCylinder, new Vector3(pos.X, pos.Y, pos.Z - 1f), new Vector3(), new Vector3(),
                new Vector3(MARKER_SCALE), Color.FromArgb(255, 0, 0, 255));

            Ped playerPed = Game.PlayerPed;
            if (playerPed != null)
            {
                if (World.GetDistance(playerPed.Position, pos) < MARKER_SCALE)
                {
                    Screen.DisplayHelpTextThisFrame("Press ~INPUT_CONTEXT~ to open rent menu.");
                }
            }
        }
    }

    class HeliRents : BaseScript
    {
        public static HeliRent[] heliRents = new HeliRent[]
        {
            new HeliRent(new Vector3(-171.4629f, -2389.876f, 5.999996f))
        };

        public HeliRents()
        {
            Tick += OnTick;
        }

        private async Task OnTick()
        {
            foreach (HeliRent heliRent in heliRents) heliRent.Tick();

            await Task.FromResult(0);
        }
    }
}
