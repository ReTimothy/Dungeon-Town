using System;
using System.Collections.Generic;
using System.Text;

namespace Engine
{
    public static class World
    {
        public static readonly List<Item> Items = new List<Item>();
        public static readonly List<Monster> Monsters = new List<Monster>();
        public static readonly List<Quest> Quests = new List<Quest>();
        public static readonly List<Building> Buildings = new List<Building>();
        public static readonly List<String> Descriptions = new List<String>();

        // Weapons
        public const int ITEM_ID_RUSTY_SWORD = 1;
        public const int ITEM_ID_STONE_SWORD = 2;
        public const int ITEM_ID_BRONZE_SWORD = 3;
        public const int ITEM_ID_IRON_SWORD = 4;
        public const int ITEM_ID_STEEL_SWORD = 5;
        public const int ITEM_ID_CARBON_STEEL_SWORD = 6;
        public const int ITEM_ID_MITHRIL_SWORD = 7;
        public const int ITEM_ID_DIAMOND_SWORD = 8;
        public const int ITEM_ID_ADAMANTINE_SWORD = 9;
        public const int ITEM_ID_TEST_SWORD = 10;

        // Quest items
        public const int ITEM_ID_RAT_TAIL = 50;
        public const int ITEM_ID_VIPER_TONGUE = 51;
        public const int ITEM_ID_SPIDER_FANG = 52;
        public const int ITEM_ID_ZOMBIE_DIGIT = 53;
        public const int ITEM_ID_SKELETON_FEMUR = 54;
        public const int ITEM_ID_ELEMENTAL_CORE = 55;
        public const int ITEM_ID_VAMPIRE_GLITTER = 56;
        public const int ITEM_ID_DREADLORD_EYE = 57;
        public const int ITEM_ID_WEREWOLF_EAR = 58;

        // Healing potion(s)
        public const int ITEM_ID_HEALING_POTION_MINOR = 100;
        public const int ITEM_ID_HEALING_POTION_LESSER = 101;
        public const int ITEM_ID_HEALING_POTION_BASIC = 102;
        public const int ITEM_ID_HEALING_POTION_GREATER = 103;
        public const int ITEM_ID_HEALING_POTION_SUPERIOR = 104;

        // Resources
        public const int ITEM_ID_LUMBER = 150;
        public const int ITEM_ID_STONE = 151;
        public const int ITEM_ID_IRON = 152;        
           
        // Monsters
        public const int MONSTER_ID_RAT = 1;
        public const int MONSTER_ID_SNAKE = 2;
        public const int MONSTER_ID_GIANT_SPIDER = 3;
        public const int MONSTER_ID_ZOMBIE = 4;
        public const int MONSTER_ID_SKELETON = 5;
        public const int MONSTER_ID_FIRE_ELEMENTAL = 6;
        public const int MONSTER_ID_ICE_ELEMENTAL = 7;
        public const int MONSTER_ID_VAMPIRE = 8;
        public const int MONSTER_ID_DREADLORD = 9;
        public const int MONSTER_ID_WEREWOLF = 10;

        // Quests
        public const int QUEST_ID_KILL_10_RATS = 1;
        public const int QUEST_ID_KILL_10_SNAKES = 2;
        public const int QUEST_ID_KILL_10_SPIDERS = 3;
        public const int QUEST_ID_KILL_10_ZOMBIES = 4;
        public const int QUEST_ID_KILL_10_SKELETONS = 5;
        public const int QUEST_ID_KILL_15_ELEMENTALS = 6;
        public const int QUEST_ID_KILL_10_VAMPIRES = 7;
        public const int QUEST_ID_KILL_10_DREADLORDS = 8;
        public const int QUEST_ID_KILL_10_WEREWOLVES = 9;

