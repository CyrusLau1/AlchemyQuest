// Alchemy Quest by Cyrus Lau
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;
using System.Text.RegularExpressions;
using System.Security;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using System.Runtime.CompilerServices;

namespace AlchemyQuest
{
    public class Player
    {   
        // Player default stats
        public string? name;
        public decimal damage = 1; // Player attack
        public decimal health = 50; // Player health
        public decimal hitrate = 67; // Player hitrate in %, i.e. defualt is 67%)
        public int damageMulti = 1; // Damage multi used later (x2 from a potion)
        public decimal damageResist = 1; // Damage resist used later (10%/0.9x from quest)
        public int luck = 0; // Luck used in harvesting (+2 from a potion)
        public int lives = 3; // Player lives, 0 --> game over
        
        public static void Stats()
        {
            Console.Clear();
            // Display player stats (health, damage, hitrate)
            Program.Print($"{Program.currentPlayer.name}'s Stats");
            Console.WriteLine("=================================");
            Console.WriteLine($"Damage: {Program.currentPlayer.damage * Program.currentPlayer.damageMulti}");
            Console.WriteLine($"Health: {Program.currentPlayer.health}");
            Console.WriteLine($"Hit Rate: {Program.currentPlayer.hitrate}%");
            Console.WriteLine("=================================");
            Console.Write("Press Enter to return...");
            Program.ReadKey();
            Console.Clear();
            AlchemyLab.Lab();
        }
        

    } // end class Player
    public class Program
    {
        public static readonly Player currentPlayer = new Player();
        static void Main(string[] args)
        {
            // Start the game
            Start();
        }

        public static void Print(string text, int speed = 2, bool allowSkip = true)
        {
            // Text "animation"
            foreach (char c in text)
            {
                Console.Write(c); // Outputs each character individually
                System.Threading.Thread.Sleep(speed); // Time between each character

                // Check if space bar is pressed and allowSkip is enabled
                if (allowSkip && Console.KeyAvailable && Console.ReadKey(true).Key == ConsoleKey.Spacebar)
                {
                    // Immediately displays text
                    speed = 0;
                }
            }
            Console.WriteLine();
        }

        public static void ReadKey()
        {
            // Prevents users from spamming keys before a Console.ReadKey()
            while(Console.KeyAvailable) 
            {
                Console.ReadKey(true);
            }
            Console.ReadKey(true);
        }

        public static void Start()
        {   
            // Welcome player and ask for name
            Print("- Welcome to Alchemy Quest! -", 50, false);
            Console.Write("Enter your name: ");
            currentPlayer.name = Console.ReadLine()?.Trim();

            string namePattern = @"^[a-zA-Z0-9_ ]+$";

            if (string.IsNullOrWhiteSpace(currentPlayer.name))
            {
                // Sets default name if player doesn't enter a name
                currentPlayer.name = "Cirala"; // Alaric spelled backwards
                Print($"[Mysterious Voice]: In the magical land of Eldralore, you are a keen apprentice to the wise alchemist, Master Alaric. Master named you {currentPlayer.name} when he found you in the forest ten years ago.");
            }
            else if (currentPlayer.name.Length > 20)
            {
                // Error if player name is too long, repeats
                Print("[!] Your name is too long. Please enter a valid name. Max length: 20 characters. \n...");
                Console.WriteLine("\n[Press Enter whenever \"...\" is displayed to continue.]");
                Program.ReadKey(); // Wait until player presses Enter (or any other key)
                Console.Clear(); // Clears current console
                Start();
            }
            else if (!Regex.IsMatch(currentPlayer.name, namePattern))
            {
                Console.WriteLine("[!] Your name can only contain alphanumeric characters (letters of the English alphabet & numbers), spaces, and underscores. Please enter a valid name.\n...");
                Console.WriteLine("\n[Press Enter (or any key) whenever \"...\" is displayed to continue.]");
                Program.ReadKey(); 
                Console.Clear(); 
                Start();
            }
            else
            {
                Print($"[Mysterious Voice]: In the magical land of Eldralore, you, {currentPlayer.name}, are a keen apprentice to the wise alchemist, Master Alaric.\n...");
            }
            // Generate game lore/prologue
            Console.WriteLine("[Press Enter (or any key) whenever \"...\" is displayed to continue.]");
            Program.ReadKey();
            Console.Clear();
            Print("[Mysterious Voice]: *** Everything that you hear from me, the Mysterious Voice, includes important information that will help you progress through your journey. \n...", 10, false);
            Program.ReadKey();
            Print("[Mysterious Voice]: You can skip most of the text in the game by pressing the SPACEBAR while the text is being generated.\n...", 10, false);
            Program.ReadKey();
            Print("[Mysterious Voice]: It is STRONGLY advised NOT to skip ANYTHING if it is your first time playing, or you might miss out on important information.\n...", 10, false);
            Program.ReadKey();
            Print("[DISCLAIMER!]: There is NO saving system as of now. The game will likely take more than 30 minutes to finish if this is your first time playing.\n...", 10, false);
            Program.ReadKey();
            Console.Clear();

            Print("Harvesting...", 50, false);
            Console.Clear();
            Print("Harvesting...", 50, false);
            Console.Clear();
            Print("Harvesting...", 50, false);
            Console.Clear();
            Print("In the heart of the Mystic Forest, you are gathering ingredients for potion brewing, surrounded by the gentle hum of magic. But the tranquility is shattered by a thunderous blast, ripping through the serenity. \n...");
            Program.ReadKey();
            Print("Hurrying back to the Alchemy Lab, your heart sinks at the sight of devastation. The lab, once a haven of knowledge, now lies in ruins, a victim of dark forces. With trembling hands, you conjure a spell to glimpse into the past. \n...");
            Program.ReadKey();
            Print("The mist of magic reveals a mysterious cloaked figure, their presence ominous as they capture Master Alaric and vanish into the night. Determination courses through your mind as you vow to repair the lab and rescue master from the clutches of darkness. \n...");
            Program.ReadKey();
            Print("This is just the beginning of your journey. With resolve in your heart, you set forth to uncover the truth and restore peace to Eldralore. \n...");
            Program.ReadKey();
            Console.Clear();
            // Repair the lab
            RepairLab();
            Print("[Mysterious Voice]: Get ready to explore the realm of Eldralore, gather ingredients from different magical locations, and brew potions to strengthen yourself in the hope of defeating enemies to save Master Alaric and keeping Eldralore safe. Let the adventure begin! \n...");
            Program.ReadKey();
            Console.Clear();
            // Start at Lab
            AlchemyLab.Lab();
        
        }

        public static void RepairLab()
        {
            // Player must repair the lab to continue
            Print("As you survey the wreckage of the lab, your eyes catch sight of a piece of scrap paper lying amidst the debris.\nPicking it up, you find a jumble of letters hastily scribbled upon it: \"L C H Y E A M.\" \n...");
            Program.ReadKey();
            Print("You discover that unscrambling these letters may hold the key to repairing the lab.");
            Print("[Mysterious Voice]: Unscramble the word and type it here: ", 2, false);
            string? unscrambledWord = Console.ReadLine()?.ToLower().Trim();
            if (unscrambledWord == "alchemy")
            {
                Console.Clear();
                Print("The pieces of the lab start to shimmer and fuse together as you chant the word.\n[Mysterious Voice]: Congratulations! You've repaired the lab. \n...");
                Program.ReadKey();
                Console.Clear();
                // Continue the game
            }
            else
            {
                Console.Clear();
                Print("[!] The word doesn't seem to match. Try again. [Tip: It is a word related to the game, starting with A and ending with Y. Use an online unscrambler if needed.] \n...");
                Program.ReadKey();
                Console.Clear();
                RepairLab(); // Repeats
            }
        }

    } // end class Program

    public class AlchemyLab
    {
        private static bool labGuideShown = false;
        // Location = Alchemy Lab
        public static void Lab()
        {
            if (!labGuideShown) // Show lab guide if first time visiting lab
            {
                Program.Print("[Mysterious Voice]: You have now entered the Alchemy Lab, the starting point of your journey.\n...");
                Program.ReadKey();
                Program.Print("[Mysterious Voice]: In the center of the lab is the Cauldron, where you can brew many different kinds of potions with mystical ingredients.\n...");
                Program.ReadKey();
                Program.Print("[Mysterious Voice]: On the left is the staircase leading down to the basement. Search the basement for bottles, which are essential for every potion. You might even discover a rare grimoire here!\n...");
                Program.ReadKey();
                Program.Print("[Mysterious Voice]: On the right is a meticulously crafted magic circle, on which you can check your stats.\n...");
                Program.ReadKey();
                Program.Print("[Mysterious Voice]: Most of the time, you will be outside the lab, harvesting ingredients, completing quests and defeating enemies.\n...");
                Program.ReadKey();
                Program.Print("[Mysterious Voice]: To start off, it is highly recommended that you go down to the basement and talk to the Silky Spider Queen, before doing anything else.\n...");
                Program.ReadKey();
                Program.Print("[Mysterious Voice]: After receiving her reward, you will be able to gain a deeper understanding of the world of Eldralore.\n...");
                Program.ReadKey();
                Program.Print("[Mysterious Voice]: Your ultimate goal is to strengthen yourself enough to defeat the final boss and rescue Master Alaric.\n...");
                Program.ReadKey();
                Program.Print("[Mysterious Voice]: [SPOILER!]: To get the Good Ending, simply brew the Holy Elixir of Restoration and defeat the Astral Enigma. To get the Bad Ending, defeat the Astral Enigma without brewing/owning the elixir. To get the Secret Ending, complete all the quests and defeat the secret boss, True Astral Enigma.\n...");
                Program.ReadKey();
                Program.Print("[Mysterious Voice]: And that's it! Rest assured, my voice will occassionally appear in your mind to provide you with guidance!\n...");
                Program.ReadKey();
                Console.Clear();
                labGuideShown = true;
            }
            
            while (true)
            {
                // Display Lab options
                Program.Print("You are at the Alchemy Lab. What will you do?:");
                Console.WriteLine("=================================");
                Console.WriteLine("Options:");
                Console.WriteLine("[1] Brew Potions");
                Console.WriteLine("[2] Enter Basement");
                Console.WriteLine("[3] Check Ingredient Pouch");
                Console.WriteLine("[4] Check Brewed Potions");
                Console.WriteLine("[5] Check Player Stats");
                Console.WriteLine("[6] Exit Lab & Explore");
                Console.WriteLine("=================================");
                Console.Write("Your choice: "); // Asks player for choice
                string? labChoice = Console.ReadLine()?.Trim();

                if (labChoice == "1")
                {
                    // Player chooses to brew potions
                    Console.Clear();
                    Program.Print("You walk to the cauldron to brew potions. \n...");
                    Program.ReadKey();
                    Console.Clear();
                    BrewPotions.Cauldron();
                    
                
                    }
                
                else if (labChoice =="2")
                {
                    // Player chooses to enter Basement
                    Console.Clear();
                    Program.Print("You enter the mysterious Basement. \n...");
                    Program.ReadKey();
                    Console.Clear();
                    Locations.Basement();
                    
                }

                else if (labChoice == "3")
                {   
                    // Player chooses to check inventory
                    Console.Clear();
                    Program.Print("You look inside your magical ingredient pouch. \n...");
                    Program.ReadKey();
                    PlayerInventory.DisplayInventory();
                    Lab();
                    
                }

                else if (labChoice == "4")
                {
                    // Player chooses to check brewed potions
                    Console.Clear();
                    Program.Print("You try to remember which potions you have brewed. \n...");
                    Program.ReadKey();
                    BrewPotions.BrewedPotions();
                   
                }

                else if (labChoice == "5")
                {
                    // Player chooses to check stats
                    Console.Clear();
                    Program.Print("You stand on a magic circle to check your stats. \n...");
                    Program.ReadKey();
                    Player.Stats();
                    
                }

                else if (labChoice == "6")
                {
                    // Player chooses to exit Lab
                    Console.Clear();
                    Program.Print("As you exit the Lab, you slowly approach the Crossroads. \n...");
                    Program.ReadKey();
                    Console.Clear();
                    Locations.Crossroads();
                }

                else{
                    // Invalid choice
                    Program.Print("Error!", 25);
                    Console.WriteLine("[!] Please enter an integer between 1 and 6. \n...");
                    Program.ReadKey();
                    Console.Clear();
                }
            }
        }
    } // end class AlchemyLab

    public class MastersDiary
    { // Master Alaric's diary  
        private static bool diaryGuideShown = false;
        private static readonly Dictionary<string, string> recipes = new Dictionary<string, string>()
        { // Dictionary of all potion descriptions and recipes
            {
                "Noctis Vision Brew",
                "- Provides night vision\n- Increases hit rate by 5%\n- Required for Lunar Caverns\n- Recipe:\n  - 1 Bottle (Basement)\n  - 4 Firefly Essence (Forest)\n  - 4 Starlight Dew (Meadow)"
            },
            {
                "Aqua Breather Potion",
                "- Provides underwater breathing\n- Increases health by 25\n- Required for Sapphire Serenity Lake\n- Recipe:\n  - 1 Bottle (Basement)\n  - 4 Azure Hydrangea Petal (Forest)\n  - 4 Enchanted Nectar (Meadow)"
            },
            {
                "Naturae Blessing Brew",
                "- Receive a blessing from Mother Nature\n- Increases base damage by 2 and health by 50\n- Recipe:\n  - 1 Bottle (Basement)\n  - 3 Mystic Roots (Forest)\n  - 6 Azure Hydrangea Petal (Forest)\n  - 5 Enchanted Nectar (Meadow)"
            },
            {
                "Dark Pyroblast Infusion",
                "- Increases affinity with fire, grants ability to throw stronger fireballs\n- Increases hit rate by 10%\n- Deal 2x damage to all enemies\n- Recipe:\n  - 1 Bottle (Basement)\n  - 8 Firefly Essence (Forest)\n  - 2 Mystic Roots (Forest)\n  - 6 Starlight Dew (Meadow)"
            },
            {
                "Arcane Spirit Elixir",
                "- Summons a strong companion which aids in battle and helps decipher the hidden messages at the ruins' entrance, grants the ability to traverse the ruins\n- Helps search for rare ingredients and grimoires\n- Increases base damage by 1, health by 30 and hit rate by 5%\n- Luck for all grimoires and rare ingredients (5% or below) +2%. (Failure chance/Most common ingredient -2%)\n- Required for Arcane Ruins\n- Recipe:\n  - 1 Bottle (Basement)\n  - 2 Mystic Roots (Forest)\n  - 2 Nebula Dust (Meadow)\n  - 2 Abyssal Serpent Scale (Lake)\n  - 2 Glowing Crystal (Caverns)"
            },
            {
                "Levitas Elixir",
                "- Grants levitation\n- Increases base damage by 3\n- Required for Ethereal Summit\n- Recipe:  \n- 1 Bottle (Basement)\n  - 2 Icy Shadowstone Shards (Ruins)\n  - 2 Holy Abyssal Serpent's Fangs (Lake Enemy)\n  - 10 Firefly Essence (Forest)\n  - 3 Lunar Moss (Caverns)"
            },
            {
                "Frost Resistance Potion",
                "- Provides frost resistance\n- Increases health by 30\n- Required for Ethereal Summit\n- Recipe:  \n- 1 Bottle (Basement)\n  - 4 Ancient Glyphroot Powder (Ruins)\n  - 2 Moonlight Earthpulse Golem Core (Caverns Enemy)\n  - 7 Starlight Dew (Meadow)\n  - 3 Seashell Essence (Lake)"
            },
            {
                "Holy Elixir of Restoration",
                "- Dispels curses upon consumption\n- Brewer of the elixir (player) receives a holy blessing, increasing their power\n- Increases base damage by 1 and health by 15\n- [Good ending if brewed; bad ending if not brewed]\n- Recipe:\n  - 1 Bottle (Basement)\n  - 1 Arcane Relic Fragments (Ruins)\n  - 1 Earthpulse Stonefruit (Caverns)\n  - 1 Holy Water (Lake)\n  - 3 Mystic Roots (Forest)\n  - 4 Nebula Dust (Meadow)\n  - 1 Holy Roasted Chicken (Forest Quest) [NOT Cursed Roasted Chicken]"
            },
            {
                "???",
                "- ? ? ?\n"
            }
        };

