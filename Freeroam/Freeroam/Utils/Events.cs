namespace Freeroam.Utils
{
    static class Events
    {
        public const string FIRSTTIME = "freeroam:newplayer";

        public const string MONEY_ADD = "freeroam:addmoney";
        public const string MONEY_REMOVE = "freeroam:removemoney";

        public const string XP_ADD = "freeroam:addxp";

        public const string DISPLAY_DRAW = "freeroam:drawdisplay";

        public const string PLAYERSKIN_CHANGE = "freeroam:changeskin";

        public const string CHALLENGE_START = "freeroam:startchallenge";
        public const string CHALLENGE_STOP = "freeroam:stopchallenge";

        public const string MISSION_START = "freeroam:startmission";
        public const string MISSION_STOP = "freeroam:stopmission";
    }
}
