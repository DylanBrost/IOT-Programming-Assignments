using System;
using System.IO;                                            

internal class Program                                      // TO Do List Manager.
{
    static string[] toDoList = new string[10];              // The string array and int variable are outside the main body so the other methods can see them.
    static int taskCount = 0;

    static string folderPath = "C:\\IOT";

    public static void Main(string[] args)
    {
        Directory.CreateDirectory(folderPath);

        DisplayMenu();
    }

    public static void DisplayMenu()
    {
        bool mainMenu = true;

        while (mainMenu)
        {
            Console.Clear();
            Console.WriteLine("\n=== TODO List Manager ===");                      // The main display menu, which prompts the user to make a menu choice. 
            Console.WriteLine("\n1. Add a task");
            Console.WriteLine("2. View all tasks");
            Console.WriteLine("3. Mark task as complete");
            Console.WriteLine("4. Delete a task");
            Console.WriteLine("5. View incomplete tasks only");
            Console.WriteLine("6. Export to File");
            Console.WriteLine("7. Import from File");
            Console.WriteLine("8. Exit");
            Console.WriteLine("\n=======================");

            Console.Write("\n\nEnter Menu choice: ");
            string decision = Console.ReadLine();
            int choice;

            while (!int.TryParse(decision, out choice) || choice < 1 || choice > 8)
            {
                Console.Clear();
                Console.Write("Invalid choice. Please enter a number between 1 and 8: ");
                decision = Console.ReadLine();
            }

            switch (choice)                                                                // The switch conditional is used to call the methods based on the users choices.
            {
                case 1:
                    Console.Clear();
                    AddTask();
                    break;
                case 2:
                    Console.Clear();
                    ViewAllTasks();
                    break;
                case 3:
                    Console.Clear();
                    MarkTaskComplete();
                    break;
                case 4:
                    Console.Clear();
                    DeleteTask();
                    break;
                case 5:
                    Console.Clear();
                    ViewIncompleteTask();
                    break;
                case 6:
                    Console.Clear();
                    ExportTasks();
                    break;
                case 7:
                    Console.Clear();
                    ImportTasks();
                    break;
                case 8:
                    mainMenu = false;
                    break;
            }
        }
    }

    public static void AddTask()
    {                                                                           // Method for adding task to the users list, while also letting them know if they hit
        if (taskCount >= 10)                                                   // the array cap. Should they hit enter with out typing anything they will get a error.
        {
            Console.WriteLine("Your task list is full!");
            Console.ReadLine();
            return;
        }

        Console.Write("Enter a new task: ");
        string newTask = Console.ReadLine();

        while (newTask == "")
        {
            Console.WriteLine("Your input is invalid, please enter a task:");
            newTask = Console.ReadLine();
        }

        toDoList[taskCount] = newTask;
        taskCount++;

        Console.Clear();
        Console.WriteLine($"{newTask} has been added.");
        Console.WriteLine("\nHit Enter to return...");
        Console.ReadLine();
    }

    public static void ViewAllTasks()                                       // Allows the User to view all tasks, also letting them know if there are no tasks added.
    {
        if (taskCount == 0)
        {
            Console.WriteLine("You have no tasks.");
            Console.WriteLine("\nHit Enter to return...");
            Console.ReadLine();
            return;
        }

        Console.WriteLine("Your Current Tasks:");
        for (int i = 0; i < taskCount; i++)
        {
            Console.WriteLine((i + 1) + ". " + toDoList[i]);
        }

        Console.WriteLine("Hit Enter to return...");
        Console.ReadLine();
    }

