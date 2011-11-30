using System;
using System.Collections.Generic;
using System.Text;

namespace Absinthe.Core
{
    public class UserEvents
    {
        ///<summary>Used as a delegate to bubble status messages up to the user</summary>
        ///<param name="TextMsg">The text message to be passed up</param>
        public delegate void UserStatusEventHandler(string TextMsg);
        /// <summary>
        /// Used as a delegate to bubble normal messages up to the user
        /// </summary>
        /// <param name="TextMsg">The text message to be passed up</param>
        public delegate void UserMessageEventHandler(string TextMsg);
    }
}
