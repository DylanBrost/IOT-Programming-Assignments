using System;
using System.IO;

internal class Program
{
    static string playerName;                           //global variables
    static int playerHealth = 100;

    static bool hasKey = false;
    static bool hasTorch = false;
    static bool hasPotion = false;
    static bool treasureFound = false;

    static string[] inventory = new string[3];
    static int itemCount = 0;

    static string folderPath = "C:\\IOT";

    public static void Main(string[] args)
    {
        Directory.CreateDirectory(folderPath);

        bool playAgain = true;

        while (playAgain)
        {
            ResetStats();
            StartGame();

            SceneBeach();

            Console.WriteLine("\nType 'y' or 'yes' to restart the game, or anything else to exit.)");
            string response = Console.ReadLine();
            response = response.ToLower();
            playAgain = (response == "y" || response == "yes");
        }
    }

    public static void LogStatus()                                          //log status method
    {
        string fullPath = folderPath + "\\" + playerName + ".txt";

        string csvLine = $"{playerHealth},{hasKey},{hasTorch},{treasureFound},{itemCount},{inventory[0]},{inventory[1]},{inventory[2]}";

        using (StreamWriter writer = new StreamWriter(fullPath, true))
        {
            writer.WriteLine(csvLine);
        }

        Console.WriteLine($"\nSTATUS LOGGED Saved to {fullPath}");
        Console.WriteLine("Press any key to continue...");
        Console.ReadKey();
    }

    public static void DisplayStatus()
    {
        Console.WriteLine($"\n>>> {playerName} Status <<<");
        Console.WriteLine($"Health: {playerHealth}");
        Console.WriteLine("Inventory:");

        if (itemCount == 0)
        {
            Console.WriteLine("Empty");
        }
        else
        {
            for (int i = 0; i < itemCount; i++)
            {
                Console.WriteLine(i + 1 + ". " + inventory[i]);
            }
        }

    }
    public static void ResetStats()
    {

        playerName = "";                                    //Put the player stats into a method
        playerHealth = 100;
        hasTorch = false;
        hasKey = false;
        hasPotion = false;
        treasureFound = false;
        inventory = new string[3];
        itemCount = 0;

    }
    public static void StartGame()
    {
        Console.Clear();
        Console.WriteLine("======== Welcome to the \"The Island of the Lost Treasure!\" ========");
        Console.WriteLine("\nYou have just woke up and relize you are stranded on this uncharted island.");                             //What the player sees on entering the game
        Console.WriteLine("Your memory is a little fuzzy but you recall being on a ship before everthing goes blank.");
        Console.WriteLine("\nDo you remember your Name?(Enter in your name): ");

        playerName = Console.ReadLine();

        if (playerName == "")
        {
            Console.WriteLine("WELL GARY IT IS THEN....");                                                                              //if player dosent enter a name, there are named Gary by default
            playerName = "GARY";                                                                                                       // and they end there life and get a gameover
        }
        else
        {
            Console.WriteLine($"Welcome {playerName}, hope you live longer then the last guy!");
        }
        DisplayStatus();
        Console.ReadKey();          // Started using ReadKey over Readline based on feedback from last presentation
    }

    public static void SceneBeach()                                     //First scene
    {
        
        while (true)
        {
            Console.Clear();
            Console.WriteLine(">>> The Beach <<<");
            Console.WriteLine($"\nAfter finally remembering your name \"{playerName}\", You look around at your surrounding's as you feel the sand between your toes.");
            DisplayStatus();

            if (playerName == "GARY")
            {
                Console.WriteLine($"\nThen it hits you that your name is \"{playerName}\", You feel a weird sensation, so you turn right back around, to the ocean you came from....never to be seen again.");
                Console.ReadKey();
                EndGame();
                return; 
            }
            else
            {
                Console.WriteLine("\nYou decide you need scout this part of the island, if you are going to survive.");
                Console.WriteLine("You notice two potential area's to start with the Cliffside or the Jungle.");
                Console.WriteLine("\n1. Jungle");
                Console.WriteLine("2. Cliffside");
                Console.WriteLine("3. Log Status");
                Console.Write("\nEnter choice: ");

                string input = Console.ReadLine();
                int choice;

                if (!int.TryParse(input, out choice) || choice < 1 || choice > 3)
                {
                    continue; 
                }

                if (choice == 3)
                {
                    LogStatus();
                    continue; 
                }
                else if (choice == 1)
                {
                    Console.WriteLine("\nYou choose to set off for the Jungle.");
                    Console.WriteLine("As you approach you can already hear howling noises and see darting eyes piercing towards you.");
                    Console.WriteLine("You are already starting to regret your decision, but theres no turning back now....");
                    Console.WriteLine("\nPress any Key to continue the adventure into the Jungle.");
                    Console.ReadKey();
                    SceneJungle();
                    return; 
                }
                else
                {
                    Console.WriteLine("\nYou decide to search the Cliffside...");
                    Console.WriteLine("As Your walking towards the Cliffside, you start to relize how towering these cliffs really are.");
                    Console.WriteLine("\"These will be great for getting an overlay of the island!\" a quick thought to yourself.");
                    Console.WriteLine("You press onward...");
                    Console.WriteLine("Press any key to continue the adventure.");
                    Console.ReadKey();
                    SceneCliffSide();
                    return; 
                }
            }
        }
    }

