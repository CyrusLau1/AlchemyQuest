using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;
using System.Security;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;


namespace AlchemyQuest
{

    public class Player
    {   
        // Player stats
        public string? name;
        public decimal damage = 1;
        public decimal health = 50;
        public decimal hitrate = 67;
        public int fireballStrength = 1;
        
        public static void Stats()
        {
            Console.Clear();
            // Display player stats (health, damage, hitrate)
            Program.Print($"{Program.currentPlayer.name}'s Stats");
            Console.WriteLine("=================================");
            Console.WriteLine($"Damage: {Program.currentPlayer.damage * Program.currentPlayer.fireballStrength}");
            Console.WriteLine($"Health: {Program.currentPlayer.health}");
            Console.WriteLine($"Hit Rate: {Program.currentPlayer.hitrate}%");
            Console.WriteLine("=================================");
            Console.Write("Press Enter to return...");
            Console.ReadKey();
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

        public static void Print(string text, int speed = 1)
    {
        // Text "animation"
        foreach (char c in text)
        {
            Console.Write(c);
            System.Threading.Thread.Sleep(speed);
        }
        Console.WriteLine();
    }
        public static void Start()
        {   
            // Welcome player and ask for name
            Print("Welcome to Alchemy Quest!");
            Console.Write("Enter your name: ");
            currentPlayer.name = Console.ReadLine();
            if (currentPlayer.name == "" || currentPlayer.name is null)
            {
                // Sets default name if player doesn't enter a name
                currentPlayer.name = "Cirala"; // Alaric spelled backwards
                Print($"In the magical land of Eldralore, you are a keen apprentice to the wise alchemist, Master Alaric. Master named you {currentPlayer.name} when he found you in the forest ten years ago.");
            }
            else if (currentPlayer.name.Length > 20)
            {
                // Error if player name is too long, repeats
                Print("[!] Your name is too long. Max length: 20 characters. \n...");
                Console.ReadKey();
                Console.Clear();
                Start();
            }
            else
            {
                Print($"In the magical land of Eldralore, you, {currentPlayer.name}, are a keen apprentice to the wise alchemist, Master Alaric.");
            }
            // Generate game lore
            Console.WriteLine("Press Enter to continue...");
            Console.ReadKey();
            Console.Clear();
            Print("One day, a malicious creature called the Astral Enigma appears. It's after something in Master Alaric's experiments and ends up cursing him, taking him to a mysterious place called the Ethereal Summit. \n...");
            Console.ReadLine();
            Print("Now, with your master missing and under a curse, you are on a mission. You start at the Alchemy Lab, armed with your master's teachings, to figure out the curse, defeat the Astral Enigma, and rescue him. It's your time to shine, apprentice! \n...");
            Console.ReadLine();
            Print("Get ready to explore the realm of Eldralore, gather ingredients from different magical locations, and brew potions to strengthen yourself to defeat enemies in the hope of saving Master Alaric and keeping Eldralore safe. Let the adventure begin! \n...");
            Console.ReadLine();
            Console.Clear();
            
            // Start at the Lab
            AlchemyLab.Lab();
        
        }

        
    } // end class Program

    public class AlchemyLab
    {
        // Location = Alchemy Lab
        public static void Lab()
        {
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
                Console.Write("Your choice: ");
                string? labChoice = Console.ReadLine();
                Console.Clear();

                if (labChoice == "1")
                {
                    // Player chooses to brew potions
                    Program.Print("You walk to the cauldron to brew potions. \n...");
                    Console.ReadKey();
                    Console.Clear();
                    BrewPotions.Cauldron();
                    
                
                    }
                
                else if (labChoice =="2")
                {
                    // Player chooses to enter Basement
                    Program.Print("You enter the mysterious Basement. \n...");
                    Console.ReadKey();
                    Console.Clear();
                    Locations.Basement();
                    
                }

                else if (labChoice == "3")
                {   
                    // Player chooses to check inventory
                    Program.Print("You look inside your magical ingredient pouch. \n...");
                    Console.ReadKey();
                    PlayerInventory.DisplayInventory();
                    Lab();
                    
                }

                else if (labChoice == "4")
                {
                    // Player chooses to check brewed potions
                    Program.Print("You try to remember which potions you have brewed. \n...");
                    Console.ReadKey();
                    BrewPotions.BrewedPotions();
                   
                }

                else if (labChoice == "5")
                {
                    // Player chooses to check stats
                    Program.Print("You stand on a magic circle to check your stats. \n...");
                    Console.ReadKey();
                    Player.Stats();
                    
                }

                else if (labChoice == "6")
                {
                    // Player chooses to exit Lab
                    Program.Print("As you exit the Lab, you slowly approach the Crossroads. \n...");
                    Console.ReadKey();
                    Console.Clear();
                    Locations.Crossroads();
                }

                else{
                    // Invalid choice
                    Program.Print("Error!", 25);
                    Console.WriteLine("Please enter an integer from 1 to 6. \n...");
                    Console.ReadKey();
                    Console.Clear();
                }
            }
        }
    } // end class AlchemyLab


    public class BrewPotions
    {
        // Potion brewing
        // List of potions in cauldron
        public static List<string> cauldron = new List<string> {"Noctis Vision Brew", "Aqua Breather Potion", "Naturae Blessing Brew", "Dark Pyroblast Infusion", "Arcane Spirit Elixir", "Levitas Elixir", "Frost Resistance Potion", "Holy Elixir of Restoration"};
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