        // Buildings
        public const int BUILDING_ID_HOME = 1;
        public const int BUILDING_ID_BLACKSMITH = 2;
        public const int BUILDING_ID_LUMBER_MILL = 3;
        public const int BUILDING_ID_QUARRY = 4;
        public const int BUILDING_ID_MINE = 5;
        public const int BUILDING_ID_OUTFITTER = 6;
        public const int BUILDING_ID_TOWN_HALL = 7;
        public const int BUILDING_ID_TRAPPER = 8;
        public const int BUILDING_ID_HERBALIST = 9;
        public const int BUILDING_ID_MAGIC_SHOP = 10;
        public const int BUILDING_ID_SKILL_TRAINER = 11;
        public const int BUILDING_ID_MAGIC_TRAINER = 12;

        static World()
        {
            PopulateItems();
            PopulateMonsters();
            PopulateQuests();
            PopulateBuildings();
            PopulateDescriptions();
        }

        private static void PopulateItems()
        {
            // Weapons: id, name, namePlural, cost, description, minDamage, maxDamage
            Items.Add(new Weapon(ITEM_ID_RUSTY_SWORD, "Rusty sword", "Rusty swords", 0, "Your old trusty, rusty sword.", 0, 5));
            Items.Add(new Weapon(ITEM_ID_STONE_SWORD, "Stone sword", "Stone swords", 10, "At least it's impervious to rust.", 3, 8));
            Items.Add(new Weapon(ITEM_ID_BRONZE_SWORD, "Bronze sword", "Bronze swords", 25, "Getting sharper.", 5, 13));
            Items.Add(new Weapon(ITEM_ID_IRON_SWORD, "Iron sword", "Iron swords", 40, "The archetype sword.", 10, 15));
            Items.Add(new Weapon(ITEM_ID_STEEL_SWORD, "Steel sword", "Steel swords", 100, "Upgraded archetype sword.", 12, 20));
            Items.Add(new Weapon(ITEM_ID_CARBON_STEEL_SWORD, "Carbon steel sword", "Carbon steel swords", 200, "Improved upgraded archetype sword.", 10, 40));
            Items.Add(new Weapon(ITEM_ID_MITHRIL_SWORD, "Mithril sword", "Mithril swords", 400, "Now we're getting somewhere.", 25, 60));
            Items.Add(new Weapon(ITEM_ID_DIAMOND_SWORD, "Diamond sword", "Diamond swords", 750, "Is this really the best idea?", 50, 80));
            Items.Add(new Weapon(ITEM_ID_ADAMANTINE_SWORD, "Adamantine sword", "Adamantine swords", 1500, "The sword all swords aspire to be.", 100, 150));
            Items.Add(new Weapon(ITEM_ID_TEST_SWORD, "Developer's special sword", "Test swords", 999999999, "Unfunny.", 1000, 1500));

            // Items: id, name, namePlural, cost, description
            Items.Add(new Item(ITEM_ID_RAT_TAIL, "Rat tail", "Rat tails", 0, "It's a tail. From a rat."));
            Items.Add(new Item(ITEM_ID_VIPER_TONGUE, "Viper tongue", "Viper tongues", 0, "Slithery."));
            Items.Add(new Item(ITEM_ID_SPIDER_FANG, "Spider fang", "Spider fangs", 0, "It's huge!"));
            Items.Add(new Item(ITEM_ID_ZOMBIE_DIGIT, "Zombie digit", "Zombie digits", 0, "Yeah, it's -that- one."));
            Items.Add(new Item(ITEM_ID_SKELETON_FEMUR, "Skeleton femur", "Skeleton femurs", 0, "I don't want to work, I just want to bang on my drums all day"));
            Items.Add(new Item(ITEM_ID_ELEMENTAL_CORE, "Elemental core", "Elemental cores", 0, "Coreful not to drop it."));
            Items.Add(new Item(ITEM_ID_VAMPIRE_GLITTER, "Vampire glitter", "Vampire glitter", 0, "Ew."));
            Items.Add(new Item(ITEM_ID_DREADLORD_EYE, "Dreadlord eye", "Dreadlord eyes", 0, "It's still glaring at you."));
            Items.Add(new Item(ITEM_ID_WEREWOLF_EAR, "Werewolf ear", "Werewolf ears", 0, "Less hairy than you expected."));


        // Healing potions: id, name, namePlural, cost, description, amounttoheal
        Items.Add(new Potion(ITEM_ID_HEALING_POTION_MINOR, "Minor healing potion", "Healing potions", 5, "It's a potion. It heals minor damage.", 10));
            Items.Add(new Potion(ITEM_ID_HEALING_POTION_LESSER, "Lesser healing potion", "Healing potions", 15, "It's a potion. It heals lesser damage.", 18));
            Items.Add(new Potion(ITEM_ID_HEALING_POTION_BASIC, "Basic healing potion", "Healing potions", 30, "It's a potion. It heals.", 30));
            Items.Add(new Potion(ITEM_ID_HEALING_POTION_GREATER, "Greater healing potion", "Healing potions", 65, "It's a potion. It heals greater damage.", 46));
            Items.Add(new Potion(ITEM_ID_HEALING_POTION_SUPERIOR, "Superior healing potion", "Healing potions", 120, "It's a potion. It heals superior damage.", 75));

            // Resources
            Items.Add(new Item(ITEM_ID_LUMBER, "Lumber", "Lumber", 0, "Woody"));
            Items.Add(new Item(ITEM_ID_STONE, "Stone", "Stone", 0, "Rock hard"));
            Items.Add(new Item(ITEM_ID_IRON, "Iron", "Iron", 0, "Ironic"));
            
        }