        private static readonly Dictionary<string, string> locations = new Dictionary<string, string>()
        { // Dictionary of all locations with description and ingredients
            {
                "Crossroads",
                "- Desceiption: A convergence of paths where travelers must make crucial decisions.\n- Ingredients:\n  - None"
            },
            {
                "Mystic Forest",
                "- Description: A dense woodland shrouded in mystery and magic.\n- Ingredients:\n  - Mystic Roots (10%)\n  - Firefly Essence (40%)\n  - Azure Hydrangea Petal (30%)"
            },
            {
                "Lunar Meadow",
                "- Description: An open expanse bathed in the gentle glow of starlight.\n- Ingredients:\n  - Nebula Dust (15%)\n  - Starlight Dew (25%)\n  - Enchanted Nectar (25%)"
            },
            {
                "Sapphire Serenity Lake",
                "- Description: A serene lake reflecting the beauty of the surrounding landscape.\n- Ingredients:\n  - Seashell Essence (30%)\n  - Abyssal Serpent Scale (20%)\n  - Holy Water (5%)"
            },
            {
                "Lunar Caverns",
                "- Description: A network of caves illuminated by the soft glow of lunar crystals.\n- Ingredients:\n  - Glowing Crystal (25%)\n  - Lunar Moss (35%)\n  - Earthpulse Stonefruit (5%)\n  - Nothing (35%)"
            },
            {
                "Arcane Ruins",
                "- Description: Crumbling remnants of an ancient civilization, infused with magical energy.\n - Ingredients:\n  - Ancient Glyphroot Powder (30%)\n  - Icy Shadowstone Shards (25%)\n  - Arcane Relic Fragments (5%)\n  - Nothing (40%)"
            },
            {
                "Ethereal Summit",
                "- Description: The highest peak in the realm, touching the heavens themselves. [COLD & DANGEROUS]\n- Ingredients:\n  - None"
            },
            {
                "Basement",
                "- Description: My basement.\n- Ingredients:\n  - Bottle (36%)\n  - Silky Spiderwebs (57%)\n  - Golden Silky Spiderweb (5%)\n  - Ancient Grimoire of Fury (2%)\n"
            }
        };

        private static readonly Dictionary<string, string> enemies = new Dictionary<string, string>()
        { // Dictionary of all enemies with stats and drops
            {
                "Crazy Cursed Chicken (Crossroads)",
                "- Damage: 1\n- Hit rate: 66%\n- Health: 5\n- Drops:\n  - Cursed Roasted Chicken (100%) [Master Alaric's favourite food, but it's cursed.]"
            },
            {
                "Holy Abyssal Serpent (Sapphire Serenity Lake))",
                "- Damage: 14\n- Hit rate: 66%\n- Health: 40\n- Drops:\n  - Holy Abyssal Serpent's Fangs (80%)\n  - Poseidon's Grimoire of Fury (20%) [Increases attack by 0.3, health by 4 and hit rate by 0.5% for every grimoire found (stacks)]"
            },
            {
                "Moonlight Earthpulse Golem (Lunar Caverns)",
                "- Damage: 12\n- Hit rate: 35%\n- Health: 88\n- Drops:\n  - Moonlight Earthpulse Golem Core (80%)\n  - Gaia's Grimoire of Fury (20%) [Increases attack by 0.2, health by 6, and hit rate by 0.5% for every grimoire found (stacks)]"
            },
            {
                "Astr-?`+@&%~#",
                "- DMG 12| HR 72%/ HP 200 ! H E L P-"
            },
            {
                "???",
                "- ? ? ?\n"
            }
        };

        public static void DisplayDiary() // Displays Master's diary
        {
            if (!diaryGuideShown) // If first time reading diary, show diary guide
            {
                Program.Print("[Mysterious Voice]: You have now unlocked Master Alaric's Diary, a valuable tome containing Master Alaric's knowledge, recipes, and insights gathered over a lifetime of alchemical research..\n...");
                Program.ReadKey();
                Program.Print("[Mysterious Voice]: Navigate through the pages of the diary to discover recipes for powerful potions, information about rare ingredients, details about locations, and enemy stats!\n...");
                Program.ReadKey();
                Console.Clear();
                diaryGuideShown = true;
            }
            Console.WriteLine("Master Alaric's Diary");
            Console.WriteLine("=======================");
            Console.WriteLine("Choose a section to read:");
            Console.WriteLine("[1] Recipes");
            Console.WriteLine("[2] Locations");
            Console.WriteLine("[3] Enemies");
            Console.WriteLine("[4] Stop Reading");
            Console.WriteLine("=======================");
            Console.Write("Your choice: ");
            string? sectionChoice = Console.ReadLine()?.Trim();
            while (true)
            {
                if (sectionChoice == "1") // Player chooses to read recipes
                {
                    Console.Clear();
                    Console.WriteLine("\nRecipes:");
                    Console.WriteLine("============================");
                    ShowSection(recipes);
                    Console.WriteLine("-----");
                    Console.WriteLine("Press Enter to return to Table of Contents.");
                    Program.ReadKey();
                    Console.Clear();
                    DisplayDiary();
                }
                else if (sectionChoice == "2") // Player chooses to read about locations
                {
                    Console.Clear();
                    Console.WriteLine("\nLocations:");
                    Console.WriteLine("============================");
                    ShowSection(locations);
                    Console.WriteLine("-----");
                    Console.WriteLine("Press Enter to return to Table of Contents.");
                    Program.ReadKey();
                    Console.Clear();
                    DisplayDiary();
                }
                else if (sectionChoice == "3") // Player chooses to read about enemies
                {
                    Console.Clear();
                    Console.WriteLine("\nEnemies:");
                    Console.WriteLine("============================");
                    ShowSection(enemies);
                    Console.WriteLine("-----");
                    Console.WriteLine("Press Enter to return to Table of Contents.");
                    Program.ReadKey();
                    Console.Clear();
                    DisplayDiary();
                }
                else if (sectionChoice == "4") // Player stops reading
                {
                    Console.Clear();
                    Locations.Basement();
                }
                else // Invalid choice
                {
                    Program.Print("Error!", 25);
                    Console.WriteLine("[!] Please enter an integer between 1 and 4.\n...");
                    Program.ReadKey();
                    Console.Clear();
                    DisplayDiary();
                }
            }
        }

        public static void ShowSection(Dictionary<string, string> section)
        {
            foreach (var entry in section) // Display content in dictionaries in each diary section
            {
                Console.WriteLine($"{entry.Key}:");
                Console.WriteLine(entry.Value);
                Console.WriteLine();
            }
        }
    } // end class MastersDiary

    public class BrewPotions
    {
        private static bool brewingGuideShown = false;
        // Potion brewing
        // List of potions in cauldron
        public static List<string> cauldron = new List<string> {"Noctis Vision Brew", "Aqua Breather Potion", "Naturae Blessing Brew", "Dark Pyroblast Infusion", "Arcane Spirit Elixir", "Levitas Elixir", "Frost Resistance Potion", "Holy Elixir of Restoration", "Holy Roasted Chicken", "Malevolent Elixir of Calamity"};
        // List of potions brewed
        public static List<string> brewedpotions = new List<string> {};

        private static bool CanBrewPotion(Dictionary<string, int> requiredIngredients)
        {
            foreach (var ingredient in requiredIngredients)
            {
                // If inventory does not contain all required ingredients, return false
                string ingredientName = ingredient.Key;
                int requiredQuantity = ingredient.Value;
                if (!PlayerInventory.inventory.ContainsKey(ingredientName) || PlayerInventory.inventory[ingredientName] < requiredQuantity)
                {
                    return false;
                }
            }
            // Otherwise, if ivnentory does contain all required ingredients, return true
            return true;
        }

        private static Dictionary<string, int> noctisVisionIngredients = new Dictionary<string, int>
        {
            // Dictionary of Noctis Vision Brew required ingredients
            { "Bottle", 1 },
            { "Firefly Essence", 4 },
            { "Starlight Dew", 4 }
        };

        private static Dictionary<string, int> aquaBreatherIngredients = new Dictionary<string, int>
        {
            // Dictionary of Aqua Breather Potion required ingredients
            { "Bottle", 1 },
            { "Azure Hydrangea Petal", 4 },
            { "Enchanted Nectar", 4 }
        };

        private static Dictionary<string, int> naturaeBlessingIngredients = new Dictionary<string, int>
        {
            // Dictionary of Naturae Blessing Brew required ingredients
            { "Bottle", 1 },
            { "Mytic Roots", 3 },
            { "Hydrangea Petal", 6 },
            { "Enchanted Nectar", 5 }
        };

        private static Dictionary<string, int> darkPyroblastIngredients = new Dictionary<string, int>
        {
            // Dictionary of Dark Pyroblast Infusion required ingredients
            { "Bottle", 1 },
            { "Firefly Essence", 8 },
            { "Mystic Roots", 2 },
            { "Starlight Dew", 6 }
        };

        private static Dictionary<string, int> arcaneSpiritIngredients = new Dictionary<string, int>
        {
            // Dictionary of Arcane Spirit Elixir required ingredients
            { "Bottle", 1 },
            { "Mytic Roots", 2 },
            { "Nebula Dust", 2 },
            { "Abyssal Serpent Scale", 2 },
            { "Glowing Crystal", 2 }
        };

        private static Dictionary<string, int> levitasIngredients = new Dictionary<string, int>
        {
            // Dictionary of Levitas Elixir required ingredients
            { "Bottle", 1 },
            { "Ancient Glyphroot Powder", 4 },
            { "Firefly Essence", 10 },
            { "Moonlight Earthpulse Golem Core", 2 },
            { "Lunar Moss", 3 }
        };

        private static Dictionary<string, int> frostResistanceIngredients = new Dictionary<string, int>
        {
            // Dictionary of Frost Resistance Potion required ingredients
            { "Bottle", 1 },
            { "Icy Shadowstone Shards", 2 },
            { "Starlight Dew", 7 },
            { "Holy Abyssal Serpent's Fangs", 2 },
            { "Seashell Essence", 3 }
        };

        private static Dictionary<string, int> holyRoastedChicken = new Dictionary<string, int>
        {
            // Dictionary of Holy Roasted Chicken ingredients
            { "Cursed Roasted Chicken", 7 },
        };
        private static Dictionary<string, int> holyElixirIngredients = new Dictionary<string, int>
        {
            // Dictionary of Holy Elixir of Restoration required ingredients
            { "Bottle", 1 },
            { "Arcane Relic Fragments", 1 },
            { "Earthpulse Stonefruit" , 1 },
            { "Holy Water", 1 },
            { "Mystic Roots", 3 },
            { "Nebula Dust", 4 },
            { "Holy Roasted Chicken", 1}
        };

        private static Dictionary<string, int> malevolentElixirIngredients = new Dictionary<string, int>
        {
            // Dictionary of Malevolent Elixir of Calamity required ingredients
            { "Bottle", 1 },
            { "Malevolent Doombringer Crystal", 1 },
            { "Ancient Grimoire of Fury" , 1 },
            { "Poseidon's Grimoire of Fury", 1 },
            { "Gaia's Grimoire of Fury", 1 },
        };

