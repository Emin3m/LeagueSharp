#region

using System;
using LeagueSharp;
using LeagueSharp.Common;
using SharpDX;
using System.Drawing;

#endregion

namespace AutoLevelSpell
{
    internal class Program
    {
        public static Menu Menu;
        public static int[] seq = new int[18];
        public static int[] def_seq = new int[18];
        public static Boolean first = true;
        public static double offset = 45;
        private static void Main(string[] args)
        {
            CustomEvents.Game.OnGameLoad += Game_OnGameLoad;
        }

        private static void Game_OnGameLoad(EventArgs args)
        {
            def_seq = GetSequence();
            var champion = ObjectManager.Player.ChampionName;

            Menu = new Menu("LevelUp by Emin3m", "Emin3m`s AutoLevel", true);
            Menu.AddItem(new MenuItem(champion + "Enabled", "Enabled").SetValue(true));
            Menu.Item(champion + "Enabled").ValueChanged += Enabled_ValueChanged;
            Menu.AddItem(new MenuItem(champion + "Level1", "Level 1").SetValue(new Slider(def_seq[0], 1, 4)));
            Menu.Item(champion + "Level1").ValueChanged += Level1_ValueChanged;
            Menu.AddItem(new MenuItem(champion + "Level2", "Level 2").SetValue(new Slider(def_seq[1], 1, 4)));
            Menu.Item(champion + "Level2").ValueChanged += Level2_ValueChanged;
            Menu.AddItem(new MenuItem(champion + "Level3", "Level 3").SetValue(new Slider(def_seq[2], 1, 4)));
            Menu.Item(champion + "Level3").ValueChanged += Level3_ValueChanged;
            Menu.AddItem(new MenuItem(champion + "Level4", "Level 4").SetValue(new Slider(def_seq[3], 1, 4)));
            Menu.Item(champion + "Level4").ValueChanged += Level4_ValueChanged;
            Menu.AddItem(new MenuItem(champion + "Level5", "Level 5").SetValue(new Slider(def_seq[4], 1, 4)));
            Menu.Item(champion + "Level5").ValueChanged += Level5_ValueChanged;
            Menu.AddItem(new MenuItem(champion + "Level6", "Level 6").SetValue(new Slider(def_seq[5], 1, 4)));
            Menu.Item(champion + "Level6").ValueChanged += Level6_ValueChanged;
            Menu.AddItem(new MenuItem(champion + "Level7", "Level 7").SetValue(new Slider(def_seq[6], 1, 4)));
            Menu.Item(champion + "Level7").ValueChanged += Level7_ValueChanged;
            Menu.AddItem(new MenuItem(champion + "Level8", "Level 8").SetValue(new Slider(def_seq[7], 1, 4)));
            Menu.Item(champion + "Level8").ValueChanged += Level8_ValueChanged;
            Menu.AddItem(new MenuItem(champion + "Level9", "Level 9").SetValue(new Slider(def_seq[8], 1, 4)));
            Menu.Item(champion + "Level9").ValueChanged += Level9_ValueChanged;
            Menu.AddItem(new MenuItem(champion + "Level10", "Level 10").SetValue(new Slider(def_seq[9], 1, 4)));
            Menu.Item(champion + "Level10").ValueChanged += Level10_ValueChanged;
            Menu.AddItem(new MenuItem(champion + "Level11", "Level 11").SetValue(new Slider(def_seq[10], 1, 4)));
            Menu.Item(champion + "Level11").ValueChanged += Level11_ValueChanged;
            Menu.AddItem(new MenuItem(champion + "Level12", "Level 12").SetValue(new Slider(def_seq[11], 1, 4)));
            Menu.Item(champion + "Level12").ValueChanged += Level12_ValueChanged;
            Menu.AddItem(new MenuItem(champion + "Level13", "Level 13").SetValue(new Slider(def_seq[12], 1, 4)));
            Menu.Item(champion + "Level13").ValueChanged += Level13_ValueChanged;
            Menu.AddItem(new MenuItem(champion + "Level14", "Level 14").SetValue(new Slider(def_seq[13], 1, 4)));
            Menu.Item(champion + "Level14").ValueChanged += Level14_ValueChanged;
            Menu.AddItem(new MenuItem(champion + "Level15", "Level 15").SetValue(new Slider(def_seq[14], 1, 4)));
            Menu.Item(champion + "Level15").ValueChanged += Level15_ValueChanged;
            Menu.AddItem(new MenuItem(champion + "Level16", "Level 16").SetValue(new Slider(def_seq[15], 1, 4)));
            Menu.Item(champion + "Level16").ValueChanged += Level16_ValueChanged;
            Menu.AddItem(new MenuItem(champion + "Level17", "Level 17").SetValue(new Slider(def_seq[16], 1, 4)));
            Menu.Item(champion + "Level17").ValueChanged += Level17_ValueChanged;
            Menu.AddItem(new MenuItem(champion + "Level18", "Level 18").SetValue(new Slider(def_seq[17], 1, 4)));
            Menu.Item(champion + "Level18").ValueChanged += Level18_ValueChanged;
            
            Menu.AddToMainMenu();
            seq = new[] { Menu.Item(champion + "Level1").GetValue<Slider>().Value, Menu.Item(champion + "Level2").GetValue<Slider>().Value, Menu.Item(champion + "Level3").GetValue<Slider>().Value, Menu.Item(champion + "Level4").GetValue<Slider>().Value, Menu.Item(champion + "Level5").GetValue<Slider>().Value, Menu.Item(champion + "Level6").GetValue<Slider>().Value, Menu.Item(champion + "Level7").GetValue<Slider>().Value, Menu.Item(champion + "Level8").GetValue<Slider>().Value, Menu.Item(champion + "Level9").GetValue<Slider>().Value, Menu.Item(champion + "Level10").GetValue<Slider>().Value, Menu.Item(champion + "Level11").GetValue<Slider>().Value, Menu.Item(champion + "Level12").GetValue<Slider>().Value, Menu.Item(champion + "Level13").GetValue<Slider>().Value, Menu.Item(champion + "Level14").GetValue<Slider>().Value, Menu.Item(champion + "Level15").GetValue<Slider>().Value, Menu.Item(champion + "Level16").GetValue<Slider>().Value, Menu.Item(champion + "Level17").GetValue<Slider>().Value, Menu.Item(champion + "Level18").GetValue<Slider>().Value };
            Game.PrintChat("[00:00] <font color='#C80046'>AutoLevelUp Spells by Emin3m loaded...</font>");
            Game.PrintChat("[00:00] <font color='#C80046'>You have " + offset + " seconds time to change your settings for this champion after the AutoLevelSpell starts working!</font>");
            Game.PrintChat("[00:00] <font color='#C80046'>Note: 1 = Q - 2 = W - 3 = E - 4 = R. Wrong or impossible sequences are not catched!!!</font>");
            Game.OnGameProcessPacket += Game_OnGameProcessPacket;
            
        }

