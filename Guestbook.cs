using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Console;
using System.IO;

namespace GuestbookApp
{
    class Guestbook
    {
        private string GuestbookFile = "MyGuestbook.txt";
        private string TitleArt = @"

 _____                 _   _                 _    
|  __ \               | | | |               | |   
| |  \/_   _  ___  ___| |_| |__   ___   ___ | | __
| | __| | | |/ _ \/ __| __| '_ \ / _ \ / _ \| |/ /
| |_\ \ |_| |  __/\__ \ |_| |_) | (_) | (_) |   < 
 \____/\__,_|\___||___/\__|_.__/ \___/ \___/|_|\_\
                                                  
";
        

        public void Run()
            // Startar från Program.cs
            // Startar delar av funktioner
        {
            Title = "Gästbok";
            DisplayIntro();
            CreateGuestbookFile();
            RunMenu();
            DisplayOutro();
        }
        private void RunMenu()
            // Menyvalen med switchcase för alternativen. 
        {
            string choice;
            do
            {
                choice = GetChoice();
                switch (choice)
                {
                    case "1":
                        DisplayGuestbookContents();
                        break;
                    case "2":
                        ClearFile();
                        break;
                    case "3":
                        AddEntry();
                        break;
                    case "4":
                        ClearOneFile();
                        break;
                    default:
                        break;
                }
            } while (choice != "5");
        }
        private string GetChoice()
            // Menyvalfunktion med Lyssning av knapptryck samt felhantering vid val icke inom 1-5
        {
            bool isChoiceValid = false;
            string choice;

            do
            {
                Clear();
                ForegroundColor = ConsoleColor.DarkGray;
                WriteLine(TitleArt);
                ForegroundColor = ConsoleColor.Black;
                WriteLine("\nVad vill du göra?");
                WriteLine(" > 1 - Läsa gästboken");
                WriteLine(" > 2 - Rensa hela gästboken");
                WriteLine(" > 3 - Lägg till i gästboken");
                WriteLine(" > 4 - Radera ett inlägg");
                WriteLine(" > 5 - Avsluta");

                ForegroundColor = ConsoleColor.DarkBlue;
                choice = ReadLine().Trim();
                ForegroundColor = ConsoleColor.Black;

                if (choice == "1" || choice == "2" || choice == "3" || choice == "4" || choice == "5")
                {
                    isChoiceValid = true;
                }
                else
                {
                    ForegroundColor = ConsoleColor.Red;
                    WriteLine($"\"{choice}\" Är inte giltigt. Välj mellan 1 - 5.");
                    WaitForKey();
                }
            } while (!isChoiceValid);

            return choice;

        }
        private void CreateGuestbookFile()
            // skapar textfilen om den inte redan finns
        {

            if (!File.Exists(GuestbookFile))
            {
                File.CreateText(GuestbookFile);
            }
        }

        private void DisplayIntro()
            // Sätter titel färger samt välkomsttext
        {
            ForegroundColor = ConsoleColor.Black;
            BackgroundColor = ConsoleColor.White;
            Clear();
            WriteLine(TitleArt);
            WriteLine("\n Välkommen till gästboken");
            WaitForKey();
        }

        private void DisplayOutro()
            // Exit funktion
        {
            ForegroundColor = ConsoleColor.Black;
            WriteLine("Tack för att du har använt gästboken");
            ReadKey(true);
        }

        private void WaitForKey()
            // Väntar på knpptryckfunktion
        {
            ForegroundColor = ConsoleColor.DarkGray;
            WriteLine("\nTryck på en tangent");
            ReadKey(true);
        }

        private void DisplayGuestbookContents()
            // Visar hela listan med gästboksinlägg från textfilen
        {
            ForegroundColor = ConsoleColor.DarkMagenta;
            string GuestbookText = File.ReadAllText(GuestbookFile);
            WriteLine("\n=== Gästbokens innehåll===");
            WriteLine(GuestbookText);
            
            WaitForKey();
        }

        private void ClearFile()
        {
            //Rensar hela textfilen
            ForegroundColor = ConsoleColor.Black;
            File.WriteAllText(GuestbookFile, "");
            WriteLine("\nGästboken raderad");
            WaitForKey();
        }
        private void ClearOneFile()
            // Ta bort ett enskilt gästboksinlägg
        {
            ForegroundColor = ConsoleColor.Black;
            Console.WriteLine("Skriv in vilket nummer du vill radera i gästboken.");

            // lopar igenom filerna
            string[] lines = File.ReadAllLines(GuestbookFile);
            int i = 0;
            foreach (string line in lines)
            {
                i++;
                WriteLine($"{i} - {line}");
            }
            int postNumber = int.Parse(ReadLine());
            // Deletar posten
            lines[postNumber - 1] = "";
            File.WriteAllLines(GuestbookFile, lines);
            WriteLine("\nInlägg borttaget");
            WaitForKey();
        }

        private void AddEntry()
            // Lägga till i gästboken
        {
            ForegroundColor = ConsoleColor.Black;
            WriteLine("Skriv in ditt namn ");
            string name = ReadLine();
            if (name.Length == 0)
            {
                WriteLine("Skriv in ett namn");
            }
            else
            {
                // funktion för att kunna skriva ordet EXIT för att komma ur och lägga till i gästboken
                WriteLine("\nVad vill du lägga till? (Skriv EXIT och enter för att lägga till det du skrivit)");

                ForegroundColor = ConsoleColor.DarkMagenta;
                string newEntry = "";
                bool shouldContinue = true;
                while (shouldContinue)
                {
                    string line = ReadLine();
                    if (line.ToLower().Trim() == "exit")
                    {
                        shouldContinue = false;
                    }
                    else
                    {
                        newEntry += "Författare: " + name + " Inlägg: " + line + "\n";
                    }
                }

                File.AppendAllText(GuestbookFile,  newEntry);
                ForegroundColor = ConsoleColor.Black;
                WriteLine("Gästboken har blivit uppdaterad");
                WaitForKey();
            }
        }
    }
}
