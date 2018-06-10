DRP = {}
DRP.handlers = {}
DRP.commands = {}

DRP.name = "DiscordRichPresence"

DRP.savedVars = {}
DRP.savedVars.name = "DiscordRichPresence_SavedVars"
DRP.savedVars.version = 1

DRP.savedVars.ZO = nil
DRP.savedVars.defaults = {
    ["initial"] = nil,
    ["account"] = nil,
    ["name"] = nil,
    ["gender"] = nil,
    ["race"] = nil,
    ["class"] = nil,
    ["isChampion"] = nil,
    ["level"] = nil,
    ["alliance"] = nil,
    ["zone"] = nil,
    ["isgrouped"] = nil,
    ["inDungeon"] = nil,
    ["groupSize"] = nil,
    ["groupRole"] = nil
}

-------------------------------------------------------------
--                        FUNCTIONS                        --
-------------------------------------------------------------

function DRP:Initialize()
    local savedVars = DRP.savedVars
    if DRP.savedVars.ZO == nil then
        DRP.savedVars.ZO = ZO_SavedVars:NewAccountWide(savedVars.name, savedVars.version, nil, savedVars.defaults)
    end

    EVENT_MANAGER:RegisterForEvent(DRP.name, EVENT_PLAYER_ACTIVATED, DRP.handlers.OnPlayerActivated)
end

function DRP:StoreCharacterData(isInitialLoad)
    local u = "player";
    
    DRP.savedVars.ZO["initial"] = isInitialLoad
    DRP.savedVars.ZO["account"] = GetUnitDisplayName(u)
    DRP.savedVars.ZO["name"] = GetUnitName(u)
    DRP.savedVars.ZO["gender"] = GetUnitGender(u)
    DRP.savedVars.ZO["race"] = GetUnitRace(u)
    DRP.savedVars.ZO["class"] = GetUnitClass(u)
    DRP.savedVars.ZO["alliance"] = GetUnitAlliance(u)
    DRP.savedVars.ZO["zone"] = GetUnitZone(u)
    DRP.savedVars.ZO["isChampion"] = IsUnitChampion(u)
    DRP.savedVars.ZO["level"] = GetUnitEffectiveChampionPoints(u)
    if DRP.savedVars.ZO["isChampion"] == false then
        DRP.savedVars.ZO["level"] = GetUnitLevel(u)
    end

    DRP.savedVars.ZO["isGrouped"] = IsUnitGrouped(u)
    DRP.savedVars.ZO["groupSize"] = GetGroupSize()
    DRP.savedVars.ZO["inDungeon"] = IsUnitInDungeon(u)
    -- 2 for "tank"
    DRP.savedVars.ZO["groupRole"] = GetGroupMemberAssignedRole(u)
end

--------------------------------------------------------------
--                         HANDLERS                         --
--------------------------------------------------------------

function DRP.handlers.OnAddOnLoaded(event, addonName)
    if addonName == DRP.name then
        DRP:Initialize()
    end
end

function DRP.handlers.OnPlayerActivated(event, isInitialLoad)
    DRP.StoreCharacterData(isInitialLoad)
end

EVENT_MANAGER:RegisterForEvent(DRP.name, EVENT_ADD_ON_LOADED, DRP.handlers.OnAddOnLoaded)
