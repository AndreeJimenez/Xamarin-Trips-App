using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace AppTrips.Triggers
{
    public class RatingTrigger : TriggerAction<Entry>
    {
        //private string _oldValue = string.Empty;

        protected override void Invoke(Entry sender)
        {
            int n;
            var isNumeric = int.TryParse(sender.Text, out n);
            if (string.IsNullOrWhiteSpace(sender.Text) || !isNumeric)
            {
                sender.Text = ""; //_oldValue;
            }
            else
            {
                if (n < 1)
                {
                    sender.Text = "1";
                }
                else if (n > 5)
                {
                    sender.Text = "5";
                }
            }
            //_oldValue = sender.Text;
        }
    }
}
