DS = {}
DS.handlers = {}

DS.name = "DiscordStatus"

DS.meta = {
    name = "Discord Status",
    author = "Medallyon#5012",
    version = "0.4.7",
    website = "https://www.esoui.com/downloads/info2054-DiscordStatusUpdater.html"
}

DS.savedVars = {}
DS.savedVars.name = "DiscordStatus_SavedVars"
DS.savedVars.version = 1

DS.savedVars.ZO = nil
DS.savedVars.defaults = {
    ["settings"] = {
        ["prioritySave"] = true,
        ["autoReload"] = false
    },
    ["reloaded"] = true,
    ["isLoggedIn"] = false,

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

function DS:CreateNewSavedVars()
    DS.savedVars.ZO = ZO_SavedVars:NewAccountWide(DS.savedVars.name, DS.savedVars.version, nil, DS.savedVars.defaults)
end

function DS:Initialize()
    EVENT_MANAGER:UnregisterForEvent(DS.name, EVENT_ADD_ON_LOADED)

    if DS.savedVars.ZO == nil then
        DS:CreateNewSavedVars()
    end

    DS:CreateAddonMenu()

    ZO_PreHook("Logout", DS.Logout)

    -- Register Event for after a loading screen / zone change
    EVENT_MANAGER:RegisterForEvent(DS.name, EVENT_PLAYER_ACTIVATED, DS.handlers.OnPlayerActivated)

    -- Register Events for quest-related things
    EVENT_MANAGER:RegisterForEvent(DS.name, EVENT_QUEST_ADDED, DS.handlers.SaveQuestHandler)
    EVENT_MANAGER:RegisterForEvent(DS.name, EVENT_QUEST_ADVANCED, DS.handlers.SaveQuestHandler)
    EVENT_MANAGER:RegisterForEvent(DS.name, EVENT_QUEST_SHOW_JOURNAL_ENTRY, DS.handlers.SaveQuestHandler)
    EVENT_MANAGER:RegisterForEvent(DS.name, EVENT_ACTIVE_QUEST_TOOL_CHANGED, DS.handlers.SaveQuestHandler)
    EVENT_MANAGER:RegisterForEvent(DS.name, EVENT_ZONE_STORY_QUEST_ACTIVITY_TRACKED, DS.handlers.SaveQuestHandler)
end

function DS:CreateAddonMenu()
    DS.LAM = LibAddonMenu2

    -- Register & Initialize Addon Settings Panel
    DS.LAM:RegisterAddonPanel(DS.name .. "_Settings", {
        type = "panel",
        name = DS.meta.name,
        author = DS.meta.author,
        version = DS.meta.version,
        website = DS.meta.website,

        registerForRefresh = true,
        slashCommand = "/ds"
    })

    -- Register actual Addon Settings
    DS.LAM:RegisterOptionControls(DS.name .. "_Settings", {
        [1] = {
            type = "description",
            text = "This Addon attempts to set your Status on Discord based on your current location or activity in-game.\n\nIf you're using the |c00b3b3Priority Save|r option, it is unknown how fast your status will update. It seems to be random how long it takes for your status to be updated using this method. But at least with this option, you are avoiding an additional loading screen.\n\nNOTE: Don't forget to start |cff0000DiscordStatusClient.exe|r found in the |cdaa520Client|r folder that comes with the Addon."
        },

        [2] = {
            type = "checkbox",
            reference = "DRP_LAM_control_prioritySave",
            name = "Use Priority Save",
            tooltip = "Automatically update presence when your active Zone or Activity changes.",
            default = true,

            getFunc = function()
                return DS.savedVars.ZO["settings"]["prioritySave"]
            end,

            setFunc = function(value)
                DS.savedVars.ZO["settings"]["prioritySave"] = value

                DRP_LAM_control_prioritySave:UpdateDisabled()
                DRP_LAM_control_autoReload:UpdateDisabled()
            end,

            disabled = function()
                if DS.savedVars.ZO["settings"]["autoReload"] == true then
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
                return DS.savedVars.ZO["settings"]["autoReload"]
            end,

            setFunc = function(value)
                DS.savedVars.ZO["settings"]["autoReload"] = value

                DRP_LAM_control_prioritySave:UpdateDisabled()
                DRP_LAM_control_autoReload:UpdateDisabled()
            end,

            disabled = function()
                if DS.savedVars.ZO["settings"]["prioritySave"] == true then
                    return true
                end

                return false
            end
        }
    })
end

function DS:StoreCharacterData()
    local u = "player"
    local zo = DS.savedVars.ZO
    if zo == nil then
        DS:CreateNewSavedVars()
    end

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

function DS.IsInDifferentZone()
    if GetUnitZone("player") ~= DS.savedVars.ZO["zone"] then
        return true
    end

    return false
end

function DS.Logout()
    DS.savedVars.ZO["isLoggedIn"] = false;

    -- ensure the original function is called afterwards
    return false;
end

--------------------------------------------------------------
--                         HANDLERS                         --
--------------------------------------------------------------

function DS.handlers.OnAddOnLoaded(event, addonName)
    if addonName == DS.name then
        DS:Initialize()
    end
end

function DS.handlers.OnPlayerActivated(event, isInitialLoad)
    DS.savedVars.ZO["isLoggedIn"] = true;

    local isInDifZone = DS:IsInDifferentZone()
    if isInDifZone == false then return end

    DS.StoreCharacterData()

    if DS.savedVars.ZO["settings"]["prioritySave"] == true then
        GetAddOnManager():RequestAddOnSavedVariablesPrioritySave(DS.name)
    end

    DS.savedVars.ZO["reloaded"] = not DS.savedVars.ZO["reloaded"]

    if DS.savedVars.ZO["settings"]["autoReload"] == true
        and DS.savedVars.ZO["settings"]["prioritySave"] == false
        and DS.savedVars.ZO["reloaded"] == false
    then
        ReloadUI()
    end
end

function DS.handlers.SaveQuestHandler(event, journalIndex)
    local zo = DS.savedVars.ZO
    if zo == nil then
        DS:CreateNewSavedVars()
    end

    local activeQuest = GetJournalQuestName(journalIndex)
    if zo["activeQuest"] == activeQuest
        or zo["settings"]["prioritySave"] == false
    then return end

    zo["activeQuest"] = activeQuest
    GetAddOnManager():RequestAddOnSavedVariablesPrioritySave(DS.name)
end

EVENT_MANAGER:RegisterForEvent(DS.name, EVENT_ADD_ON_LOADED, DS.handlers.OnAddOnLoaded)
