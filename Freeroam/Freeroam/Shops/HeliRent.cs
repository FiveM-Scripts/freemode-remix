using CitizenFX.Core;
using CitizenFX.Core.UI;
using Freeroam.Utils;
using NativeUI;
using System;
using System.Drawing;
using System.Threading.Tasks;

namespace Freeroam.Shops
{
    internal struct BuyableHeli
    {
        public VehicleHash MODEL { get; private set; }
        public int PRICE { get; private set; }
        public string DISPLAY_NAME { get; private set; }

        public BuyableHeli(VehicleHash model, int price)
        {
            MODEL = model;
            PRICE = price;

            string labelName = Util.GetDisplayNameFromVehicleModel(model);
            DISPLAY_NAME = Util.GetLabelText(labelName);
        }
    }

    internal class HeliRent
    {
        private static BuyableHeli[] buyableHelis = new BuyableHeli[]
        {
            new BuyableHeli(VehicleHash.Maverick, 150),
            new BuyableHeli(VehicleHash.Buzzard2, 200),
            new BuyableHeli(VehicleHash.Buzzard, 350)
        };

        private static float MARKER_SCALE { get { return 2f; } }

        public Vector3 POSITION { get; private set; }

        private MenuPool menuPool = new MenuPool();
        private UIMenu rentMenu;

        public HeliRent(Vector3 pos)
        {
            POSITION = pos;

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
            foreach (BuyableHeli buyableHeli in buyableHelis)
            {
                UIMenuItem heliItem = new UIMenuItem(buyableHeli.DISPLAY_NAME);
                heliItem.SetRightLabel(buyableHeli.PRICE + "$");
                rentMenu.AddItem(heliItem);
            }

            rentMenu.OnItemSelect += OnItemSelect;
        }

        private async void OnItemSelect(dynamic sender, UIMenuItem item, int index)
        {
            int price = buyableHelis[index].PRICE;
            BaseScript.TriggerEvent(Events.MONEY_REMOVE, price);
            BaseScript.TriggerEvent(Events.XP_ADD, 1);

            Vector3 playerPos = Game.PlayerPed.Position;
            Vector3 spawnPos; float spawnHeading;
            Util.GetClosestVehNode(playerPos, out spawnPos, out spawnHeading, 20);

            Vehicle heli = await World.CreateVehicle(buyableHelis[index].MODEL, spawnPos, spawnHeading);
            Blip heliBlip = heli.AttachBlip();
            heliBlip.Sprite = BlipSprite.Helicopter;
            heliBlip.Name = "Rented Heli";
            heliBlip.Color = BlipColor.Blue;
            heliBlip.Scale = 0.9f;

            heli.MarkAsNoLongerNeeded();
        }

        public void Tick()
        {
            Ped playerPed = Game.PlayerPed;
            if (playerPed != null)
            {
                World.DrawMarker(MarkerType.VerticalCylinder, new Vector3(POSITION.X, POSITION.Y, POSITION.Z - 1f), new Vector3(), new Vector3(),
                    new Vector3(MARKER_SCALE), Color.FromArgb(255, 0, 0, 255));

                menuPool.ProcessMenus();
                if (World.GetDistance(playerPed.Position, POSITION) < MARKER_SCALE)
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
            new HeliRent(new Vector3(1215.608f, -3076.717f, 5.897452f))
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
