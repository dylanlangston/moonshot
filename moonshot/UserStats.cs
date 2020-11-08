using System;

namespace moonshot
{
    class PlayerType
    {
        public const string farmer = "Farmer";
        public const string banker = "Banker";
        public const string carpenter = "Carpenter";
    }
    class PartyMembers
    {
        string name;
        string status; 
    }
    class Inventory
    {

    }
    public class UserStats
    {
        int Money = 0;
        String playerType = PlayerType.farmer;
        int progress;
        Inventory inventory = new Inventory();
        string status;
    }
}
