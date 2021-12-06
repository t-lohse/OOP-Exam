using Stregsystem.UI;

namespace Stregsystem
{
    internal static class Program
    {
        //TODO: Overwrite files to save new balances
        //TODO: Add comments to controller and UI
        //TODO: Checks if users are in the stregsystem?
        public static void Main(string[] args)
        {
            Stregsystem sts = new Stregsystem();
            StregsystemCli ui = new StregsystemCli(sts);
            StregsystemController controller = new StregsystemController(sts, ui);
            ui.Start();
        }
    }
}