        public static void Cauldron()
        {
            if (!brewingGuideShown) // If first time visiting cauldron, show brewing guide
            {
                Program.Print("[Mysterious Voice]: This is the Cauldron, where you will brew powerful potions with various effects to aid you on their journey..\n...");
                Program.ReadKey();
                Program.Print("[Mysterious Voice]: Gather the required ingredients for the potion you wish to brew. Most ingredients can be found by harvesting in different locations and few will be dropped from enemies.\n...");
                Program.ReadKey();
                Program.Print("[Mysterious Voice]: Recipes for potions are shown in Master Alaric's Diary at the basement, so complete Silky Spider Queen's quest if you still haven't done so!\n...");
                Program.ReadKey();
                Program.Print("[Mysterious Voice]: Make sure to type the number of the potion to brew it, not the name. You can only brew each potion once, and you can check which ones you have already brewed at the Lab.\n...");
                Program.ReadKey();
                Program.Print("[Mysterious Voice]: All potions strengthen your character by providing you with stat increases, and some potions are required to further access locked locations. \n...");
                Program.ReadKey();
                Program.Print("[Mysterious Voice]: And that's it! Happy potion brewing! \n...");
                Program.ReadKey();
                Console.Clear();
                brewingGuideShown = true;
            }
            while (true)
            {
                // Display potions available for brewing
                Program.Print("The Cauldron");
                Console.WriteLine("=================================");
                Console.WriteLine("Choose a potion to brew:");
                Console.WriteLine($"[1] {cauldron[0]}");
                Console.WriteLine($"[2] {cauldron[1]}");
                Console.WriteLine($"[3] {cauldron[2]}");
                Console.WriteLine($"[4] {cauldron[3]}");
                Console.WriteLine($"[5] {cauldron[4]}");
                Console.WriteLine($"[6] {cauldron[5]}");
                Console.WriteLine($"[7] {cauldron[6]}");
                Console.WriteLine($"[8] {cauldron[7]}");
                Console.WriteLine($"[9] Return");
                Console.WriteLine("=================================");
                Console.Write("Your choice: ");
                string? brewChoice = Console.ReadLine()?.ToLower().Trim();

                if (brewChoice == "1" && !CanBrewPotion(noctisVisionIngredients) || brewChoice == "2" && !CanBrewPotion(aquaBreatherIngredients) || brewChoice == "3" && !CanBrewPotion(naturaeBlessingIngredients) || brewChoice == "4" && !CanBrewPotion(darkPyroblastIngredients) || brewChoice == "5" && !CanBrewPotion(arcaneSpiritIngredients) || brewChoice == "6" && !CanBrewPotion(levitasIngredients) || brewChoice == "7" && !CanBrewPotion(frostResistanceIngredients) || brewChoice == "8" && !CanBrewPotion(holyElixirIngredients) || Quests.HolyChickenQuestCompleted() && brewChoice == "holychicken" && !CanBrewPotion(holyRoastedChicken) || brewChoice == "secret" && !CanBrewPotion(malevolentElixirIngredients)) // ADD || OTHER POTIONS
                {
                    // Not enough ingredients
                    Console.Clear();
                    Program.Print("Failure!", 50);
                    Program.Print("[!] You do not have enough ingredients to brew this potion.\n...");
                    Program.ReadKey();
                    Console.Clear();
                }

                else if (brewChoice == "1" && cauldron[0] == "Noctis Vision Brew")
                {
                    // Player has enough ingredients to brew the Noctis Vision Brew
                    Program.Print("Success!", 50);
                    Program.Print("[Mysterious Voice]: You have brewed the Noctis Vision Brew. You have gained night vision and are now able to explore the Lunar Caverns. It is recommended to brew the Aqua Breather potion next.");
                    Console.WriteLine("Hit rate +5%! \n...");
                    Program.currentPlayer.hitrate += 5;
                    PlayerInventory.inventory["Bottle"] -= 1;
                    PlayerInventory.inventory["Firefly Essence"] -= 4;
                    PlayerInventory.inventory["Starlight Dew"] -= 4;
                    Program.ReadKey();
                    Console.Clear();
                    cauldron[0] = "X Brewed X";
                    brewedpotions.Add("Noctis Vision Brew");
                }

                else if (brewChoice == "2" && cauldron[1] == "Aqua Breather Potion")
                {
                    // Player has enough ingredients to brew the Aqua Breather Potion
                    Program.Print("Success!", 50);
                    Program.Print("[Mysterious Voice]: You have brewed the Aqua Breather Potion. You have gained water breathing are now able to explore the Sapphire Serenity Lake. It is recommended to brew the Naturae Blessing Brew next, assuming you have alreayd brewed the Noctis Vision Brew.");
                    Console.WriteLine("Health +25! \n...");
                    Program.currentPlayer.health += 25;
                    Program.ReadKey();
                    Console.Clear();
                    cauldron[1] = "X Brewed X";
                    brewedpotions.Add("Aqua Breather Potion");
                }

                else if (brewChoice == "3" && cauldron[2] == "Naturae Blessing Brew")
                {
                    // Player has enough ingredients to brew the Naturae Blessing Brew
                    Program.Print("Success!", 50);
                    Program.Print("[Mysterious Voice]: You have brewed the Naturae Blessing Brew. You have received a blessing from Mother Nature. It is recommended to brew the Dark Pyroblast Infusion next.");
                    Console.WriteLine("Damage +2!");
                    Console.WriteLine("Health +50! \n...");
                    Program.currentPlayer.damage += 2;
                    Program.currentPlayer.health += 50;
                    Program.ReadKey();
                    Console.Clear();
                    cauldron[2] = "X Brewed X";
                    brewedpotions.Add("Naturae Blessing Brew");
                }

                else if (brewChoice == "4" && cauldron[3] == "Dark Pyroblast Infusion")
                {
                    // Player has enough ingredients to brew the Dark Pyroblast Infusion
                    Program.Print("Success!", 50);
                    Program.Print("[Mysterious Voice]: You have brewed the Dark Pyroblast Infusion. You can now shoot much stronger and more accurate fireballs. It is recommended to brew the Arcane Spirit Elixir next.");
                    Console.WriteLine("Hitrate +10%!");
                    Console.WriteLine("All attacks deal 2x damage! \n...");
                    Program.currentPlayer.hitrate += 10;
                    Program.currentPlayer.damageMulti += 1;
                    Program.ReadKey();
                    Console.Clear();
                    cauldron[3] = "X Brewed X";
                    brewedpotions.Add("Dark Pyroblast Infusion");
                }

                else if (brewChoice == "5" && cauldron[4] == "Arcane Spirit Elixir")
                {
                    // Player has enough ingredients to brew the Arcane Spirit Elixir
                    Program.Print("Success!", 50);
                    Program.Print("[Mysterious Voice]: You have brewed the Arcane Spirit Elixir. You have summoned the Arcane Spirit, which will aid you in your journey through the Arcane Ruins. It is recommended to brew the Levitas Elixir or Frost Resistance Potion next.");
                    Console.WriteLine("Damage +1!");
                    Console.WriteLine("Health +30!");
                    Console.WriteLine("Hitrate +5%!");
                    Console.WriteLine("Increased chances of obtaining grimoires and rare ingredients (+2%). \n...");
                    Program.currentPlayer.health += 30;
                    Program.currentPlayer.damage += 1;
                    Program.currentPlayer.hitrate += 5;
                    Program.currentPlayer.luck += 2;
                    Program.ReadKey();
                    Console.Clear();
                    cauldron[4] = "X Brewed X";
                    brewedpotions.Add("Arcane Spirit Elixir");
                }

                else if (brewChoice == "6" && cauldron[5] == "Levitas Elixir")
                {
                    // Player has enough ingredients to brew the Levitas Elixir
                    Program.Print("Success!", 50);
                    Program.Print("[Mysterious Voice]: You have brewed the Levitas Elixir. You can now levitate at will, and will be able to continue your journey up to the Ethereal Summit, if the Frost Resistance Potion is also brewed.");
                    Console.WriteLine("Health +30! \n...");
                    Program.currentPlayer.health += 30;
                    Program.ReadKey();
                    Console.Clear();
                    cauldron[5] = "X Brewed X";
                    brewedpotions.Add("Levitas Elixir");
                }

                else if (brewChoice == "7" && cauldron[6] == "Frost Resistance Potion")
                {
                    // Player has enough ingredients to brew the Frost Resistance Potion
                    Program.Print("Success!", 50);
                    Program.Print("[Mysterious Voice]: You have brewed the Frost Resistance Potion. You have gained frost resistance, and will be able to continue your journey up to the Ethereal Summit, if the Levitas Elixir is also brewed.");
                    Console.WriteLine("Damage +3! \n...");
                    Program.currentPlayer.damage += 3;
                    Program.ReadKey();
                    Console.Clear();
                    cauldron[6] = "X Brewed X";
                    brewedpotions.Add("Frost Resistance Potion");
                }

                else if (brewChoice == "8" && cauldron[7] == "Holy Elixir of Restoration")
                {
                    // Player has enough ingredients to brew the Holy Elixir of Restoration
                    Program.Print("Success!", 50);
                    Program.Print("[Mysterious Voice]: You have finally brewed the Holy Elixir of Restoration! The heavens have bestowed upon you a holy blessing, and you will be able to save Master Alaric by disspelling his curse after defeating his kidnapper.");
                    Console.WriteLine("Damage +1!");
                    Console.WriteLine("Health +15! \n...");
                    Program.currentPlayer.damage += 1;
                    Program.currentPlayer.health += 15;
                    Program.ReadKey();
                    Console.Clear();
                    cauldron[7] = "X Brewed X";
                    brewedpotions.Add("Holy Elixir of Restoration");
                }

                else if (Quests.HolyChickenQuestCompleted() && brewChoice == "holychicken" && cauldron[8] == "Holy Roasted Chicken")
                {
                    // Player has enough ingredients to brew the Holy Elixir of Restoration
                    Program.Print("Success!", 50);
                    Program.Print("[Mysterious Voice]: You have produced a Holy Roasted Chicken, an essential ingredient in brewing the Holy Elixir of Restoration!");
                    PlayerInventory.AddIngredient("Holy Roasted Chicken", 1);
                    Program.ReadKey();
                    Console.Clear();
                    cauldron[8] = "Empty";
                }

                else if (brewChoice == "secret" && cauldron[9] == "Malevolent Elixir of Calamity")
                {
                    // Player has enough ingredients to brew the Holy Elixir of Restoration
                    Program.Print("Success!", 50);
                    Program.Print("[Mysterious Voice]: You have brewed the Malevolent Elixir of Calamity! You are now probably strong enough to defeat the True Astral Enigma.");
                    Console.WriteLine("Damage +5!");
                    Console.WriteLine("Health +150!");
                    Console.WriteLine("Hit rate +1%! \n...");
                    Program.currentPlayer.damage += 5;
                    Program.currentPlayer.health += 150;
                    Program.currentPlayer.hitrate += 1;
                    Program.ReadKey();
                    Console.Clear();
                    cauldron[9] = "X Brewed X";
                    brewedpotions.Add("Malevolent Elixir of Calamity");
                }

                else if (brewChoice == "1" && cauldron[0] == "X Brewed X" || brewChoice == "2" && cauldron[1] == "X Brewed X" || brewChoice == "3" && cauldron[2] == "X Brewed X" || brewChoice == "4" && cauldron[3] == "X Brewed X" || brewChoice == "5" && cauldron[4] == "X Brewed X" || brewChoice == "6" && cauldron[5] == "X Brewed X" || brewChoice == "7" && cauldron[6] == "X Brewed X" || brewChoice == "8" && cauldron[7] == "X Brewed X" || brewChoice == "holychicken" && cauldron[8] == "Empty")
                {
                    // Player has already brewed the potion
                    Program.Print("Failure!", 50);
                    Program.Print("[!] You have already brewed this potion! You can only brew each potion once.\n...");
                    Program.ReadKey();
                    Console.Clear();
                }

                else if (brewChoice == "9")
                {
                    // Player chooses to return
                    Console.Clear();
                    Program.Print("You walk away from the cauldron. \n...");
                    Program.ReadKey();
                    Console.Clear();
                    AlchemyLab.Lab();
            
                }

                else if (brewChoice != "1" || brewChoice != "2" ||brewChoice != "3" || brewChoice != "4" || brewChoice != "5" || brewChoice != "6" || brewChoice != "7" || brewChoice != "8" || brewChoice != "9")
                { 
                    // Invalid choice
                    Program.Print("Error!", 25);
                    Console.WriteLine("[!] Please enter an integer between 1 and 9.\n...");
                    Program.ReadKey();
                    Console.Clear();
                    
                }
            
            }

        }


        public static void BrewedPotions()
        {   
            Console.Clear();
            // Display potions already brewed by player
            Program.Print($"{Program.currentPlayer.name}'s Potions");
            Console.WriteLine("=================================");
            
            for (int i = 0; i < brewedpotions.Count; i++)
            {
                var pot = brewedpotions[i];
                Console.WriteLine($"[{i + 1}] {pot}");
            }
            if (brewedpotions.Count == 0)
            {
                // No potions brewed
                Console.WriteLine("[?] You have not brewed any potions yet.");
            }
            Console.WriteLine("=================================");
            Console.Write("Press Enter to return...");
            Program.ReadKey();
            Console.Clear();
            AlchemyLab.Lab();
        }

    } // end class BrewPotions


    public class Battles
    {
        private static bool battleGuideShown = false; 
        // Battle Crazy Cursed Chicken
        public static void BattleChicken()
        {
            if (!battleGuideShown) // If first encounter with an enemy, show battle guide
            {
                Program.Print("[Mysterious Voice]: Here I am again! Engage in battles by encountering enemies while exploring different locations within the game world.\n...");
                Program.ReadKey();
                Program.Print("[Mysterious Voice]: Cast Fireball: Deal damage to the enemy equivalent to your base damage. This is a reliable option for steadily wearing down your opponent's health.\n...");
                Program.ReadKey();
                Program.Print("[Mysterious Voice]: Mana Shield: Reduce your damage output to 30% of your base damage, but gain a 70% reduction in incoming damage from the enemy. Use this option when facing strong opponents or when your health/enemy's health is low.\n...");
                Program.ReadKey();
                Program.Print("[Mysterious Voice]: Flee Battle: Make a daring escape attempt to exit the battle. There's a 70% chance of success, allowing you to evade the enemy and live to fight another day. However, there's also a 30% chance of failure, resulting in injury and continued combat.\n...");
                Program.ReadKey();
                Program.Print("[Mysterious Voice]: Defeat the enemy by depleting its health to zero to emerge victorious. Each enemy drops different rewards - some are required for potions; some are required for quests, and some enemies have rare drops (grimoires) which further strengthen your character (increases stats)! \n...");
                Program.ReadKey();
                Console.Clear();
                battleGuideShown = true;
            }

            Program.Print("You encounter a weird-looking chicken on the Crossroads.\n...");
            Program.ReadKey();
            Program.Print("[Crazy Cursed Chicken]: Cluck-cluck-cluck! Cuckoo!!! Cluck-cluck-cluck! Cuckoo!!! \n...");
            Program.ReadKey();
            Console.Clear();
            Combat("Crazy Cursed Chicken", 2, 5, 66);
            Locations.Crossroads();
        }

        // Battle Holy Abyssal Serpent
        public static void BattleSerpent()
        {
            if (!battleGuideShown)
            {
                Program.Print("[Mysterious Voice]: Here I am again! Engage in battles by encountering enemies while exploring different locations within the game world.\n...");
                Program.ReadKey();
                Program.Print("[Mysterious Voice]: Cast Fireball: Deal damage to the enemy equivalent to your base damage. This is a reliable option for steadily wearing down your opponent's health.\n...");
                Program.ReadKey();
                Program.Print("[Mysterious Voice]: Mana Shield: Reduce your damage output to 30% of your base damage, but gain a 70% reduction in incoming damage from the enemy. Use this option when facing strong opponents or when your health/enemy's health is low.\n...");
                Program.ReadKey();
                Program.Print("[Mysterious Voice]: Flee Battle: Make a daring escape attempt to exit the battle. There's a 70% chance of success, allowing you to evade the enemy and live to fight another day. However, there's also a 30% chance of failure, resulting in injury and continued combat.\n...");
                Program.ReadKey();
                Program.Print("[Mysterious Voice]: Defeat the enemy by depleting its health to zero to emerge victorious. Each enemy drops different rewards - some are required for potions; some are required for quests, and some enemies have rare drops (grimoires) which further strengthen your character! \n...");
                Program.ReadKey();
                Console.Clear();
                battleGuideShown = true;
            }
            Program.Print("You encounter a Holy Abyssal Serpent at the Sapphire Serenity Lake. \n...");
            Program.ReadKey();
            Program.Print("[Holy Abyssal Serpent]: Hssss... Foolish mortal, dare you trespass upon sacred waters? Your presence defiles the purity of this realm. Prepare to face the wrath of the abyss.\n...");
            Program.ReadKey();
            Console.Clear();
            Program.Print("What will you do?");
            
            Combat("Holy Abyssal Serpent", 14, 40, 66);
            Locations.SerenityLake();
        }

