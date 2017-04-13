using CitizenFX.Core;
using CitizenFX.Core.Native;
using System.Threading.Tasks;
using static CitizenFX.Core.BaseScript;

namespace Freeroam.Utils
{
    static class Util
    {
        public static uint GetHashKey(string name)
        {
            return Function.Call<uint>(Hash.GET_HASH_KEY, name);
        }

        public async static Task<Ped> CreatePed(Model model, Vector3 pos, float heading = 0f)
        {
            int hash = model.Hash;
            Function.Call(Hash.REQUEST_MODEL, hash);
            while (!Function.Call<bool>(Hash.HAS_MODEL_LOADED, hash))
            {
                await Delay(1);
            }

            int pedId = Function.Call<int>(Hash.CREATE_PED, 26, hash, pos.X, pos.Y, pos.Z, heading, true, true);
            return new Ped(pedId);
        }

        public static void GetClosestVehNode(Vector3 pos, out Vector3 outPos, out float outHeading)
        {
            OutputArgument resultPos = new OutputArgument();
            OutputArgument resultHeading = new OutputArgument();

            Function.Call(Hash.GET_CLOSEST_VEHICLE_NODE_WITH_HEADING, pos.X, pos.Y, pos.Z, resultPos, resultHeading, 1f, 3f, 0f);

            outPos = resultPos.GetResult<Vector3>();
            outHeading = resultHeading.GetResult<float>();
        }

        public static void GetClosestVehNode(Vector3 pos, out Vector3 outPos, out float outHeading, int nth = 0)
        {
            OutputArgument resultPos = new OutputArgument();
            OutputArgument resultHeading = new OutputArgument();

            Function.Call(Hash.GET_NTH_CLOSEST_VEHICLE_NODE_WITH_HEADING, pos.X, pos.Y, pos.Z, nth, resultPos, resultHeading, 90f, 9f, 3.0f, 2.5f);

            outPos = resultPos.GetResult<Vector3>();
            outHeading = resultHeading.GetResult<float>();
        }

        public static int GetVehicleNodeTrafficDensity(Vector3 pos)
        {
            OutputArgument densityOut = new OutputArgument();

            Function.Call(Hash.GET_VEHICLE_NODE_PROPERTIES, pos.X, pos.Y, pos.Z, densityOut, null);

            return densityOut.GetResult<int>();
        }

        public static Vector3 GetClosestSafeCoords(Vector3 pos)
        {
            OutputArgument resultPos = new OutputArgument();

            Function.Call(Hash.GET_SAFE_COORD_FOR_PED, pos.X, pos.Y, pos.Z, true, resultPos, 16f);

            return resultPos.GetResult<Vector3>();
        }

        public static float GetVehKMHSpeed(Vehicle veh)
        {
            return veh.Speed * 3.6f;
        }

        public static Vehicle GetClosestVeh(Vector3 pos, float radius, int model = 0)
        {
            int closestVeh = Function.Call<int>(Hash.GET_CLOSEST_VEHICLE, pos.X, pos.Y, pos.Z, radius, model, 0);
            return closestVeh == 0 ? null : new Vehicle(closestVeh);
        }
    }
}
