DRP = {}
DRP.handlers = {}
DRP.commands = {}

DRP.name = "DiscordRichPresence"

DRP.meta = {
    name = "Discord Rich Presence",
    author = "@Medallyon#5012",
    version = "0.2.1",
    website = "https://github.com/Medallyon/ESO_Discord_RichPresence_Client"
}

DRP.savedVars = {}
DRP.savedVars.name = "DiscordRichPresence_SavedVars"
DRP.savedVars.version = 1

DRP.savedVars.ZO = nil
DRP.savedVars.defaults = {
    ["settings"] = {
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
    ["bg_Description"] = nil
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

    EVENT_MANAGER:RegisterForEvent(DRP.name, EVENT_PLAYER_ACTIVATED, DRP.handlers.OnPlayerActivated)
end

function DRP:CreateAddonMenu()
    DRP.LAM = LibStub:GetLibrary("LibAddonMenu-2.0")

    DRP.LAM:RegisterAddonPanel(DRP.name .. "_Options", {
        type = "panel",
        name = DRP.meta.name,
        author = DRP.meta.author,
        version = DRP.meta.version,
        website = DRP.meta.website
    })

    DRP.LAM:RegisterOptionControls(DRP.name .. "_Options", {
        {
            type = "checkbox",
            name = "Auto Reload",
            tooltip = "Automatically reload the UI when entering a new Zone.",
            warning = "This will increase your time spent in loading screens",
            getFunc = function()
                return DRP.savedVars.ZO["settings"]["autoReload"]
            end,
            setFunc = function(value)
                DRP.savedVars.ZO["settings"]["autoReload"] = value
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
    zo["prefersDPS"], zo["prefersTank"], zo["prefersHeal"] = GetGroupMemberRoles(u)

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

    DRP.StoreCharacterData()
    DRP.savedVars.ZO["reloaded"] = not DRP.savedVars.ZO["reloaded"]

    if isInDifZone == true
    and DRP.savedVars.ZO["settings"]["autoReload"] == true
    and DRP.savedVars.ZO["reloaded"] == false
    then
        ReloadUI()
    end
end

EVENT_MANAGER:RegisterForEvent(DRP.name, EVENT_ADD_ON_LOADED, DRP.handlers.OnAddOnLoaded)