        // Battle Moonlight Earthpulse Golem
        public static void BattleGolem()
        {   
            if (!battleGuideShown)
            {
                Program.Print("[Mysterious Voice]: Here I am again! Engage in battles by encountering enemies while exploring different locations within the game world.\n...");
                Program.ReadKey();
                Program.Print("[Mysterious Voice]: Cast Fireball: Deal damage to the enemy equivalent to your base damage. This is a reliable option for steadily wearing down your opponent's health.\n...");
                Program.ReadKey();
                Program.Print("[Mysterious Voice]: Mana Shield: Reduce your damage output to 30% of your base damage, but gain a 70% reduction in incoming damage from the enemy. Use this option when facing strong opponents or when your health/enemy's health is low.\n...");
                Program.ReadKey();
                Program.Print("[Mysterious Voice]: Flee Battle: Make a daring escape attempt to exit the battle. There's a 70% chance of success, allowing you to evade the enemy and live to fight another day. However, there's also a 30% chance of failure, resulting in injury and continued combat.\n...");
                Program.ReadKey();
                Program.Print("[Mysterious Voice]: Defeat the enemy by depleting its health to zero to emerge victorious. Each enemy drops different rewards - some are required for potions; some are required for quests, and some enemies have rare drops (grimoires) which further strengthen your character! \n...");
                Program.ReadKey();
                Console.Clear();
                battleGuideShown = true;
            }
            Program.Print("You encounter a Moonlight Earthpulse Golem in the Lunar Caverns. \n...");
            Program.ReadKey();
            Program.Print("[Moonlight Earthpulse Golem]: Intruder, you tread upon hallowed ground. Leave this place, or face the consequences. The moon's power flows through me, and I shall not hesitate to crush those who disturb its sanctity. \n...");
            Program.ReadKey();
            Console.Clear();
            Program.Print("What will you do?");

            Combat("Moonlight Earthpulse Golem", 12, 88, 35);
            Locations.LunarCaverns();
        }

        // Battle Astral Enigma
        public static void BattleBoss()
        {
            Program.Print("You finally encounter the Astral Enigma. \n...");
            Program.ReadKey();
            Program.Print("[Astral Enigma]: Foolish mortal, you dare challenge the might of the Astral Enigma? Your defiance shall be your undoing. Prepare to be consumed by the boundless void. \n...");
            Program.ReadKey();
            Console.Clear();
            Program.Print("What will you do?");
            
            Combat("Astral Enigma", 12, 200, 72);
            Locations.EtherealSummit();
        }

        public static void BattleSecretBoss()
        {
            Program.Print("? ? ?", 300, false);
            Program.Print("[You]: What is happening?\n...");
            Program.ReadKey();
            Console.Clear();
            Program.Print("The True Astral Enigma has finally appeared. \n...");
            Program.ReadKey();
            Program.Print("[True Astral Enigma]: At last, you have unlocked the gateway to true power. Mortal, I am the embodiment of cosmic chaos, the harbinger of oblivion. Embrace your fate, for the universe bends to my will. \n...");
            Program.ReadKey();
            Console.Clear();
            Program.Print("What will you do?");

            Combat("True Astral Enigma", 28, 444, 82);
            Locations.EtherealSummit();
        }

        public static void Combat(string name, decimal damageE, decimal healthE, decimal hitrateE)
        {
            // Enemy stats
            string n ="";
            decimal d = 0;
            decimal h = 0;
            decimal r = 0;
            
            n = name;
            d = damageE * Program.currentPlayer.damageResist;
            h = healthE;
            r = hitrateE;
            
            // Player stat adjustments
            decimal attack = Program.currentPlayer.damage * Program.currentPlayer.damageMulti;
            decimal currenthealth = Program.currentPlayer.health;

            // Battle enemies
            while(h > 0 && Program.currentPlayer.health > 0)
            {   
                // Display battle situation (player and enemy health) + player options
                Console.WriteLine($"[{Program.currentPlayer.name}]: {currenthealth}/{Program.currentPlayer.health} HP  |  [{n}]: {h}/{healthE} HP");
                Console.WriteLine("=====================================");
                Console.WriteLine("Choose an option:");
                Console.WriteLine($"[1] Cast Fireball (DMG: {attack})");
                Console.WriteLine($"[2] Mana Shield (DMG: {attack * 0.3m} + Enemy DMG -70%)");
                Console.WriteLine("[3] Flee Battle");
                Console.WriteLine("=====================================");
                Console.Write("Your choice: ");
                Random rnd = new Random();
                int hitChance = rnd.Next(1, 101);
                int enemyHitChance = rnd.Next(1, 101);
                int fleeChance = rnd.Next(1, 101);
                string? input = Console.ReadLine()?.Trim();
                if(input == "1" && hitChance <= Program.currentPlayer.hitrate && enemyHitChance <= r)
                {   
                    // Both player and enemy attack successful
                    Console.WriteLine($"With haste, you summon a blazing fireball in your hands and hurl it towards the {n}! Furiously, the {n} strikes you back with a heavy blow.");
                    Console.WriteLine($"You deal {attack} damage to the {n}, and lose {d} health from its attack.");
                    currenthealth -= d;
                    h -= attack;
                }
                else if (input == "1" && hitChance > Program.currentPlayer.hitrate && enemyHitChance <= r)
                {
                    // Player attack missed and enemy attack successful
                    Console.WriteLine($"With haste, you summon a blazing fireball in your hands and hurl it towards the {n}. Unfortunately, your fireball missed it, and it strikes you back with a heavy blow.");
                    Console.WriteLine($"You lose {d} health from its attack.");
                    currenthealth -= d;
                }
                else if (input == "1" && hitChance <= Program.currentPlayer.hitrate && enemyHitChance > r)
                {
                    // Player attack successful and enemy attack missed
                    Console.WriteLine($"With haste, you summon a blazing fireball in your hands and hurl it towards the {n}. Furiously, the {n} tried to strike you back and missed its attack.");
                    Console.WriteLine($"You deal {attack} damage to the {n}.");
                    h -= attack;
                }
                else if (input == "1" && hitChance > Program.currentPlayer.hitrate && enemyHitChance > r)
                {
                    // Both player and enemy attack missed
                    Console.WriteLine("You both missed your attacks! How unfortunate!");
                }
                else if(input == "2" && hitChance <= Program.currentPlayer.hitrate && enemyHitChance <= r)
                {   
                    // Both player and enemy attack successful; player casts shield
                    Console.WriteLine($"With haste, you cast a mana shield around you and punch the {n}! Furiously, the {n} strikes you back, and your mana shield protects you.");
                    Console.WriteLine($"You deal {attack * 0.3m} damage to the {n}, and lose {d * 0.3m} health from its attack.");
                    currenthealth -= d * 0.3m;
                    h -= attack * 0.3m;
                }
                else if (input == "2" && hitChance > Program.currentPlayer.hitrate && enemyHitChance <= r)
                {
                    // Player casts shield and misses attack; enemy attack successful
                    Console.WriteLine($"With haste, you cast a mana shield around you and try to punch the {n}! Unfortunately, you missed your punch. Furiously, the {n} strikes you back, but your mana shield protects you.");
                    Console.WriteLine($"You lose {d * 0.3m} health from its attack.");
                    currenthealth -= d * 0.3m;
                }
                else if (input == "2" && hitChance <= Program.currentPlayer.hitrate && enemyHitChance > r)
                {
                    // Player casts shield and attacks successfully; enemy attack missed
                    Console.WriteLine($"With haste, you cast a mana shield around you and punch the {n}! Furiously, the {n} tries to strike you but misses its attack, leaving your mana shield useless.");
                    Console.WriteLine($"You deal {attack * 0.3m} damage to the {n}.");
                    h -= attack * 0.3m;
                }
                else if (input == "2" && hitChance > Program.currentPlayer.hitrate && enemyHitChance > r)
                {
                    // Both player and enemy attack missed;
                    Console.WriteLine("You casted a mana shield, but you both missed your attacks! How unfortunate!");
                }
                else if(input == "3")
                {   
                    // Player chooses to flee
                    if (fleeChance <= 70)
                    {
                        // Successfully flees and goes back to original location
                        Console.WriteLine("You used Master Alaric's Shadow Movement Technique and successfully fled the battle!");
                        Console.WriteLine("You heal yourself back to max health with a healing spell. \n...");
                        Program.ReadKey();
                        Console.Clear();
                        break;
                    }
                    else if (fleeChance > 70)
                    {
                        // Fails to flee and continues battle
                        Console.WriteLine($"As you sprint away from the {n}, its strike catches you in the back, sending you sprawling onto the ground.");
                        Console.WriteLine($"You lose {d} health from its attack and failed to escape.");
                        currenthealth -= d;

                    }
                }
                else
                {   
                    // Invalid choice
                    Program.Print("Error!", 25);
                    Console.WriteLine("[!] Please enter an integer between 1 and 3.");
                }
                Console.WriteLine("...");
                Program.ReadKey();
                Console.Clear();

            }
            
            if (currenthealth <= 0)
            {
                // Player dies, player loses 1 life
                Program.Print($"DEFEAT... You have been slain by the {n}...", 150);
                Program.ReadKey();
                Program.currentPlayer.lives -= 1;
                Program.Print($"[Mysterious Voice]: You have lost one life. You have {Program.currentPlayer.lives} more live(s).");
                

                if (Program.currentPlayer.lives <= 0) // If player loses all lives, game over
                {   
                    Program.Print("GAME OVER...", 1500);
                    Program.ReadKey();
                    System.Environment.Exit(0);
                }

            }

            if (h <=0)
            {
                // Player defeats enemy, display vistory message 
                Program.Print($"Victory! You have defeated the {n}!", 50);
                if (n == "Crazy Cursed Chicken")
                {
                    // Drop item
                    Program.Print("You obtained 1 Cursed Roasted Chicken!");
                    PlayerInventory.AddIngredient("Cursed Roasted Chicken", 1);
                    Console.WriteLine($"You heal yourself back to max health with a healing spell. \n...");
                    Program.ReadKey();
                    Console.Clear();
                }
                else if (n == "Holy Abyssal Serpent")
                {   
                    double dropOutcome = new Random().NextDouble() * 100;
                    if (dropOutcome <= 80 - Program.currentPlayer.luck) // 80% (-2% from Arcane Spirit Elixir --> +2% for grimoire)
                    {
                        // Normal enemy drop
                        Program.Print("You obtained 1 Holy Abyssal Serpent's Fangs!");
                        PlayerInventory.AddIngredient("Holy Abyssal Serpent's Fangs", 1);
                        Console.WriteLine($"You heal yourself back to max health with a healing spell. \n...");
                        Program.ReadKey();
                        Console.Clear();
                    }
                    else
                    {
                        // Rare drop
                        Program.Print("You obtained 1 Poseidon's Grimoire of Fury (Legendary)!");
                        PlayerInventory.AddIngredient("Poseidon's Grimoire of Fury", 1);
                        Console.WriteLine("Damage +0.3!");
                        Console.WriteLine("Health +4!");
                        Console.WriteLine("Hitrate +0.5%!");
                        Console.WriteLine($"You heal yourself back to max health with a healing spell. \n...");
                        Program.currentPlayer.damage += 0.3m;
                        Program.currentPlayer.health += 4;
                        Program.currentPlayer.hitrate += 0.5m;
                        Program.ReadKey();
                        Console.Clear();

                    }
                }
                else if (n == "Moonlight Earthpulse Golem")
                {   
                    double dropOutcome = new Random().NextDouble() * 100;
                    if (dropOutcome <= 80 - Program.currentPlayer.luck)
                    {
                        // Normal enemy drop
                        Program.Print("You obtained 1 Moonlight Earthpulse Golem Core!");
                        PlayerInventory.AddIngredient("Moonlight Earthpulse Golem Core", 1);
                        Console.WriteLine($"You heal yourself back to max health with a healing spell. \n...");
                        Program.ReadKey();
                        Console.Clear();
                    }
                    else
                    {
                        // Rare drop
                        Program.Print("You obtained 1 Gaia's Grimoire of Fury (Legendary)!");
                        PlayerInventory.AddIngredient("Gaia's Grimoire of Fury", 1);
                        Console.WriteLine("Damage +0.2!");
                        Console.WriteLine("Health +6!");
                        Console.WriteLine("Hitrate +0.5%!");
                        Console.WriteLine($"You heal yourself back to max health with a healing spell. \n...");
                        Program.currentPlayer.damage += 0.2m;
                        Program.currentPlayer.health += 6;
                        Program.currentPlayer.hitrate += 0.5m;
                        Program.ReadKey();
                        Console.Clear();

                    }
                }
                else if (n == "Astral Enigma")
                {   
                    if (BrewPotions.brewedpotions.Contains("Holy Elixir of Restoration"))
                    {
                        // If the Holy Elixir of Restoration is brewed and not given away
                        // GOOD ENDING
                        Program.Print("As the Astral Enigma falls before your might, the realm of Eldralore is bathed in a warm glow. Master Alaric, freed from his curse, embraces you with gratitude. \n...");
                        Program.ReadKey();
                        Program.Print("Together, you rebuild the alchemy lab, strengthening its defenses against future threats. \n...");
                        Program.ReadKey();
                        Program.Print("The people of Eldralore hail you as a hero, and your name echoes throughout the land. \n...");
                        Program.ReadKey();
                        Program.Print("With the Astral Enigma defeated and balance restored, you continue your journey as a revered alchemist, using your skills to protect the realm and unlock its endless mysteries.\n...");
                        Program.ReadKey();
                        Program.Print($"[Mysterious Voice]: Congratulations, {Program.currentPlayer.name}! You have completed Alchemy Quest with the Good Ending. Thank you for playing!\n...", 4, false);
                        Program.ReadKey();
                        System.Environment.Exit(0);
                    }
                    else
                    {
                        // BAD ENDING
                        Program.Print("With determination and skill, you face the Astral Enigma in a climactic battle, ultimately defeating the malevolent creature. \n...");
                        Program.ReadKey();
                        Program.Print("However, without the Holy Elixir of Restoration, Master Alaric's curse remains unbroken. \n...");
                        Program.ReadKey();
                        Program.Print("As the dust settles, you return to the Alchemy Lab, where the weight of your failure hangs heavy in the air. \n...");
                        Program.ReadKey();
                        Program.Print("Despite your victory over the Astral Enigma, Eldralore remains shrouded in uncertainty and darkness. \n...");
                        Program.ReadKey();
                        Program.Print("Master Alaric's absence serves as a constant reminder of the price of negligence, leaving you to ponder what could have been if only you had brewed the elixir and saved your mentor.\n...");
                        Program.ReadKey();
                        Program.Print($"[Mysterious Voice]: Congratulations, {Program.currentPlayer.name}! You have completed Alchemy Quest with the Bad Ending. Thank you for playing!\n...", 4, false);
                        Program.ReadKey();
                        System.Environment.Exit(0);
                    }
                }
                else if (n == "True Astral Enigma")
                {
                    // SECRET ENDING
                    Program.Print("Empowered by the Malevolent Elixir of Calamity, you confront the True Astral Enigma with unmatched strength.\n...");
                    Program.ReadKey();
                    Program.Print("The battle is fierce, but you emerge victorious, shattering the darkness that threatened Eldralore, dispelling Master Alaric's curse. \n...");
                    Program.ReadKey();
                    Program.Print("However, as you stand amidst the ruins of the Ethereal Summit, a chilling realization dawns upon you. \n...");
                    Program.ReadKey();
                    Program.Print("Amidst the celebration, a shadow looms over Master Alaric's face. He sees the toll the Malevolent power has taken on you, his once-loyal apprentice. \n...");
                    Program.ReadKey();
                    Program.Print("The Malevolent power that once fueled your triumph now consumes you from within, twisting your mind and soul. \n...");
                    Program.ReadKey();
                    Program.Print("Despite defeating the True Astral Enigma, you find yourself succumbing to the same darkness you sought to vanquish. \n...");
                    Program.ReadKey();
                    Program.Print("As the realm falls into chaos once more, your name becomes synonymous with fear and destruction. \n...");
                    Program.ReadKey();
                    Program.Print("You have saved Eldralore from one evil, only to become another, forever bound by the Malevolent forces you dared to wield.\n...");
                    Program.ReadKey();
                    Program.Print($"[Mysterious Voice]: Congratulations, {Program.currentPlayer.name}! You have completed Alchemy Quest with the SECRET Ending. Thank you for playing!\n...", 4, false);
                    Program.ReadKey();
                    System.Environment.Exit(0);
                }
            }
        }
            
 
    }  // end class Battles

