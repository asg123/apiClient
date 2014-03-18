using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace testAPIOAuth.Classes
{
    public class ChatListItem
    {
        public string id { get; set; }
        public string status { get; set; }
        public string type { get; set; }
        public string date { get; set; }
        public int duration { get; set; }
        public string channel { get; set; }
        public string source { get; set; }
        public List<int> attended_by { get; set; }
    }

    public class ChatList
    {
        public List<ChatListItem> chats { get; set; }
    }
}
