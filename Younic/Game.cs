using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System;
using System.Collections.Generic;

public class Game
{
    private Player player;
    private List<Monster> monsters;
    private SaveLoadManager saveLoadManager;
    private GameConfig gameConfig;
    private Shop shop;

    public Game()
    {
        player = new Player();
        monsters = new List<Monster>
        {
            new Monster("Gobelin", 30, 5),
            new Monster("Orc", 50, 10),
            new Monster("Dragon", 100, 20)
        };
        saveLoadManager = new SaveLoadManager(player, monsters);
        gameConfig = new GameConfig(player, monsters);
        shop = new Shop(player);
    }

    public void Start()
    {
        bool quitter = false;
        while (!quitter)
        {
            Console.Clear();
            Console.WriteLine("====================================");
            Console.WriteLine("     Bienvenue dans le RPG Console");
            Console.WriteLine("====================================");
            Console.WriteLine("1. Nouvelle Partie");
            Console.WriteLine("2. Charger une Partie");
            Console.WriteLine("3. Éditeur de Jeu");
            Console.WriteLine("4. Quitter");
            Console.WriteLine("====================================");
            Console.Write("Choisissez une option (1-4) : ");

            string choix = Console.ReadLine();

            switch (choix)
            {
                case "1":
                    NouvellePartie();
                    break;
                case "2":
                    ChargerPartie();
                    break;
                case "3":
                    EditeurDeJeu();
                    break;
                case "4":
                    quitter = true;
                    break;
                default:
                    Console.WriteLine("Option invalide. Essayez à nouveau.");
                    break;
            }
        }

        Console.WriteLine("Merci d'avoir joué !");
    }

    private void NouvellePartie()
    {
        gameConfig.LoadConfiguration();
        Console.Clear();
        Console.WriteLine("Nouvelle partie commencée !");
        Console.WriteLine($"Vie: {player.Vie}, Pièces: {player.Pieces}");
        Console.WriteLine("Appuyez sur une touche pour continuer...");
        Console.ReadKey();
        MenuPartie();
    }

    private void ChargerPartie()
    {
        if (!saveLoadManager.LoadGame())
        {
            Console.WriteLine("Aucune sauvegarde trouvée.");
            Console.WriteLine("Appuyez sur une touche pour revenir au menu principal...");
            Console.ReadKey();
            return;
        }
        Console.WriteLine("Partie chargée avec succès !");
        Console.WriteLine($"Vie: {player.Vie}, Pièces: {player.Pieces}");
        Console.WriteLine("Appuyez sur une touche pour continuer...");
        Console.ReadKey();
        MenuPartie();
    }

    private void MenuPartie()
    {
        bool quitterPartie = false;
        while (!quitterPartie)
        {
            Console.Clear();
            Console.WriteLine("====================================");
            Console.WriteLine("        Menu de la Partie");
            Console.WriteLine("====================================");
            Console.WriteLine("1. Lancer un Combat");
            Console.WriteLine("2. Aller au Magasin");
            Console.WriteLine("3. Sauvegarder la Partie");
            Console.WriteLine("4. Revenir au Menu Principal");
            Console.WriteLine("====================================");
            Console.Write("Choisissez une option (1-4) : ");

            string choix = Console.ReadLine();

            switch (choix)
            {
                case "1":
                    LancerCombat();
                    break;
                case "2":
                    shop.Magasin();
                    break;
                case "3":
                    SauvegarderPartie();
                    break;
                case "4":
                    quitterPartie = true;
                    break;
                default:
                    Console.WriteLine("Option invalide. Essayez à nouveau.");
                    break;
            }
        }
    }

