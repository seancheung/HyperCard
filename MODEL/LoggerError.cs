using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using log4net;

namespace MODEL
{
    public class LoggerError
    {
        private static readonly ILog logger = LogManager.GetLogger("App.Error");

        /// <summary>
        /// Save error log
        /// </summary>
        /// <param name="msg">Error message to save</param>
        public static void Log(object msg)
        {
            if (logger.IsErrorEnabled)
            {
                logger.Error(msg);
            }
        }
    }
}
