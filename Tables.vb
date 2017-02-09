Imports System.Drawing.Text.PrivateFontCollection
Public Module Tables

    'Strings
    Public CREATURE_ENTRIES_PATH As String = Application.StartupPath + "\tables\creature_template_entries.txt"
    Public FACTION_ENTRIES_PATH As String = Application.StartupPath + "\tables\factions.txt"
    Public CREATURE_TEMPLATE_1_ENTRIES_PATH As String = Application.StartupPath + "\tables\creature_template1.txt"
    Public ITEM_1_ENTRIES_PATH As String = Application.StartupPath + "\tables\items.txt"
    Public EXTENDED_COST_ENTRIES_PATH As String = Application.StartupPath + "\tables\extended_cost.txt"
    Public QUEST_XP_ENTRIES_PATH As String = Application.StartupPath + "\tables\questxp.txt"

    Public FACTION_NOT_AVAILABLE As String = "✖ Faction Not Available!"
    Public FACTION_AVAILABLE As String = "✓ "

    Public TICK As String = "✓"
    Public CROSS As String = "✖"

    Public ENTRY_NOT_AVAILABLE As String = "✖ Not Available!"
    Public ENTRY_AVAILABLE As String = "✓ Available!"

    Public ERR_ITEM_NOT_FOUND As String = "<Item not found>"

    Public RACES() As String = {"Human", "Orc", "Dwarf", "Undead", "Night Elf", "Tauren", "Gnome", "Troll", "Draenei", "Blood Elf"}
    Public RACES_MASK() As Integer = {0, 1, 2, 4, 3, 5, 6, 7, 10, 9}
    Public CLASSES() As String = {"Warrior", "Paladin", "Hunter", "Rogue", "Priest", "Death Knight", "Shaman", "Mage", "Warlock", "Druid"}
    Public CLASSES_MASK() As Integer = {0, 1, 2, 3, 4, 5, 6, 7, 8, 10}

    Public RACES_DICT As New Dictionary(Of String, Integer)
    Public CLASS_DICT As New Dictionary(Of String, Integer)

    Public EXAMPLE_TITLE As String = "A Quest"
    Public EXAMPLE_DETAILS As String = "Kill 9 thing"
    Public EXAMPLE_OBJECTIVE As String = "Thing 0/9"
    Public EXAMPLE_DESCRIPTION As String = "Those Things are dangerous, kill them"
    Public EXAMPLE_REWARDS As String = "You will receive: "


    ' font
    Public FONT_COLLECTION As New Drawing.Text.PrivateFontCollection()

    Public FONT_MORPHEUS As Font
    Public FONT_FRIZ As Font


    'Enums
    Public Enum DATA_TYPE
        FACTIONS
        TEMPLATE1
        ITEMS1
        EXTCOST
    End Enum

    Public Enum CHOICE_TYPE
        FACTION
        CREATURE
        ITEMS
    End Enum

    'Queries
    Public VENDOR_QUERY As String = "DELETE FROM creature_template WHERE entry = {0};" + vbCrLf + _
        "INSERT INTO `creature_template` (`entry`, `difficulty_entry_1`, `difficulty_entry_2`, `difficulty_entry_3`, `KillCredit1`, `KillCredit2`, `modelid1`, `modelid2`, `modelid3`, `modelid4`, `name`, `subname`, `IconName`, `gossip_menu_id`, `minlevel`, `maxlevel`, `exp`, `faction`, `npcflag`, `speed_walk`, `speed_run`, `scale`, `rank`, `mindmg`, `maxdmg`, `dmgschool`, `attackpower`, `dmg_multiplier`, `baseattacktime`, `rangeattacktime`, `unit_class`, `unit_flags`, `unit_flags2`, `dynamicflags`, `family`, `trainer_type`, `trainer_spell`, `trainer_class`, `trainer_race`, `minrangedmg`, `maxrangedmg`, `rangedattackpower`, `type`, `type_flags`, `lootid`, `pickpocketloot`, `skinloot`, `resistance1`, `resistance2`, `resistance3`, `resistance4`, `resistance5`, `resistance6`, `spell1`, `spell2`, `spell3`, `spell4`, `spell5`, `spell6`, `spell7`, `spell8`, `PetSpellDataId`, `VehicleId`, `mingold`, `maxgold`, `AIName`, `MovementType`, `InhabitType`, `HoverHeight`, `Health_mod`, `Mana_mod`, `Armor_mod`, `RacialLeader`, `movementId`, `RegenHealth`, `mechanic_immune_mask`, `flags_extra`, `ScriptName`, `VerifiedBuild`) VALUES" + vbCrLf + _
        "({0},'0','0','0','0','0',{1},'0','0','0',""{2}"",""{3}"",NULL,'0','82','83','0',{4},{6},'1','1.14286',{5},'0','13','17','0','42','1','2000','2000','1','512','2048','0','0','0','0','0','0','9','13','100','7','0','0','0','0','0','0','0','0','0','0','0','0','0','0','0','0','0','0','0','0','0','0','','0','3','1','1','1','1','0','0','1','0','2','','12340');" + vbCrLf + vbCrLf + _
        "DELETE FROM npc_vendor WHERE entry = {0};" + vbCrLf + "INSERT INTO `npc_vendor` (`entry`, `item`, `ExtendedCost`) VALUES" + vbCrLf
    Public VENDOR_QUERY_SINGLE As String = "({0},'{1}','{2}')"
    Public VENDOR_QUERY_WINDOW_SIZE As Size = New Size(700, 300)

    'Functions
    Public Sub modifyItem(ByVal num As NumericUpDown, ByVal lbl As Label, ByVal source As String())
        For Each s As String In source
            Dim template As String()
            template = s.Split(",")
            If template(0) = num.Value.ToString() Then
                lbl.Text = ACS.TICK & " " & template(1)
                lbl.ForeColor = Color.Green
                Exit Sub
            End If
        Next
        lbl.Text = ACS.ENTRY_NOT_AVAILABLE
        lbl.ForeColor = Color.Red
    End Sub


End Module
