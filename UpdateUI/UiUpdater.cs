using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoOrganiser.UpdateUI
{
    public static class UiUpdater
    {
        public static void UpdateTextBox(TextBox textBox, string newString)
        {
            textBox.Invoke(new Action(() => textBox.Text = newString));
        }
    }
}