    private void LancerCombat()
    {
        Monster monstre = GenererMonstre();
        Console.Clear();
        Console.WriteLine($"Un {monstre.Nom} sauvage apparaît !");
        Console.WriteLine($"Vie du monstre : {monstre.Vie} HP");

        bool combatEnCours = true;

        while (combatEnCours && monstre.Vie > 0)
        {
            Console.Clear();
            Console.WriteLine($"Votre vie : {player.Vie} HP, Pièces : {player.Pieces}.");
            Console.WriteLine("Entrez un pattern de flèches pour attaquer :");

            string entrees = CapturerEntrees();
            int degatsInfliges = 0;

            if (entrees == "↑↓→←") degatsInfliges = 10;
            else if (entrees == "↑↑↓↓→←") degatsInfliges = 30;

            Console.Clear();

            if (degatsInfliges > 0)
            {
                Console.WriteLine($"Attaque réussie ! Vous infligez {degatsInfliges} dégâts.");
                monstre.Vie -= degatsInfliges;
                Console.WriteLine($"Vie restante du {monstre.Nom} : {monstre.Vie} HP");

                if (monstre.Vie <= 0)
                {
                    Console.WriteLine($"Vous avez vaincu le {monstre.Nom} !");
                    player.Pieces += 10;
                    Console.WriteLine("Vous gagnez 10 pièces !");
                    combatEnCours = false;
                }
            }
            else
            {
                Console.WriteLine("Vous avez échoué votre attaque !");
            }

            if (combatEnCours)
            {
                int degatsMonstre = new Random().Next(10, 31);
                player.Vie -= degatsMonstre;
                Console.WriteLine($"Le {monstre.Nom} riposte et vous inflige {degatsMonstre} dégâts !");

                if (player.Vie <= 0)
                {
                    Console.WriteLine("Vous êtes mort ! Game Over.");
                    combatEnCours = false;
                }
            }

            Console.WriteLine("Appuyez sur une touche pour continuer...");
            Console.ReadKey();
        }
    }

    private Monster GenererMonstre()
    {
        Random rand = new Random();
        int index = rand.Next(0, monsters.Count);
        return monsters[index];
    }

    private string CapturerEntrees()
    {
        string result = "";
        while (result.Length < 6)
        {
            ConsoleKey key = Console.ReadKey(true).Key;

            if (key == ConsoleKey.UpArrow || key == ConsoleKey.DownArrow ||
                key == ConsoleKey.LeftArrow || key == ConsoleKey.RightArrow)
            {
                switch (key)
                {
                    case ConsoleKey.UpArrow: result += "↑"; break;
                    case ConsoleKey.DownArrow: result += "↓"; break;
                    case ConsoleKey.LeftArrow: result += "←"; break;
                    case ConsoleKey.RightArrow: result += "→"; break;
                }

                if (result == "↑↓→←" || result == "↑↑↓↓→←")
                {
                    return result;
                }
            }
        }

        return result;
    }

    private void SauvegarderPartie()
    {
        saveLoadManager.SaveGame();
        Console.WriteLine("Partie sauvegardée avec succès !");
        Console.WriteLine("Appuyez sur une touche pour continuer...");
        Console.ReadKey();
    }

    private void EditeurDeJeu()
    {
        Console.Clear();
        Console.WriteLine("====================================");
        Console.WriteLine("       Éditeur de Jeu");
        Console.WriteLine("====================================");
        Console.WriteLine("1. Modifier la Vie de départ");
        Console.WriteLine("2. Modifier le nombre de Pièces");
        Console.WriteLine("3. Modifier les monstres");
        Console.WriteLine("4. Revenir au Menu Principal");
        Console.WriteLine("====================================");
        Console.Write("Choisissez une option (1-4) : ");

        string choix = Console.ReadLine();
        switch (choix)
        {
            case "1":
                Console.Write("Entrez la nouvelle vie de départ: ");
                player.Vie = int.Parse(Console.ReadLine());
                break;

            case "2":
                Console.Write("Entrez le nouveau nombre de pièces de départ: ");
                player.Pieces = int.Parse(Console.ReadLine());
                break;

            case "3":
                ModifierMonstres();
                break;

            case "4":
                return;

            default:
                Console.WriteLine("Option invalide.");
                break;
        }

        Console.WriteLine("Appuyez sur une touche pour continuer...");
        Console.ReadKey();
    }

    private void ModifierMonstres()
    {
        Console.Clear();
        Console.WriteLine("====================================");
        Console.WriteLine("       Modifier les Monstres");
        Console.WriteLine("====================================");

        for (int i = 0; i < monsters.Count; i++)
        {
            Monster monstre = monsters[i];
            Console.WriteLine($"{i + 1}. {monstre.Nom} - Vie: {monstre.Vie}, Dégâts: {monstre.Degats}");
        }

        Console.WriteLine("Choisissez le monstre à modifier (1-3) : ");
        int index = int.Parse(Console.ReadLine()) - 1;

        if (index >= 0 && index < monsters.Count)
        {
            Console.Write("Nouvelle vie du monstre: ");
            monsters[index].Vie = int.Parse(Console.ReadLine());
            Console.Write("Nouveaux dégâts du monstre: ");
            monsters[index].Degats = int.Parse(Console.ReadLine());
        }

        Console.WriteLine("Monstre modifié !");
    }
}