    public class PlayerInventory 
    {
        public static Dictionary<string, int> inventory = new Dictionary<string, int>();

        // Method to add an ingredient to the inventory
        public static void AddIngredient(string ingredient, int quantity) {
            if (inventory.ContainsKey(ingredient)) 
            {
            // Increases quantity of ingredient if already exists
                inventory[ingredient] += quantity; 
            } 
            else
            {
            // Adds ingredient to inventory if doesn't exist
                inventory.Add(ingredient, quantity); 
            }
        }

        // Method to display player inventory
        public static void DisplayInventory() {
            Console.Clear();
            Program.Print($"{Program.currentPlayer.name}'s Ingredient Pouch");
            Console.WriteLine("=====================================");
            
            if (inventory.Count == 0)
            {
                // No ingredients collected
                Console.WriteLine("[?] Your magical pouch is empty..");
            }
                
            else
            {
                foreach (var ingredient in inventory) {
                // Displays quantity and name of each ingredient
                Console.WriteLine($"[-] {ingredient.Value}x {ingredient.Key}"); 
                if (ingredient.Value == 0)
                {
                    inventory.Remove(ingredient.Key);
                }

            }
            }
            Console.WriteLine("=====================================");
            Console.Write("Press Enter to return.");
            Program.ReadKey();
            Console.Clear();
        }
    } // end class PlayerInventory

    public class Locations
    {
        public static bool harvestGuideShown = false;
        public static void Crossroads()
        // Location = Crossroads
        {
            Console.WriteLine("You arrive at the Crossroads. What will you do?");
            Console.WriteLine("=================================");
            // Display Crossroad options
            Console.WriteLine("[1] Battle Crazy Cursed Chicken");
            Console.WriteLine("[2] Continue left to Mystic Forest");
            Console.WriteLine("[3] Continue right to Starlight Meadow");
            Console.WriteLine("[4] Continue straight to Arcane Ruins");
            Console.WriteLine("[5] Return to Lab");
            Console.WriteLine("=================================");
            Console.Write("Your choice: ");
            string? crossroadsChoice = Console.ReadLine()?.Trim();

            switch (crossroadsChoice)
            {
                case "1":
                // Player chooses to battle enemy
                    Console.Clear();
                    Battles.BattleChicken();
                    break;
                case "2":
                // Player chooses to go to Mystic Forest
                    Program.Print("You decide to continue to the Mystic Forest. \n...");
                    Program.ReadKey();
                    Console.Clear();
                    MysticForest();
                    break;

                case "3":
                // Player chooses to go to Starlight Meadow
                    Program.Print("You decide to continue to the Starlight Meadow. \n...");
                    Program.ReadKey();
                    Console.Clear();
                    StarlightMeadow(); 
                    break;

                case "4":
                // Player chooses to go to Arcane Ruines
                    if (BrewPotions.brewedpotions.Contains("Arcane Spirit Elixir"))
                    {
                        // Player has brewed required potion and continues 
                        Program.Print("You decide to continue to the Arcane Ruins. \n...");
                        Program.ReadKey();
                        Console.Clear();
                        ArcaneRuins(); 
                    }
                    else 
                    {
                        // Player has restricted entry as potion not brewed
                        Program.Print("[You]: These ruins ahead look ancient and mysterious. But there's something unsettling about them...\n...");
                        Program.ReadKey();
                        Program.Print("You glance at inscriptions carved on the stones. Deciphering those inscriptions with the aid of a guardian of the ruins could reveal hidden knowledge.\n...");
                        Program.ReadKey();
                        Program.Print("[You]: So, to enter the ruins and unveil the mysteries, I must summon forth an arcane spirit to decipher the hidden messages. \n[!] You do not have access to this location yet.\n...");
                        Program.ReadKey();
                        Console.Clear();
                        Crossroads();
                    }
                    break;

                case "5":
                // Player chooses to return to Lab
                    Program.Print("You decide to return to the Alchemy Lab. \n...");
                    Program.ReadKey();
                    Console.Clear();
                    AlchemyLab.Lab();
                    break;

                default:
                // Invalid choice and repeats
                    Program.Print("Error!", 25);
                    Console.WriteLine("[!] Please enter an integer between 1 and 5. \n...");
                    Program.ReadKey();
                    Console.Clear();
                    Crossroads();
                    break;
            }
        }

        public static void MysticForest()
        // Location = Mystic Forest
        {
            if (!harvestGuideShown) // If first time visiting either Mystic Forest or Starlight Meadow, show harvesting and quests guide
            {
                Program.Print("[Mysterious Voice]: Hey there! Harvesting is a crucial aspect of gameplay, allowing you to gather resources such as ingredients, minerals, and rare items from various locations in the world of Eldralore.\n...");
                Program.ReadKey();
                Program.Print("[Mysterious Voice]: You can explore different locations such as forests, meadows, lakes, ruins, and caves to find resources for harvesting. \n...");
                Program.ReadKey();
                Program.Print("[Mysterious Voice]: As you gather ingredients, you can keep track of them in your magical Ingredient Pouch. \n...");
                Program.ReadKey();
                Program.Print("[Mysterious Voice]: Harvesting requires patience and persistence. Take your time to gather resources methodically and efficiently without rushing\n...");
                Program.ReadKey();
                Program.Print("[Mysterious Voice]: On the other hand, Quests are tasks or missions assigned by non-player characters (NPCs), e.g. Silky Spider Queen, that offer rewards upon completion.\n...");
                Program.ReadKey();
                Program.Print("[Mysterious Voice]: Talk to NPCs at various locations to receive quests. Pay attention to quest objectives and instructions provided by NPCs to understand what tasks need to be completed. \n...");
                Program.ReadKey();
                Program.Print("[Mysterious Voice]: Some quests are compulsory to progress through the game, while some are side quests which are only required for the Secret Ending.\n...");
                Program.ReadKey();
                Program.Print("[Mysterious Voice]: Upon meeting the quest conditions, return to the NPC to claim your rewards.\n...");
                Program.ReadKey();
                Console.Clear();
                harvestGuideShown = true;
            }

            Console.WriteLine("You arrive at the Mystic Forest. What will you do?");
            Console.WriteLine("=================================");

            // Display Mystic Forest options
            Console.WriteLine("[1] Harvest Mystical Herbs");
            Console.WriteLine("[2] Continue to explore the Sapphire Serenity Lake");
            Console.WriteLine("[3] Check Ingredient Pouch");
            Console.WriteLine("[4] Talk to Mysterious Homeless Eldralorean");
            Console.WriteLine("[5] Return to Crossroads");
            Console.WriteLine("[6] Return to Lab");
            Console.WriteLine("=================================");
            Console.Write("Your choice: ");
            string? forestChoice = Console.ReadLine()?.Trim();

            switch (forestChoice)
            {
                case "1":
                // Player chooses to harvest ingredients
                    Harvesting.HarvestForest();
                    break;

                case "2":
                // Player chooses to go to Sapphire Serenity Lake
                    if (BrewPotions.brewedpotions.Contains("Aqua Breather Potion"))
                    {
                        // Player has brewed required potion and continues
                        Program.Print("You decide to continue to the Sapphire Serenity Lake. \n...");
                        Program.ReadKey();
                        Console.Clear();
                        SerenityLake(); 
                    }
                    else  
                    {
                        // Player has restricted entry as potion not brewed
                        Program.Print("[You]: That lake looks really deep... The air's getting thinner, and it's freezing cold.\n...");
                        Program.ReadKey();
                        Program.Print("You glance at the lake but notice it's really deep. Swimming down there without preparation sounds risky.\n...");
                        Program.ReadKey();
                        Program.Print("[You]: Gotta gear up before diving in. I'd have to brew something that lets me breathe underwater. Can't wait to uncover the secrets down there! \n[!] You do not have access to this location yet.\n...");
                        Program.ReadKey();
                        Console.Clear();
                        MysticForest();
                    }
                    break;
                
                case "3":
                // Player chooses to check ingredient pouch
                    PlayerInventory.DisplayInventory();
                    MysticForest();
                    break;

                case "4":
                //Player chooses to talk to Mysterious Homeless Eldralorean
                    Console.Clear();
                    Quests.MysteriousEldralorean();
                    break;

                case "5":
                // Player chooses to return to Crossroads
                    Program.Print("You decide to return to the Crossroads. \n...");
                    Program.ReadKey();
                    Console.Clear();
                    Locations.Crossroads();
                    break;
                
                case "6":
                // Player chooses to return to Lab
                    Program.Print("You decide to return to the Alchemy Lab. \n...");
                    Program.ReadKey();
                    Console.Clear();
                    AlchemyLab.Lab(); 
                    break;

                default:
                // Invalid choice and repeats
                    Program.Print("Error!", 25);
                    Console.WriteLine("[!] Please enter an integer between 1 and 6. \n...");
                    Program.ReadKey();
                    Console.Clear();
                    MysticForest();
                    break;
            }
        }

        public static void StarlightMeadow()
        // Location = Starlight Meadow
        {
            if (!harvestGuideShown) // If first time visiting either Mystic Forest or Starlight Meadow, show harvesting and quests guide
            {
                Program.Print("[Mysterious Voice]: Hey there! Harvesting is a crucial aspect of gameplay, allowing you to gather resources such as ingredients, minerals, and rare items from various locations in the world of Eldralore.\n...");
                Program.ReadKey();
                Program.Print("[Mysterious Voice]: You can explore different locations such as forests, meadows, lakes, ruins, and caves to find resources for harvesting. \n...");
                Program.ReadKey();
                Program.Print("[Mysterious Voice]: As you gather ingredients, you can keep track of them in your magical Ingredient Pouch. \n...");
                Program.ReadKey();
                Program.Print("[Mysterious Voice]: Harvesting requires patience and persistence. Take your time to gather resources methodically and efficiently without rushing\n...");
                Program.ReadKey();
                Program.Print("[Mysterious Voice]: On the other hand, Quests are tasks or missions assigned by non-player characters (NPCs), e.g. Silky Spider Queen, that offer rewards upon completion.\n...");
                Program.ReadKey();
                Program.Print("[Mysterious Voice]: Talk to NPCs at various locations to receive quests. Pay attention to quest objectives and instructions provided by NPCs to understand what tasks need to be completed. \n...");
                Program.ReadKey();
                Program.Print("[Mysterious Voice]: Some quests are compulsory to progress through the game, while some are side quests which are only required for the Secret Ending.\n...");
                Program.ReadKey();
                Program.Print("[Mysterious Voice]: Upon meeting the quest conditions, return to the NPC to claim your rewards.\n...");
                Program.ReadKey();
                Console.Clear();
                harvestGuideShown = true;
            }

            Console.WriteLine("You arrive at the Starlight Meadow. What will you do?");
            Console.WriteLine("=================================");
            // Display Starlight Meadow options
            Console.WriteLine("[1] Harvest Starlight Matter");
            Console.WriteLine("[2] Continue to explore the Lunar Caverns");
            Console.WriteLine("[3] Check Ingredient Pouch");
            Console.WriteLine("[4] Talk to Wise Celestial Sage");
            Console.WriteLine("[5] Return to Crossroads");
            Console.WriteLine("[6] Return to Lab");
            Console.WriteLine("=================================");
            Console.Write("Your choice: ");
            string? meadowChoice = Console.ReadLine()?.Trim();

            switch (meadowChoice)
            {
                case "1":
                // Player chooses to harvest ingredients
                    Harvesting.HarvestMeadow();
                    break;

                case "2":
                // Player chooses to continue to Lunar Caverns
                    if (BrewPotions.brewedpotions.Contains("Noctis Vision Brew"))
                    {
                        // Player has brewed required potion and continues
                        Program.Print("You decide to continue to the Lunar Caverns. \n...");
                        Program.ReadKey();
                        Console.Clear();
                        LunarCaverns(); 
                    }
                    else 
                    {
                        // Player has restricted entry as potion not brewed
                        Program.Print("[You]: Those caverns look like they go deep into the earth! But it's so dark in there.\n...");
                        Program.ReadKey();
                        Program.Print("Peering into the cavern's depths, you can't see anything but darkness. Exploring without preparation might be a bad idea.\n...");
                        Program.ReadKey();
                        Program.Print("[You]: Gotta gear up before delving into these caverns. I'd have to brew something that lets me see in the dark. Can't wait to uncover the secrets inside! \n[!] You do not have access to this location yet.\n...");
                        Program.ReadKey();
                        Console.Clear();
                        StarlightMeadow();
                    }
                    break;

                case "3":
                // Player chooses to check ingredient pouch
                    PlayerInventory.DisplayInventory();
                    StarlightMeadow();
                    break;
                
                case "4":
                // Player chooses to talk to Wise Celestial Sage
                    Console.Clear();
                    Quests.CelestialSage();
                    break;

                case "5":
                // Player chooses to return to Crossroads
                    Program.Print("You decide to return to the Crossroads. \n...");
                    Program.ReadKey();
                    Console.Clear();
                    Locations.Crossroads();
                    break;
                
                case "6":
                // Player chooses to return to Lab
                    Program.Print("You decide to return to the Alchemy Lab. \n...");
                    Program.ReadKey();
                    Console.Clear();
                    AlchemyLab.Lab(); 
                    break;

                default:
                // Invalid choice and repeats
                    Program.Print("Error!", 25);
                    Console.WriteLine("[!] Please enter an integer between 1 and 6. \n...");
                    Program.ReadKey();
                    Console.Clear();
                    StarlightMeadow();
                    break;
            }
        }

