using System;
using System.Collections.Generic;

namespace moonshot
{
    public class PlayerType
    {
        public const string apollo11 = "Apollo 11";
        public const string apollo12 = "Apollo 12";
        public const string apollo14 = "Apollo 14";
    }
    public class PlayerStatus
    {
        public const string good = "Good";
        public const string fair = "Fair";
        public const string poor = "Poor";
    }
    public class PartyMembers {
        internal List<PartyMember> Party;

        public override string ToString()
        {
            string output = "<PartyMembers>";
            foreach (PartyMember member in this.Party) {
                output += "<Member><Name>" + member.name + "</Name><Status>" + member.status + "</Status></Member>";
            }
            output += "</PartyMembers>";
            return output;
        }
    }
    public class PartyMember
    {
        internal PartyMember(string nameIn, string statusIn) {
            name = nameIn;
            status = statusIn;
        }
        internal string name;
        internal string status; 
    }
    public class apollo11 : PartyMembers {
        internal apollo11() {
            Party = new List<PartyMember>()
            {
                new PartyMember("Neil", PlayerStatus.good),
                new PartyMember("Michael", PlayerStatus.good),
                new PartyMember("Edwin (Buzz)", PlayerStatus.good)
            };
        }
    }
    public class apollo12 : PartyMembers {
        internal apollo12() {
            Party = new List<PartyMember>()
            {
                new PartyMember("Charles", PlayerStatus.good),
                new PartyMember("Richard", PlayerStatus.good),
                new PartyMember("Alan", PlayerStatus.good)
            };
        }
    }
    public class apollo14 : PartyMembers {
        internal apollo14() {
            Party = new List<PartyMember>()
            {
                new PartyMember("Alan", PlayerStatus.good),
                new PartyMember("Stuart", PlayerStatus.good),
                new PartyMember("Edgar", PlayerStatus.good)
            };
        }
    }
    public class InventoryItem
    {
        public InventoryItem(string nameIn, int idIn, int valueIn) {
            name = nameIn;
            id = idIn;
            value = valueIn;
        }
        public string name;
        public int id;
        public int value;
    }
    public class Inventory
    {
        internal static List<InventoryItem> Items = new List<InventoryItem>(){new InventoryItem("test", 1, 1)};
        public override string ToString()
        {
            string output = "<Items>";
            foreach (InventoryItem item in Items) {
                output += "<Item><Name>" + item.name + "</Name><Id>" + item.id + "</Id><Value>" + item.value + "</Value></Item>";
            }
            output += "</Items>";
            return output;
        }
    }
    public class UserStats
    {
        public int Money = 0;
        public String playerType = PlayerType.apollo11;
        public PartyMembers crew = new apollo11();
        public Inventory inventory = new Inventory();
        public string status = PlayerStatus.good;
        public override string ToString()
        {
            return "<Money>" + Money.ToString() + "</Money><PlayerType>" + playerType + "</PlayerType><Status>" + status + "</Status>" + inventory.ToString() + crew.ToString();
        }
    }
}
