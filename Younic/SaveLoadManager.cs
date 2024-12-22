using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System;
using System.IO;
using System.Linq;

public class SaveLoadManager
{
    private Player player;
    private List<Monster> monsters;
    private string filePath = "sauvegarde.txt";

    public SaveLoadManager(Player player, List<Monster> monsters)
    {
        this.player = player;
        this.monsters = monsters;
    }

    public bool LoadGame()
    {
        if (!File.Exists(filePath))
        {
            return false;
        }

        try
        {
            string[] lignes = File.ReadAllLines(filePath);
            foreach (string ligne in lignes)
            {
                string[] parts = ligne.Split('=');
                if (parts.Length != 2) continue;

                string key = parts[0].Trim();
                string value = parts[1].Trim();

                switch (key)
                {
                    case "Vie":
                        player.Vie = int.Parse(value);
                        break;
                    case "Pieces":
                        player.Pieces = int.Parse(value);
                        break;
                    case "PotionVie":
                        player.PotionVie = bool.Parse(value);
                        break;
                    case "PotionForce":
                        player.PotionForce = bool.Parse(value);
                        break;
                    case "Degats":
                        player.Degats = int.Parse(value);
                        break;
                }
            }

            return true;
        }
        catch (Exception)
        {
            return false;
        }
    }

    public void SaveGame()
    {
        try
        {
            using (StreamWriter writer = new StreamWriter(filePath))
            {
                writer.WriteLine($"Vie={player.Vie}");
                writer.WriteLine($"Pieces={player.Pieces}");
                writer.WriteLine($"PotionVie={player.PotionVie}");
                writer.WriteLine($"PotionForce={player.PotionForce}");
                writer.WriteLine($"Degats={player.Degats}");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Erreur lors de la sauvegarde : {ex.Message}");
        }
    }
}