        private static void PopulateMonsters()
        {
            // name, currentHP, maxHP, id, maxDamage, xpReward, goldReward, loottable list
            Monster rat = new Monster("Rat", 5, 5, MONSTER_ID_RAT, 3, 3, 2);
            rat.LootTable.Add(new LootItem(ItemByID(ITEM_ID_RAT_TAIL), 100, false));

            Monster snake = new Monster("Snake", 7, 7, MONSTER_ID_SNAKE, 5, 5, 5);
            snake.LootTable.Add(new LootItem(ItemByID(ITEM_ID_VIPER_TONGUE), 100, false));

            Monster giantSpider = new Monster("Giant Spider", 10, 10, MONSTER_ID_GIANT_SPIDER, 8, 10, 15);
            giantSpider.LootTable.Add(new LootItem(ItemByID(ITEM_ID_SPIDER_FANG), 100, false));

            Monster zombie = new Monster("Zombie", 14, 14, MONSTER_ID_ZOMBIE, 11, 16, 29);
            zombie.LootTable.Add(new LootItem(ItemByID(ITEM_ID_ZOMBIE_DIGIT), 100, false));

            Monster skeleton = new Monster("Skeleton", 20, 20, MONSTER_ID_SKELETON, 15, 23, 40);
            skeleton.LootTable.Add(new LootItem(ItemByID(ITEM_ID_SKELETON_FEMUR), 100, false));

            Monster fireElemental = new Monster("Fire elemental", 29, 29, MONSTER_ID_FIRE_ELEMENTAL, 19, 29, 55);
            fireElemental.LootTable.Add(new LootItem(ItemByID(ITEM_ID_ELEMENTAL_CORE), 100, false));

            Monster iceElemental = new Monster("Ice elemental", 40, 40, MONSTER_ID_ICE_ELEMENTAL, 23, 38, 75);
            iceElemental.LootTable.Add(new LootItem(ItemByID(ITEM_ID_ELEMENTAL_CORE), 100, false));

            Monster vampire = new Monster("Vampire", 55, 55, MONSTER_ID_VAMPIRE, 28, 49, 100);
            vampire.LootTable.Add(new LootItem(ItemByID(ITEM_ID_VAMPIRE_GLITTER), 100, false));

            Monster dreadlord = new Monster("Dreadlord", 75, 75, MONSTER_ID_DREADLORD, 33, 61, 130);
            dreadlord.LootTable.Add(new LootItem(ItemByID(ITEM_ID_DREADLORD_EYE), 100, false));

            Monster werewolf = new Monster("Werewolf", 100, 100, MONSTER_ID_WEREWOLF, 40, 80, 175);
            werewolf.LootTable.Add(new LootItem(ItemByID(ITEM_ID_WEREWOLF_EAR), 100, false));

            Monsters.Add(rat);
            Monsters.Add(snake);
            Monsters.Add(giantSpider);
            Monsters.Add(zombie);
            Monsters.Add(skeleton);
            Monsters.Add(fireElemental);
            Monsters.Add(iceElemental);
            Monsters.Add(vampire);
            Monsters.Add(dreadlord);
            Monsters.Add(werewolf);
        }

