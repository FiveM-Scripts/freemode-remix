using CitizenFX.Core;
using CitizenFX.Core.UI;
using Freeroam.Utils;
using System;
using System.Drawing;
using System.Threading.Tasks;

namespace Freeroam.Progress
{
    public class Money : BaseScript
    {
        private Text drawingText;

        private Text moneyChangeText;
        private int moneyChangeTextAmount;
        private int moneyChangeTextShowProgress;

        private int money;

        public Money()
        {
            money = Storage.GetInt(Storage.MONEY);

            PointF drawingTextPos = new PointF(220f, 680f);
            float drawingTextScale = 0.7f;
            Color drawingTextColor = Color.FromArgb(255, 0, 153, 0);
            Font drawingTextFont = Font.ChaletComprimeCologne;
            Alignment drawingTextAlign = Alignment.Left;
            drawingText = new Text($"{money} $", drawingTextPos, drawingTextScale, drawingTextColor, drawingTextFont, drawingTextAlign, true, true);

            float moneyChangeTextScale = 0.5f;
            moneyChangeText = new Text("", new PointF(drawingTextPos.X, drawingTextPos.Y - 25f), moneyChangeTextScale,
                Color.Empty, drawingTextFont, drawingTextAlign, true, true);

            EventHandlers[Events.MONEY_ADD] += new Action<int>(AddMoney);
            EventHandlers[Events.MONEY_REMOVE] += new Action<int>(RemoveMoney);
            EventHandlers[Events.MONEY_GET] += new Action<Action<int>>(GetMoney);
            EventHandlers[Events.MONEY_HASENOUGH] += new Action<Action<int>>(GetMoney);

            Tick += OnTick;
        }

        private void AddMoney(int amount)
        {
            SetMoney(money + amount);
        }

        private void RemoveMoney(int amount)
        {
            SetMoney(money - amount);
        }

        private void SetMoney(int newMoney)
        {
            drawingText.Caption = $"{newMoney} $";
            moneyChangeTextAmount = newMoney - money;

            if (moneyChangeTextAmount > 0) moneyChangeText.Caption = $"+ {moneyChangeTextAmount}$";
            else moneyChangeText.Caption = $"- {moneyChangeTextAmount}$";

            if (newMoney > 0) moneyChangeText.Color = Color.FromArgb(255, 0, 153, 0);
            else moneyChangeText.Color = Color.FromArgb(255, 153, 0, 0);

            moneyChangeTextShowProgress = 0;

            money = newMoney < 0 ? 0 : newMoney;
            Storage.SetInt(Storage.MONEY, money);
        }

        private void GetMoney(Action<int> cb)
        {
            cb(money);
        }

        private void HasEnoughMoney(int amount, Action<bool> cb)
        {
            cb(money - amount >= 0);
        }

        private async Task OnTick()
        {
            drawingText.Draw();

            if (moneyChangeTextShowProgress < 300)
            {
                moneyChangeText.Draw();
                moneyChangeTextShowProgress++;
            }

            await Task.FromResult(0);
        }
    }
}
