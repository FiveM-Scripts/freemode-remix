﻿namespace Freeroam.Utils
{
    static class Events
    {
        public static string MONEY_ADD { get { return "freeroam:addmoney"; } }
        public static string MONEY_REMOVE { get { return "freeroam:removemoney"; } }
        public static string MONEY_GET { get { return "freeroam:getmoney"; } }
        public static string MONEY_HASENOUGH { get { return "freeroam:hasenoughmoney"; } }

        public static string XP_ADD { get { return "freeroam:addxp"; } }
        public static string XP_GET { get { return "freeroam:getxp"; } }
    }
}