        private static void PopulateQuests()
        {
            // id, name, description, rewardXP, rewardGold, questCompletionItems list
            Quest killTenRats =
                new Quest(
                    QUEST_ID_KILL_10_RATS,
                    "Kill 10 rats and gather their tails.",
                    "You know the drill, kill those buggers. You'll receive a healing potion and 10 gold pieces.", 50, 100);
            killTenRats.QuestCompletionItems.Add(new QuestCompletionItem(ItemByID(ITEM_ID_RAT_TAIL), 10));
            killTenRats.RewardItem = ItemByID(ITEM_ID_HEALING_POTION_LESSER);

            Quest killTenSnakes =
                new Quest(
                    QUEST_ID_KILL_10_SNAKES,
                    "Kill 10 snakes and gather their tongues.",
                    "We have many envelopes that need sealing, go get us some snake tongues for that.", 75, 120);
            killTenSnakes.QuestCompletionItems.Add(new QuestCompletionItem(ItemByID(ITEM_ID_VIPER_TONGUE), 10));
            killTenSnakes.RewardItem = ItemByID(ITEM_ID_HEALING_POTION_LESSER);

            Quest killTenSpiders =
                new Quest(
                    QUEST_ID_KILL_10_SPIDERS,
                    "Kill 10 spiders and gather their fangs.",
                    "My hole puncher broke yesterday, get me some spider fangs to make a new one.", 100, 160);
            killTenSpiders.QuestCompletionItems.Add(new QuestCompletionItem(ItemByID(ITEM_ID_SPIDER_FANG), 10));
            killTenSpiders.RewardItem = ItemByID(ITEM_ID_HEALING_POTION_BASIC);

            Quest killTenZombies =
                new Quest(
                    QUEST_ID_KILL_10_ZOMBIES,
                    "Kill 10 zombies and gather their fingers.",
                    "I wanna flip someone the ultimate bird, get me 10 zombie fingers!", 150, 210);
            killTenZombies.QuestCompletionItems.Add(new QuestCompletionItem(ItemByID(ITEM_ID_ZOMBIE_DIGIT), 10));
            killTenZombies.RewardItem = ItemByID(ITEM_ID_HEALING_POTION_BASIC);

            Quest killTenSkeletons =
                new Quest(
                    QUEST_ID_KILL_10_SKELETONS,
                    "Kill 10 skeletons and gather their femurs.",
                    "We need some xylophone mallets for our orchestra. Go get me some skeletal femurs.", 175, 275);
            killTenSkeletons.QuestCompletionItems.Add(new QuestCompletionItem(ItemByID(ITEM_ID_SKELETON_FEMUR), 10));
            killTenSkeletons.RewardItem = ItemByID(ITEM_ID_HEALING_POTION_BASIC);

        Quest killFifteenElementals =
                new Quest(
                    QUEST_ID_KILL_15_ELEMENTALS,
                    "Kill 10 elementals and gather their cores.",
                    "The blacksmith needs 15 elemental cores, doesn't matter from which ones.", 200, 340);
            killFifteenElementals.QuestCompletionItems.Add(new QuestCompletionItem(ItemByID(ITEM_ID_ELEMENTAL_CORE), 15));
            killFifteenElementals.RewardItem = ItemByID(ITEM_ID_HEALING_POTION_GREATER);

            Quest killTenVampires =
                new Quest(
                    QUEST_ID_KILL_10_VAMPIRES,
                    "Kill 10 vampires and gather their glitter.",
                    "I wanna send someone I hate a card. Go get some organic glitter from the vampires in the dungeon.", 225, 400);
            killTenVampires.QuestCompletionItems.Add(new QuestCompletionItem(ItemByID(ITEM_ID_VAMPIRE_GLITTER), 10));
            killTenVampires.RewardItem = ItemByID(ITEM_ID_HEALING_POTION_GREATER);

            Quest killTenDreadlords =
                new Quest(
                    QUEST_ID_KILL_10_DREADLORDS,
                    "Kill 10 dreadlords and gather their eyes.",
                    "Our trapper needs something 'extra' for his special art exhibit. Go gather 10 dreadlord eyes.", 250, 500);
            killTenDreadlords.QuestCompletionItems.Add(new QuestCompletionItem(ItemByID(ITEM_ID_DREADLORD_EYE), 10));
            killTenDreadlords.RewardItem = ItemByID(ITEM_ID_HEALING_POTION_SUPERIOR);

            Quest killTenWerewolves =
                new Quest(
                    QUEST_ID_KILL_10_WEREWOLVES,
                    "Kill 10 werewolves and gather their ears.",
                    "The oldies in town have some hearing trouble. Get us some werewolf ears to aid in that.", 300, 750);
            killTenWerewolves.QuestCompletionItems.Add(new QuestCompletionItem(ItemByID(ITEM_ID_WEREWOLF_EAR), 10));
            killTenWerewolves.RewardItem = ItemByID(ITEM_ID_HEALING_POTION_SUPERIOR);

            Quests.Add(killTenRats);
            Quests.Add(killTenSnakes);
            Quests.Add(killTenSpiders);
            Quests.Add(killTenZombies);
            Quests.Add(killTenSkeletons);
            Quests.Add(killFifteenElementals);
            Quests.Add(killTenVampires);
            Quests.Add(killTenDreadlords);
            Quests.Add(killTenWerewolves);
        }

