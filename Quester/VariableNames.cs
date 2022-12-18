using System.Collections.Generic;

namespace Quester
{
    internal struct VariableNames
    {
        private static readonly Dictionary<uint, string> Names = new Dictionary<uint, string>
        {
            [0x000000F3] = "a1",
            [0x000000F4] = "a2",
            [0x000000F5] = "a3",
            [0x000000F6] = "a4",
            [0x000000F7] = "a5",
            [0x000000F8] = "a6",
            [0x0000012A] = "db",
            [0x00000141] = "mg",
            [0x0000014B] = "no",
            [0x0000014F] = "tg",
            [0x000002CA] = "eel",
            [0x000002CF] = "ice",
            [0x000002D3] = "gem",
            [0x000002DD] = "bow",
            [0x000002De] = "box",
            [0x000002E4] = "man",
            [0x000002E6] = "map",
            [0x000002E8] = "len",
            [0x000002EE] = "inn",
            [0x000002EF] = "key",
            [0x000002F4] = "mob",
            [0x000002FB] = "npc",
            [0x000002FE] = "rat",
            [0x00000303] = "orc",
            [0x00000321] = "yes",
            [0x00000325] = "spy",
            [0x000005D7] = "gem1",
            [0x000005D8] = "bear",
            [0x000005D8] = "gem2",
            [0x000005D9] = "gem3",
            [0x000005DA] = "gem4",
            [0x000005DB] = "bank",
            [0x000005DC] = "bard",
            [0x000005EF] = "bats",
            [0x00000609] = "ally",
            [0x00000615] = "book",
            [0x00000617] = "mace",
            [0x00000619] = "gems",
            [0x0000061D] = "fire",
            [0x0000061F] = "mage",
            [0x00000625] = "boss",
            [0x00000625] = "lady",
            [0x00000627] = "npc1",
            [0x00000628] = "npc2",
            [0x00000629] = "npc3",
            [0x0000062A] = "duel",
            [0x0000062A] = "foil",
            [0x00000630] = "gold",
            [0x00000632] = "lich",
            [0x00000637] = "orc1",
            [0x00000638] = "orc2",
            [0x00000639] = "orc3",
            [0x0000063A] = "orc4",
            [0x0000063B] = "home",
            [0x0000063D] = "necs",
            [0x0000064F] = "item",
            [0x0000065C] = "iron",
            [0x00000660] = "pawn",
            [0x00000664] = "lord",
            [0x0000066B] = "monk",
            [0x0000066D] = "love",
            [0x0000066F] = "rats",
            [0x00000673] = "yes1",
            [0x00000674] = "yes2",
            [0x00000675] = "yes3",
            [0x00000676] = "yes4",
            [0x00000677] = "ring",
            [0x00000677] = "nono",
            [0x00000677] = "yes5",
            [0x00000678] = "yes6",
            [0x00000679] = "orcs",
            [0x00000679] = "note",
            [0x0000067c] = "wand",
            [0x0000067D] = "rock",
            [0x0000067E] = "vamp",
            [0x00000683] = "time",
            [0x00000686] = "shop",
            [0x00000691] = "thug",
            [0x0000069C] = "star",
            [0x000006B2] = "wolf",
            [0x00000957] = "2dung",
            [0x00000C2C] = "agent",
            [0x00000C48] = "darkb",
            [0x00000C4B] = "decoy",
            [0x00000C50] = "child",
            [0x00000C53] = "delay",
            [0x00000C6F] = "mage1",
            [0x00000C70] = "mage2",
            [0x00000C71] = "arena",
            [0x00000C71] = "mage3",
            [0x00000C72] = "mage4",
            [0x00000C7C] = "elder",
            [0x00000C7D] = "bribe",
            [0x00000C8C] = "giant",
            [0x00000C91] = "gold1",
            [0x00000C92] = "gold2",
            [0x00000CA2] = "flesh",
            [0x00000CA6] = "heist",
            [0x00000CA7] = "enemy",
            [0x00000CA9] = "harpy",
            [0x00000CA9] = "magic",
            [0x00000CAB] = "gimme",
            [0x00000CAF] = "lamia",
            [0x00000CB1] = "mages",
            [0x00000CC0] = "maker",
            [0x00000CC6] = "ghost",
            [0x00000CCC] = "giver",
            [0x00000CCF] = "item1",
            [0x00000CD0] = "item2",
            [0x00000CD1] = "item3",
            [0x00000CE4] = "guard",
            [0x00000CE5] = "drugs",
            [0x00000CEF] = "dummy",
            [0x00000CF2] = "local",
            [0x00000CF6] = "metal",
            [0x00000CF8] = "crypt",
            [0x00000CFC] = "pearl",
            [0x00000D06] = "frost",
            [0x00000D06] = "rebel",
            [0x00000D0F] = "place",
            [0x00000D17] = "house",
            [0x00000D1D] = "noble",
            [0x00000D1F] = "mitem",
            [0x00000D2D] = "vamp1",
            [0x00000D2E] = "vamp2",
            [0x00000D2F] = "vamp3",
            [0x00000D37] = "patsy",
            [0x00000D37] = "time1",
            [0x00000D38] = "time2",
            [0x00000D39] = "time3",
            [0x00000D3E] = "nomap",
            [0x00000D43] = "money",
            [0x00000D4C] = "lover",
            [0x00000D54] = "thief",
            [0x00000D60] = "tiger",
            [0x00000D6C] = "other",
            [0x00000D6F] = "vamps",
            [0x00000D78] = "timer",
            [0x00000D7F] = "mummy",
            [0x00000D83] = "rogue",
            [0x00000D84] = "queen",
            [0x00000D8F] = "posse",
            [0x00000D93] = "qtime",
            [0x00000D9C] = "token",
            [0x00000D9D] = "widow",
            [0x00000DA4] = "nymph",
            [0x00000DB4] = "ruler",
            [0x00000DB6] = "total",
            [0x00000DB6] = "witch",
            [0x00000DD5] = "store",
            [0x0000126C] = "2agent",
            [0x00001864] = "badpcn",
            [0x0000188D] = "daedra",
            [0x000018A8] = "banker",
            [0x000018D0] = "dbgold",
            [0x000018F4] = "gaffer",
            [0x000018FA] = "damsel",
            [0x00001915] = "castle",
            [0x00001934] = "archer",
            [0x00001944] = "healer",
            [0x00001945] = "cleric",
            [0x0000194C] = "father",
            [0x00001986] = "amulet",
            [0x00001998] = "finger",
            [0x000019B1] = "escape",
            [0x000019C1] = "given1",
            [0x000019D2] = "dreugh",
            [0x000019DA] = "hermit",
            [0x000019F9] = "guard1",
            [0x000019FA] = "guard2",
            [0x000019FB] = "guard3",
            [0x000019FC] = "guard4",
            [0x00001A04] = "cousin",
            [0x00001A1F] = "palace",
            [0x00001A3B] = "guards",
            [0x00001A40] = "mggold",
            [0x00001A50] = "hooker",
            [0x00001A54] = "master",
            [0x00001A5F] = "house1",
            [0x00001A60] = "house2",
            [0x00001A61] = "house3",
            [0x00001A68] = "knight",
            [0x00001A70] = "lesser",
            [0x00001A7C] = "letter",
            [0x00001A84] = "scarab",
            [0x00001A98] = "ranger",
            [0x00001AB3] = "oneday",
            [0x00001ACC] = "hunter",
            [0x00001ACC] = "shaman",
            [0x00001AD6] = "school",
            [0x00001AE8] = "mondun",
            [0x00001AE8] = "qgiven",
            [0x00001AEC] = "qgiver",
            [0x00001AF8] = "shield",
            [0x00001AFA] = "target",
            [0x00001B14] = "reward",
            [0x00001B26] = "tavern",
            [0x00001B35] = "temple",
            [0x00001B44] = "weapon",
            [0x00001B4B] = "prince",
            [0x00001B4C] = "murder",
            [0x00001B50] = "poison",
            [0x00001B56] = "priest",
            [0x00001B74] = "spider",
            [0x00001B77] = "victim",
            [0x00001B80] = "potion",
            [0x00001B86] = "snitch",
            [0x00001B94] = "sister",
            [0x00001BB3] = "questg",
            [0x00001BEE] = "yesmap",
            [0x00001BF7] = "spouse",
            [0x00001BFC] = "wraith",
            [0x00001C0C] = "wizard",
            [0x00001C57] = "zombie",
            [0x00002520] = "2dagger",
            [0x0000269F] = "2palace",
            [0x000026FC] = "2letter",
            [0x00002757] = "2ransom",
            [0x0000314B] = "daedra1",
            [0x0000314C] = "daedra2",
            [0x0000314D] = "daedra3",
            [0x0000314E] = "daedra4",
            [0x0000318D] = "daedras",
            [0x000031F6] = "acrobat",
            [0x00003205] = "agentuk",
            [0x00003221] = "acolyte",
            [0x00003238] = "dbguild",
            [0x00003254] = "casfort",
            [0x00003276] = "chemist",
            [0x000032AD] = "failure",
            [0x000032C0] = "centaur",
            [0x00003300] = "breaker",
            [0x00003346] = "coastal",
            [0x0000335C] = "fighter",
            [0x000033A2] = "hideout",
            [0x000033AD] = "clothes",
            [0x000033DE] = "contact",
            [0x00003407] = "bowdung",
            [0x00003429] = "friend1",
            [0x0000342A] = "friend2",
            [0x0000342B] = "friend3",
            [0x0000342C] = "brother",
            [0x0000342C] = "friend4",
            [0x0000344E] = "duelist",
            [0x0000345C] = "burglar",
            [0x0000345F] = "package",
            [0x00003493] = "flowers",
            [0x00003497] = "mapdung",
            [0x00003498] = "dungeon",
            [0x000034B8] = "paladin",
            [0x000034C5] = "jewelry",
            [0x000034C8] = "mansion",
            [0x000034D3] = "marknpc",
            [0x000034EF] = "foundme",
            [0x00003500] = "killmon",
            [0x00003529] = "letter1",
            [0x0000352A] = "letter2",
            [0x00003536] = "readmap",
            [0x0000353C] = "mfriend",
            [0x0000355F] = "message",
            [0x00003573] = "keytime",
            [0x00003594] = "mmaster",
            [0x00003599] = "grizzly",
            [0x000035A4] = "teacher",
            [0x000035C7] = "newdung",
            [0x000035C7] = "qgenemy",
            [0x000035DA] = "mondead",
            [0x000035F4] = "huntend",
            [0x000035FC] = "scholar",
            [0x00003610] = "seducer",
            [0x00003628] = "onehour",
            [0x0000362F] = "relitem",
            [0x0000362F] = "replace",
            [0x00003637] = "mondung",
            [0x00003659] = "reward1",
            [0x0000365A] = "reward2",
            [0x0000365B] = "reward3",
            [0x00003670] = "lovgold",
            [0x00003679] = "peryite",
            [0x00003693] = "revenge",
            [0x000036A4] = "monster",
            [0x000036C0] = "sneaker",
            [0x000036DD] = "vampire",
            [0x000036FB] = "weapons",
            [0x0000375B] = "spiders",
            [0x00003760] = "soldier",
            [0x00003777] = "myndung",
            [0x00003784] = "warrior",
            [0x0000378E] = "prophet",
            [0x00003795] = "success",
            [0x00003798] = "ukcrypt",
            [0x000037B8] = "traitor",
            [0x000037E4] = "queston",
            [0x0000383C] = "upfront",
            [0x0000387D] = "witness",
            [0x00005077] = "2myndung",
            [0x000063BC] = "daedroth",
            [0x000064E0] = "champion",
            [0x00006523] = "fakename",
            [0x0000659C] = "daughter",
            [0x000065A4] = "castfort",
            [0x0000672A] = "gianteel",
            [0x000067C7] = "clothing",
            [0x000067ED] = "contact1",
            [0x000067EE] = "contact2",
            [0x000067FE] = "artifact",
            [0x00006817] = "conhouse",
            [0x00006854] = "assassin",
            [0x0000688E] = "dirtypit",
            [0x000068D2] = "atronach",
            [0x00006936] = "firsthit",
            [0x00006944] = "goldgoth",
            [0x00006961] = "dungeon1",
            [0x00006962] = "dungeon2",
            [0x00006963] = "dungeon3",
            [0x00006977] = "hintdung",
            [0x000069A4] = "hitguard",
            [0x000069F4] = "guardian",
            [0x00006A05] = "evilfocs",
            [0x00006A19] = "keptgems",
            [0x00006A2F] = "evilitem",
            [0x00006A30] = "informer",
            [0x00006A44] = "merchant",
            [0x00006A7B] = "dummyorc",
            [0x00006AB0] = "oblivion",
            [0x00006AC7] = "painting",
            [0x00006B00] = "lessgold",
            [0x00006B19] = "readnote",
            [0x00006B27] = "itemdung",
            [0x00006B5E] = "placemap",
            [0x00006BCC] = "nobleman",
            [0x00006C2F] = "newplace",
            [0x00006C3C] = "qgfriend",
            [0x00006CA0] = "mondung2",
            [0x00006CCC] = "talisman",
            [0x00006CF6] = "huntstop",
            [0x00006D79] = "monster1",
            [0x00006D7A] = "monster2",
            [0x00006D7B] = "monster3",
            [0x00006D7C] = "monster4",
            [0x00006D8F] = "thankyou",
            [0x00006DA7] = "ringdung",
            [0x00006DB0] = "scorpion",
            [0x00006DE4] = "skeleton",
            [0x00006DE7] = "nononono",
            [0x00006E13] = "vampname",
            [0x00006E2D] = "vampires",
            [0x00006E2F] = "vampitem",
            [0x00006E38] = "mtraitor",
            [0x00006E69] = "weaponss",
            [0x00006E71] = "timeforq",
            [0x00006F24] = "qmonster",
            [0x00006F50] = "wereboar",
            [0x00006F5B] = "orsinium",
            [0x00006F60] = "villager",
            [0x00006FE5] = "treasure",
            [0x00006FF0] = "sorceror",
            [0x00006FF4] = "smuggler",
            [0x00006FF9] = "queston1",
            [0x00006FFA] = "queston2",
            [0x00007002] = "werewolf",
            [0x00007054] = "spriggan",
            [0x00007085] = "yesclick",
            [0x00007157] = "withouse",
            [0x000071DC] = "woodsman",
            [0x000099FE] = "2artifact",
            [0x00009CBC] = "2ndparton",
            [0x0000A23C] = "1stparton",
            [0x0000C7B4] = "barbarian",
            [0x0000C976] = "alchemist",
            [0x0000C993] = "challenge",
            [0x0000CAEF] = "fakeplace",
            [0x0000CD1E] = "betrothed",
            [0x0000CE84] = "bodyguard",
            [0x0000D02D] = "artifact1",
            [0x0000D02E] = "artifact2",
            [0x0000D02F] = "artifact3",
            [0x0000D030] = "artifact4",
            [0X0000D031] = "artifact5",
            [0x0000D075] = "bookstore",
            [0x0000D0A4] = "competior",
            [0x0000D0D8] = "mageguild",
            [0x0000D0DF] = "magicitem",
            [0x0000D2CC] = "kidnapper",
            [0x0000D33C] = "givetoken",
            [0x0000D4BC] = "informant",
            [0x0000D4F6] = "keptmetal",
            [0x0000D50F] = "dummymage",
            [0x0000D51F] = "pchasitem",
            [0x0000D588] = "guildhall",
            [0x0000D6B7] = "safehouse",
            [0x0000D6EF] = "itemplace",
            [0x0000D738] = "messenger",
            [0x0000D742] = "qgclicked",
            [0x0000D7BF] = "realmummy",
            [0x0000D80C] = "patsagent",
            [0x0000D8D3] = "extratime",
            [0x0000D95F] = "religitem",
            [0x0000D9DA] = "lordsmail",
            [0x0000D9F0] = "lovechild",
            [0x0000DA2C] = "huntstart",
            [0x0000DAB7] = "lovehouse",
            [0x0000DBD3] = "scorpions",
            [0x0000DCD7] = "vamphouse",
            [0x0000DCED] = "vamprelic",
            [0x0000DD2E] = "vamprival",
            [0x0000DD50] = "vampproof",
            [0x0000DF69] = "villainss",
            [0x0000DFD7] = "prophouse",
            [0x0000E07D] = "questdone",
            [0x0000E0E3] = "questtime",
            [0x0000E1E3] = "totaltime",
            [0x0000E383] = "towertime",
            [0x0000E417] = "townhouse",
            [0x0000E627] = "wrongdung",
            [0x00018F34] = "daedralord",
            [0x0001928F] = "agentplace",
            [0x000195EF] = "battlemage",
            [0x00019717] = "childhouse",
            [0x00019765] = "clearclick",
            [0x00019AF7] = "fatherdung",
            [0x00019C69] = "depository",
            [0x00019F47] = "dragonling",
            [0x00019FC9] = "apothecary",
            [0x00019FCD] = "firedaedra",
            [0x00019FD4] = "dispatcher",
            [0x0001A193] = "escapetime",
            [0x0001A1C8] = "competitor",
            [0x0001A20B] = "gimmegimme",
            [0x0001A30C] = "magicsword",
            [0x0001A318] = "magesguild",
            [0x0001A415] = "aurielsbow",
            [0x0001A4B8] = "gettraitor",
            [0x0001A654] = "givereward",
            [0x0001A90C] = "ingredient",
            [0x0001A910] = "hitseducer",
            [0x0001AA28] = "dummydarkb",
            [0x0001AA4F] = "founditem1",
            [0x0001AA50] = "founditem2",
            [0x0001AA6F] = "pchasitem1",
            [0x0001AA70] = "pchasitem2",
            [0x0001AA71] = "pchasitem3",
            [0x0001AAB8] = "hittraitor",
            [0x0001ABA0] = "pcgetsgold",
            [0x0001ABC0] = "guildmaker",
            [0x0001ACFC] = "readletter",
            [0x0001AD31] = "nightblade",
            [0x0001ADD7] = "rebelhouse",
            [0x0001ADF7] = "itemindung",
            [0x0001B042] = "npcclicked",
            [0x0001B0B7] = "noblehouse",
            [0x0001B12F] = "pickupitem",
            [0x0001B29A] = "shamandead",
            [0x0001B4FB] = "qgiverhome",
            [0x0001B674] = "sheogorath",
            [0x0001B78F] = "thiefplace",
            [0x0001B797] = "thiefhouse",
            [0x0001B7D3] = "teleportpc",
            [0x0001B924] = "vampleader",
            [0x0001B9AE] = "vampkilled",
            [0x0001BA94] = "vampreward",
            [0x0001BAF3] = "rippername",
            [0x0001BCD3] = "shortdelay",
            [0x0001BEEC] = "spellsword",
            [0x0001C185] = "werewolves",
            [0x0001C18C] = "questgiver",
            [0x0001C1E3] = "traveltime",
            [0x0001C3D7] = "witchhouse",
            [0x0001C7B7] = "storehouse",
            [0x0001C928] = "stronghold",
            [0x00027D1C] = "2shedungent",
            [0x00028FB7] = "2storehouse",
            [0x00031BC2] = "daedclicked",
            [0x000327F6] = "alchemyshop",
            [0x00032BF2] = "ancientlich",
            [0x00032C1C] = "darkbmember",
            [0x00032E49] = "childlocale",
            [0x0003342C] = "clickqgiver",
            [0x000337D2] = "iceatronach",
            [0x000338CC] = "bloodfather",
            [0x00033BA0] = "destination",
            [0x00033FEF] = "hidingplace",
            [0x000341B8] = "findtraitor",
            [0x00034417] = "contactdung",
            [0x00034C7C] = "givenletter",
            [0x00034E77] = "givershouse",
            [0x00034FF4] = "hitguardian",
            [0x0003544D] = "dummydaedra",
            [0x00035717] = "hookerhouse",
            [0x00035A0D] = "frostdaedra",
            [0x00035C48] = "lettergiven",
            [0x000362B2] = "pickuplocal",
            [0x000365F7] = "scholardung",
            [0x000365FE] = "relartifact",
            [0x00036C57] = "targethouse",
            [0x00036F1C] = "thiefmember",
            [0x000371C2] = "vampclicked",
            [0x00037697] = "ripperhouse",
            [0x00037BF7] = "victimhouse",
            [0x00037C14] = "queenreward",
            [0x0003815A] = "traitordead",
            [0x00063E8B] = "daedraprince",
            [0x000666A9] = "falseletter1",
            [0x000666AA] = "falseletter2",
            [0x000666AB] = "falseletter3",
            [0x000666AC] = "falseletter4",
            [0x000668A7] = "clickonenemy",
            [0x0006799C] = "finddaughter",
            [0x000685D2] = "fireatronach",
            [0x00068642] = "enemyclicked",
            [0x00069178] = "aurielshield",
            [0x0006A3EF] = "meetingplace",
            [0x0006B2C7] = "mensclothing",
            [0x0006B48D] = "lesserdaedra",
            [0x0006BC6F] = "pickedupitem",
            [0x0006C4D2] = "ironatronach",
            [0x0006C638] = "pickupregion",
            [0x0006CD2E] = "hunterkilled",
            [0x0006CF75] = "oracletemple",
            [0x0006D40B] = "mistresshome",
            [0x0006E38F] = "sleepingmage",
            [0x0006E698] = "thievesguild",
            [0x00070077] = "sistershouse",
            [0x00070CC2] = "rulerclicked",
            [0x000C7C90] = "daedraseducer",
            [0x000CC097] = "daughterhouse",
            [0x000CCFB0] = "clickoblivion",
            [0x000CD2EC] = "clickonqgiver",
            [0x000CD81B] = "betrothedhome",
            [0x000D0AD2] = "fleshatronach",
            [0x000D9A14] = "scholarreward",
            [0x000D9D5F] = "religiousitem",
            [0x000D9EB8] = "missingperson",
            [0x000DB798] = "skeffingcoven",
            [0x000E0914] = "traitorreward",
            [0x000E0DF6] = "questfinished",
            [0x00199CF4] = "betrayguardian",
            [0x001A5D0C] = "giveingredient",
            [0x001A9C53] = "executiondelay",
            [0x001B6FBB] = "revealmonsters",
            [0x001C82C7] = "womensclothing",
            [0x00358AC2] = "oblivionclicked",
            // newly found
            [0x00006895] = "giveback",
            [0x00006D67] = "rewards1",
            [0x00003446] = "givebox",
            [0x0001C326] = "startquest",
            [0x0001C26C] = "queststart",
            [0X000065D0] = "castopen",
            [0X0000386C] = "usewand",
            [0X00006913] = "boxopens",
            [0X00001B1C] = "tatler",
            [0X0000C91E] = "barenziah",
            [0X00003581] = "gortwog",
            [0X00002D81] = "Gortwog",
            [0X0006E2BD] = "scourgbarrow",
            [0x0006A7C3] = "necromancers",
            [0x0001B727] = "thiefhome1",
            [0x00003333] = "daytime",
            [0x0006A575] = "incurability",
            [0x0001A9A9] = "informant1",
            [0X0001B51C] = "shedungent",
            [0x0000031A] = "out",
            [0x0000D4C3] = "midnight3",
            [0x000036CD] = "thieves",
            [0x00003543] = "knights",
            [0X000E38E5] = "stoletreasure",
            [0x0000D9E9] = "persecute",
            [0X0000D80B] = "noblehome",
            [0X0001BB9B] = "priesthome",
            [0X0000DB7B] = "thiefhome",
            [0X0006AA7B] = "merchanthome",
            [0x000002E5] = "nec",
            [0x00000155] = "uk",
            [0x000DBD6B] = "thiefevidence",
            [0X000D866B] = "nobleevidence",
            [0X001BBF6B] = "priestevidence",
            [0X000D576B] = "merchevidence",
            [0X0000B04D] = "Brisienna",
            [0X00016B9D] = "Kingoworms",
            [0X0000BEFF] = "Underking",
            [0X00002A85] = "Eadwyre",
            [0X00003285] = "eadwyre",
            [0X00002D3E] = "Gothryd",
            [0X00005691] = "Akorithi",
            [0X00018FF8] = "daggerfall",
            [0X00068FF8] = "indaggerfall",
            [0X0000D04D] = "brisienna",
            [0X00000DBF] = "totem",
            [0X000068FA] = "lichdead",
        };

        public static string LookUp(uint value)
        {
            var isHashed = (value & 0xFF000000) == 0;
            if (isHashed)
            {
                return Names.TryGetValue(value, out var name) 
                    ? name 
                    : null;
            }

            return $"{value & 0x00FFFFFF}";
        }
    }
}