        private static void Game_OnGameProcessPacket(EventArgs args)
        {
            TimeSpan time = TimeSpan.FromSeconds(Game.ClockTime);

            if (time.TotalSeconds > offset && first)
            {
                first = false;
                Game.PrintChat("[00:00] " + time.TotalSeconds + "</font>");
           
                changeSeq(0);
            }
        }


        private static void Enabled_ValueChanged(object sender, OnValueChangeEventArgs e)
        {
            AutoLevel.Enabled(e.GetNewValue<bool>());         
        }

        private static void Level1_ValueChanged(object sender, OnValueChangeEventArgs e)
        {
            seq[0] = e.GetNewValue<Slider>().Value;
            changeSeq(1);
        }

        private static void Level2_ValueChanged(object sender, OnValueChangeEventArgs e)
        {
            seq[1] = e.GetNewValue<Slider>().Value;
            changeSeq(2);
        }

        private static void Level3_ValueChanged(object sender, OnValueChangeEventArgs e)
        {
            seq[2] = e.GetNewValue<Slider>().Value;
            changeSeq(3);
        }

        private static void Level4_ValueChanged(object sender, OnValueChangeEventArgs e)
        {
            seq[3] = e.GetNewValue<Slider>().Value;
            changeSeq(4);
        }

        private static void Level5_ValueChanged(object sender, OnValueChangeEventArgs e)
        {
            seq[4] = e.GetNewValue<Slider>().Value;
            changeSeq(5);
        }

        private static void Level6_ValueChanged(object sender, OnValueChangeEventArgs e)
        {
            seq[5] = e.GetNewValue<Slider>().Value;
            changeSeq(6);
        }

        private static void Level7_ValueChanged(object sender, OnValueChangeEventArgs e)
        {
            seq[6] = e.GetNewValue<Slider>().Value;
            changeSeq(7);
        }

        private static void Level8_ValueChanged(object sender, OnValueChangeEventArgs e)
        {
            seq[7] = e.GetNewValue<Slider>().Value;
            changeSeq(8);
        }

