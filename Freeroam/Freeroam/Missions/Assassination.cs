using CitizenFX.Core;
using CitizenFX.Core.Native;
using CitizenFX.Core.UI;
using Freeroam.Utils;
using System.Threading.Tasks;

namespace Freeroam.Missions
{
    class Assassination : Mission
    {
        private static Vector3[] targetSpawns = new Vector3[]
        {
            new Vector3(-829f, -1219f, 6f),
            new Vector3(-3021f, 84f, 11f),
            new Vector3(6f, -711f, 45f),
            new Vector3(378f, -1901f, 24f),
            new Vector3(-291f, -428f, 29f),
            new Vector3(-1155f, -524f, 31f)
        };

        private Ped[] targets = new Ped[6];
        private bool enableTick = false;

        public async void Start()
        {
            for (int i = 0; i < targets.Length - 1; i++)
            {
                Ped target = await Util.CreatePed(PedHash.Business01AMY, targetSpawns[i]);
                target.Task.WanderAround();

                Function.Call(Hash.FLASH_MINIMAP_DISPLAY);

                Blip blip = target.AttachBlip();
                blip.Sprite = BlipSprite.Enemy;
                blip.Color = BlipColor.Red;
                blip.Name = Strings.MISSIONS_ASSASSINATION_BLIP;
                blip.Scale = 0.8f;

                targets[i] = target;
            }

            Util.DisplayHelpText(Strings.MISSIONS_ASSASSINATION_INFO);
            Screen.ShowSubtitle(Strings.MISSIONS_ASSASSINATION_START, 15000);
            enableTick = true;
        }

        public void Stop(bool success)
        {
            if (!success)
            {
                foreach (Ped target in targets)
                {
                    if (target != null)
                    {
                        target.AttachedBlip.Delete();
                        target.MarkAsNoLongerNeeded();
                    }
                }
            }
            else
            {
                Screen.ShowNotification(Strings.MISSIONS_ASSASSINATION_ALLTARGETSKILLED);
                BaseScript.TriggerEvent(Events.MONEY_ADD, 350);
                BaseScript.TriggerEvent(Events.XP_ADD, 13);
            }
        }

        public async Task Tick()
        {
            if (enableTick)
            {
                if (Game.PlayerPed.IsDead)
                {
                    BaseScript.TriggerEvent(Events.MISSION_STOP, false);
                    enableTick = false;
                }
                else
                {
                    int livingTargets = 0;
                    for (int i = targets.Length - 1; i > -1; i--)
                    {
                        if (targets[i] != null)
                        {
                            if (!targets[i].IsDead) livingTargets++;
                            else
                            {
                                Screen.ShowNotification(Strings.MISSIONS_ASSASSINATION_TARGETKILLED);

                                if (Game.Player.WantedLevel < 3) Game.Player.WantedLevel = 3;

                                targets[i].AttachedBlip.Delete();
                                targets[i].MarkAsNoLongerNeeded();
                                targets[i] = null;
                            }
                        }
                    }

                    if (livingTargets == 0) BaseScript.TriggerEvent(Events.MISSION_STOP, true);
                }
            }

            await Task.FromResult(0);
        }
    }
}