    public static void SceneJungle()                        // Jungle scene
    {
        while (true)
        {
            Console.Clear();
            Console.WriteLine(">>> The Jungle <<<");
            Console.WriteLine("\nYou push your way into the dense jungle.");
            Console.WriteLine("While tredging through this accursed jungle, you trip over a stick.");
            Console.WriteLine("Thankfully all it injures is your ego.");
            Console.WriteLine("You notice that the further you push the darker the jungle gets.");
            Console.WriteLine("You look back at the stick, thinking...");
            DisplayStatus();

            Console.WriteLine("\nWhat are you thinking?");
            Console.WriteLine("1. Break the stick off and use it as a torch as you push further in?");
            Console.WriteLine("2. Just keep pushing on.");
            Console.WriteLine("3. Log Status"); 
            Console.Write("\nEnter choice: ");

            string input = Console.ReadLine();
            int choice;

            if (!int.TryParse(input, out choice) || choice < 1 || choice > 3)
            {
                continue;
            }

            if (choice == 3)
            {
                LogStatus();
                continue;
            }
            else if (choice == 1)
            {
                Console.WriteLine("\nYou snap the stick off and reach for it!");
                if (itemCount < 3)
                {
                    if (!hasTorch)
                    {
                        inventory[itemCount] = "Torch";
                        itemCount++;
                        hasTorch = true;
                        Console.WriteLine("You pick up the stick and light it. (added to inventory)");
                    }
                    else
                    {
                        Console.WriteLine("You already have a perfectly fine stick. You toss it and continue on.");
                    }
                }
                else
                {
                    Console.WriteLine("Your inventory is full. You leave the stick.");
                }
                Console.WriteLine("\nYou head for what looks like a cave entrance.");
                Console.WriteLine("Press any key to continue...");
                Console.ReadKey();
                SceneCave();
                return;
            }
            else
            {
                Console.WriteLine("\nYou keep pushing on into the darkness with no sense of direction and trip again!");
                Console.WriteLine("This time you not so lucky and fall into a pit.");
                playerHealth -= 30;
                Console.WriteLine("You climb out wounded (-30 health).");

                if (playerHealth <= 0)
                {
                    Console.WriteLine("You are gravely injured...");
                    Console.WriteLine("Press Enter to continue...");
                    Console.ReadLine();
                    EndGame();
                    return;
                }
                Console.WriteLine("\nYou make your way to the edge of the jungle and see the towering cliffside in front of you.");
                Console.WriteLine("Press any key to continue...");
                Console.ReadKey();
                SceneCliffSide();
                return;
            }
        }
    }