        private static void Level9_ValueChanged(object sender, OnValueChangeEventArgs e)
        {
            seq[8] = e.GetNewValue<Slider>().Value;
            changeSeq(9);
        }

        private static void Level10_ValueChanged(object sender, OnValueChangeEventArgs e)
        {
            seq[9] = e.GetNewValue<Slider>().Value;
            changeSeq(10);
        }

        private static void Level11_ValueChanged(object sender, OnValueChangeEventArgs e)
        {
            seq[10] = e.GetNewValue<Slider>().Value;
            changeSeq(11);
        }

        private static void Level12_ValueChanged(object sender, OnValueChangeEventArgs e)
        {
            seq[11] = e.GetNewValue<Slider>().Value;
            changeSeq(12);
        }

        private static void Level13_ValueChanged(object sender, OnValueChangeEventArgs e)
        {
            seq[12] = e.GetNewValue<Slider>().Value;
            changeSeq(13);
        }

        private static void Level14_ValueChanged(object sender, OnValueChangeEventArgs e)
        {
            seq[13] = e.GetNewValue<Slider>().Value;
            changeSeq(14);
        }

        private static void Level15_ValueChanged(object sender, OnValueChangeEventArgs e)
        {
            seq[14] = e.GetNewValue<Slider>().Value;
            changeSeq(15);
        }

        private static void Level16_ValueChanged(object sender, OnValueChangeEventArgs e)
        {
            seq[15] = e.GetNewValue<Slider>().Value;
            changeSeq(16);
        }

        private static void Level17_ValueChanged(object sender, OnValueChangeEventArgs e)
        {
            seq[16] = e.GetNewValue<Slider>().Value;
            changeSeq(17);
        }

        private static void Level18_ValueChanged(object sender, OnValueChangeEventArgs e)
        {
            seq[17] = e.GetNewValue<Slider>().Value;
            changeSeq(18);
        }

        private static void changeSeq(int num)
        {
            AutoLevel.Enabled(false);
            var level = new AutoLevel(seq);
            if(!first) AutoLevel.Enabled(Menu.Item(ObjectManager.Player.ChampionName + "Enabled").GetValue<bool>());
            if(num == 0) Game.PrintChat("[00:" + offset + "] <font color='#C80046'>AutoLevelUp Spells sequence loaded and starting now...</font>");            
        }


