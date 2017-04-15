using CitizenFX.Core;
using CitizenFX.Core.UI;
using Freeroam.Utils;
using NativeUI;
using System.Drawing;
using System.Threading.Tasks;

namespace Freeroam.Shops
{
    internal class HeliRent
    {
        private static VehicleHash[] buyableHelis = new VehicleHash[]
        {
            VehicleHash.Maverick,
            VehicleHash.Buzzard2,
            VehicleHash.Buzzard
        };

        private static float MARKER_SCALE { get { return 2f; } }

        private Vector3 pos;
        public Vector3 POS { get { return pos; } }

        private MenuPool menuPool = new MenuPool();
        private UIMenu rentMenu;

        public HeliRent(Vector3 pos)
        {
            this.pos = pos;

            rentMenu = new UIMenu("Heli Rent", "Choose a Helicopter");
            menuPool.Add(rentMenu);

            AddMenuHeliItems();

            Blip blip = World.CreateBlip(pos);
            blip.Sprite = BlipSprite.Helicopter;
            blip.Name = "Heli Rent";
            blip.Scale = 0.8f;
            blip.IsShortRange = true;
        }

        private void AddMenuHeliItems()
        {
            foreach (VehicleHash heliHash in buyableHelis)
            {
                string labelName = Util.GetDisplayNameFromVehicleModel(heliHash);
                string labelText = Util.GetLabelText(labelName);

                UIMenuItem heliItem = new UIMenuItem(labelText);
                rentMenu.AddItem(heliItem);
            }

            rentMenu.OnItemSelect += OnItemSelect;
        }

        private async void OnItemSelect(dynamic sender, UIMenuItem item, int index)
        {
            Vector3 playerPos = Game.PlayerPed.Position;
            Vector3 spawnPos; float spawnHeading;
            Util.GetClosestVehNode(playerPos, out spawnPos, out spawnHeading, 20);

            Vehicle heli = await World.CreateVehicle(buyableHelis[index], spawnPos, spawnHeading);
            Blip heliBlip = heli.AttachBlip();
            heliBlip.Sprite = BlipSprite.Helicopter;
            heliBlip.Name = "Rented Heli";
            heliBlip.Color = BlipColor.Blue;
            heliBlip.Scale = 0.9f;

            heli.MarkAsNoLongerNeeded();
        }

        public void Tick()
        {
            World.DrawMarker(MarkerType.VerticalCylinder, new Vector3(pos.X, pos.Y, pos.Z - 1f), new Vector3(), new Vector3(),
                new Vector3(MARKER_SCALE), Color.FromArgb(255, 0, 0, 255));

            Ped playerPed = Game.PlayerPed;
            if (playerPed != null)
            {
                menuPool.ProcessMenus();
                if (World.GetDistance(playerPed.Position, pos) < MARKER_SCALE)
                {
                    if (!rentMenu.Visible)
                    {
                        Screen.DisplayHelpTextThisFrame("Press ~INPUT_CONTEXT~ to open rent menu.");
                        if (Game.IsControlJustReleased(1, Control.Context)) rentMenu.Visible = true;
                    }
                }
                else rentMenu.Visible = false;
            }
        }
    }

    class HeliRents : BaseScript
    {
        public static HeliRent[] heliRents = new HeliRent[]
        {
            new HeliRent(new Vector3(1214.908f, -3077.384f, 5.898481f))
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
