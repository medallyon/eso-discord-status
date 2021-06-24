DRP = {}
DRP.handlers = {}
DRP.commands = {}

DRP.name = "DiscordRichPresence"

DRP.meta = {
    name = "Discord Rich Presence",
    author = "@Medallyon#5012",
    version = "0.4.2",
    website = "https://www.esoui.com/downloads/info2054-DiscordStatusUpdater.html"
}

DRP.savedVars = {}
DRP.savedVars.name = "DiscordRichPresence_SavedVars"
DRP.savedVars.version = 1

DRP.savedVars.ZO = nil
DRP.savedVars.defaults = {
    ["settings"] = {
        ["prioritySave"] = true,
        ["autoReload"] = false
    },
    ["reloaded"] = true,

    ["account"] = nil,
    ["name"] = nil,
    ["gender"] = nil,
    ["race"] = nil,
    ["class"] = nil,
    ["isChampion"] = nil,
    ["level"] = nil,
    ["alliance"] = nil,
    ["zone"] = nil,
    ["subZone"] = nil,

    ["isgrouped"] = nil,
    ["inDungeon"] = nil,
    ["groupSize"] = nil,
    ["groupRole"] = nil,
    ["prefersDPS"] = nil,
    ["prefersTank"] = nil,
    ["prefersHeal"] = nil,
    ["isDungeonVeteran"] = nil,

    ["bg_GameType"] = nil,
    ["bg_Name"] = nil,
    ["bg_Description"] = nil,

    ["activeQuest"] = nil
}

-------------------------------------------------------------
--                        FUNCTIONS                        --
-------------------------------------------------------------

function DRP:CreateNewSavedVars()
    DRP.savedVars.ZO = ZO_SavedVars:NewAccountWide(DRP.savedVars.name, DRP.savedVars.version, nil, DRP.savedVars.defaults)
end

function DRP:Initialize()
    if DRP.savedVars.ZO == nil then DRP:CreateNewSavedVars() end

    DRP:CreateAddonMenu()

    -- Register Event for after a loading screen / zone change
    EVENT_MANAGER:RegisterForEvent(DRP.name, EVENT_PLAYER_ACTIVATED, DRP.handlers.OnPlayerActivated)

    -- Register Events for quest-related things
    EVENT_MANAGER:RegisterForEvent(DRP.name, EVENT_QUEST_ADDED, DRP.handlers.SaveQuestHandler)
    EVENT_MANAGER:RegisterForEvent(DRP.name, EVENT_QUEST_ADVANCED, DRP.handlers.SaveQuestHandler)
    EVENT_MANAGER:RegisterForEvent(DRP.name, EVENT_QUEST_SHOW_JOURNAL_ENTRY, DRP.handlers.SaveQuestHandler)
    EVENT_MANAGER:RegisterForEvent(DRP.name, EVENT_ACTIVE_QUEST_TOOL_CHANGED, DRP.handlers.SaveQuestHandler)
    EVENT_MANAGER:RegisterForEvent(DRP.name, EVENT_ZONE_STORY_QUEST_ACTIVITY_TRACKED, DRP.handlers.SaveQuestHandler)
end

function DRP:CreateAddonMenu()
    DRP.LAM = LibAddonMenu2

    -- Register & Initialize Addon Settings Panel
    DRP.LAM:RegisterAddonPanel(DRP.name .. "_Settings", {
        type = "panel",
        name = DRP.meta.name,
        author = DRP.meta.author,
        version = DRP.meta.version,
        website = DRP.meta.website,

        registerForRefresh = true,
        slashCommand = "/drp"
    })

    -- Register actual Addon Settings
    DRP.LAM:RegisterOptionControls(DRP.name .. "_Settings", {
        [1] = {
            type = "description",
            text = "This Addon attempts to set your Rich Presence on Discord based on your current location or activity in-game.\n\nIf you're using the |c00b3b3Priority Save|r option, it is unknown how fast your status will update. It seems to be random how long it takes for your status to be updated using this method. But at least with this option, you are avoiding an additional loading screen.\n\nNOTE: Don't forget to start |cff0000DiscordStatusClient.exe|r found in the |cdaa520Client|r folder that comes with the Addon."
        },
        [2] = {
            type = "checkbox",
            reference = "DRP_LAM_control_prioritySave",
            name = "Use Priority Save",
            tooltip = "Automatically update presence when your active Zone or Activity changes.",
            default = true,
            getFunc = function()
                return DRP.savedVars.ZO["settings"]["prioritySave"]
            end,
            setFunc = function(value)
                DRP.savedVars.ZO["settings"]["prioritySave"] = value

                DRP_LAM_control_prioritySave:UpdateDisabled()
                DRP_LAM_control_autoReload:UpdateDisabled()
            end,
            disabled = function()
                if DRP.savedVars.ZO["settings"]["autoReload"] == true then
                    return true
                end

                return false
            end
        },
        [3] = {
            type = "checkbox",
            reference = "DRP_LAM_control_autoReload",
            name = "Auto Reload UI",
            tooltip = "Automatically reload the UI when entering a new Zone.",
            warning = "This will effectively double your time spent in loading screens.",
            default = false,
            requiresReload = true,
            getFunc = function()
                return DRP.savedVars.ZO["settings"]["autoReload"]
            end,
            setFunc = function(value)
                DRP.savedVars.ZO["settings"]["autoReload"] = value

                DRP_LAM_control_prioritySave:UpdateDisabled()
                DRP_LAM_control_autoReload:UpdateDisabled()
            end,
            disabled = function()
                if DRP.savedVars.ZO["settings"]["prioritySave"] == true then
                    return true
                end

                return false
            end
        }
    })
