using System;
using System.IO;
using Stregsystem.UI;

namespace Stregsystem
{
    static class Program
    {
        //TODO: Overwrite files to save new balances
        //TODO: Add comments to controller and UI
        public static void Main(string[] args)
        {
            Stregsystem sts = new Stregsystem();
            StregsystemCli ui = new StregsystemCli(sts);
            StregsystemController controller = new StregsystemController(sts, ui);
            ui.Start();
        }
    }
}