    public static void MarkTaskComplete()                                   //Marks a task with the [DONE] prefix
    {                                                                      // This is done by making a new string with the [DONE] prefix and storing it back in the same spot
        if (taskCount == 0)
        {
            Console.WriteLine("You have no tasks.");
            Console.WriteLine("\nHit Enter to return...");
            Console.ReadLine();
            return;
        }

        Console.WriteLine("Current Tasks:");
        for (int i = 0; i < taskCount; i++)
        {
            Console.WriteLine((i + 1) + ". " + toDoList[i]);
        }

        Console.Write("\nEnter the task number to mark as DONE: ");
        string choice = Console.ReadLine();
        int taskNumber;

        while (!int.TryParse(choice, out taskNumber) || taskNumber < 1 || taskNumber > taskCount)
        {
            Console.WriteLine($"Invalid number. Enter a number between 1 and {taskCount}: ");
            choice = Console.ReadLine();
        }

        int index = taskNumber - 1;

        toDoList[index] = "[DONE] " + toDoList[index];

        Console.WriteLine($"Task {taskNumber} marked as complete!");
        Console.WriteLine("\nHit Enter to return...");
        Console.ReadLine();
    }

    public static void DeleteTask()                                 //Prompts the user for task they wish to remove from the list
    {
        if (taskCount == 0)
        {
            Console.WriteLine("You have no tasks.");
            Console.ReadLine();
            return;
        }

        Console.WriteLine("Current Tasks:");
        for (int i = 0; i < taskCount; i++)
        {
            Console.WriteLine((i + 1) + ". " + toDoList[i]);
        }

        Console.Write("\nEnter the task number you wish to DELETE: ");
        string input = Console.ReadLine();
        int taskNumber;

        while (!int.TryParse(input, out taskNumber) || taskNumber < 1 || taskNumber > taskCount)
        {
            Console.WriteLine($"Invalid number. Enter a number between 1 and {taskCount}:");
            input = Console.ReadLine();
        }

        int index = taskNumber - 1;

        for (int i = index; i < taskCount - 1; i++)
        {
            toDoList[i] = toDoList[i + 1];
        }

        taskCount--;

        Console.WriteLine($"Task {taskNumber} DELETED.");
        Console.WriteLine("\nEnter to return...");
        Console.ReadLine();
    }

    public static void ViewIncompleteTask()                                             //Checks for tasks that dont contain the prefix [Done]  
    {
        if (taskCount == 0)
        {
            Console.WriteLine("You have no tasks.");
            Console.ReadLine();
            return;
        }

        Console.WriteLine("Incomplete Tasks:");

        int count = 0;

        for (int i = 0; i < taskCount; i++)
        {
            if (toDoList[i].Length < 6 || toDoList[i].Substring(0, 6) != "[DONE]")
            {
                Console.WriteLine((i + 1) + ". " + toDoList[i]);
                count++;
            }
        }

        if (count == 0)
        {
            Console.WriteLine("All tasks are complete!");
        }

        Console.WriteLine("Hit Enter to return...");
        Console.ReadLine();
    }

    public static void ExportTasks()
    {
        if (taskCount == 0)
        {
            Console.WriteLine("No tasks to export.");
            Console.ReadLine();
            return;
        }

        Console.Write("Enter filename to save: ");
        string name = Console.ReadLine();

        string fullPath = folderPath + "\\" + name + ".txt";

        using (StreamWriter writer = new StreamWriter(fullPath))
        {
            for (int i = 0; i < taskCount; i++)
            {
                writer.WriteLine(toDoList[i]);
            }
        }

        Console.WriteLine("Saved to: " + fullPath);
        Console.ReadLine();
    }

    public static void ImportTasks()
    {
        Console.Write("Enter filename to load: ");
        string name = Console.ReadLine();
        string fullPath = folderPath + "\\" + name + ".txt";

        if (File.Exists(fullPath))
        {
            string[] lines = File.ReadAllLines(fullPath);

            taskCount = 0;
            for (int i = 0; i < lines.Length; i++)
            {
                if (taskCount < 10)
                {
                    toDoList[taskCount] = lines[i];
                    taskCount++;
                }
            }
            Console.WriteLine("Tasks loaded!");
        }
        else
        {
            Console.WriteLine("File not found.");
        }
        Console.ReadLine();
    }
}