    public static void SceneCliffSide()         //cliff scene
    {
        while (true)
        {
            Console.Clear();
            Console.WriteLine(">>> The Cliffside <<<");
            Console.WriteLine("\nYou arrive at a towering cliff overlooking the island. The wind is strong and loud the closer you get.");
            DisplayStatus();

            Console.WriteLine("\nWhat do you do?");
            Console.WriteLine("1. Search the cliff base.");
            Console.WriteLine("2. Climb up towards a narrow path to what looks like a village?");
            Console.WriteLine("3. Log Status"); 
            Console.Write("\nEnter choice: ");

            string input = Console.ReadLine();
            int choice;

            if (!int.TryParse(input, out choice) || choice < 1 || choice > 3)
            {
                continue;
            }

            if (choice == 3)
            {
                LogStatus();
                continue;
            }
            else if (choice == 1)
            {
                Console.WriteLine("\nAs your searching around the cliff base you notice a glint amoung the rocks, so you decide to approach.");
                Console.WriteLine("The glint turns out to be a silver key. You wonder what it could open!");
                if (itemCount < 3)
                {
                    if (!hasKey)
                    {
                        inventory[itemCount] = "Key";
                        itemCount++;
                        hasKey = true;
                        Console.WriteLine("You picked up a Key. (added to inventory)");
                    }
                    else
                    {
                        Console.WriteLine("You already have a Key and its not as shiny as your current key.");
                    }
                }
                else
                {
                    Console.WriteLine("Your inventory is full. You leave the Key.");
                }
                Console.WriteLine("\nYou see the Jungle in front of you and decide to head into it.");
                Console.WriteLine("Press any key to continue...");
                Console.ReadKey();
                SceneJungle();
                return;
            }
            else
            {
                Console.WriteLine("\nYou attempt the climb up. The path is slippery and dark with the overhanging rock outcroppings.");
                if (!hasTorch)
                {
                    Console.WriteLine("Without a light, you slip and land on your back, sustaining a heavy injury!");
                    playerHealth -= 70;
                    Console.WriteLine("You manage to crawl back up (-70 health).");
                    if (playerHealth <= 0)
                    {
                        Console.WriteLine("Your injuries are too severe...");
                        Console.WriteLine("Press Enter to continue...");
                        Console.ReadLine();
                        EndGame();
                        return;
                    }
                    Console.WriteLine("Your to injured to climb up without the right tool so you head for the jungle");
                    Console.WriteLine("\nPress any key to continue...");
                    Console.ReadKey();
                    SceneJungle();
                    return;
                }
                else
                {
                    Console.WriteLine("Using your light, you climb up safely towards the village.");
                    Console.WriteLine("\nPress any key to continue to the village...");
                    Console.ReadKey();
                    SceneVillage();
                    return;
                }
            }
        }
    }

    public static void SceneCave()      //Cave scene
    {
        while (true)
        {
            Console.Clear();
            Console.WriteLine(">>> The Dark Cave <<<");
            Console.WriteLine("\nYou stand at the mouth of a cold, dripping cave. It feels creepy and quiet.");
            DisplayStatus();

            if (hasPotion)
            {
                Console.WriteLine("\n[AUTO] You decide to prepare before entering and you consume your Potion");
                playerHealth += 30;
                itemCount--;
                hasPotion = false;
                DisplayStatus();
            }

            Console.WriteLine("\nWhat do you want to do?");
            Console.WriteLine("1. Enter the cave and see what it contains.");
            Console.WriteLine("2. Return to the island (go back)");
            Console.WriteLine("3. Log Status"); 
            Console.Write("\nEnter choice: ");

            string input = Console.ReadLine();
            int choice;

            if (!int.TryParse(input, out choice) || choice < 1 || choice > 3)
            {
                continue;
            }

            if (choice == 3)
            {
                LogStatus();
                continue;
            }
            else if (choice == 2)
            {
                Console.WriteLine("\nYou decide to step away and gather more resources before attempting.");
                Console.WriteLine("Press any key to continue...");
                Console.ReadKey();
                SceneCliffSide();
                return;
            }
            else                                                                                    // Choice 1
            {
                Console.WriteLine("\nYou move carefully into the cave...");

                if (hasTorch && hasKey)
                {
                    Console.WriteLine("Your light reveals an old locked chest. You use the key to open it.");
                    Console.WriteLine("Inside glitters the Lost Treasure! You have found it!");
                    treasureFound = true;
                    Console.WriteLine("\nPress any key to see the final outcome...");
                    Console.ReadKey();
                    EndGame();
                    return;
                }
                else if (hasTorch && !hasKey)
                {
                    Console.WriteLine("You can see a locked chest, but it is chained shut. You need a key to open it.");
                    Console.WriteLine("While searching the cave you disturb loose rock and a small rockfall injures you.");
                    playerHealth -= 40;
                    Console.WriteLine("You escape the cave injured (-40 health).");
                    if (playerHealth <= 0)
                    {
                        Console.WriteLine("You were badly injured and could not survive...");
                        Console.WriteLine("Press any key to continue...");
                        Console.ReadKey();
                        EndGame();
                        return;
                    }
                    Console.WriteLine("\nPress any key to continue...");
                    Console.ReadKey();
                    SceneVillage();
                    return;
                }
                else if (!hasTorch && hasKey)
                {
                    Console.WriteLine("You have the key but no lightsource. The cave is pitch black.");
                    Console.WriteLine("You try to feel your way and trip, impaling yourself badly on a stalagmite.");
                    playerHealth -= 50;
                    Console.WriteLine("You leave the cave bleeding baddly (-50 health).");
                    if (playerHealth <= 0)
                    {
                        Console.WriteLine("You bleedout...");
                        Console.WriteLine("Press any key to continue...");
                        Console.ReadKey();
                        EndGame();
                        return;
                    }
                    Console.WriteLine("\nYou find an alternative cave entrance leading out to what looks like a village.");
                    Console.WriteLine("Press any key to continue...");
                    Console.ReadKey();
                    SceneVillage();
                    return;
                }
                else
                {
                    Console.WriteLine("You enter the dark cave with no supplies.");
                    Console.WriteLine("In the darkness you fall into a deep crevice and are seriously hurt.");
                    playerHealth -= 60;
                    Console.WriteLine("You barely escape (-60 health).");
                    if (playerHealth <= 0)
                    {
                        Console.WriteLine("You did not survive the fall...");
                        Console.WriteLine("Press any key to continue...");
                        Console.ReadKey();
                        EndGame();
                        return;
                    }
                    Console.WriteLine("\nYou find an alternative cave entrance leading out to what looks like a village.");
                    Console.WriteLine("Press any key to continue...");
                    Console.ReadKey();
                    SceneVillage();
                    return;
                }
            }
        }
    }

