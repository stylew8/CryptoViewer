using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace CryptoViewer.BL.General
{
    /// <summary>
    /// Helper methods for general functionality.
    /// </summary>
    public class Helpers
    {
        /// <summary>
        /// Creates a new transaction scope with the specified timeout duration.
        /// </summary>
        /// <param name="seconds">Timeout duration in seconds (default is 60 seconds).</param>
        /// <returns>A new <see cref="TransactionScope"/> object.</returns>
        public static TransactionScope CreateTransactionScope(int seconds = 60)
        {
            return new TransactionScope(
                TransactionScopeOption.Required,
                new TimeSpan(0, 0, seconds),
                TransactionScopeAsyncFlowOption.Enabled
            );
        }
    }
}