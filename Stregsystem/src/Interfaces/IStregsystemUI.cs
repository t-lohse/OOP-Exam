namespace Stregsystem.Interfaces
{
    public interface IStregsystemUi
    {
        /// <summary>
        /// Displays when a <c>User</c> was not found in the stregsystem.
        /// </summary>
        /// <param name="username">Username for the not found <c>User</c>.</param>
        void DisplayUserNotFound(string username);
        /// <summary>
        /// Displays when a <c>Product</c> was not found in the stregsystem.
        /// </summary>
        /// <param name="product">Id of the not found <c>Product</c>.</param>
        void DisplayProductNotFound(string product);
        /// <summary>
        /// Displays info on a specified <c>User</c>.
        /// </summary>
        /// <param name="user">The specified <c>User</c>.</param>
        void DisplayUserInfo(User user);
        /// <summary>
        /// Displays error message specifying too many arguments for given command.
        /// </summary>
        /// <param name="command">Given command.</param>
        void DisplayTooManyArgumentsError(string command);
        /// <summary>
        /// Displays message specifying an admin-command that could not be found.
        /// </summary>
        /// <param name="adminCommand">The specified admin-command.</param>
        void DisplayAdminCommandNotFoundMessage(string adminCommand);
        /// <summary>
        /// Displays when a <c>User</c> successfully buys a <c>Product</c>.
        /// </summary>
        /// <param name="transaction">The <c>Transaction</c> that occured.</param>
        void DisplayUserBuysProduct(BuyTransaction transaction);
        /// <summary>
        /// Displays when a <c>User</c> successfully buys multiple <c>Product</c>s.
        /// </summary>
        /// <param name="transaction">The <c>Transaction</c> that occured.</param>
        /// <param name="count">The amount of <c>Product</c>s bought.</param>
        void DisplayUserBuysProduct(BuyTransaction transaction, int count);
        /// <summary>
        /// Displays message, specifying when a given <c>User</c> tries to buy a <c>Product</c>,
        /// with insufficient funds.
        /// </summary>
        /// <param name="user">The specified <c>User</c>.</param>
        /// <param name="price">The price of the <c>Product</c>.</param>
        void DisplayInsufficientCash(User user, float price);

        void DisplayUserLowBalance(User user, decimal balance);
        /// <summary>
        /// Displays a general error.
        /// </summary>
        /// <param name="errorString">The given error as a <c>string</c>.</param>
        void DisplayGeneralError(string errorString); 
        /// <summary>
        /// The method which starts the stregsystem. 
        /// </summary>
        void Start();
        /// <summary>
        /// The method for closing the stregsystem.
        /// </summary>
        void Close();
        /// <summary>
        /// The <c>event</c> for when a command has been entered.
        /// </summary>
        event StregsystemEvent CommandEntered;
    }
    /// <summary>
    /// The <c>delegate</c> specifying the <c>event</c>s for commands entered.
    /// </summary>
    public delegate void StregsystemEvent(string input);
}