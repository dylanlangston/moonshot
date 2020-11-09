using System;
using System.Collections.Generic;

namespace moonshot
{
    class PlayerType
    {
        public const string apollo11 = "Apollo 11";
        public const string apollo12 = "Apollo 12";
        public const string apollo13 = "Apollo 14";
    }
    class PlayerStatus
    {
        public const string good = "Good";
        public const string fair = "Fair";
        public const string poor = "Poor";
    }
    class PartyMembers {
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
    class PartyMember
    {
        internal PartyMember(string nameIn, string statusIn) {
            name = nameIn;
            status = statusIn;
        }
        internal string name;
        internal string status; 
    }
    class apollo11 : PartyMembers {
        internal apollo11() {
            Party = new List<PartyMember>()
            {
                new PartyMember("Neil", PlayerStatus.good),
                new PartyMember("Michael", PlayerStatus.good),
                new PartyMember("Edwin (Buzz)", PlayerStatus.good)
            };
        }
    }
    class apollo12 : PartyMembers {
        internal apollo12() {
            Party = new List<PartyMember>()
            {
                new PartyMember("Charles", PlayerStatus.good),
                new PartyMember("Richard", PlayerStatus.good),
                new PartyMember("Alan", PlayerStatus.good)
            };
        }
    }
    class apollo14 : PartyMembers {
        internal apollo14() {
            Party = new List<PartyMember>()
            {
                new PartyMember("Alan", PlayerStatus.good),
                new PartyMember("Stuart", PlayerStatus.good),
                new PartyMember("Edgar", PlayerStatus.good)
            };
        }
    }
    class InventoryItem
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
    class Inventory
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
        List<PartyMember> PartyMembers = new List<PartyMember>(){};
        int Money = 0;
        String playerType = PlayerType.apollo11;
        PartyMembers crew = new apollo11();
        Inventory inventory = new Inventory();
        string status = PlayerStatus.good;
        public override string ToString()
        {
            return "<Money>" + Money.ToString() + "</Money><PlayerType>" + playerType + "</PlayerType><Status>" + status + "</Status>" + inventory.ToString() + crew.ToString();
        }
    }
}
