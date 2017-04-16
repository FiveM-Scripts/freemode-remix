namespace Freeroam.Utils
{
    static class Events
    {
        public static string FIRSTTIME { get { return "freeroam:newplayer"; } }

        public static string MONEY_ADD { get { return "freeroam:addmoney"; } }
        public static string MONEY_REMOVE { get { return "freeroam:removemoney"; } }

        public static string XP_ADD { get { return "freeroam:addxp"; } }

        public static string PLAYERSKIN_CHANGE { get { return "freeroam:changeskin"; } }
    }
}
