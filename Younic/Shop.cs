using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class Shop
{
    private Player player;

    public Shop(Player player)
    {
        this.player = player;
    }

    public void Magasin()
    {
        Console.Clear();
        Console.WriteLine("====================================");
        Console.WriteLine("       Bienvenue au Magasin");
        Console.WriteLine("====================================");
        Console.WriteLine($"Vous avez {player.Pieces} pièces.");
        Console.WriteLine("1. Acheter Potion de Vie (50 pièces)");
        Console.WriteLine("2. Acheter Potion de Force (100 pièces)");
        Console.WriteLine("3. Revenir au Menu Partie");
        Console.WriteLine("====================================");
        Console.Write("Choisissez une option (1-3) : ");

        string choix = Console.ReadLine();
        switch (choix)
        {
            case "1":
                if (player.Pieces >= 50)
                {
                    player.Pieces -= 50;
                    player.PotionVie = true;
                    Console.WriteLine("Vous avez acheté une potion de vie !");
                }
                else
                {
                    Console.WriteLine("Vous n'avez pas assez de pièces !");
                }
                break;
            case "2":
                if (player.Pieces >= 100)
                {
                    player.Pieces -= 100;
                    player.PotionForce = true;
                    Console.WriteLine("Vous avez acheté une potion de force !");
                }
                else
                {
                    Console.WriteLine("Vous n'avez pas assez de pièces !");
                }
                break;
            case "3":
                return;
            default:
                Console.WriteLine("Option invalide.");
                break;
        }

        Console.WriteLine("Appuyez sur une touche pour continuer...");
        Console.ReadKey();
    }
}
