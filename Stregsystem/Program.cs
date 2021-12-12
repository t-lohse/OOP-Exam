using Stregsystem.UI;

namespace Stregsystem
{
    internal static class Program
    {
        public static void Main(string[] args)
        {
            Stregsystem sts = new Stregsystem();
            StregsystemCli ui = new StregsystemCli(sts);
            StregsystemController controller = new StregsystemController(sts, ui);
            ui.Start();
        }
    }
}