        public static void SerenityLake()
        // Location = Sapphire Serenity Lake
        {
            Console.WriteLine("You arrive at the Sapphire Serenity Lake. What will you do?");
            Console.WriteLine("=================================");
            // Display Sapphire Serenity Lake options
            Console.WriteLine("[1] Harvest Sapphire Serenity Substances");
            Console.WriteLine("[2] Battle Holy Abyssal Serpent");
            Console.WriteLine("[3] Check Ingredient Pouch");
            Console.WriteLine("[4] Return to Mystic Forest");
            Console.WriteLine("[5] Return to Lab");
            Console.WriteLine("=================================");
            Console.Write("Your choice: ");
            string? lakeChoice = Console.ReadLine()?.Trim();

            switch (lakeChoice)
            {
                case "1":
                // Player chooses to harvest ingredients
                    Harvesting.HarvestLake();
                    break;

                case "2":
                // Player chooses to battle enemy
                    Console.Clear();
                    Battles.BattleSerpent();
                    break;

                case "3":
                // Player chooses to check ingredient pouch
                    PlayerInventory.DisplayInventory();
                    SerenityLake();
                    break;

                case "4":
                // Player chooses to return to Mystic Forest
                    Program.Print("You decide to return to the Mystic Forest. \n...");
                    Program.ReadKey();
                    Console.Clear();
                    Locations.MysticForest();
                    break;
                
                case "5":
                // Player chooses to return to Lab
                    Program.Print("You decide to return to the Alchemy Lab. \n...");
                    Program.ReadKey();
                    Console.Clear();
                    AlchemyLab.Lab(); 
                    break;

                default:
                // Invalid choice and repeats
                    Program.Print("Error!", 25);
                    Console.WriteLine("[!] Please enter an integer between 1 and 5. \n...");
                    Program.ReadKey();
                    Console.Clear();
                    SerenityLake(); // Repeat the Sapphire Serenity Lake choice
                    break;
            }
        }

        public static void LunarCaverns()
        // Location = Lunar Caverns
        {
            Console.WriteLine("You arrive at the Lunar Caverns. What will you do?");
            Console.WriteLine("=================================");
            // Display Lunar Cavernsoptions
            Console.WriteLine("[1] Harvest Lunar Constituents");
            Console.WriteLine("[2] Battle Moonlight Earthpulse Golem");
            Console.WriteLine("[3] Check Ingredient Pouch");            
            Console.WriteLine("[4] Return to Starlight Meadow");
            Console.WriteLine("[5] Return to Lab");
            Console.WriteLine("=================================");
            Console.Write("Your choice: ");
            string? cavernsChoice = Console.ReadLine()?.Trim();

            switch (cavernsChoice)
            {
                case "1":
                // Player chooses to harvest ingredients
                    Harvesting.HarvestCaverns();
                    break;

                case "2":
                // Player chooses to battle enemy
                    Console.Clear();
                    Battles.BattleGolem();
                    break;

                case "3":
                // Player chooses to check ingredient pouch
                    PlayerInventory.DisplayInventory();
                    LunarCaverns();
                    break;

                case "4":
                // Player chooses to return to Starlight Meadow
                    Program.Print("You decide to return to the Starlight Meadow. \n...");
                    Program.ReadKey();
                    Locations.StarlightMeadow();
                    break;
                
                case "5":
                // Player chooses to return to Lab
                    Program.Print("You decide to return to the Alchemy Lab. \n...");
                    Program.ReadKey();
                    AlchemyLab.Lab(); 
                    break;

                default:
                // Invalid choice and repeats
                    Program.Print("Error!", 25);
                    Console.WriteLine("[!] Please enter an integer between 1 and 5. \n...");
                    Program.ReadKey();
                    Console.Clear();
                    LunarCaverns();
                    break;
            }
        }

        public static void ArcaneRuins()
        // Location = Arcane Ruines
        {
            Console.WriteLine("You arrive at the Arcane Ruins. What will you do?");
            Console.WriteLine("=================================");
            // Display Arcane Ruins options
            Console.WriteLine("[1] Harvest Arcane Remnants");
            Console.WriteLine("[2] Continue to Ethereal Summit");
            Console.WriteLine("[3] Check Ingredient Pouch");
            Console.WriteLine("[4] Return to Crossroads");
            Console.WriteLine("[5] Return to Lab");
            Console.WriteLine("=================================");
            Console.Write("Your choice: ");
            string? ruinsChoice = Console.ReadLine()?.Trim();

            switch (ruinsChoice)
            {
                // Player chooses to harvest ingredients
                case "1":
                    Harvesting.HarvestRuins();
                    break;

                case "2":
                // Player chooses to continue to Ethereal Summit
                    if (BrewPotions.brewedpotions.Contains("Frost Resistance Potion") && BrewPotions.brewedpotions.Contains("Levitas Elixir"))
                    {
                        Program.Print("You decide to continue to the Ethereal Summit. \n...");
                        Program.ReadKey();
                        Console.Clear();
                        Locations.EtherealSummit();
                    }
                    else
                    {
                        Program.Print("[You]: Woah, that summit's way up there! The air's getting thinner, and it's freezing cold.\n...");
                        Program.ReadKey();
                        Program.Print("You shiver in the biting cold wind. Climbing up there without proper preparation seems impossible.\n...");
                        Program.ReadKey();
                        Program.Print("[You]: I'll need to brew a potion to resist the cold and maybe even something to help me float up higher. \n[!] You do not have access to this location yet.\n...");
                        Program.ReadKey();
                        Console.Clear();
                        Locations.ArcaneRuins();
                    }
                    break;

                case "3":
                // Player chooses to check ingredient pouch
                    PlayerInventory.DisplayInventory();
                    ArcaneRuins();
                    break;

                case "4":
                // Player chooses to return to Crossroads
                    Program.Print("You decide to return to the Crossroads. \n...");
                    Program.ReadKey();
                    Console.Clear();
                    Locations.Crossroads();
                    break;
                
                case "5":
                // Player chooses to return to Lab
                    Program.Print("You decide to return to the Alchemy Lab. \n...");
                    Program.ReadKey();
                    Console.Clear();
                    AlchemyLab.Lab(); 
                    break;
                case "awaken":
                // Player chooses to summon the Accursed Arcane Alchemist
                    if (Quests.CosmicAegisQuestCompleted())
                    {
                        Console.Clear();
                        Quests.ArcaneAlchemist();
                    }
                    else
                    {
                        Program.Print("Error!", 25);
                        Console.WriteLine("[!] Please enter an integer between 1 and 5. \n...");
                        Program.ReadKey();
                        Console.Clear();
                        ArcaneRuins();
                    }
                    break;

                default:
                // Invalid choice and repeats
                    Program.Print("Error!", 25);
                    Console.WriteLine("[!] Please enter an integer between 1 and 5. \n...");
                    Program.ReadKey();
                    Console.Clear();
                    ArcaneRuins();
                    break;
            }
        }

        public static void EtherealSummit()
        // Location = Ethereal Summit
        {
            Console.WriteLine("You arrive at the Ethereal Summit. What will you do?");
            Console.WriteLine("=================================");
            // Display Ethereal Summit options
            Console.WriteLine("[1] Battle the Astral Enigma and Save Master Alaric (Boss Battle)");
            Console.WriteLine("[2] Return to Arcane Ruins");
            Console.WriteLine("[3] Return to Lab");
            Console.WriteLine("=================================");
            Console.Write("Your choice: ");
            string? summitChoice = Console.ReadLine()?.Trim();

            switch (summitChoice)
            {
                case "1":
                // Player chooses to battle final boss
                    Battles.BattleBoss();
                    break;

                case "2":
                // Player chooses to return to Arcane Ruins
                    Program.Print("You decide to return to the Arcane Ruins. \n...");
                    Program.ReadKey();
                    Console.Clear();
                    Locations.ArcaneRuins();
                    break;
                
                case "3":
                // Player chooses to return to Lab
                    Program.Print("You decide to return to the Alchemy Lab. \n...");
                    Program.ReadKey();
                    Console.Clear();
                    AlchemyLab.Lab(); 
                    break;

                case "2712":
                // Player enters the secret code and decides to battle the secret boss, True Astral Enigma
                if (Quests.MalevolentCrystalQuestCompleted())
                {
                    Battles.BattleSecretBoss();
                }
                else
                {
                    Program.Print("Error!", 25);
                    Console.WriteLine("[!] Please enter an integer between 1 and 3. \n...");
                    Program.ReadKey();
                    Console.Clear();
                    EtherealSummit();
                }
                break;

                default:
                // Invalid choice and repeats
                    Program.Print("Error!", 25);
                    Console.WriteLine("[!] Please enter an integer between 1 and 3. \n...");
                    Program.ReadKey();
                    Console.Clear();
                    EtherealSummit(); 
                    break;
            }
        }

        public static void Basement()
        // Location = Basement
        {
            Console.WriteLine("You are in the Basement. What will you do?");
            Console.WriteLine("=================================");
            // Display Basement options
            Console.WriteLine("[1] Search the basement");
            if (!Quests.DiaryQuestCompleted())
            {
                Console.WriteLine("[2] ???");
            }
            else
            {
                Console.WriteLine("[2] Read Master Alaric's Diary");
            }
            Console.WriteLine("[3] Talk to Silky Spider Queen");
            Console.WriteLine("[4] Go back up to Lab");
            Console.WriteLine("=================================");
            Console.Write("Your choice: ");
            string? basementChoice = Console.ReadLine()?.Trim();

            switch (basementChoice)
            {

                case "1":
                // Player chooses to search basement
                    Harvesting.SearchBasement();
                    break;

                case "2": 
                    if (!Quests.DiaryQuestCompleted())
                    {
                        // Player hasn't completed diary quest and is unable to access diary
                        Program.Print("[Mysterious Voice]: You have not unlocked this option yet. Complete Silky Spider Queen's quest to unlock.\n...");
                        Console.ReadKey();
                        Console.Clear();
                        Basement();
                    }
                    else
                    {
                        // Player chooses to read diary
                        Program.Print("You decide to read Master Alaric's Diary. \n...");
                        Program.ReadKey();
                        Console.Clear();
                        MastersDiary.DisplayDiary();
                    }
                    break;
                
                case "3":
                //Player chooses to talk to Silky Spider Queen
                    Console.Clear();
                    Quests.SpiderQueen();
                    break;

                case "4":
                // Player chooses to leave basement
                    Program.Print("You decide to leave the basement. \n...");
                    Program.ReadKey();
                    Console.Clear();
                    AlchemyLab.Lab(); 
                    break;

                
                default:
                // Invalid choice and repeats
                    Program.Print("Error!", 25);
                    Console.WriteLine("[!] Please enter an integer between 1 and 4. \n...");
                    Program.ReadKey();
                    Console.Clear();
                    Basement();
                    break;
            }
        }

    } // end class Locations

    public class Harvesting
    {
        static int harvestingSpeed = 40; // Time to harvest ingredients (40ms for each character of "Harvesting..." --> 40ms*13 = 520ms (~0.5s))
        public static void HarvestForest()
        {
            Program.Print("Harvesting...", harvestingSpeed, false); // NOT SKIPPABLE

            // Determine the harvest outcome based on probability, and displays what is harvested to player
            double harvestOutcome = new Random().NextDouble() * 100;

            if (harvestOutcome <= 10 + Program.currentPlayer.luck) // e.g. 10% (+2% if Arcane Spirit Elixir is brewed)
            {
                Console.WriteLine("You obtained 1 Mystic Roots (Rare)! \n...");
                Program.ReadKey();
                PlayerInventory.AddIngredient("Mystic Roots", 1);
            }
            else if (harvestOutcome <= 50 + Program.currentPlayer.luck) // 40% 
            {
                Console.WriteLine("You obtained 1 Firefly Essence! \n...");
                Program.ReadKey();
                PlayerInventory.AddIngredient("Firefly Essence", 1);
            }
            else if (harvestOutcome <= 80 + Program.currentPlayer.luck) // 30%
            {
                Console.WriteLine("You obtained 1 Azure Hydrangea Petal! \n...");
                Program.ReadKey();
                PlayerInventory.AddIngredient("Azure Hydrangea Petal", 1);
            }
            else // 20% (-2% if Arcane Spirit Elixir is brewed)
            {
                Console.WriteLine("Unfortunately, you found nothing during this harvest. \n...");
                Program.ReadKey();
            }
            Console.Clear();

            Locations.MysticForest();
        }

        public static void HarvestMeadow()
        {
            Program.Print("Harvesting...", harvestingSpeed, false);

            // Determine the harvest outcome based on probability, and displays what is harvested to player
            double harvestOutcome = new Random().NextDouble() * 100;

            if (harvestOutcome <= 15 + Program.currentPlayer.luck)
            {
                Console.WriteLine("You obtained 1 Nebula Dust (Rare)! \n...");
                Program.ReadKey();
                PlayerInventory.AddIngredient("Nebula Dust", 1);
            }
            else if (harvestOutcome <= 40 + Program.currentPlayer.luck)
            {
                Console.WriteLine("You obtained 1 Starlight Dew! \n...");
                Program.ReadKey();
                PlayerInventory.AddIngredient("Starlight Dew", 1);
            }
            else if (harvestOutcome <= 65 + Program.currentPlayer.luck) 
            {
                Console.WriteLine("You obtained 1 Enchanted Nectar! \n...");
                Program.ReadKey();
                PlayerInventory.AddIngredient("Enchanted Nectar", 1);
            }
            else
            {
                Console.WriteLine("Unfortunately, you found nothing during this harvest. \n...");
                Program.ReadKey();
            }
            Console.Clear();

            Locations.StarlightMeadow(); // Repeat the Starlight Meadow choice
        }

        public static void HarvestLake()
        {
            Program.Print("Harvesting...", harvestingSpeed, false);

            // Determine the harvest outcome based on probability, and displays what is harvested to player
            double harvestOutcome = new Random().NextDouble() * 100;

            if (harvestOutcome <= 30)
            {
                Console.WriteLine("You obtained 1 Seashell Essence! \n...");
                Program.ReadKey();
                PlayerInventory.AddIngredient("Seashell Essence", 1);
            }
            else if (harvestOutcome <= 50)
            {
                Console.WriteLine("You obtained 1 Abyssal Serpent Scale! \n...");
                Program.ReadKey();
                PlayerInventory.AddIngredient("Abyssal Serpent Scale", 1);
            }
            else if (harvestOutcome <= 55 + Program.currentPlayer.luck)
            {
                Console.WriteLine("You obtained 1 Holy Water (Super Rare)! \n...");
                Program.ReadKey();
                PlayerInventory.AddIngredient("Holy Water", 1);
            }
            else
            {
                Console.WriteLine("Unfortunately, you found nothing during this harvest. \n...");
                Program.ReadKey();
            }
            Console.Clear();

            Locations.SerenityLake(); 
        }