        private static int[] GetSequence()
        {
            var sequence = new int[18];
            switch (ObjectManager.Player.ChampionName)
            {
                case "Aatrox":
                    sequence = new[] { 1, 2, 3, 2, 2, 4, 2, 3, 2, 3, 4, 3, 3, 1, 1, 4, 1, 1 };
                    break;
                case "Ahri":
                    sequence = new[] { 1, 3, 1, 2, 1, 4, 1, 2, 1, 2, 4, 2, 2, 3, 3, 4, 2, 2 };
                    break;
                case "Akali":
                    sequence = new[] { 1, 2, 1, 3, 1, 4, 1, 3, 1, 3, 4, 3, 3, 2, 2, 4, 2, 2 };
                    break;
                case "Alistar":
                    sequence = new[] { 1, 3, 2, 1, 3, 4, 1, 3, 1, 3, 4, 1, 3, 2, 2, 4, 2, 2 };
                    break;
                case "Amumu":
                    sequence = new[] { 2, 3, 3, 1, 3, 4, 3, 1, 3, 1, 4, 1, 1, 2, 2, 4, 2, 2 };
                    break;
                case "Anivia":
                    sequence = new[] { 1, 3, 1, 3, 3, 4, 3, 2, 3, 2, 4, 1, 1, 1, 2, 4, 2, 2 };
                    break;
                case "Annie":
                    sequence = new[] { 2, 1, 1, 3, 1, 4, 1, 2, 1, 2, 4, 2, 2, 3, 3, 4, 3, 3 };
                    break;
                case "Ashe":
                    sequence = new[] { 2, 3, 2, 1, 2, 4, 2, 1, 2, 1, 4, 1, 1, 3, 3, 4, 3, 3 };
                    break;
                case "Azir":
                    sequence = new[] { 2, 1, 3, 1, 1, 4, 1, 2, 1, 3, 4, 3, 3, 2, 2, 4, 2, 2 };
                    break;
                case "Blitzcrank":
                    sequence = new[] { 1, 3, 2, 3, 2, 4, 3, 2, 3, 2, 4, 3, 2, 1, 1, 4, 1, 1 };
                    break;
                case "Brand":
                    sequence = new[] { 2, 3, 2, 1, 2, 4, 2, 3, 2, 3, 4, 3, 3, 1, 1, 4, 1, 1 };
                    break;
                case "Braum":
                    sequence = new[] { 1, 3, 1, 2, 1, 4, 1, 3, 1, 3, 4, 3, 3, 2, 2, 4, 2, 2 };
                    break;
                case "Caitlyn":
                    sequence = new[] { 2, 1, 1, 3, 1, 4, 1, 3, 1, 3, 4, 3, 3, 2, 2, 4, 2, 2 };
                    break;
                case "Cassiopeia":
                    sequence = new[] { 1, 3, 1, 2, 1, 4, 1, 3, 1, 3, 4, 3, 3, 2, 2, 4, 2, 2 };
                    break;
                case "Chogath":
                    sequence = new[] { 1, 3, 2, 2, 2, 4, 2, 3, 2, 3, 4, 3, 3, 1, 1, 4, 1, 1 };
                    break;
                case "Corki":
                    sequence = new[] { 1, 2, 3, 1, 1, 4, 1, 3, 1, 3, 4, 3, 3, 2, 2, 4, 2, 2 };
                    break;
                case "Darius":
                    sequence = new[] { 1, 3, 1, 2, 1, 4, 1, 2, 1, 2, 4, 2, 3, 2, 3, 4, 3, 3 };
                    break;
                case "Diana":
                    sequence = new[] { 2, 1, 2, 3, 1, 4, 1, 1, 1, 2, 4, 2, 2, 3, 3, 4, 3, 3 };
                    break;
                case "DrMundo":
                    sequence = new[] { 2, 1, 3, 2, 2, 4, 2, 3, 2, 3, 4, 3, 3, 1, 1, 4, 1, 1 };
                    break;
                case "Draven":
                    sequence = new[] { 1, 3, 2, 1, 1, 4, 1, 2, 1, 2, 4, 2, 2, 3, 3, 4, 3, 3 };
                    break;
                case "Elise":
                    sequence = new[] { 1, 3, 1, 2, 1, 4, 1, 2, 1, 2, 4, 2, 2, 3, 3, 4, 3, 3 };
                    break;
                case "Evelynn":
                    sequence = new[] { 1, 3, 1, 2, 1, 4, 1, 3, 1, 3, 4, 3, 3, 2, 2, 4, 2, 2 };
                    break;
                case "Ezreal":
                    sequence = new[] { 1, 3, 2, 1, 1, 4, 1, 3, 1, 3, 4, 3, 3, 2, 2, 4, 2, 2 };
                    break;
                case "FiddleSticks":
                    sequence = new[] { 3, 2, 2, 1, 2, 4, 2, 1, 2, 1, 4, 1, 1, 3, 3, 4, 3, 3 };
                    break;
                case "Fiora":
                    sequence = new[] { 2, 1, 3, 2, 2, 4, 2, 3, 2, 3, 4, 3, 3, 1, 1, 4, 1, 1 };
                    break;
                case "Fizz":
                    sequence = new[] { 3, 1, 2, 1, 2, 4, 1, 1, 1, 2, 4, 2, 2, 3, 3, 4, 3, 3 };
                    break;
                case "Galio":
                    sequence = new[] { 1, 2, 1, 3, 1, 4, 1, 2, 1, 2, 4, 3, 3, 2, 2, 4, 3, 3 };
                    break;
                case "Gangplank":
                    sequence = new[] { 1, 2, 1, 3, 1, 4, 1, 2, 1, 2, 4, 2, 2, 3, 3, 4, 3, 3 };
                    break;
                case "Garen":
                    sequence = new[] { 1, 2, 3, 3, 3, 4, 3, 1, 3, 1, 4, 1, 1, 2, 2, 4, 2, 2 };
                    break;
                case "Gnar":
                    sequence = new[] { 1, 2, 3, 1, 1, 4, 1, 2, 1, 2, 4, 2, 2, 3, 3, 4, 3, 3 };
                    break;
                case "Gragas":
                    sequence = new[] { 1, 3, 2, 1, 1, 4, 1, 2, 1, 2, 4, 2, 3, 2, 3, 4, 3, 3 };
                    break;
                case "Graves":
                    sequence = new[] { 1, 3, 2, 1, 1, 4, 1, 3, 1, 3, 4, 3, 3, 2, 2, 4, 2, 2 };
                    break;
                case "Hecarim":
                    sequence = new[] { 1, 2, 1, 3, 1, 4, 1, 2, 1, 2, 4, 2, 2, 3, 3, 4, 3, 3 };
                    break;
                case "Heimerdinger":
                    sequence = new[] { 1, 2, 2, 1, 1, 4, 3, 2, 2, 2, 4, 1, 1, 3, 3, 4, 1, 1 };
                    break;
                case "Irelia":
                    sequence = new[] { 3, 1, 2, 2, 2, 4, 2, 3, 2, 3, 4, 1, 1, 3, 1, 4, 3, 1 };
                    break;
                case "Janna":
                    sequence = new[] { 3, 1, 3, 2, 3, 4, 3, 2, 3, 2, 1, 2, 2, 1, 1, 1, 4, 4 };
                    break;
                case "JarvanIV":
                    sequence = new[] { 1, 3, 1, 2, 1, 4, 1, 3, 2, 1, 4, 3, 3, 3, 2, 4, 2, 2 };
                    break;
                case "Jax":
                    sequence = new[] { 3, 2, 1, 2, 2, 4, 2, 3, 2, 3, 4, 1, 3, 1, 1, 4, 3, 1 };
                    break;
                case "Jayce":
                    sequence = new[] { 1, 3, 1, 2, 1, 4, 1, 3, 1, 3, 4, 3, 3, 2, 2, 4, 2, 2 };
                    break;
                case "Jinx":
                    sequence = new[] { 1, 3, 2, 1, 1, 4, 1, 2, 1, 2, 4, 2, 2, 3, 3, 4, 3, 3 };
                    break;
                case "Karma":
                    sequence = new[] { 1, 3, 1, 2, 3, 1, 3, 1, 3, 1, 3, 1, 3, 2, 2, 2, 2, 2 };
                    break;
                case "Karthus":
                    sequence = new[] { 1, 3, 2, 1, 1, 4, 1, 1, 3, 3, 4, 3, 3, 2, 2, 4, 2, 2 };
                    break;
                case "Kassadin":
                    sequence = new[] { 1, 2, 1, 3, 1, 4, 1, 3, 1, 3, 4, 3, 3, 2, 2, 4, 2, 2 };
                    break;
                case "Katarina":
                    sequence = new[] { 1, 3, 2, 2, 2, 4, 2, 3, 2, 1, 4, 1, 1, 1, 3, 4, 3, 3 };
                    break;
                case "Kayle":
                    sequence = new[] { 3, 2, 3, 1, 3, 4, 3, 2, 3, 2, 4, 2, 2, 1, 1, 4, 1, 1 };
                    break;
                case "Kennen":
                    sequence = new[] { 1, 3, 2, 2, 2, 4, 2, 1, 2, 1, 4, 1, 1, 3, 3, 4, 3, 3 };
                    break;
                case "Khazix":
                    sequence = new[] { 1, 3, 1, 2, 1, 4, 1, 2, 1, 2, 4, 2, 2, 3, 3, 4, 3, 3 };
                    break;
                case "KogMaw":
                    sequence = new[] { 2, 3, 2, 1, 2, 4, 2, 1, 2, 1, 4, 1, 1, 3, 3, 4, 3, 3 };
                    break;
                case "Leblanc":
                    sequence = new[] { 1, 2, 3, 1, 1, 4, 1, 2, 1, 2, 4, 2, 3, 2, 3, 4, 3, 3 };
                    break;
                case "LeeSin":
                    sequence = new[] { 3, 1, 2, 1, 1, 4, 1, 2, 1, 2, 4, 2, 2, 3, 3, 4, 3, 3 };
                    break;
                case "Leona":
                    sequence = new[] { 1, 3, 2, 2, 2, 4, 2, 3, 2, 3, 4, 3, 3, 1, 1, 4, 1, 1 };
                    break;
                case "Lissandra":
                    sequence = new[] { 1, 3, 1, 2, 1, 4, 1, 2, 1, 2, 4, 2, 2, 3, 3, 4, 3, 3 };
                    break;
                case "Lucian":
                    sequence = new[] { 1, 3, 2, 1, 1, 4, 1, 2, 1, 2, 4, 2, 2, 3, 3, 4, 3, 3 };
                    break;
                case "Lulu":
                    sequence = new[] { 3, 2, 1, 3, 3, 4, 3, 2, 3, 2, 4, 2, 2, 1, 1, 4, 1, 1 };
                    break;
                case "Lux":
                    sequence = new[] { 3, 1, 3, 2, 3, 4, 3, 1, 3, 1, 4, 1, 1, 2, 2, 4, 2, 2 };
                    break;
                case "Malphite":
                    sequence = new[] { 1, 3, 1, 2, 1, 4, 1, 3, 1, 3, 4, 3, 2, 3, 2, 4, 2, 2 };
                    break;
                case "Malzahar":
                    sequence = new[] { 1, 3, 3, 2, 3, 4, 1, 3, 1, 3, 4, 2, 1, 2, 1, 4, 2, 2 };
                    break;
                case "Maokai":
                    sequence = new[] { 3, 1, 2, 3, 3, 4, 3, 2, 3, 2, 4, 2, 2, 1, 1, 4, 1, 1 };
                    break;
                case "MasterYi":
                    sequence = new[] { 3, 1, 3, 1, 3, 4, 3, 1, 3, 1, 4, 1, 2, 2, 2, 4, 2, 2 };
                    break;
                case "MissFortune":
                    sequence = new[] { 1, 3, 1, 2, 1, 4, 1, 2, 1, 2, 4, 2, 2, 3, 3, 4, 3, 3 };
                    break;
                case "MonkeyKing":
                    sequence = new[] { 3, 1, 2, 1, 1, 4, 3, 1, 3, 1, 4, 3, 3, 2, 2, 4, 2, 2 };
                    break;
                case "Mordekaiser":
                    sequence = new[] { 3, 1, 3, 2, 3, 4, 3, 1, 3, 1, 4, 1, 1, 2, 2, 4, 2, 2 };
                    break;
                case "Morgana":
                    sequence = new[] { 1, 2, 2, 3, 2, 4, 2, 1, 2, 1, 4, 1, 1, 3, 3, 4, 3, 3 };
                    break;
                case "Nami":
                    sequence = new[] { 1, 2, 3, 2, 2, 4, 2, 2, 3, 3, 4, 3, 3, 1, 1, 4, 1, 1 };
                    break;
                case "Nasus":
                    sequence = new[] { 1, 2, 1, 3, 1, 4, 1, 2, 1, 2, 4, 2, 3, 2, 3, 4, 3, 3 };
                    break;
                case "Nautilus":
                    sequence = new[] { 2, 3, 2, 1, 2, 4, 2, 3, 2, 3, 4, 3, 3, 1, 1, 4, 1, 1 };
                    break;
                case "Nidalee":
                    sequence = new[] { 2, 3, 1, 3, 1, 4, 3, 2, 3, 1, 4, 3, 1, 1, 2, 4, 2, 2 };
                    break;
                case "Nocturne":
                    sequence = new[] { 1, 2, 1, 3, 1, 4, 1, 3, 1, 3, 4, 3, 3, 2, 2, 4, 2, 2 };
                    break;
                case "Nunu":
                    sequence = new[] { 3, 1, 3, 2, 1, 4, 3, 1, 3, 1, 4, 1, 3, 2, 2, 4, 2, 2 };
                    break;
                case "Olaf":
                    sequence = new[] { 1, 3, 1, 2, 1, 4, 1, 3, 1, 3, 4, 3, 3, 2, 2, 4, 2, 2 };
                    break;
                case "Orianna":
                    sequence = new[] { 1, 3, 2, 1, 1, 4, 1, 2, 1, 2, 4, 2, 2, 3, 3, 4, 3, 3 };
                    break;
                case "Pantheon":
                    sequence = new[] { 1, 2, 3, 1, 1, 4, 1, 3, 1, 3, 4, 3, 2, 3, 2, 4, 2, 2 };
                    break;
                case "Poppy":
                    sequence = new[] { 3, 2, 1, 1, 1, 4, 1, 2, 1, 2, 2, 2, 3, 3, 3, 3, 4, 4 };
                    break;
                case "Quinn":
                    sequence = new[] { 3, 1, 1, 2, 1, 4, 1, 3, 1, 3, 4, 3, 3, 2, 2, 4, 2, 2 };
                    break;
                case "Rammus":
                    sequence = new[] { 1, 2, 3, 3, 3, 4, 3, 2, 3, 2, 4, 2, 2, 1, 1, 4, 1, 1 };
                    break;
                case "Renekton":
                    sequence = new[] { 2, 1, 3, 1, 1, 4, 1, 2, 1, 2, 4, 2, 2, 3, 3, 4, 3, 3 };
                    break;
                case "Rengar":
                    sequence = new[] { 1, 3, 2, 1, 1, 4, 2, 1, 1, 2, 4, 2, 2, 3, 3, 4, 3, 3 };
                    break;
                case "Riven":
                    sequence = new[] { 1, 3, 2, 1, 3, 4, 1, 1, 1, 3, 4, 3, 3, 2, 2, 4, 2, 2 };
                    break;
                case "Rumble":
                    sequence = new[] { 3, 1, 1, 2, 1, 4, 1, 3, 1, 3, 4, 3, 3, 2, 2, 4, 2, 2 };
                    break;
                case "Ryze":
                    sequence = new[] { 1, 2, 1, 3, 1, 4, 1, 2, 1, 2, 4, 2, 2, 3, 3, 4, 3, 3 };
                    break;
                case "Sejuani":
                    sequence = new[] { 2, 1, 3, 3, 2, 4, 3, 2, 3, 3, 4, 2, 1, 2, 1, 4, 1, 1 };
                    break;
                case "Shaco":
                    sequence = new[] { 2, 3, 1, 3, 3, 4, 3, 2, 3, 2, 4, 2, 2, 1, 1, 4, 1, 1 };
                    break;
                case "Shen":
                    sequence = new[] { 1, 2, 3, 1, 1, 4, 1, 2, 1, 2, 4, 2, 2, 3, 3, 4, 3, 3 };
                    break;
                case "Shyvana":
                    sequence = new[] { 2, 1, 2, 3, 2, 4, 2, 3, 2, 3, 4, 3, 1, 3, 1, 4, 1, 1 };
                    break;
                case "Singed":
                    sequence = new[] { 1, 3, 1, 3, 1, 4, 1, 2, 1, 2, 4, 3, 2, 3, 2, 4, 2, 3 };
                    break;
                case "Sion":
                    sequence = new[] { 1, 3, 3, 2, 3, 4, 3, 1, 3, 1, 4, 1, 1, 2, 2, 4, 2, 2 };
                    break;
                case "Sivir":
                    sequence = new[] { 2, 1, 3, 1, 1, 4, 1, 2, 1, 2, 4, 2, 2, 3, 3, 4, 3, 3 };
                    break;
                case "Skarner":
                    sequence = new[] { 1, 2, 1, 2, 1, 4, 1, 2, 1, 2, 4, 2, 3, 3, 3, 4, 3, 3 };
                    break;
                case "Sona":
                    sequence = new[] { 1, 2, 3, 1, 1, 4, 1, 2, 1, 2, 4, 2, 2, 3, 3, 4, 3, 3 };
                    break;
                case "Soraka":
                    sequence = new[] { 1, 2, 3, 1, 1, 4, 1, 2, 1, 3, 4, 2, 3, 2, 3, 4, 2, 3 };
                    break;
                case "Swain":
                    sequence = new[] { 2, 3, 3, 1, 3, 4, 3, 1, 3, 1, 4, 1, 1, 2, 2, 4, 2, 2 };
                    break;
                case "Syndra":
                    sequence = new[] { 1, 3, 1, 2, 1, 4, 1, 3, 1, 3, 4, 3, 3, 2, 2, 4, 2, 2 };
                    break;
                case "Talon":
                    sequence = new[] { 2, 3, 1, 2, 2, 4, 2, 1, 2, 1, 4, 1, 1, 3, 3, 4, 3, 3 };
                    break;
                case "Taric":
                    sequence = new[] { 3, 2, 1, 2, 2, 4, 1, 2, 2, 1, 4, 1, 1, 3, 3, 4, 3, 3 };
                    break;
                case "Teemo":
                    sequence = new[] { 1, 3, 2, 3, 1, 4, 3, 3, 3, 1, 4, 2, 2, 1, 2, 4, 2, 1 };
                    break;
                case "Thresh":
                    sequence = new[] { 1, 3, 2, 2, 2, 4, 2, 3, 2, 3, 4, 3, 3, 1, 1, 4, 1, 1 };
                    break;
                case "Tristana":
                    sequence = new[] { 3, 2, 2, 3, 2, 4, 2, 1, 2, 1, 4, 1, 1, 1, 3, 4, 3, 3 };
                    break;
                case "Trundle":
                    sequence = new[] { 1, 2, 1, 3, 1, 4, 1, 2, 1, 3, 4, 2, 3, 2, 3, 4, 2, 3 };
                    break;
                case "Tryndamere":
                    sequence = new[] { 3, 1, 2, 1, 1, 4, 1, 2, 1, 2, 4, 2, 2, 3, 3, 4, 3, 3 };
                    break;
                case "TwistedFate":
                    sequence = new[] { 2, 1, 1, 3, 1, 4, 1, 2, 1, 2, 4, 2, 2, 3, 3, 4, 3, 3 };
                    break;
                case "Twitch":
                    sequence = new[] { 3, 2, 1, 3, 3, 4, 3, 1, 3, 1, 4, 1, 1, 2, 2, 4, 2, 2 };
                    break;
                case "Udyr":
                    sequence = new[] { 4, 2, 3, 4, 4, 2, 4, 2, 4, 2, 2, 1, 3, 3, 3, 3, 1, 1 };
                    break;
                case "Urgot":
                    sequence = new[] { 3, 1, 1, 2, 1, 4, 1, 2, 1, 3, 4, 2, 3, 2, 3, 4, 2, 3 };
                    break;
                case "Varus":
                    sequence = new[] { 1, 2, 3, 1, 1, 4, 1, 2, 1, 2, 4, 2, 2, 3, 3, 4, 3, 3 };
                    break;
                case "Vayne":
                    sequence = new[] { 1, 3, 2, 1, 1, 4, 1, 2, 1, 2, 4, 2, 2, 3, 3, 4, 3, 3 };
                    break;
                case "Veigar":
                    sequence = new[] { 1, 3, 1, 2, 1, 4, 2, 2, 2, 2, 4, 3, 1, 1, 3, 4, 3, 3 };
                    break;
                case "VelKoz":
                    sequence = new[] { 1, 2, 3, 2, 2, 4, 2, 1, 2, 1, 4, 1, 1, 3, 3, 4, 3, 3 };
                    break;
                case "Vi":
                    sequence = new[] { 2, 3, 1, 1, 1, 4, 1, 2, 1, 1, 4, 2, 2, 3, 3, 4, 3, 3 };
                    break;
                case "Viktor":
                    sequence = new[] { 3, 2, 3, 1, 3, 4, 3, 1, 3, 1, 4, 1, 2, 1, 2, 4, 2, 2 };
                    break;
                case "Vladimir":
                    sequence = new[] { 1, 2, 1, 3, 1, 4, 1, 3, 1, 3, 4, 3, 3, 2, 2, 4, 2, 2 };
                    break;
                case "Volibear":
                    sequence = new[] { 2, 3, 2, 1, 2, 4, 3, 2, 1, 2, 4, 3, 1, 3, 1, 4, 3, 1 };
                    break;
                case "Warwick":
                    sequence = new[] { 2, 1, 1, 2, 1, 4, 1, 3, 1, 3, 4, 3, 3, 3, 2, 4, 2, 2 };
                    break;
                case "Xerath":
                    sequence = new[] { 1, 3, 1, 2, 1, 4, 1, 2, 1, 2, 4, 2, 2, 3, 3, 4, 3, 3 };
                    break;
                case "XinZhao":
                    sequence = new[] { 1, 3, 1, 2, 1, 4, 1, 2, 1, 2, 4, 2, 2, 3, 3, 4, 3, 3 };
                    break;
                case "Yasuo":
                    sequence = new[] { 1, 3, 1, 2, 1, 4, 1, 3, 1, 3, 4, 3, 3, 2, 2, 4, 2, 2 };
                    break;
                case "Yorick":
                    sequence = new[] { 2, 3, 1, 3, 3, 4, 3, 2, 3, 1, 4, 2, 1, 2, 1, 4, 2, 1 };
                    break;
                case "Zac":
                    sequence = new[] { 1, 2, 3, 1, 1, 4, 1, 3, 1, 3, 4, 3, 3, 2, 2, 4, 2, 2 };
                    break;
                case "Zed":
                    sequence = new[] { 1, 2, 3, 1, 1, 4, 1, 3, 1, 3, 4, 3, 3, 2, 2, 4, 2, 2 };
                    break;
                case "Ziggs":
                    sequence = new[] { 1, 2, 3, 1, 1, 4, 1, 3, 1, 3, 4, 3, 3, 2, 2, 4, 2, 2 };
                    break;
                case "Zilean":
                    sequence = new[] { 1, 2, 1, 3, 1, 4, 1, 2, 1, 2, 4, 2, 2, 3, 3, 4, 3, 3 };
                    break;
                case "Zyra":
                    sequence = new[] { 3, 2, 1, 1, 1, 4, 1, 3, 1, 3, 4, 3, 3, 2, 2, 4, 2, 2 };
                    break;
            }
            return sequence;
        }
    }
}
