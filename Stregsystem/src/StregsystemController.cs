using System;
using System.Collections.Generic;
using System.Linq;
using Stregsystem.Interfaces;
using Stregsystem.Exceptions;

namespace Stregsystem
{
    /// <summary>
    /// Delegate defining what an admin-command should do. It should return nothing,
    /// and take a range of parameters.
    /// </summary>
    internal delegate void AdminCommand(params string[] s);
    public class StregsystemController
    {
        private readonly IStregsystem _sts;
        private readonly IStregsystemUi _ui;
        private readonly Dictionary<string, AdminCommand> _adminCommands;
        
        public StregsystemController(IStregsystem sts, IStregsystemUi ui)
        {
            _sts = sts;
            _ui = ui;
            _ui.CommandEntered += ParseCommand;
            _sts.LowBalance += _ui.DisplayUserLowBalance;
            _adminCommands = new Dictionary<string, AdminCommand>();
            PopulateDictionary();
        }
        
        /// <summary>
        /// Populates the <c>_adminCommands</c> with the commands.
        /// </summary>
        private void PopulateDictionary()
        {
            _adminCommands.Add(":q", (_) => _ui.Close());
            _adminCommands.Add(":quit", _adminCommands[":q"]);
            _adminCommands.Add(":activate", (x) => _sts.GetProductById(uint.Parse(x[0])).Active = true);
            _adminCommands.Add(":deactivate", (x) => _sts.GetProductById(uint.Parse(x[0])).Active = false);
            _adminCommands.Add(":crediton", (x) => _sts.GetProductById(uint.Parse(x[0])).CanBeBoughtOnCredit = true);
            _adminCommands.Add(":creditoff", (x) => _sts.GetProductById(uint.Parse(x[0])).CanBeBoughtOnCredit = false);
            _adminCommands.Add(":addcredits", (x) => _sts.AddCreditsToAccount(_sts.GetUserByUsername(x[0]), float.Parse(x[1])));
        }
        
        /// <summary>
        /// The method for parsing and executing a command.
        /// </summary>
        /// <param name="command">The given command.</param>
        private void ParseCommand(string command)
        {
            if (string.IsNullOrWhiteSpace(command))
                return;
            
            List<string> list = command.Split(' ')
                .Where(x => !string.IsNullOrWhiteSpace(x))
                .ToList();

            // Admin commands
            if (list[0].StartsWith(':'))
            {
                try
                {
                    _adminCommands[list[0]](list.Skip(1).ToArray());
                    return;
                }
                catch (InvalidUsernameException e)
                {
                    _ui.DisplayUserNotFound(e.Username);
                    return;
                }
                catch (NonExistingProductException e)
                {
                    _ui.DisplayProductNotFound(e.Id.ToString());
                    return;
                }
                catch (IndexOutOfRangeException)
                {
                    _ui.DisplayGeneralError($"Not enough arguments for admin-command {list[0]}");
                    return;
                }
                catch (Exception)
                {
                    _ui.DisplayAdminCommandNotFoundMessage(list[0]);
                    return;
                }
            }

            // Purchase
            User user;
            try
            {
                user = _sts.GetUserByUsername(list[0]);
            }
            catch (InvalidUsernameException e)
            {
                _ui.DisplayUserNotFound(e.Username);
                return;
            }
            list.RemoveAt(0);
            if (!list.Any())
            {
                _ui.DisplayUserInfo(user);
                return;
            }

            // Multiple products
            Stack<Product> cart = new();
            foreach (string s in list)
            {
                if (s.Contains('*'))
                {
                    // Multiple of dame product
                    var mul = s.Split('*');
                    for (int i = int.Parse(mul[0]); i > 0; i--)
                        cart.Push(_sts.GetProductById(uint.Parse(mul[1])));
                    continue;
                }
                cart.Push(_sts.GetProductById(uint.Parse(s)));
            }

            // If insufficient funds
            try
            {
                if (user.Balance < cart.Where(x => !x.CanBeBoughtOnCredit).Sum(x => x.Price))
                    throw new InsufficientCreditsException(user, cart.ToList());
            }
            catch (InsufficientCreditsException e)
            {
                _ui.DisplayInsufficientCash(e.User, e.Total);
                return;
            }

            // Make purchase
            while (cart.TryPeek(out Product _))
            {
                Product p = cart.Pop();
                if (cart.Contains(p))
                {
                    int count = 1;

                    while (cart.Contains(p))
                    {
                        _sts.BuyProduct(user, cart.Pop());
                        count++;
                    }

                    _ui.DisplayUserBuysProduct(_sts.BuyProduct(user, p), count);
                    continue;
                }
                _ui.DisplayUserBuysProduct(_sts.BuyProduct(user, p));
            }
        }
    }
}