        private static void PopulateBuildings()
        {
            //Misc buildings
            Buildings.Add(new MiscBuilding(BUILDING_ID_HOME, "Home", "btnHome", 1, 1, 0, 0, 0, 0, "Safe house"));
            Buildings.Add(new MiscBuilding(BUILDING_ID_LUMBER_MILL, "Lumber Mill", "btnLumberMill", 1, 1, 50, 0, 0, 0, "Produces Resources", 3, new Resource(ItemByID(ITEM_ID_LUMBER))));
            Buildings.Add(new MiscBuilding(BUILDING_ID_QUARRY, "Quarry", "btnQuarry", 1, BUILDING_ID_LUMBER_MILL, 100, 100, 0, 0, "Produces Resources", 3, new Resource(ItemByID(ITEM_ID_STONE))));
            Buildings.Add(new MiscBuilding(BUILDING_ID_MINE, "Mine", "btnMine", 1, BUILDING_ID_QUARRY, 150, 200, 100, 0, "Produces Resources", 3, new Resource(ItemByID(ITEM_ID_IRON))));
            Buildings.Add(new MiscBuilding(BUILDING_ID_TRAPPER, "Trapper", "btnTrapper", 1, BUILDING_ID_QUARRY, 200, 200, 50, 0, "Invisible production"));
            Buildings.Add(new MiscBuilding(BUILDING_ID_TOWN_HALL, "Town Hall", "btnTownHall", 1, BUILDING_ID_BLACKSMITH, 500, 300, 300, 200, "Quest delivery"));

            //Stores
            Store blacksmith = new Store(BUILDING_ID_BLACKSMITH, "Blacksmith", "btnBlacksmith", 1, BUILDING_ID_MINE, 250, 200, 200, 200, "Weapons");
            blacksmith.Stock.Add(new VendorItem(ItemByID(ITEM_ID_STONE_SWORD), 10, false));
            blacksmith.Stock.Add(new VendorItem(ItemByID(ITEM_ID_BRONZE_SWORD), 25, false));
            blacksmith.Stock.Add(new VendorItem(ItemByID(ITEM_ID_IRON_SWORD), 50, false));
            blacksmith.Stock.Add(new VendorItem(ItemByID(ITEM_ID_STEEL_SWORD), 250, false));

            Store outfitter= new Store(BUILDING_ID_OUTFITTER, "Outfitter", "btnOutfitter", 1, BUILDING_ID_TRAPPER, 200, 400, 100, 0, "Armor");
            //outfitter.Stock.Add(new VendorItem(ItemByID(), 10, false));
            
            Buildings.Add(blacksmith);
            Buildings.Add(outfitter);
                        
        }

