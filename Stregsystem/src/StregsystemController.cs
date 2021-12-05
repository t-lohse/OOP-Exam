using System;
using System.Collections.Generic;
using System.Linq;
using Stregsystem.Interfaces;
using Stregsystem.Exceptions;

namespace Stregsystem
{
    delegate void AdminCommand(params string[] s);
    public class StregsystemController
    {
        private IStregsystem _sts;
        private IStregsystemUI _ui;
        private Dictionary<string, AdminCommand> _adminCommands;
        
        public StregsystemController(IStregsystem sts, IStregsystemUI ui)
        {
            _sts = sts;
            _ui = ui;
            _ui.CommandEntered += ParseCommand;
            _adminCommands = new Dictionary<string, AdminCommand>();
            PopulateDictionary();
        }
        
        //TODO: Add helper command
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


        private void ParseCommand(string command)
        {
            List<string> list = command.Split(' ')
                .Where(x => !string.IsNullOrWhiteSpace(x))
                .ToList();

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
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    _ui.DisplayAdminCommandNotFoundMessage(list[0]);
                    return;
                }
            }

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

            Stack<Product> cart = new();
            foreach (string s in list)
            {
                if (s.Contains('*'))
                {
                    var mul = s.Split('*');
                    for (int i = int.Parse(mul[0]); i > 0; i--)
                        cart.Push(_sts.GetProductById(uint.Parse(mul[1])));
                    continue;
                }
                cart.Push(_sts.GetProductById(uint.Parse(s)));
            }

            try
            {
                if (user.Balance < cart.Sum(x => x.Price))
                    throw new InsufficientCreditsException(user, cart.ToList());
            }
            catch (InsufficientCreditsException e)
            {
                _ui.DisplayInsufficientCash(e.User, e.Total);
            }

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

                    _ui.DisplayUserBuysProduct(count, _sts.BuyProduct(user, p));
                    continue;
                }
                _ui.DisplayUserBuysProduct(_sts.BuyProduct(user, p));
            }
        }
    }
}