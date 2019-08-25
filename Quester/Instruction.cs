namespace Quester
{
    internal enum Instruction
    {
        PlaceItemAt = 0,
        WhenGivingItemToNpc = 1,
        TriggerOnMobKills = 2,
        TriggerOnItemFound = 3,
        QuestSuccess = 4,
        Unknown5_ItemLocation = 5,
        EndQuest = 6,
        Unset = 7,
        StartQuest = 8,
        CreateFoe = 9,
        AddTopics = 10,
        RemoveTopics = 11,
        StartTimer = 12,
        StopTimer = 13,
        Unknown17_LocationValueValue = 17,
        AddLocationToMap = 19,
        TriggerOnMobHurtByPlayer = 21,
        PlaceMobAtLocation = 22,
        CreateLogEntry = 23,
        RemoveLogFromSlot = 24,
        PlaceItemOnCharacter = 26,
        ShowLocationOnMap = 27,
        TriggerOnClickNpc = 28,
        PromptYesNo = 29,
        PlaceNpcAt = 30,
        WhenTimeOfDayBetween = 31,
        PickOneOf = 34,
        CycleNextState = 35,
        GiveItemToPlayer = 36,
        CheckNpcReputation = 37,
        AddRumor = 38,
        PlaceItemOnMob = 39,
        WhenAtLocation = 43,
        DeleteNpc = 44,
        HideNpc = 46,
        ShowNpc = 48,
        CureAllDisease = 49,
        PlayMovie = 50,
        DisplayMessage = 51,
        TriggerOnAndStates = 52,
        TriggerOnOrStates = 53,
        MakePermanent = 54,
        StartNpcEscort = 55,
        StopNpcEscort = 56,
        WhenItemIsUsed = 57,
        CureVampirism = 58,
        CureLycanthropy = 59,
        PlaySoundOnce = 60,
        AdjustReputationWithNpc = 61,
        OverrideWeather = 62,
        Unknown63_Mob = 63,
        Unknown64_Mob = 64,
        AdjustLegalReputation = 65,
        DoneWithMob = 68,
        SetMobPowerLevel = 69,
        WhenPlayerHasItems = 70,
        TakePlayerGold = 71,
        Unknown72_Value = 72,
        WhenPlayerCasts = 73,
        PlaceItemOnPlayer = 76,
        TriggerOnPlayerLevel = 77,
        Unknown78_Value = 78,
        TriggerOnFactionReputation = 79,
        EnsureNpcLocation = 81,
        StopNpcConversation = 82,
        Unknown83_Location = 83,
        PlaySound = 84,
        SetQuestor = 85,
        UnsetQuestor = 86,
        SendFoe = 87
    }
}