        public static void HarvestCaverns()
        {
            Program.Print("Harvesting...", harvestingSpeed, false);

            // Determine the harvest outcome based on probability, and displays what is harvested to player
            double harvestOutcome = new Random().NextDouble() * 100;

            if (harvestOutcome <= 25)
            {
                Console.WriteLine("You obtained 1 Glowing Crystal! \n...");
                Program.ReadKey();
                PlayerInventory.AddIngredient("Glowing Crystal", 1);
            }
            else if (harvestOutcome <= 60)
            {
                Console.WriteLine("You obtained 1 Lunar Moss! \n...");
                Program.ReadKey();
                PlayerInventory.AddIngredient("Lunar Moss", 1);
            }
            else if (harvestOutcome <= 65 + Program.currentPlayer.luck)
            {
                Console.WriteLine("You obtained 1 Earthpulse Stonefruit (Super Rare)! \n...");
                Program.ReadKey();
                PlayerInventory.AddIngredient("Earthpulse Stonefruit", 1);
            }
            else
            {
                Console.WriteLine("Unfortunately, you found nothing during this harvest. \n...");
                Program.ReadKey();
            }
            Console.Clear();

            Locations.LunarCaverns(); 
        }
        
        public static void HarvestRuins()
        {
            Program.Print("Harvesting...", harvestingSpeed, false);

            // Determine the harvest outcome based on probability, and displays what is harvested to player
            double harvestOutcome = new Random().NextDouble() * 100;

            if (harvestOutcome <= 30)
            {
                Console.WriteLine("You obtained 1 Ancient Glyphroot Powder! \n...");
                Program.ReadKey();
                PlayerInventory.AddIngredient("Ancient Glyphroot Powder", 1);
            }
            else if (harvestOutcome <= 55)
            {
                Console.WriteLine("You obtained 1 Icy Shadowstone Shards! \n...");
                Program.ReadKey();
                PlayerInventory.AddIngredient("Icy Shadowstone Shards", 1);
            }
            else if (harvestOutcome <= 60 + Program.currentPlayer.luck)
            {
                Console.WriteLine("You obtained 1 Arcane Relic Fragments (Super Rare)! \n...");
                Program.ReadKey();
                PlayerInventory.AddIngredient("Arcane Relic Fragments", 1);
            }
            else
            {
                Console.WriteLine("Unfortunately, you found nothing during this harvest. \n...");
                Program.ReadKey();
            }
            Console.Clear();

            Locations.ArcaneRuins(); 
        }

        public static void SearchBasement()
        {
            Program.Print("Searching...", harvestingSpeed, false);

            // Determine the harvest outcome based on probability, and displays what is harvested to player
            double harvestOutcome = new Random().NextDouble() * 100;

            if (harvestOutcome <= 36)
            {
                Console.WriteLine("You obtained 1 Bottle! \n...");
                Program.ReadKey();
                PlayerInventory.AddIngredient("Bottle", 1);
            }
            else if (harvestOutcome <= 41)
            {
                Console.WriteLine("You obtained 1 Golden Silky Spiderweb! \n...");
                Program.ReadKey();
                PlayerInventory.AddIngredient("Golden Silky Spiderweb", 1);
            }
            else if (harvestOutcome <= 43 + Program.currentPlayer.luck)
            {
                Console.WriteLine("You obtained 1 Ancient Grimoire of Fury (Legendary)! \n...");
                Program.ReadKey();
                PlayerInventory.AddIngredient("Ancient Grimoire of Fury", 1);
                Program.currentPlayer.damage += 0.1m;
                Program.currentPlayer.health += 2;
            }
            else
            {
                Console.WriteLine("Unfortunately, you were covered in tons of spiderwebs while searching. \nYou obtained 1 Silky Spiderweb. \n...");
                PlayerInventory.AddIngredient("Silky Spiderwebs", 1);
                Program.ReadKey();
            }
            Console.Clear();

            Locations.Basement(); 
        }

    } // end class Harvesting

    public class Quests
    {
        private static bool hasTalkedToQueen = false;
        private static bool hasTalkedToEldralorean = false;
        private static bool hasTalkedToSage = false;
        private static bool hasTalkedToAlchemist = false;

        // Silky Spider Queen quest
        public static void SpiderQueen()
        {
            // Check if the player has already talked to the queen
            if (!hasTalkedToQueen)
            {
                Program.Print("As you explore the basement of the alchemy lab, you stumble upon a hidden chamber.");
                Program.Print("In the chamber, a shimmering figure emerges from the shadows. \n...");
                Program.ReadKey();
                Program.Print($"[Silky Spider Queen]: Welcome, {Program.currentPlayer.name}, young apprentice of Master Alaric. I have long awaited your arrival. \n...");
                Program.ReadKey();
                Program.Print("[You]: Who are you? \n...");
                Program.ReadKey();
                Program.Print("[You]: Wait... Are you the Silky Spider Queen?! I remember master mentioning you at some point, but I never got to meet you, your majesty. \n...");
                Program.ReadKey();
                Program.Print("[Silky Spider Queen]: I am the Silky Spider Queen. Your master provided me with shelter long ago and I have resided here ever since. \n...");
                Program.ReadKey();
                Program.Print("[Silky Spider Queen]: My kingdom lies beneath the alchemy lab, connected through hidden tunnels and passageways.\n...");
                Program.ReadKey();
                Program.Print("[You]: I understand, your majesty. However, master has been captured by some unknown entity!\n...");
                Program.ReadKey();
                Program.Print($"[Silky Spider Queen]: That's atrocious! Then, I have a task for you, young apprentice {Program.currentPlayer.name}.\n...");
                Program.ReadKey();
                Program.Print("[Silky Spider Queen]: My kingdom is in need of 22 Silky Spiderwebs and 1 Golden Silky Spiderweb.\n...");
                Program.ReadKey();
                Program.Print("[Silky Spider Queen]: Should you succeed in gathering these, I shall grant you access to the secret diary of your master, which will greatly aid you in your journey.\n...");
                Program.ReadKey();
                Program.Print("[You]: Very well, your majesty, I accept your quest. I will talk to you again once I have gathered the spiderwebs.\n...");
                Program.ReadKey();
                Program.Print("[!] You must complete this quest to progress through the game. The diary contains most of the information you need to know. \n...");
                Program.ReadKey();
                // Updates that quest dialogue has already been displayed once
                hasTalkedToQueen = true;
                Console.Clear();
                Locations.Basement();   
                    
            }
    
            else
            {
                // If player has not completed quest
                if (!DiaryQuestCompleted())
                {
                    CompleteDiaryQuest();
                }

                // If player has already completed quest
                else if (DiaryQuestCompleted())
                {
                    Program.Print($"[Silky Spider Queen]: Greetings, young apprentice {Program.currentPlayer.name}. You've returned. You've already completed my quest. You can access Master Alaric's Diary here in the basement. [Secret Number One: 2]\n...");
                    Program.ReadKey();
                    Console.Clear();
                    Locations.Basement();
                }
            }
        }
        public static void CompleteDiaryQuest()
        {
            // Check if player has enough spiderwebs, is so complete the quest
            if (PlayerInventory.inventory.ContainsKey("Silky Spiderwebs") && PlayerInventory.inventory["Silky Spiderwebs"] >= 22 && PlayerInventory.inventory.ContainsKey("Golden Silky Spiderweb") && PlayerInventory.inventory["Golden Silky Spiderweb"] >= 1)
            {
                // Remove items from inventory
                PlayerInventory.inventory["Silky Spiderwebs"] -= 22;
                PlayerInventory.inventory["Golden Silky Spiderweb"] -= 1;

                // Reward player with Master Alaric's Diary
                PlayerInventory.AddIngredient("Master Alaric's Diary", 1);

                Program.Print("The Silky Spider Queen greets you once again.\n...");
                Program.ReadKey();
                Program.Print($"[Silky Spider Queen]: {Program.currentPlayer.name}, you have done a great service to my kingdom. I thank you for your valour and determination.\n...");
                Program.ReadKey();
                Program.Print("[Silky Spider Queen]: Here's your reward - Master Alaric's Diary. It will certainly be a great help to you in your journey. Farewell. [P.S. The first secret number is 2.]\n...");
                Program.ReadKey();
                Console.Clear();
                Locations.Basement();
            }
            else
            {
                Program.Print("The Silky Spider Queen greets you once again.\n...");
                Program.ReadKey();
                Program.Print("[Silky Spider Queen]: Have you gathered the spiderwebs as requested?\n...");
                Program.ReadKey();
                Program.Print("[You]: Not yet, your majesty. I still haven't collected 22 Silky Spiderwebs and a Golden Silky Spiderweb.\n...");
                Program.ReadKey();
                Console.Clear();
                Locations.Basement();
            }
        }

        public static bool DiaryQuestCompleted()
        {   
            return PlayerInventory.inventory.ContainsKey("Master Alaric's Diary");
        }


        // Mysterious Homeless Eldralorean quest
        public static void MysteriousEldralorean()
        {
            // Check if the player has already talked to the Eldralorean
            if (!hasTalkedToEldralorean)
            {
                Program.Print("As you explore the Mystic Forest, you stumble upon a hidden alcove nestled among the trees.");
                Program.Print("Within the alcove, an old, weathered figure sits hunched over a small fire, his features obscured by shadows.\n...");
                Program.ReadKey();
                Program.Print("[Mysterious Homeless Eldralorean]: Who dares disturb my solitude?\n...");
                Program.ReadKey();
                Program.Print($"[You]: My apologies, sir. I did not mean to intrude. I am {Program.currentPlayer.name}, an apprentice of Master Alaric, seeking knowledge to aid in his rescue.\n...");
                Program.ReadKey();
                Program.Print("[Mysterious Homeless Eldralorean]: Master Alaric, you say? He was a good friend of mine long ago. What happened to him?\n...");
                Program.ReadKey();
                Program.Print("[You]: Master Alaric has been kidnapped by a mysterious entity, and I seek the means to rescue him. Do you have any information that could help me?\n...");
                Program.ReadKey();
                Program.Print($"[Mysterious Homeless Eldralorean]: Hmph, I may have a task for you, young apprentice {Program.currentPlayer.name}, if you prove yourself worthy.\n...");
                Program.ReadKey();
                Program.Print("[Mysterious Homeless Eldralorean]: My stomach grumbles, and I long for a taste of roasted chicken. Bring me 3 Cursed Roasted Chickens, and I may share with you a secret of great value.\n...");
                Program.ReadKey();
                Program.Print("[You]: Very well, I shall return with the chickens. Thank you for your guidance.\n...");
                Program.ReadKey();
                Program.Print("[!] You must complete this quest to brew the Holy Elixir of Restoration in order to obtain the Good Ending. If you wish to obtain the Bad Ending, do not brew the elixir.\n...");
                Program.ReadKey();
                hasTalkedToEldralorean = true;
                Console.Clear();
                Locations.MysticForest();
            }
    
            else
            {
                // Check if the player has already completed the quest
                if (!HolyChickenQuestCompleted())
                {
                    CompleteHolyChickenQuest();   
                }
                else
                {
                    // If the player has already completed the quest
                    Program.Print($"[Mysterious Homeless Eldralorean]: Hey, {Program.currentPlayer.name}, you've returned. Here's the method to produce a Holy Roasted Chicken - obtain 7 Cursed Roasted Chickens, and type \"holychicken\" at your Cauldron. [Secret Number Two: 7]\n...");
                    Program.ReadKey();
                    Console.Clear();
                    Locations.MysticForest();
                }
        }
        }

        public static void CompleteHolyChickenQuest()
        {
            // Check if the player has enough cursed roasted chickens
            if (PlayerInventory.inventory.ContainsKey("Cursed Roasted Chicken") && PlayerInventory.inventory["Cursed Roasted Chicken"] >= 3)
            {
                // Remove the items from the player's inventory
                PlayerInventory.inventory["Cursed Roasted Chicken"] -= 3;
                // Reward the player
                PlayerInventory.AddIngredient("Necklace of Respect", 1);

                // Dialogue upon completing the quest
                Program.Print("The Mysterious Homeless Edralorean greets you once again.\n...");
                Program.ReadKey();
                Program.Print($"[Mysterious Homeless Eldralorean]: Great, {Program.currentPlayer.name}, you've returned with the cursed roasted chickens. Well done, young apprentice.\n...");
                Program.ReadKey();
                Program.Print("[Mysterious Homeless Eldralorean]: As promised, I shall share with you a secret of great value.\n...");
                Program.ReadKey();
                Program.Print("[Mysterious Homeless Eldralorean]: To produce a Holy Roasted Chicken, you must obtain 7 Cursed Roasted Chickens, and type \"holychicken\" at your Cauldron.\n...");
                Program.ReadKey();
                Program.Print("[Mysterious Homeless Eldralorean]: You will need this to brew the Holy Elixir of Restoration. May it serve you well on your journey. Talk to me again if you ever forget the recipe.\n...");
                Program.ReadKey();
                Program.Print("[Mysterious Homeless Eldralorean]: And here, a token of my respect - the Necklace of Respect. Wear it proudly, for you have earned it. [P.S. The second secret number is 7.]\n...");
                Program.ReadKey();
                Program.Print("Respect +999!\n...");
                Program.ReadKey();
                Console.Clear();
                Locations.MysticForest();
            }
            else
            {
                // If the player hasn't completed the quest yet
                Program.Print("The Mysterious Homeless Eldralorean greets you once again.\n...");
                Program.ReadKey();
                Program.Print("[Mysterious Homeless Eldralorean]: Have you gathered the 3 Cursed Roasted Chickens as requested?\n...");
                Program.ReadKey();
                Program.Print("[You]: Not yet, I still need to collect more.\n...");
                Program.ReadKey();
                Console.Clear();
                Locations.MysticForest();
            }

        }
        public static bool HolyChickenQuestCompleted()
        {   
            return PlayerInventory.inventory.ContainsKey("Necklace of Respect");
        }
    
