using System;
using System.Collections.Generic;
using System.Threading;

class Program
{
    static string GetFighterName(string prompt)
    {
        Console.Write(prompt);
        string name = Console.ReadLine();
        return name;
    }
    static int GetPlayerAttackChoice()
    {
        Console.WriteLine("Välj attack:");
        Console.WriteLine("1. Hög chans att träffa, låg skada");
        Console.WriteLine("2. Låg chans att träffa, hög skada");

        int choice;
        while (!int.TryParse(Console.ReadLine(), out choice) || choice < 1 || choice > 2)
        {
            Console.WriteLine("Ogiltigt val. Välj 1 eller 2.");
        }

        return choice;
    }
    static int CalculateAttackDamage(int hitChance, int minDamage, int maxDamage)
    {
        Random random = new Random();
        int hitRoll = random.Next(40, 100); // Slumpa ett tal mellan 40 och 100 för träffsannolikhet
        if (hitRoll <= hitChance)
        {
            return random.Next(minDamage, maxDamage + 1); // Slumpa skadan inom det angivna intervallet
        }
        else
        {
            return 0; // Slaget missade
        }
    }
    static void Main()
    {
        int playerHitChance = 70;
        int playerMinDamage = 10;
        int playerMaxDamage = 30;

        Console.WriteLine("Välkommen till Slagsmålsspelet!");

        string fighterAName = GetFighterName("Vad Heter Du?: ");
        string[] fighterBNames = { "KSI", "Ray Leonard", "Muhammed Ali" };
        Random random = new Random();
        string fighterBName = fighterBNames[random.Next(fighterBNames.Length)];

        Console.WriteLine("The One!");
        Thread.Sleep(1);
        Console.WriteLine("The Only!");
        Thread.Sleep(1);
        Console.WriteLine(fighterAName);
        Console.WriteLine(@"(9o_o)9");

        Console.WriteLine();

        Console.WriteLine("Now From The Other Side We Have..");
        Thread.Sleep(1);
        Console.WriteLine();

        int fighterAHp = 100; // Hit points för slagskämpe A

        Opponent opponent = GetOpponentChoice();

        Fighter fighterB = new Fighter(opponent.Name, opponent.Hp, opponent.HitChance, 10, 30);
        int round = 1;

        Console.WriteLine("A Famouse Boxer Called!");

        Console.WriteLine(fighterB.Name);

        Console.WriteLine(@"(ง⌐■_■)ง");

        Console.WriteLine("Let The Fight Begin!");

        while (fighterAHp > 0 && fighterB.Hp > 0)
        {
            Console.WriteLine($"Runda {round}");
            Console.WriteLine($"{fighterAName} HP: {fighterAHp}");
            Console.WriteLine($"{fighterB.Name} HP: {fighterB.Hp}");

            // Implementera spelarens val av attacker 
            int playerAttackChoice = GetPlayerAttackChoice();

            // Slumpa slagskämpe B:s attack 
            int fighterBAttackChoice = random.Next(2); // Anta 2 attacker (0 och 1)

            // Utför attacker och beräkna skada
            int playerDamage = CalculateAttackDamage(playerHitChance, playerMinDamage, playerMaxDamage);
            int fighterBDamage = CalculateAttackDamage(fighterB.HitChance, fighterB.MinDamage, fighterB.MaxDamage);
            // skadan på slagskämparna
            fighterAHp -= fighterBDamage;
            fighterB.Hp -= playerDamage;


            Console.WriteLine($"{fighterAName} attackerar med skada {playerDamage}");
            Console.WriteLine($"{fighterB.Name} attackerar med skada {fighterBDamage}");
            

            round++;
        }

        // Visa vinnarna
        if (fighterAHp <= 0)
        {
            Console.WriteLine($"{fighterB.Name} vann!");
        }
        else if (fighterB.Hp <= 0)
        {
            Console.WriteLine($"{fighterAName} vann!");
        }
        else
        {
            Console.WriteLine($"{fighterAName} gick ner med {fighterB.Name}");
        }

        Console.WriteLine("Spelet är över. Vill du spela igen? (ja/nej)");
        string playAgain = Console.ReadLine();
        if (playAgain.ToLower() == "ja")
        {
            Main(); // Starta om spelet om användaren vill spela igen
        }
        else
        {
            Console.WriteLine("Tack för att du spelade!");
        }
    }

    static Opponent GetOpponentChoice()
    {
        Console.WriteLine("Välj en motståndare:");

        // lista av olika motståndareeeee
        List<Opponent> opponents = new()
        {
            new Opponent("KSI", 150, 75),
            new Opponent("Ray Leonard", 120, 85),
            new Opponent("Muhammed Ali", 180, 65)
        };

        for (int i = 0; i < opponents.Count; i++)
        {
            Console.WriteLine($"{i + 1}. {opponents[i].Name} (HP: {opponents[i].Hp}, Träffsäkerhet: {opponents[i].HitChance}%)");
        }

        int choice;
        while (!int.TryParse(Console.ReadLine(), out choice) || choice < 1 || choice > opponents.Count)
        {
            Console.WriteLine("Ogiltigt val. Välj en motståndare från listan.");
        }

        return opponents[choice - 1];
    }
}
class Opponent
{
    public string Name { get; }
    public int BaseHp { get; }
    public int Hp { get; }
    public int HitChance { get; }

    public Opponent(string name, int baseHp, int hitChance)
    {
        Name = name;
        BaseHp = baseHp;
        Hp = BaseHp;
        HitChance = hitChance;
    }
}

class Fighter
{
    public string Name { get; }
    public int Hp { get; set; }
    public int HitChance { get; }
    public int MinDamage { get; }
    public int MaxDamage { get; }

    public Fighter(string name, int hp, int hitChance, int minDamage, int maxDamage)
    {
        Name = name;
        Hp = hp;
        HitChance = hitChance;
        MinDamage = minDamage;
        MaxDamage = maxDamage;
    }
}