        private static void PopulateDescriptions()
        {
            Descriptions.Add("There's a round stone in the center, around it are a dozen skeletons in a circle. Nothing else of note resides in this room");
            Descriptions.Add("You enter a humid area. There are several braziers scattered around, somehow they're still burning, or burning again.");
            Descriptions.Add("You enter a shady area. It's filled with tombs, but their owners are spread across the floor.");
            Descriptions.Add("Your torch allows you to see empty chests and broken statues, worn down and wiped out by time itself.");
            Descriptions.Add("The floor is riddled with shredded blue prints and a half finished machine sits in a corner.");
            Descriptions.Add("There's a pile of skeletons in the center, all burned and black.");
            Descriptions.Add("Your torch allows you to see broken mining equipment, destroyed and rusted over.");
            Descriptions.Add("An unexplained breeze can be felt in this room.");
            Descriptions.Add("A group of demonic faces have been carved into the walls.");
            Descriptions.Add("Patches of mushrooms grow here.");
            Descriptions.Add("An iron brazier and rotting carpet sit in the center of the room, and someone has scrawled a diagram of a mechanical trap on the east wall.");
            Descriptions.Add("A stair ascends to a balcony hanging from the north wall, and the floor is covered in perfect hexagonal tiles.");
            Descriptions.Add("A mural of a sun god covers the ceiling, and someone has scrawled \"Save yourself, kill the others\" on the north wall.");
            Descriptions.Add("A fountain decorated with tormented faces sits in the south-west corner of the room, and a pile of torn paper lies in the south side of the room.");
            Descriptions.Add("Several square holes are cut into the ceiling and floor, and someone has scrawled a drawing of a dragon on the east wall.");
            Descriptions.Add("Lit candles are scattered across the floor, and a pile of spoiled meat lies in the north-west corner of the room.");
            Descriptions.Add("Someone has scrawled \"The Shield of Virtue is sundered\" on the west wall.");
            Descriptions.Add("A narrow shaft descends from the room into a plundered tomb below, and someone has scrawled \"Fratelch has no beard\" in dwarvish runes on the north wall.");
            Descriptions.Add("A wooden platform hangs over a deep pit in the east side of the room, and a pile of empty bottles lies in the farthest corner of the room.");
            Descriptions.Add("A mural of geometric patterns covers the ceiling, and someone has scrawled a baleful symbol on the north wall.");
            Descriptions.Add("A faded and torn tapestry hangs from the north wall, and a fountain decorated with five water-breathing dragon heads sits in the center of the room.");
            Descriptions.Add("Several iron cages are scattered throughout the room and the ceiling is full of cracks.");
            //Descriptions.Add("");
        }


        public static Item ItemByID(int id)
        {
            foreach (Item item in Items)
            {
                if (item.ID == id)
                {
                    return item;
                }
            }

            return null;
        }

        public static Monster MonsterByID(int id)
        {
            foreach (Monster monster in Monsters)
            {
                if (monster.ID == id)
                {
                    return monster;
                }
            }

            return null;
        }

        public static Quest QuestByID(int id)
        {
            foreach (Quest quest in Quests)
            {
                if (quest.ID == id)
                {
                    return quest;
                }
            }

            return null;
        }

        public static Building BuildingByID(int id)
        {
            foreach (Building building in Buildings)
            {
                if (building.ID == id)
                {
                    return building;
                }
            }

            return null;
        }

        public static String ReturnDescription()
        {
            return Descriptions[RandomNumberGenerator.NumberBetween(0, Descriptions.Count - 1)];
        }
    }
}