        private static Dictionary<string, int> holyElixirIngredients = new Dictionary<string, int>
        {
            // Dictionary of Holy Elixir of Restoration required ingredients
            { "Bottle", 1 },
            { "Arcane Relic Fragments", 1 },
            { "Earthpulse Stonefruit" , 1 },
            { "Holy Water", 1 },
            { "Mystic Roots", 3 },
            { "Nebula Dust", 4 },
            { "Roasted Chicken", 5}
        };

        public static void Cauldron()
        {
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
                string? brewChoice = Console.ReadLine();

                if (brewChoice == "1" & !CanBrewPotion(noctisVisionIngredients) || brewChoice == "2" & !CanBrewPotion(aquaBreatherIngredients) || brewChoice == "3" & !CanBrewPotion(naturaeBlessingIngredients) || brewChoice == "4" & !CanBrewPotion(darkPyroblastIngredients) || brewChoice == "5" & !CanBrewPotion(arcaneSpiritIngredients) || brewChoice == "6" & !CanBrewPotion(levitasIngredients) || brewChoice == "7" & !CanBrewPotion(frostResistanceIngredients) || brewChoice == "8" & !CanBrewPotion(holyElixirIngredients)) // ADD || OTHER POTIONS
                {
                    // Not enough ingredients
                    Console.Clear();
                    Program.Print("Failure!", 50);
                    Console.WriteLine("You do not have enough ingredients to brew this potion.\n...");
                    Console.ReadKey();
                    Console.Clear();
                }

                else if (brewChoice == "1" & cauldron[0] == "Noctis Vision Brew")
                {
                    // Player has enough ingredients to brew the Noctis Vision Brew
                    Program.Print("Success!", 50);
                    Console.WriteLine("You have brewed the Noctis Vision Brew. You have gained night vision and are now able to explore the Lunar Caverns.");
                    Console.WriteLine("Hit rate +5%! \n...");
                    Program.currentPlayer.hitrate += 5;
                    PlayerInventory.inventory["Bottle"] -= 1;
                    PlayerInventory.inventory["Firefly Essence"] -= 4;
                    PlayerInventory.inventory["Starlight Dew"] -= 4;
                    Console.ReadKey();
                    Console.Clear();
                    cauldron[0] = "X Brewed X";
                    brewedpotions.Add("Noctis Vision Brew");
                }


                    // ADD SITUATIONS FOR NOT ENOUGH INGREDIENTS


                else if (brewChoice == "2" & cauldron[1] == "Aqua Breather Potion")
                {
                    // Player has enough ingredients to brew the Aqua Breather Potion
                    Program.Print("Success!", 50);
                    Console.WriteLine("You have brewed the Aqua Breather Potion. You have gained water breathing are now able to explore the Sapphire Serenity Lake.");
                    Console.WriteLine("Health +25! \n...");
                    Program.currentPlayer.health += 25;
                    Console.ReadKey();
                    Console.Clear();
                    cauldron[1] = "X Brewed X";
                    brewedpotions.Add("Aqua Breather Potion");
                }

                else if (brewChoice == "3" & cauldron[2] == "Naturae Blessing Brew")
                {
                    // Player has enough ingredients to brew the Naturae Blessing Brew
                    Program.Print("Success!", 50);
                    Console.WriteLine("You have brewed the Naturae Blessing Brew. You have received a blessing from Mother Nature.");
                    Console.WriteLine("Damage +2!");
                    Console.WriteLine("Health +50! \n...");
                    Program.currentPlayer.damage += 2;
                    Program.currentPlayer.health += 50;
                    Console.ReadKey();
                    Console.Clear();
                    cauldron[2] = "X Brewed X";
                    brewedpotions.Add("Naturae Blessing Brew");
                }

                else if (brewChoice == "4" & cauldron[3] == "Dark Pyroblast Infusion")
                {
                    // Player has enough ingredients to brew the Dark Pyroblast Infusion
                    Program.Print("Success!", 50);
                    Console.WriteLine("You have brewed the Dark Pyroblast Infusion. You can now shoot much stronger and more accurate fireballs.");
                    Console.WriteLine("Hitrate +10%!");
                    Console.WriteLine("All attacks deal 2x damage! \n...");
                    Program.currentPlayer.hitrate += 10;
                    Program.currentPlayer.fireballStrength += 1;
                    Console.ReadKey();
                    Console.Clear();
                    cauldron[3] = "X Brewed X";
                    brewedpotions.Add("Dark Pyroblast Infusion");
                }

                else if (brewChoice == "5" & cauldron[4] == "Arcane Spirit Elixir")
                {
                    // Player has enough ingredients to brew the Arcane Spirit Elixir
                    Program.Print("Success!", 50);
                    Console.WriteLine("You have brewed the Arcane Spirit Elixir. You have summoned the Arcane Spirit, which will aid you in your journey through the Arcane Ruins.");
                    Console.WriteLine("Damage +1!");
                    Console.WriteLine("Health +30!");
                    Console.WriteLine("Hitrate +5%! \n...");
                    Program.currentPlayer.health += 30;
                    Program.currentPlayer.damage += 1;
                    Program.currentPlayer.hitrate += 5;
                    Console.ReadKey();
                    Console.Clear();
                    cauldron[4] = "X Brewed X";
                    brewedpotions.Add("Arcane Spirit Elixir");
                }

                else if (brewChoice == "6" & cauldron[5] == "Levitas Elixir")
                {
                    // Player has enough ingredients to brew the Levitas Elixir
                    Program.Print("Success!", 50);
                    Console.WriteLine("You have brewed the Levitas Elixir. You can now levitate at will, and will be able to continue your journey up to the Ethereal Summit, if the Frost Resistance Potion is also brewed.");
                    Console.WriteLine("Health +30! \n...");
                    Program.currentPlayer.health += 30;
                    Console.ReadKey();
                    Console.Clear();
                    cauldron[5] = "X Brewed X";
                    brewedpotions.Add("Levitas Elixir");
                }

                else if (brewChoice == "7" & cauldron[6] == "Frost Resistance Potion")
                {
                    // Player has enough ingredients to brew the Frost Resistance Potion
                    Program.Print("Success!", 50);
                    Console.WriteLine("You have brewed the Frost Resistance Potion. You have gained frost resistance, and will be able to continue your journey up to the Ethereal Summit, if the Levitas Elixir is also brewed.");
                    Console.WriteLine("Damage +3! \n...");
                    Program.currentPlayer.damage += 3;
                    Console.ReadKey();
                    Console.Clear();
                    cauldron[6] = "X Brewed X";
                    brewedpotions.Add("Frost Resistance Potion");
                }

                else if (brewChoice == "8" & cauldron[7] == "Holy Elixir of Restoration")
                {
                    // Player has enough ingredients to brew the Holy Elixir of Restoration
                    Program.Print("Success!", 50);
                    Console.WriteLine("You have finally brewed the Holy Elixir of Restoration! The heavens have bestowed upon you a holy blessing, and you will be able to save Master Alaric by disspelling his curse after defeating his kidnapper.");
                    Console.WriteLine("Damage +1!");
                    Console.WriteLine("Health +15! \n...");
                    Program.currentPlayer.damage += 1;
                    Program.currentPlayer.health += 15;
                    Console.ReadKey();
                    Console.Clear();
                    cauldron[7] = "X Brewed X";
                    brewedpotions.Add("Holy Elixir of Restoration");
                }

                else if (brewChoice == "1" & cauldron[0] == "X Brewed X" || brewChoice == "2" & cauldron[1] == "X Brewed X" || brewChoice == "3" & cauldron[2] == "X Brewed X" || brewChoice == "4" & cauldron[3] == "X Brewed X" || brewChoice == "5" & cauldron[4] == "X Brewed X" || brewChoice == "6" & cauldron[5] == "X Brewed X" || brewChoice == "7" & cauldron[6] == "X Brewed X" || brewChoice == "8" & cauldron[7] == "X Brewed X")
                {
                    // Player has already brewed the potion
                    Program.Print("Failure!", 50);
                    Console.WriteLine("You have already brewed this potion! You can only brew each potion once.\n...");
                    Console.ReadKey();
                    Console.Clear();
                }

                else if (brewChoice == "9")
                {
                    // Player chooses to return
                    Console.Clear();
                    Program.Print("You walk away from the cauldron. \n...");
                    Console.ReadKey();
                    Console.Clear();
                    AlchemyLab.Lab();
            
                }

                else if (brewChoice != "1" || brewChoice != "2" ||brewChoice != "3" || brewChoice != "4" || brewChoice != "5" || brewChoice != "6" || brewChoice != "7" || brewChoice != "8" || brewChoice != "9")
                { 
                    // Invalid choice
                    Program.Print("Error!", 25);
                    Console.WriteLine("Please enter an integer between 1 and 9.\n...");
                    Console.ReadKey();
                    Console.Clear();
                    
                }

                
            
            }

        }


