﻿using CitizenFX.Core.Native;

namespace Freeroam.Utils
{
    static class Storage
    {
        // TODO: Unique keys for each server
        public static string MONEY { get { return "ObjQAeAAmlqDaRRXJCV0"; } }
        public static string XP { get { return "419yOkOQpp5UEk24yZhE"; } }

        public static void SetInt(string key, int value)
        {
            Function.Call((Hash)Util.GetHashKey("SET_RESOURCE_KVP_INT"), key, value);
        }

        public static int GetInt(string key)
        {
            return Function.Call<int>((Hash)Util.GetHashKey("GET_RESOURCE_KVP_INT"), key);
        }
    }
}