        // Wise Celestial Sage quest
        public static void CelestialSage()
        {
            if (!hasTalkedToSage)
            {
                // Dialogue when player first encounters the Celestial Sage
                Program.Print("[Wise Celestial Sage]: Greetings, young traveler. Welcome to the Starlight Meadow, a place where the celestial bodies dance in harmony.\n...");
                Program.ReadKey();
                Program.Print($"[You]: Hello, wise sage. My name is {Program.currentPlayer.name}. I seek knowledge and guidance to aid me on my journey.\n...");
                Program.ReadKey();
                Program.Print("[Wise Celestial Sage]: Ah, knowledge is the foundation upon which the universe is built. Let me share with you the wonders of the cosmos.\n...");
                Program.ReadKey();
                Program.Print("[Wise Celestial Sage]: In our vast universe, the closest star to Earth is our very own Sun, a brilliant sphere of light and energy.\n...");
                Program.ReadKey();
                Program.Print("[Wise Celestial Sage]: Mars, known as the 'Red Planet,' captivates the imagination with its rust-colored surface and mysterious past.\n...");
                Program.ReadKey();
                Program.Print("[Wise Celestial Sage]: Among the moons that orbit the planets, Ganymede stands out as the largest in our Solar System, a moon of Jupiter.\n...");
                Program.ReadKey();
                Program.Print("[Wise Celestial Sage]: And here, in the tranquil embrace of Starlight Meadow, we find ourselves surrounded by the serene beauty of the cosmos.\n...");
                Program.ReadKey();
                Program.Print($"[Wise Celestial Sage]: Ah, remember these wonders, {Program.currentPlayer.name}, for they shall guide you on your journey.\n...");
                Program.ReadKey();
                Program.Print("[Wise Celestial Sage]: Now then, I shall impart upon you five questions to test your knowledge.\n...");
                Program.ReadKey();
                Program.Print("[Wise Celestial Sage]: Ah, answer them correctly, and you shall be rewarded. Answer incorrectly, and you must seek further enlightenment.\n...");
                Program.ReadKey();
                Program.Print("[You]: Alright, I will return to you when I am ready.\n...");
                Program.ReadKey();
                Program.Print("[!] This is an optional side quest which will contribute to the Secret Ending. Only complete this if you wish to obtain the Secret Ending. You can still obtain the Bad Ending (but not Good Ending) if you complete this while not fulfilling the conditions for the Secret Ending (defeat True Astral Enigma).\n...");
                Program.ReadKey();
                Console.Clear();
                hasTalkedToSage = true;
                Console.Clear();
                Locations.StarlightMeadow();

        
            }
            else
            {
                if (!CosmicAegisQuestCompleted())
                {
                    // If player hasn't completed the quest
                    Program.Print("The Wise Celestial Sage greets you once again.\n...");
                    Program.ReadKey();
                    Program.Print("[Wise Celestial Sage]: Ah, here are my questions. Good luck! You must answer all of them correctly to receive my reward, and you will have to restart if you get any of them wrong.\n...");
                    Program.ReadKey();
                    AskCosmicQuestions();
                }
                else
                {
                    // If the player has already completed the quest
                    Program.Print($"[Wise Celestial Sage]: Ah, you've returned, {Program.currentPlayer.name}. Do you seek more enlightenment? Remember, the Cosmic Aegis reduces enemy damage by 10%! Type \"awaken\" at the Arcane Ruins to summon the Accursed Arcane Alchemist. [Secret Number Three: 1]\n...");
                    Program.ReadKey();
                    Console.Clear();
                    Locations.StarlightMeadow();
                }
            }
        }

        public static void AskCosmicQuestions()
        {
            int questionNumber = 1;
            int correctAnswers = 0;
            bool answeredCorrectly = true;

            // Ask the questions
            while (questionNumber <= 5)
            {
                // Display the question
                string correctAnswer = DisplayCosmicQuestion(questionNumber);

                // Gets input (answer)
                Console.Write("[You]: ");
                string? playerAnswer = Console.ReadLine()?.ToLower().Trim();

                // Check if answer is correct
                if (playerAnswer == correctAnswer)
                {
                    Program.Print("[Wise Celestial Sage]: Ah, correct! You have shown great knowledge of the cosmos.\n...");
                    Program.ReadKey();
                    correctAnswers++;
                    questionNumber++;
                    Console.Clear();
                }
                else
                {
                    Program.Print("[Wise Celestial Sage]: Ah, that is incorrect.\n...");
                    Program.ReadKey();
                    Console.Clear();
                    answeredCorrectly = false;
                    break;
                }
            }

            if (answeredCorrectly && correctAnswers == 5)
            {
                // If the player has answered all questions correctly
                Program.Print($"[Wise Celestial Sage]: Ah, impressive! {Program.currentPlayer.name}, you have proven yourself to be a true seeker of knowledge.\n...");
                Program.ReadKey();
                Program.Print("[Wise Celestial Sage]: As promised, I shall grant you a reward - the Cosmic Aegis.\n...");
                Program.ReadKey();
                Program.Print("[Wise Celestial Sage]: Wear it proudly, for it shall protect you from the harshness of the cosmos.\n...");
                Program.ReadKey();
                Program.Print("Damage Resistance + 10%! [You now receive 10% less damage from all enemies.]\n...");
                Program.currentPlayer.damageResist = 0.9m;
                Program.ReadKey();
                Program.Print("[Wise Celestial Sage]: Ah, I shall also impart upon you a secret - the means to summon the Accursed Arcane Alchemist at the Arcane Ruins.\n...");
                Program.ReadKey();
                Program.Print("[Wise Celestial Sage]: Simply type the word \"awaken\" at the Arcane Ruins, and he shall appear before you.\n...");
                Program.ReadKey();
                Program.Print($"[Wise Celestial Sage]: Farewell, {Program.currentPlayer.name}. May the stars guide you on your journey. [P.S. The third secret number is 1.]\n...");
                Program.ReadKey();
                PlayerInventory.AddIngredient("Cosmic Aegis", 1);
                Console.Clear();
                Locations.StarlightMeadow();
            }
            else
            {   // Player got a question wrong and has to restart
                Program.Print("[Wise Celestial Sage]: How disappointing! You've got a question wrong... Seek further enlightenment, traveler. Return to me when you are ready to answer all my questions correctly.\n...");
                Program.ReadKey();
                Console.Clear();
                Locations.StarlightMeadow();
            }
        }

        public static string DisplayCosmicQuestion(int questionNumber)
        {
            switch (questionNumber)
            {
                case 1:
                    Program.Print("[Wise Celestial Sage]: Question 1. What is the closest star to Earth?");
                    Console.WriteLine("A) Alpha Centauri");
                    Console.WriteLine("B) Sun");
                    Console.WriteLine("C) Sirius");
                    Console.WriteLine("D) Proxima Centauri");
                    return "b";
                case 2:
                    Program.Print("[Wise Celestial Sage]: Question 2. Which planet is known as the 'Red Planet'?");
                    Console.WriteLine("A) Mars");
                    Console.WriteLine("B) Venus");
                    Console.WriteLine("C) Jupiter");
                    Console.WriteLine("D) Saturn");
                    return "a";
                case 3:
                    Program.Print("[Wise Celestial Sage]: Question 3. What is the name of the largest moon in the Solar System?");
                    Console.WriteLine("A) Europa");
                    Console.WriteLine("B) Titan");
                    Console.WriteLine("C) Ganymede");
                    Console.WriteLine("D) Io");
                    return "c";
                case 4:
                    Program.Print("[Wise Celestial Sage]: What is the current question number?");
                    Console.WriteLine("A) 5");
                    Console.WriteLine("B) 4");
                    Console.WriteLine("C) 6");
                    Console.WriteLine("D) 3");
                    return "b";
                case 5:
                    Program.Print("[Wise Celestial Sage]: Question 5. What is the name of the current location?");
                    Console.WriteLine("A) Moonlight Forest");
                    Console.WriteLine("B) Starlight Forest");
                    Console.WriteLine("C) Moonlight Meadow");
                    Console.WriteLine("D) Starlight Meadow");
                    return "d";
                default:
                    return "";
            }
        }

        public static bool CosmicAegisQuestCompleted()
        {   
            return PlayerInventory.inventory.ContainsKey("Cosmic Aegis");
        }

        // Accursed Arcane Alchemist quest
        public static void ArcaneAlchemist()
        {
            if (!hasTalkedToAlchemist) // Initial dialogue with NPC
            {
                Program.Print("As you chant the word \"awaken,\" a profound silence fills the air, followed by a faint whisper echoing through the arcane ruins. \n...");
                Program.ReadKey();
                Program.Print("Suddenly, a ripple of dark energy spreads across the chamber, fusing into the form of a mysterious figure draped in tattered robes.\n...");
                Program.ReadKey();
                Program.Print("[Accursed Arcane Alchemist]: Ah, you must have been acknowledged by the Wise Celestial Sage. Why have you summoned me?\n...");
                Program.ReadKey();
                Program.Print($"[You]: I, {Program.currentPlayer.name}, seek further knowledge and power to aid me on my journey.\n...");
                Program.ReadKey();
                Program.Print("[Accursed Arcane Alchemist]: Knowledge and power are intertwined in the fabric of the universe. I can offer you both, but at a cost.\n...");
                Program.ReadKey();
                Program.Print("[Accursed Arcane Alchemist]: To unlock the secrets of the Malevolent Doombringer Crystal, you must first prove your worth.\n...");
                Program.ReadKey();
                Program.Print("[Accursed Arcane Alchemist]: I have been cursed by the True Astral Enigma, and only the Holy Elixir of Restoration can dispell it.\n...");
                Program.ReadKey();
                Program.Print("[Accursed Arcane Alchemist]: Bring me the Holy Elixir of Restoration, a potion of divine origin that holds great power. [You will keep the stats increases from the Holy Elixir of Restoration.]\n...");
                Program.ReadKey();
                Program.Print("[Accursed Arcane Alchemist]: In return for it, I shall bestow upon you the Malevolent Doombringer Crystal, a relic of formidable strength.\n...");
                Program.ReadKey();
                Program.Print("[Accursed Arcane Alchemist]: But beware, for with great power comes great peril. The True Astral Enigma awaits, and only the Malevolent Elixir of Calamity can stand against it.\n...");
                Program.ReadKey();
                Program.Print($"[Accursed Arcane Alchemist]: Will you accept this quest, {Program.currentPlayer.name}? Only return to me if you wish to accept. (Type \"awaken\" again to summon.)\n...");
                Program.ReadKey();
                Program.Print("[!] This is an optional side quest which will contribute to the Secret Ending. Only complete this if you wish to obtain the Secret Ending. (You can still obtain the Good Ending if you complete this and defeat the normal Astral Enigma instead of the True one.)\n...");
                Console.Clear();
                hasTalkedToAlchemist = true; // Update that the player has talked to the alchemist
                Locations.ArcaneRuins();
                
                
            }

            else
            {
                if (!MalevolentCrystalQuestCompleted()) // If quest hasn't been completed yet
                {
                    CompleteArcaneAlchemistQuest();
                }

                 else
                {    // If quest is already completed
                    Program.Print($"[Blessed Arcane Alchemist]: I appreciate your help, {Program.currentPlayer.name}. My curse has been dispelled thanks to you. Remember, you must obtain 1 Ancient Grimoire of Fury, 1 Poseidon's Grimoire of Fury and 1 Gaia's Grimoire of Fury, along with the crystal I gave you, to brew the Malevolent Elixir of Calamity.\n...");
                    Program.ReadKey();
                    Program.Print("[Blessed Arcane Alchemist]: Also, to battle the True Astral Enigma, type the secret numbers you obtained in order (from the quests), at the Ethereal Summit.");
                    Console.Clear();
                    Program.Print("[Example: 1st number: 1, 2nd: 2, 3rd: 3, 4th: 4 --> Secret code = 1234 --> type 1234 at the Ethereal Summit. (This is just an example. The actual secret code isn't 1234.)]");
                    Console.Clear();
                    Locations.ArcaneRuins();
                }
            }
        }

        public static void CompleteArcaneAlchemistQuest()
        {
            if (BrewPotions.brewedpotions.Contains("Holy Elixir of Restoration"))
            {
                // Remove Holy Elixir of Restoration from brewed potions
                BrewPotions.brewedpotions.Remove("Holy Elixir of Restoration");
                // Reward player with the Malevolent Doombringer Crystal
                PlayerInventory.AddIngredient("Malevolent Doombringer Crystal", 1);
                Program.Print("The Accursed Arcane Alchemist greets you once again.\n...");
                Program.ReadKey();
                Program.Print($"[Accursed Arcane Alchemist]: {Program.currentPlayer.name}, you have returned with the Holy Elixir of Restoration. Excellent!\n...");
                Program.ReadKey();
                Program.Print("[Accursed Arcane Alchemist]: As promised, here is the Malevolent Doombringer Crystal, a relic of great power.\n...");
                Program.ReadKey();
                Program.Print("Damage +2!");
                Program.Print("Health +50!");
                Program.Print("Hit rate +1%\n...");
                Program.currentPlayer.damage += 2;
                Program.currentPlayer.health += 50;
                Program.currentPlayer.hitrate +=1;
                Program.ReadKey();
                Program.Print("[Accursed Arcane Alchemist]: But remember, its true potential can only be unlocked with the Malevolent Elixir of Calamity.\n...");
                Program.ReadKey();
                Program.Print("[Accursed Arcane Alchemist]: To brew it, you will need to throw one of each grimoire (1 Ancient Grimoire of Fury, 1 Poseidon's Grimoire of Fury, 1 Gaia's Grimoire of Fury), along with the Malevolent Doombringer Crystal, into your Cauldron.\n...");
                Program.ReadKey();
                Program.Print("[Accursed Arcane Alchemist]: Simply type \"secret\" at the Cauldron to begin the brewing process. [You will keep the stats increases from the Malevolent Doombringer Crystal.]\n...");
                Program.ReadKey();
                Program.Print("[Accursed Arcane Alchemist]: Also, the real perpetrator who captured your master is the True Astral Enigma. He is the one who cursed me. The normal Astral Enigma is simply a clone of him.\n...");
                Program.ReadKey();
                Program.Print("[Accursed Arcane Alchemist]: If you wish to battle him, type the sequence of numbers you received after every quest, at the Ethereal Summit. By the way, the forth secret number is 2.\n...");
                Program.ReadKey();
                Program.Print("[Accursed Arcane Alchemist]: For example, if the first secret number is 1; second is 2; third is 3; forth is 4, type \"1234\" at the Ethereal Summit to summon the True Astral Enigma.\n...");
                Program.ReadKey();
                Program.Print("[Accursed Arcane Alchemist]: However, use caution. The True Astral Enigma is not to be underestimated.\n...");
                Program.ReadKey();
                Program.Print("[Accursed Arcane Alchemist]: I highly recommend you to brew the Malevolent Elixir of Calamity before battling him, or else you will most likely fail.\n...");
                Program.ReadKey();
                Program.Print($"[Accursed Arcane Alchemist]: Farewell, {Program.currentPlayer.name}, and may the cosmos guide your path.\n...");
                Program.ReadKey();
                Console.Clear();
                Locations.ArcaneRuins();
            }
            else
            {   
                Program.Print("The Accursed Arcane Alchemist greets you once again.\n...");
                Program.ReadKey();
                Program.Print("[Accursed Arcane Alchemist]: Return to me when you possess the Holy Elixir of Restoration.\n...");
                Program.ReadKey();
                Console.Clear();
                Locations.ArcaneRuins();
            }
        }

        public static bool MalevolentCrystalQuestCompleted()
        {
            if (PlayerInventory.inventory.ContainsKey("Malevolent Doombringer Crystal") || BrewPotions.brewedpotions.Contains("Malevolent Elixir of Calamity"))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    } // end class Quests
}