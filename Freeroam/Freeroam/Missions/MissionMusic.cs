using CitizenFX.Core.Native;
using System;

namespace Freeroam.Missions
{
    class MissionMusic
    {
        private Music music;

        public MissionMusic(Music music)
        {
            this.music = music;
        }

        public void PlayCalmMusic()
        {
            Function.Call(Hash.TRIGGER_MUSIC_EVENT, music.CALM_MUSIC);
        }

        public void PlayActionMusic()
        {
            Function.Call(Hash.TRIGGER_MUSIC_EVENT, music.CALM_MUSIC);
            Function.Call(Hash.TRIGGER_MUSIC_EVENT, music.ACTION_MUSIC);
        }

        public void StopMusic()
        {
            Function.Call(Hash.CANCEL_MUSIC_EVENT, music.CALM_MUSIC);
            Function.Call(Hash.CANCEL_MUSIC_EVENT, music.ACTION_MUSIC);
        }
    }

    internal struct Music
    {
        public string CALM_MUSIC { get; private set; }
        public string ACTION_MUSIC { get; private set; }

        public Music(string calmMusic, string actionMusic)
        {
            CALM_MUSIC = calmMusic;
            ACTION_MUSIC = actionMusic;
        }
    }

    static class MissionMusics
    {
        private static Music[] musics = new Music[]
        {
            new Music("APT_YA_START_DEFEND", "APT_YA_ACTION")
        };

        public static MissionMusic GetRandomMissionMusic()
        {
            Music randomMusic = musics[new Random().Next(musics.Length)];
            return new MissionMusic(randomMusic);
        }
    }
}