        public static void BrewedPotions()
        {   
            Console.Clear();
            // Display potions already brewed by player
            Console.WriteLine($"{Program.currentPlayer.name}'s Potions");
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
            Console.ReadKey();
            Console.Clear();
            AlchemyLab.Lab();
        }

    } // end class BrewPotions


    public class Battles
    {
        // Battle Crazy Cursed Chicken
        public static void BattleChicken()
        {
            Program.Print("You encounter a weird-looking chicken on the Crossroads. What will you do?");
            
            Combat("Crazy Cursed Chicken", 2, 5, 66);
            Locations.Crossroads();
        }

        // Battle Holy Abyssal Serpent
        public static void BattleSerpent()
        {
            Program.Print("You encounter a Holy Abyssal Serpent at the Sapphire Serenity Lake. What will you do?");
            
            Combat("Holy Abyssal Serpent", 14, 40, 66);
            Locations.SerenityLake();
        }

        // Battle Moonlight Earthpulse Golem
        public static void BattleGolem()
        {
            Program.Print("You encounter a Moonlight Earthpulse Golem in the Lunar Caverns. What will you do?");
            
            Combat("Moonlight Earthpulse Golem", 12, 88, 35);
            Locations.LunarCaverns();
        }

        // Battle Astral Enigma
        public static void BattleBoss()
        {
            Program.Print("You finally encounter the Astral Enigma. What will you do?");
            
            Combat("Astral Enigma", 12, 200, 72);
            
        }

        public static void Combat(string name, decimal damageE, decimal healthE, decimal hitrateE)
        {
            // Enemy stats
            string n ="";
            decimal d = 0;
            decimal h = 0;
            decimal r = 0;
            
            n = name;
            d = damageE;
            h = healthE;
            r = hitrateE;
            
            // Player stat adjustments
            decimal attack = Program.currentPlayer.damage * Program.currentPlayer.fireballStrength;
            decimal currenthealth = Program.currentPlayer.health;
            // Battle enemies
            while(h > 0)
            {   
                // Display battle situation (player and enemy health) + player options
                Console.WriteLine($"[{Program.currentPlayer.name}] {currenthealth}/{Program.currentPlayer.health} HP  |  [{n}] {h}/{healthE} HP");
                Console.WriteLine("=====================================");
                Console.WriteLine("Choose an option:");
                Console.WriteLine($"[1] Cast Fireball (DMG: {attack})");
                Console.WriteLine($"[2] Mana Shield (DMG: {attack / 4} + Enemy DMG -75%)");
                Console.WriteLine("[3] Flee Battle");
                Console.WriteLine("=====================================");
                Console.Write("Your choice: ");
                Random rnd = new Random();
                int hitChance = rnd.Next(1, 101);
                int enemyHitChance = rnd.Next(1, 101);
                int fleeChance = rnd.Next(1, 101);
                string? input = Console.ReadLine();
                if(input == "1" & hitChance <= Program.currentPlayer.hitrate & enemyHitChance <= r)
                {   
                    // Both player and enemy attack successful
                    Console.WriteLine($"With haste, you summon a blazing fireball in your hands and hurl it towards the {n}! Furiously, the {n} strikes you back with a heavy blow.");
                    Console.WriteLine($"You deal {attack} damage to the {n}, and lose {d} health from its attack.");
                    currenthealth -= d;
                    h -= attack;
                }
                else if (input == "1" & hitChance > Program.currentPlayer.hitrate & enemyHitChance <= r)
                {
                    // Player attack missed and enemy attack successful
                    Console.WriteLine($"With haste, you summon a blazing fireball in your hands and hurl it towards the {n}. Unfortunately, your fireball missed it, and it strikes you back with a heavy blow.");
                    Console.WriteLine($"You lose {d} health from its attack.");
                    currenthealth -= d;
                }
                else if (input == "1" & hitChance <= Program.currentPlayer.hitrate & enemyHitChance > r)
                {
                    // Player attack successful and enemy attack missed
                    Console.WriteLine($"With haste, you summon a blazing fireball in your hands and hurl it towards the {n}. Furiously, the {n} tried to strike you back and missed its attack.");
                    Console.WriteLine($"You deal {attack} damage to the {n}.");
                    h -= attack;
                }
                else if (input == "1" & hitChance > Program.currentPlayer.hitrate & enemyHitChance > r)
                {
                    // Both player and enemy attack missed
                    Console.WriteLine("You both missed your attacks! How unfortunate!");
                }
                else if(input == "2" & hitChance <= Program.currentPlayer.hitrate & enemyHitChance <= r)
                {   
                    // Both player and enemy attack successful; player casts shield
                    Console.WriteLine($"With haste, you cast a mana shield around you and punch the {n}! Furiously, the {n} strikes you back, and your mana shield protects you.");
                    Console.WriteLine($"You deal {attack / 4} damage to the {n}, and lose {d / 4} health from its attack.");
                    currenthealth -= d / 4;
                    h -= attack / 4;
                }
                else if (input == "2" & hitChance > Program.currentPlayer.hitrate & enemyHitChance <= r)
                {
                    // Player casts shield and misses attack; enemy attack successful
                    Console.WriteLine($"With haste, you cast a mana shield around you and try to punch the {n}! Unfortunately, you missed your punch. Furiously, the {n} strikes you back, but your mana shield protects you.");
                    Console.WriteLine($"You lose {d / 4} health from its attack.");
                    currenthealth -= d / 4;
                }
                else if (input == "2" & hitChance <= Program.currentPlayer.hitrate & enemyHitChance > r)
                {
                    // Player casts shield and attacks successfully; enemy attack missed
                    Console.WriteLine($"With haste, you cast a mana shield around you and punch the {n}! Furiously, the {n} tries to strike you but misses its attack, leaving your mana shield useless.");
                    Console.WriteLine($"You deal {attack / 4} damage to the {n}.");
                    h -= attack / 4;
                }
                else if (input == "2" & hitChance > Program.currentPlayer.hitrate & enemyHitChance > r)
                {
                    // Both player and enemy attack missed;
                    Console.WriteLine("You casted a mana shield, but you both missed your attacks! How unfortunate!");
                }
                else if(input == "3")
                {   
                    // Player chooses to flee
                    if (fleeChance <= 75)
                    {
                        // Successfully flees and goes back to original location
                        Console.WriteLine("You used Master Alaric's Shadow Movement Technique and successfully fled the battle!");
                        Console.WriteLine("You heal yourself with a healing spell ...");
                        Console.ReadKey();
                        break;
                    }
                    else if (fleeChance > 75)
                    {
                        // Fails to flee and continues battle
                        Console.WriteLine($"As you sprint away from the {n}, its strike catches you in the back, sending you sprawling onto the ground.");
                        Console.WriteLine($"You lose {d} health from its attack and failed to escape.");
                        currenthealth -= d;
        
                    }
                else
                {   
                    // Invalid choice
                    Program.Print("Error!", 25);
                    Console.WriteLine("Please enter an integer from 1 to 3.");
                    Console.ReadKey();
                    
                }

                }
                Console.WriteLine("...");
                Console.ReadKey();
                Console.Clear();
                if (currenthealth <= 0)
                {
                    // Player dies
                    Program.Print($"GAME OVER... You have been slain by the {n}...", 150);
                    Console.ReadKey();
                    System.Environment.Exit(0);

                }

            }

            if (h <=0)
            {
                // Player defeats enemy
                Program.Print($"Victory! You have defeated the {n}!", 50);
                if (n == "Crazy Cursed Chicken")
                {
                    // Drop item
                    Program.Print("You obtained 1 Roasted Chicken!");
                    PlayerInventory.AddIngredient("Crazy Cursed Chicken", 1);
                    Console.WriteLine($"You heal yourself back to max health with a healing spell. \n...");
                    Console.ReadKey();
                    Console.Clear();
                }
                else if (n == "Holy Abyssal Serpent")
                {   
                    double dropOutcome = new Random().NextDouble() * 100;
                    if (dropOutcome <= 80)
                    {
                        // Normal enemy drop
                        Console.WriteLine("You obtained 1 Holy Abyssal Serpent's Fangs!");
                        PlayerInventory.AddIngredient("Holy Abyssal Serpent's Fangs", 1);
                        Console.WriteLine($"You heal yourself back to max health with a healing spell. \n...");
                        Console.ReadKey();
                        Console.Clear();
                    }
                    else
                    {
                        // Rare drop
                        Console.WriteLine("You obtained 1 Poseidon's Grimoire of Fury!");
                        PlayerInventory.AddIngredient("Poseidon's Grimoire of Fury", 1);
                        Console.WriteLine("Damage +0.3!");
                        Console.WriteLine("Health +4!");
                        Console.WriteLine("Hitrate +0.5%!");
                        Console.WriteLine($"You heal yourself back to max health with a healing spell. \n...");
                        Program.currentPlayer.damage += 0.3m;
                        Program.currentPlayer.health += 4;
                        Program.currentPlayer.hitrate += 0.5m;
                        Console.ReadKey();
                        Console.Clear();

                    }
                }
                else if (n == "Moonlight Earthpulse Golem")
                {   
                    double dropOutcome = new Random().NextDouble() * 100;
                    if (dropOutcome <= 80)
                    {
                        // Normal enemy drop
                        Console.WriteLine("You obtained 1 Moonlight Earthpulse Golem Core!");
                        PlayerInventory.AddIngredient("Moonlight Earthpulse Golem Core", 1);
                        Console.WriteLine($"You heal yourself back to max health with a healing spell. \n...");
                        Console.ReadKey();
                        Console.Clear();
                    }
                    else
                    {
                        // Rare drop
                        Console.WriteLine("You obtained 1 Gaia's Grimoire of Fury!");
                        PlayerInventory.AddIngredient("Gaia's Grimoire of Fury", 1);
                        Console.WriteLine("Damage +0.2!");
                        Console.WriteLine("Health +6!");
                        Console.WriteLine("Hitrate +0.5%!");
                        Console.WriteLine($"You heal yourself back to max health with a healing spell. \n...");
                        Program.currentPlayer.damage += 0.2m;
                        Program.currentPlayer.health += 6;
                        Program.currentPlayer.hitrate += 0.5m;
                        Console.ReadKey();
                        Console.Clear();

                    }
                }
                else if (n == "Astral Enigma")
                {   
                    if (BrewPotions.cauldron[7] == "X Brewed X")
                    {
                        // If the Holy Elixir of Restoration is brewed
                        // GOOD ENDING
                    }
                    else
                    {
                        // BAD ENDING

                    }
                }
            }
        }
            
 
    }

     // end class Battles

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
            Console.ReadKey();
            Console.Clear();
        }
    } // end class PlayerInventory

    public class Locations
    {
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
            string? crossroadsChoice = Console.ReadLine();

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
                    Console.ReadKey();
                    Console.Clear();
                    MysticForest();
                    break;

                case "3":
                // Player chooses to go to Starlight Meadow
                    Program.Print("You decide to continue to the Starlight Meadow. \n...");
                    Console.ReadKey();
                    Console.Clear();
                    StarlightMeadow(); 
                    break;

                case "4":
                // Player chooses to go to Arcane Ruines
                    if (BrewPotions.cauldron[4] == "X Brewed X")
                    {
                        // Player has brewed required potion and continues 
                        Program.Print("You decide to continue to the Arcane Ruins. \n...");
                        Console.ReadKey();
                        Console.Clear();
                        ArcaneRuins(); 
                    }
                    else 
                    {
                        // Player has restricted entry as potion not brewed
                        Program.Print("[!] You are unable to access this location.");
                        Console.WriteLine("You must brew the Arcane Spirit Elixir before adventuring into the Arcane Ruins. \n...");
                        Console.ReadKey();
                        Console.Clear();
                        Crossroads();
                    }
                    break;

                case "5":
                // Player chooses to return to Lab
                    Console.Clear();
                    Program.Print("You decide to return to the Alchemy Lab. \n...");
                    Console.ReadKey();
                    Console.Clear();
                    AlchemyLab.Lab();
                    break;

                default:
                // Invalid choice and repeats
                    Program.Print("Error!", 25);
                    Console.WriteLine("Please enter a number from 1 to 5. \n...");
                    Console.ReadKey();
                    Console.Clear();
                    Crossroads();
                    break;
            }
        }

        public static void MysticForest()
        // Location = Mystic Forest
        {
            Console.WriteLine("You arrive at the Mystic Forest. What will you do?");
            Console.WriteLine("=================================");

            // Display Mystic Forest options
            Console.WriteLine("[1] Harvest Mystical Herbs");
            Console.WriteLine("[2] Continue to explore the Sapphire Serenity Lake");
            Console.WriteLine("[3] Check Ingredient Pouch");
            Console.WriteLine("[4] Return to Crossroads");
            Console.WriteLine("[5] Return to Lab");
            Console.WriteLine("=================================");
            Console.Write("Your choice: ");
            string? forestChoice = Console.ReadLine();

            switch (forestChoice)
            {
                case "1":
                // Player chooses to harvest ingredients
                    Harvesting.HarvestForest();
                    break;

                case "2":
                // Player chooses to go to Sapphire Serenity Lake
                    if (BrewPotions.cauldron[1] == "X Brewed X")
                    {
                        // Player has brewed required potion and continues
                        Program.Print("You decide to continue to the Sapphire Serenity Lake. \n...");
                        Console.ReadKey();
                        Console.Clear();
                        SerenityLake(); 
                    }
                    else  
                    {
                        // Player has restricted entry as potion not brewed
                        Program.Print("[!] You are unable to access this location.");
                        Console.WriteLine("You must brew the Aqua Breather Potion before adventuring into the Sapphire Serenity Lake. \n...");
                        Console.ReadKey();
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
                // Player chooses to return to Crossroads
                    Program.Print("You decide to return to the Crossroads. \n...");
                    Console.ReadKey();
                    Console.Clear();
                    Locations.Crossroads();
                    break;
                
                case "5":
                // Player chooses to return to Lab
                    Program.Print("You decide to return to the Alchemy Lab. \n...");
                    Console.ReadKey();
                    Console.Clear();
                    AlchemyLab.Lab(); 
                    break;

                default:
                // Invalid choice and repeats
                    Program.Print("Error!", 25);
                    Console.WriteLine("Please enter a number from 1 to 5. \n...");
                    Console.ReadKey();
                    Console.Clear();
                    MysticForest();
                    break;
            }
        }

        public static void StarlightMeadow()
        // Location = Starlight Meadow
        {
            Console.WriteLine("You arrive at the Starlight Meadow. What will you do?");
            Console.WriteLine("=================================");
            // Display Starlight Meadow options
            Console.WriteLine("[1] Harvest Starlight Matter");
            Console.WriteLine("[2] Continue to explore the Lunar Caverns");
            Console.WriteLine("[3] Check Ingredient Pouch");
            Console.WriteLine("[4] Return to Crossroads");
            Console.WriteLine("[5] Return to Lab");
            Console.WriteLine("=================================");
            Console.Write("Your choice: ");
            string? meadowChoice = Console.ReadLine();

            switch (meadowChoice)
            {
                case "1":
                // Player chooses to harvest ingredients
                    Harvesting.HarvestMeadow();
                    break;

                case "2":
                // Player chooses to continue to Lunar Caverns
                    if (BrewPotions.cauldron[0] == "X Brewed X")
                    {
                        // Player has brewed required potion and continues
                        Program.Print("You decide to continue to the Lunar Caverns. \n...");
                        Console.ReadKey();
                        Console.Clear();
                        LunarCaverns(); 
                    }
                    else 
                    {
                        // Player has restricted entry as potion not brewed
                        Program.Print("[!] You are unable to access this location.");
                        Console.WriteLine("You must brew the Noctis Vision Brew before adventuring into the Lunar Caverns. \n...");
                        Console.ReadKey();
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
                // Player chooses to return to Crossroads
                    Console.Clear();
                    Program.Print("You decide to return to the Crossroads. \n...");
                    Console.ReadKey();
                    Console.Clear();
                    Locations.Crossroads();
                    break;
                
                case "5":
                // Player chooses to return to Lab
                    Console.Clear();
                    Program.Print("You decide to return to the Alchemy Lab. \n...");
                    Console.ReadKey();
                    Console.Clear();
                    AlchemyLab.Lab(); 
                    break;

                default:
                // Invalid choice and repeats
                    Program.Print("Error!", 25);
                    Console.WriteLine("Please enter a number from 1 to 5. \n...");
                    Console.ReadKey();
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
            string? lakeChoice = Console.ReadLine();

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
                    Console.ReadKey();
                    Console.Clear();
                    Locations.MysticForest();
                    break;
                
                case "5":
                // Player chooses to return to Lab
                    Program.Print("You decide to return to the Alchemy Lab. \n...");
                    Console.ReadKey();
                    Console.Clear();
                    AlchemyLab.Lab(); 
                    break;

                default:
                // Invalid choice and repeats
                    Program.Print("Error!", 25);
                    Console.WriteLine("Please enter a number from 1 to 5. \n...");
                    Console.ReadKey();
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
            string? cavernsChoice = Console.ReadLine();

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
                    Console.ReadKey();
                    Locations.StarlightMeadow();
                    break;
                
                case "5":
                // Player chooses to return to Lab
                    Console.Clear();
                    Program.Print("You decide to return to the Alchemy Lab. \n...");
                    Console.ReadKey();
                    AlchemyLab.Lab(); 
                    break;

                default:
                // Invalid choice and repeats
                    Program.Print("Error!", 25);
                    Console.WriteLine("Please enter a number from 1 to 5. \n...");
                    Console.ReadKey();
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
            string? ruinsChoice = Console.ReadLine();

            switch (ruinsChoice)
            {
                // Player chooses to harvest ingredients
                case "1":
                    Harvesting.HarvestRuins();
                    break;

                case "2":
                // Player chooses to continue to Ethereal Summit
                    Console.Clear();
                    Program.Print("You decide to continue to the Ethereal Summit. \n...");
                    Console.ReadKey();
                    Console.Clear();
                    Locations.EtherealSummit();
                    break;

                case "3":
                // Player chooses to check ingredient pouch
                    PlayerInventory.DisplayInventory();
                    ArcaneRuins();
                    break;

                case "4":
                // Player chooses to return to Crossroads
                    Console.Clear();
                    Program.Print("You decide to return to the Crossroads. \n...");
                    Console.ReadKey();
                    Console.Clear();
                    Locations.Crossroads();
                    break;
                
                case "5":
                //// Player chooses to return to Lab
                    Console.Clear();
                    Program.Print("You decide to return to the Alchemy Lab. \n...");
                    Console.ReadKey();
                    Console.Clear();
                    AlchemyLab.Lab(); 
                    break;

                default:
                // Invalid choice and repeats
                    Program.Print("Error!", 25);
                    Console.WriteLine("Please enter a number from 1 to 5. \n...");
                    Console.ReadKey();
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
            Console.WriteLine("[1] Battle the Astral Enigma and Save Master Alaric");
            Console.WriteLine("[2] Return to Arcane Ruins");
            Console.WriteLine("[3] Return to Lab");
            Console.WriteLine("=================================");
            Console.Write("Your choice: ");
            string? summitChoice = Console.ReadLine();

            switch (summitChoice)
            {
                case "1":
                // Player chooses to battle final boss
                    Battles.BattleBoss();
                    break;

                case "2":
                // Player chooses to return to Arcane Ruins
                    Program.Print("You decide to return to the Arcane Ruins. \n...");
                    Console.ReadKey();
                    Console.Clear();
                    Locations.ArcaneRuins();
                    break;
                
                case "3":
                // Player chooses to return to Lab
                    Program.Print("You decide to return to the Alchemy Lab. \n...");
                    Console.ReadKey();
                    Console.Clear();
                    AlchemyLab.Lab(); 
                    break;

                default:
                // Invalid choice and repeats
                    Program.Print("Error!", 25);
                    Console.WriteLine("Please enter a number from 1 to 3. \n...");
                    Console.ReadKey();
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
            Console.WriteLine("[2] Read Master Alaric's Diary");
            Console.WriteLine("[3] Go back up to Lab");
            Console.WriteLine("=================================");
            Console.Write("Your choice: ");
            string? basementChoice = Console.ReadLine();

            switch (basementChoice)
            {

                case "1":
                // Player chooses to search basement
                    Harvesting.SearchBasement();
                    break;

                case "2":
                // Player chooses to read diary
                    Program.Print("You found Master Alaric's Diary and decide to read it. \n...");
                    Console.ReadKey();
                    Console.Clear();
                    Locations.ArcaneRuins(); // PLACEHOLDER FOR DIARY
                    break;
                
                case "3":
                // Player chooses to leave basement
                    Program.Print("You decide to leave the basement. \n...");
                    Console.ReadKey();
                    Console.Clear();
                    AlchemyLab.Lab(); 
                    break;

                default:
                // Invalid choice and repeats
                    Program.Print("Error!", 25);
                    Console.WriteLine("Please enter a number from 1 to 3. \n...");
                    Console.ReadKey();
                    Console.Clear();
                    Basement();
                    break;
            }
        }

    } // end class Locations

    public class Harvesting
    {
        public static void HarvestForest()
        {
            Program.Print("Harvesting...", 40);

            // Determine the harvest outcome based on probability, and displays what is harvested to player
            double harvestOutcome = new Random().NextDouble() * 100;

            if (harvestOutcome <= 10) // e.g. 10%
            {
                Console.WriteLine("You obtained 1 Mystic Roots! \n...");
                Console.ReadKey();
                PlayerInventory.AddIngredient("Mystic Roots", 1);
            }
            else if (harvestOutcome <= 50) // 40%
            {
                Console.WriteLine("You obtained 1 Firefly Essence! \n...");
                Console.ReadKey();
                PlayerInventory.AddIngredient("Firefly Essence", 1);
            }
            else if (harvestOutcome <= 80) // 30%
            {
                Console.WriteLine("You obtained 1 Azure Hydrangea Petal! \n...");
                Console.ReadKey();
                PlayerInventory.AddIngredient("Azure Hydrangea Petal", 1);
            }
            else // 20%
            {
                Console.WriteLine("Unfortunately, you found nothing during this harvest. \n...");
                Console.ReadKey();
            }
            Console.Clear();

            Locations.MysticForest();
        }

        public static void HarvestMeadow()
        {
            Program.Print("Harvesting...", 40);

            // Determine the harvest outcome based on probability, and displays what is harvested to player
            double harvestOutcome = new Random().NextDouble() * 100;

            if (harvestOutcome <= 15)
            {
                Console.WriteLine("You obtained 1 Nebula Dust! \n...");
                Console.ReadKey();
                PlayerInventory.AddIngredient("Nebula Dust", 1);
            }
            else if (harvestOutcome <= 40)
            {
                Console.WriteLine("You obtained 1 Starlight Dew! \n...");
                Console.ReadKey();
                PlayerInventory.AddIngredient("Starlight Dew", 1);
            }
            else if (harvestOutcome <= 65)
            {
                Console.WriteLine("You obtained 1 Enchanted Nectar! \n...");
                Console.ReadKey();
                PlayerInventory.AddIngredient("Enchanted Nectar", 1);
            }
            else
            {
                Console.WriteLine("Unfortunately, you found nothing during this harvest. \n...");
                Console.ReadKey();
            }
            Console.Clear();

            Locations.StarlightMeadow(); // Repeat the Starlight Meadow choice
        }

        public static void HarvestLake()
        {
            Program.Print("Harvesting...", 40);

            // Determine the harvest outcome based on probability, and displays what is harvested to player
            double harvestOutcome = new Random().NextDouble() * 100;

            if (harvestOutcome <= 30)
            {
                Console.WriteLine("You obtained 1 Seashell Essence! \n...");
                Console.ReadKey();
                PlayerInventory.AddIngredient("Seashell Essence", 1);
            }
            else if (harvestOutcome <= 50)
            {
                Console.WriteLine("You obtained 1 Abyssal Serpent Scale! \n...");
                Console.ReadKey();
                PlayerInventory.AddIngredient("Abyssal Serpent Scale", 1);
            }
            else if (harvestOutcome <= 55)
            {
                Console.WriteLine("You obtained 1 Holy Water! \n...");
                Console.ReadKey();
                PlayerInventory.AddIngredient("Holy Water", 1);
            }
            else
            {
                Console.WriteLine("Unfortunately, you found nothing during this harvest. \n...");
                Console.ReadKey();
            }
            Console.Clear();

            Locations.SerenityLake(); 
        }

        public static void HarvestCaverns()
        {
            Program.Print("Harvesting...", 40);

            // Determine the harvest outcome based on probability, and displays what is harvested to player
            double harvestOutcome = new Random().NextDouble() * 100;

            if (harvestOutcome <= 25)
            {
                Console.WriteLine("You obtained 1 Glowing Crystal! \n...");
                Console.ReadKey();
                PlayerInventory.AddIngredient("Glowing Crystal", 1);
            }
            else if (harvestOutcome <= 60)
            {
                Console.WriteLine("You obtained 1 Lunar Moss! \n...");
                Console.ReadKey();
                PlayerInventory.AddIngredient("Lunar Moss", 1);
            }
            else if (harvestOutcome <= 65)
            {
                Console.WriteLine("You obtained 1 Earthpulse Stonefruit! \n...");
                Console.ReadKey();
                PlayerInventory.AddIngredient("Earthpulse Stonefruit", 1);
            }
            else
            {
                Console.WriteLine("Unfortunately, you found nothing during this harvest. \n...");
                Console.ReadKey();
            }
            Console.Clear();

            Locations.LunarCaverns(); 
        }
        
        public static void HarvestRuins()
        {
            Program.Print("Harvesting...", 40);

            // Determine the harvest outcome based on probability, and displays what is harvested to player
            double harvestOutcome = new Random().NextDouble() * 100;

            if (harvestOutcome <= 30)
            {
                Console.WriteLine("You obtained 1 Ancient Glyphroot Powder! \n...");
                Console.ReadKey();
                PlayerInventory.AddIngredient("Ancient Glyphroot Powder", 1);
            }
            else if (harvestOutcome <= 55)
            {
                Console.WriteLine("You obtained 1 Icy Shadowstone Shards! \n...");
                Console.ReadKey();
                PlayerInventory.AddIngredient("Icy Shadowstone Shards", 1);
            }
            else if (harvestOutcome <= 60)
            {
                Console.WriteLine("You obtained 1 Arcane Relic Fragments! \n...");
                Console.ReadKey();
                PlayerInventory.AddIngredient("Arcane Relic Fragments", 1);
            }
            else
            {
                Console.WriteLine("Unfortunately, you found nothing during this harvest. \n...");
                Console.ReadKey();
            }
            Console.Clear();

            Locations.ArcaneRuins(); 
        }

        public static void SearchBasement()
        {
            Program.Print("Searching...", 40);

            // Determine the harvest outcome based on probability, and displays what is harvested to player
            double harvestOutcome = new Random().NextDouble() * 100;

            if (harvestOutcome <= 36)
            {
                Console.WriteLine("You obtained 1 Bottle! \n...");
                Console.ReadKey();
                PlayerInventory.AddIngredient("Bottle", 1);
            }
            else if (harvestOutcome <= 41)
            {
                Console.WriteLine("You obtained 1 Golden Silky Spiderweb! \n...");
                Console.ReadKey();
                PlayerInventory.AddIngredient("Golden Silky Spiderweb", 1);
            }
            else if (harvestOutcome <= 43)
            {
                Console.WriteLine("You obtained 1 Ancient Grimoire of Fury! \n...");
                Console.ReadKey();
                PlayerInventory.AddIngredient("Ancient Grimoire of Fury", 1);
                Program.currentPlayer.damage += 0.1m;
                Program.currentPlayer.health += 2;
            }
            else
            {
                Console.WriteLine("Unfortunately, you were covered in tons of spiderwebs while searching. \n...");
                PlayerInventory.AddIngredient("Silky Spiderwebs", 1);
                Console.ReadKey();
            }
            Console.Clear();

            Locations.Basement(); 
        }

    } // end class Harvesting
}
    