end

function DRP:StoreCharacterData()
    local u = "player"
    local zo = DRP.savedVars.ZO
    if zo == nil then DRP:CreateNewSavedVars() end

    -- Character- and Zone-related Information
    zo["account"] = GetUnitDisplayName(u)
    zo["name"] = GetUnitName(u)
    zo["gender"] = GetUnitGender(u)
    zo["race"] = GetUnitRace(u)
    zo["class"] = GetUnitClass(u)
    zo["alliance"] = GetUnitAlliance(u)
    zo["zone"] = GetUnitZone(u)
    zo["parentZone"] = GetZoneNameById(GetParentZoneId(GetZoneId(GetCurrentMapZoneIndex())))
    zo["subZone"] = GetPlayerActiveSubzoneName()
    zo["isChampion"] = IsUnitChampion(u)
    zo["level"] = GetUnitEffectiveChampionPoints(u)
    if zo["isChampion"] == false then
        zo["level"] = GetUnitLevel(u)
    end

    -- Group-related Information
    zo["isGrouped"] = IsUnitGrouped(u)
    zo["groupSize"] = GetGroupSize()
    zo["inDungeon"] = IsUnitInDungeon(u)
    zo["groupRole"] = GetGroupMemberAssignedRole(u)
    zo["isDungeonVeteran"] = GetCurrentZoneDungeonDifficulty()
    zo["dungeonRole"] = GetGroupMemberSelectedRole(u)

    -- PvP-related Information
    bg = GetCurrentBattlegroundId()
    zo["bg_GameType"] = GetBattlegroundGameType(bg)
    zo["bg_Name"] = GetBattlegroundName(bg)
    zo["bg_Description"] = GetBattlegroundDescription(bg)
end

function DRP.IsInDifferentZone()
    if GetUnitZone("player") ~= DRP.savedVars.ZO["zone"] then
        return true
    end

    return false
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
    local isInDifZone = DRP:IsInDifferentZone()
    if isInDifZone == false then return end

    DRP.StoreCharacterData()

    if DRP.savedVars.ZO["settings"]["prioritySave"] == true
    then
        GetAddOnManager():RequestAddOnSavedVariablesPrioritySave(DRP.name)
    end

    DRP.savedVars.ZO["reloaded"] = not DRP.savedVars.ZO["reloaded"]

    if DRP.savedVars.ZO["settings"]["autoReload"] == true
    and DRP.savedVars.ZO["settings"]["prioritySave"] == false
    and DRP.savedVars.ZO["reloaded"] == false
    then
        ReloadUI()
    end
end

function DRP.handlers.SaveQuestHandler(event, journalIndex)
    local zo = DRP.savedVars.ZO
    if zo == nil then DRP:CreateNewSavedVars() end

    local activeQuest = GetJournalQuestName(journalIndex)
    if zo["activeQuest"] == activeQuest
    or zo["settings"]["prioritySave"] == false
    then return end

    zo["activeQuest"] = activeQuest
    GetAddOnManager():RequestAddOnSavedVariablesPrioritySave(DRP.name)
end

EVENT_MANAGER:RegisterForEvent(DRP.name, EVENT_ADD_ON_LOADED, DRP.handlers.OnAddOnLoaded)
