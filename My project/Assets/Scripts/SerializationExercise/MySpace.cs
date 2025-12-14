//using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace MyNamespace
{
    public class AccountInfo
    {
        public string email_addr { get; set; }
        public DateTime created_on { get; set; }
    }

    public class ArmorSlot
    {
        public string item_code { get; set; }
        public string name_txt { get; set; }
        public string rarity { get; set; }
        public int def_bonus { get; set; }

        public ResistMap resistmap { get; set; }
    }

    public class Audio
    {
        public double vol_master { get; set; }
        public double vol_fx { get; set; }
        public double vol_music { get; set; }
    }

    public class BagMain
    {
        public string itm_id { get; set; }
        public string itm_name { get; set; }
        public int qty { get; set; }

        public ExtraVals extravals { get; set; }
    }

    public class BagResource
    {
        public string res_type { get; set; }
        public int units { get; set; }
    }

    public class Character
    {
        public string char_id { get; set; }
        public string char_name { get; set; }

        public string classtype { get; set; }
        public int level_cur { get; set; }
        public int xp_next_lvl { get; set; }
        public Stats stats { get; set; }
        public Equipment equipment { get; set; }
    }

    public class ControlsMap
    {
        public double sens_mouse { get; set; }
        public Keybinds keybinds { get; set; }
    }

    public class Equipment
    {
        public WeaponSlot weapon_slot { get; set; }
        public ArmorSlot armor_slot { get; set; }
    }

    public class ExtraVals
    {
        public int energy_lvl { get; set; }
    }

    public class Graphics
    {
        public string quality_lvl { get; set; }
        public int fov_angle { get; set; }
    }

    public class Inventory
    {
        public List<BagMain> bag_main { get; set; }
        public List<BagResource> bag_resources { get; set; }
    }

    public class Keybinds
    {
        public string move_forward { get; set; }
        public string move_back { get; set; }
        public string special_skill { get; set; }
        public string ultimate_skill { get; set; }
    }

    public class MatchHistory
    {
        public string match_id { get; set; }
        public int start_time_unix { get; set; }
        public int duration_sec { get; set; }
        public string result { get; set; }
        public Score score { get; set; }
    }

    public class Objective
    {
        public string desc { get; set; }
        public bool done { get; set; }
    }

    public class QuestsActive
    {
        public string q_id { get; set; }
        public string qTitle { get; set; }

        public int progressstep { get; set; }
        public List<Objective> objectives { get; set; }
    }

    public class ResistMap
    {
        public int fire { get; set; }
        public int ice { get; set; }
        public int shock { get; set; }
    }

    public class Root
    {
        public string player_id { get; set; }
        public string nickname { get; set; }
        public AccountInfo account_info { get; set; }
        public Character character { get; set; }
        public List<QuestsActive> quests_active { get; set; }
        public Inventory inventory { get; set; }
        public List<MatchHistory> match_history { get; set; }
        public SettingsCfg settings_cfg { get; set; }
    }

    public class Score
    {
        public int kills { get; set; }
        public int assists { get; set; }
        public int deaths { get; set; }
    }

    public class SettingsCfg
    {
        public Audio audio { get; set; }
        public Graphics graphics { get; set; }
        public ControlsMap controls_map { get; set; }
    }

    public class Stats
    {
        public int hp_max { get; set; }
        public int mp_max { get; set; }
        public int atk_pwr { get; set; }

        public double def_rate { get; set; }

        public double critchance { get; set; }
    }

    public class WeaponSlot
    {
        public string item_code { get; set; }
        public string name_txt { get; set; }
        public string rarity { get; set; }
        public int dmg_val { get; set; }

        public double fireRatesec { get; set; }
    }

}