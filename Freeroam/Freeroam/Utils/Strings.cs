namespace Freeroam.Utils
{
    static class Strings
    {
        /* NOTIFICATIONS */

        public const string PLAYER_JOINED = "{0} joined the game.";
        public const string PLAYER_LEFT = "{0} left the game.";

        /* LEVEL */

        public const string LEVEL = "Level";
        public const string LEVEL_UP = "Level Up!"; 

        /* CRIMES */

        public const string CRIME_SPEEDING_PAYOUT = "Speeding Bonus!"; 

        public const string CRIME_STUNTING_PAYOUT = "Airtime Bonus!"; 

        /* INTERACTION MENU */

        public const string INTERACTIONMENU_MAIN_TITLE = "Freeroam"; 
        public const string INTERACTIONMENU_MAIN_SUBTITLE = "Interaction"; 

        public const string INTERACTIONMENU_ABOUT = "About"; 
        public const string INTERACTIONMENU_ABOUT_VERSION = "Version: 1.0.0"; 
        public const string INTERACTIONMENU_ABOUT_AUTHOR = "Made By Scammer"; 
        public const string INTERACTIONMENU_ABOUT_AUTHOR_DESC = "What are you doing in here?"; 

        public const string INTERACTIONMENU_PLAYERSKIN_MENU = "Player Skin"; 
        public const string INTERACTIONMENU_PLAYERSKIN_SUBTITLE = "Change your skin"; 
        public const string INTERACTIONMENU_PLAYERSKIN_SEARCH = "Search"; 
        public const string INTERACTIONMENU_PLAYERSKIN_CHANGING = "Changing Player model..."; 
        public const string INTERACTIONMENU_PLAYERSKIN_INVALID = "Invalid Player model."; 

        /* PHONE MENU */

        public const string PHONEMENU_MAIN_TITLE = "Phone"; 
        public const string PHONEMENU_MAIN_SUBTITLE = "Main Menu";

        public const string PHONEMENU_MISSIONS_MENU = "Missions";
        public const string PHONEMENU_MISSIONS_MISSIONRUNNING = "A mission is already in progress.";
        public const string PHONEMENU_MISSIONS_MISSIONSTARTED = "{0} started a new mission.";
        public const string PHONEMENU_MISSIONS_MISSIONSTOPPED = "{0} finished a mission.";
        public const string PHONEMENU_MISSIONS_MISSIONCOOLDOWN = "You have to wait {0} seconds until you can start another mission.";

        /* MISSIONS */

        public const string MISSIONS_FAIL = "~r~Mission failed.";

        public const string MISSIONS_ASSASSINATION_START = "Take out the ~r~targets~w~.";
        public const string MISSIONS_ASSASSINATION_BLIP = "Target";
        public const string MISSIONS_ASSASSINATION_INFO = "The ~r~targets~w~ are being constantly watched by their bodyguards " +
            "and the police, so you will have to likely deal with them. Also once you die the mission counts as failed.";
        public const string MISSIONS_ASSASSINATION_TARGETKILLED = "You killed a ~r~target~w~.";
        public const string MISSIONS_ASSASSINATION_ALLTARGETSKILLED = "You assassinated all ~r~targets~w~.";
    }
}
