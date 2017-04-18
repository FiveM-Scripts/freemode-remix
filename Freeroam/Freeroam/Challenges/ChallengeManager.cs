using CitizenFX.Core;
using Freeroam.Utils;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Freeroam.Challenges
{
    interface Challenge
    {
        void Start();
        void Stop();
        void Tick();
    }

    internal static class Challenges
    {
        public static string DRIVE_DISTANCE = "drivedistance";
    }

    class ChallengeManager : BaseScript
    {
        private static Dictionary<string, Type> challenges = new Dictionary<string, Type>()
        {
            [Challenges.DRIVE_DISTANCE] = typeof(DriveDistanceChallenge)
        };

        private Challenge currentChallenge;

        public ChallengeManager()
        {
            EventHandlers[Events.CHALLENGE_START] += new Action<string>(StartChallenge);
            EventHandlers[Events.CHALLENGE_STOP] += new Action(StopChallenge);

            Tick += OnTick;
        }

        private void StartChallenge(string challengeKey)
        {
            if (currentChallenge != null) throw new ChallengeAlreadyRunningException();

            if (Game.PlayerPed != null)
            {
                Type challenge;
                if (challenges.TryGetValue(challengeKey, out challenge))
                {
                    currentChallenge = (Challenge)Activator.CreateInstance(challenge);
                }
            }
        }

        private void StopChallenge()
        {
            if (currentChallenge != null)
            {
                currentChallenge.Stop();
                currentChallenge = null;
            }
        }

        private async Task OnTick()
        {


            await Task.FromResult(0);
        }

        private class ChallengeAlreadyRunningException : Exception { }
    }
}