    public static void SceneVillage()           //Village scene
    {
        while (true)
        {
            Console.Clear();
            Console.WriteLine(">>> The Village <<<");
            Console.WriteLine("\nYou find a dark creepy village with a smoking hut. A lone hermit watches from the doorway.");
            DisplayStatus();

            Console.WriteLine("\nWhat do you do?");
            Console.WriteLine("1. Talk to the hermit");
            Console.WriteLine("2. Search the village for useful items");
            Console.WriteLine("3. Go back to the beach");
            Console.WriteLine("4. Log Status");
            Console.Write("\nEnter choice: ");

            string input = Console.ReadLine();
            int choice;

            
            if (!int.TryParse(input, out choice) || choice < 1 || choice > 4)
            {
                continue;
            }

            if (choice == 4)
            {
                LogStatus();
                continue;
            }
            else if (choice == 3)
            {
                Console.WriteLine("\nYou head back to the beach to re-evaluate your options.");
                Console.WriteLine("Press any key to continue...");
                Console.ReadKey();
                SceneBeach();
                return;
            }
            else if (choice == 1)
            {
                Console.WriteLine("\nThe hermit is strange, but offers a small lantern and a clue about the cave.");
                if (!hasTorch && itemCount < 3)
                {
                    inventory[itemCount] = "Lantern";
                    itemCount++;
                    hasTorch = true;
                    Console.WriteLine("You accept the Lantern.");
                }
                else if (!hasTorch)
                {
                    Console.WriteLine("Your inventory is full and you cannot take the Lantern.");
                }
                else
                {
                    Console.WriteLine("You already have a light source.");
                }

                Console.WriteLine("\nThe hermit says: \"Shine and Glow will lead you true, when exploring dark places.\"");
                Console.WriteLine("\nPress any key to continue...");
                Console.ReadKey();
                SceneCave();
                return;
            }
            else                                                                                                    // Choice 2
            {
                Console.WriteLine("\nYou search around and find a small healing potion on its side in a corner.");
                if (itemCount < 3)
                {
                    inventory[itemCount] = "potion";            //health item
                    itemCount++;
                    hasPotion = true;
                    Console.WriteLine("You picked up the potion.");
                }
                else
                {
                    Console.WriteLine("Your inventory is full. You leave the potion.");
                }
                Console.WriteLine("\nPress any key to continue...");
                Console.ReadKey();
                SceneCave();
                return;
            }
        }
    }

    public static void EndGame()        //What player sees when triggering endgame method
    {
        Console.Clear();
        Console.WriteLine("----- GAME OVER -----");
        Console.WriteLine("\n");
        DisplayStatus();

        if (treasureFound)
        {
            Console.WriteLine($"\nWell done, {playerName}! You found the Lost Treasure!");
        }
        else if (playerHealth <= 0)
        {
            Console.WriteLine($"\n{playerName}. You have joined the many souls lost in search of the treasure.");
        }
        else
        {
            Console.WriteLine($"\nGame ended. You survived with {playerHealth} health.");
            if (!treasureFound)
            {
                Console.WriteLine("You did not find the treasure this time.");
            }
        }

        Console.WriteLine("\nPress any key to continue...");
        Console.ReadKey();
    }
}