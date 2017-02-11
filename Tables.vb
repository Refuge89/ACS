Imports System.Drawing.Text.PrivateFontCollection
Public Module Tables

    'Strings
    Public CREATURE_ENTRIES_PATH As String = Application.StartupPath + "\tables\creature_template_entries.txt"
    Public FACTION_ENTRIES_PATH As String = Application.StartupPath + "\tables\factions.txt"
    Public CREATURE_TEMPLATE_1_ENTRIES_PATH As String = Application.StartupPath + "\tables\creature_template1.txt"
    Public ITEM_1_ENTRIES_PATH As String = Application.StartupPath + "\tables\items.txt"
    Public EXTENDED_COST_ENTRIES_PATH As String = Application.StartupPath + "\tables\extended_cost.txt"
    Public QUEST_XP_ENTRIES_PATH As String = Application.StartupPath + "\tables\questxp.txt"
    Public INTERFACE_DISPLAY_NAME As String = Application.StartupPath + "\tables\displayinterface.txt"
    Public ITEM_DISPLAY As String = Application.StartupPath + "\tables\itemdisplay.txt"

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

    Public ICON_GOLD As Bitmap = My.Resources.money_gold_2_
    Public ICON_SILVER As Bitmap = My.Resources.money_silver_1_
    Public ICON_COPPER As Bitmap = My.Resources.money_copper_1_


    ' font
    Public FONT_COLLECTION As New Drawing.Text.PrivateFontCollection()

    Public FONT_MORPHEUS As Font
    Public FONT_FRIZ As Font
    Public FONT_MONOSPACE As New Font("Monospace", 9, FontStyle.Regular, GraphicsUnit.Pixel, 0)


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

    'Positions
    Public POINT_QUEST_TITLE As New Point(20, 100)
    Public POINT_QUEST_OBJ As New Point(25, 120)
    Public POINT_QUEST_DESC_BOLD As New Point(20, 240)
    Public POINT_QUEST_DESCRIPTION As New Point(25, 260)
    Public POINT_QUEST_REWARDS_BOLD As New Point(20, 340)
    Public POINT_QUEST_REWARDS As New Point(25, 362)

    Public START_POINT_REWARDS As New Point(30, 385)


    ' Size
    Public ICON_REWARD_SIZE As New Size(15, 15)
    Public REWARDS_SPACE_LENGTH As Integer = 140

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

    Public Function findItemNameById(ByVal id As Int64) As String
        Dim result As String
        For Each s As String In IO.File.ReadAllLines(ITEM_1_ENTRIES_PATH)
            Dim template As String()
            template = s.Split(",")
            If template(0) = id.ToString() Then
                result = template(1)
                Return result
            End If
        Next
        result = ACS.ENTRY_NOT_AVAILABLE
        Return result
    End Function

    Public Function getItemDisplayNameById(ByVal id As Int64) As String
        Dim result As String = ""
        Dim displayId As String = ""

        For Each l As String In IO.File.ReadAllLines(ITEM_DISPLAY)
            If l.StartsWith(Chr(34)) Then
                Continue For
            End If

            If l.Split(",")(0) = id.ToString() Then
                displayId = l.Split(",")(1)
            End If
        Next

        If Not displayId = "" Then
            For Each l As String In IO.File.ReadAllLines(INTERFACE_DISPLAY_NAME)
                If l.Split(",")(0) = displayId Then
                    result = l.Split(",")(1)
                End If
            Next
        End If

        Return result
    End Function

    Public Function getItemIconURL(ByVal name As String) As String
        Return "https://wow.zamimg.com/images/wow/icons/medium/" & name & ".jpg"
    End Function

    Public Function getFactionNameById(ByVal faction As Int64) As String
        For Each s As String In IO.File.ReadAllLines(FACTION_ENTRIES_PATH)
            Dim temp As String()
            temp = s.Split(",")

            Dim name As String = temp(1)
            Dim id As Integer = Convert.ToUInt64(temp(0))

            If id = faction Then
                Return name
                Exit For
            End If
        Next
        Return "Not found!"
    End Function

    Public Function ResizeIcon(ByVal SourceImage As Drawing.Image, ByVal s As Size) As Drawing.Bitmap

        Dim TargetWidth As Int32 = s.Width
        Dim TargetHeight As Int32 = s.Height
        Dim bmSource = New Drawing.Bitmap(SourceImage)
        Dim bmDest As New Drawing.Bitmap(TargetWidth, TargetHeight, Drawing.Imaging.PixelFormat.Format32bppArgb)

        Dim nSourceAspectRatio = bmSource.Width / bmSource.Height
        Dim nDestAspectRatio = bmDest.Width / bmDest.Height

        Dim NewX = 0
        Dim NewY = 0
        Dim NewWidth = bmDest.Width
        Dim NewHeight = bmDest.Height

        If nDestAspectRatio = nSourceAspectRatio Then
            'same ratio
        ElseIf nDestAspectRatio > nSourceAspectRatio Then
            'Source is taller
            NewWidth = Convert.ToInt32(Math.Floor(nSourceAspectRatio * NewHeight))
            NewX = Convert.ToInt32(Math.Floor((bmDest.Width - NewWidth) / 2))
        Else
            'Source is wider
            NewHeight = Convert.ToInt32(Math.Floor((1 / nSourceAspectRatio) * NewWidth))
            NewY = Convert.ToInt32(Math.Floor((bmDest.Height - NewHeight) / 2))
        End If

        Using grDest = Drawing.Graphics.FromImage(bmDest)
            With grDest
                .CompositingQuality = Drawing.Drawing2D.CompositingQuality.HighQuality
                .InterpolationMode = Drawing.Drawing2D.InterpolationMode.HighQualityBicubic
                .PixelOffsetMode = Drawing.Drawing2D.PixelOffsetMode.HighQuality
                .SmoothingMode = Drawing.Drawing2D.SmoothingMode.AntiAlias
                .CompositingMode = Drawing.Drawing2D.CompositingMode.SourceOver

                .DrawImage(bmSource, NewX, NewY, NewWidth, NewHeight)
            End With
        End Using

        Return bmDest
    End Function


    Public Function addSpaceToText(ByVal text As String, ByVal length As Integer, ByVal font As Font)
        Dim textSize As Size = TextRenderer.MeasureText(text, font)
        Dim spaceSize As Size = TextRenderer.MeasureText(" ", font)

        Dim s As String = ""
        Dim result As String = text
        If textSize.Width >= length Then

            While TextRenderer.MeasureText(result, font).Width >= length
                result = result.Remove(result.Length - 2, 2)
            End While

            result = result + "..."
            Return result
        Else

            While TextRenderer.MeasureText(result, font).Width < length
                result = result + " "
            End While

            Return result
        End If
    End Function

End Module
