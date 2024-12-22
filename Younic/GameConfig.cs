using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System;
using System.Collections.Generic;
using System.IO;

public class GameConfig
{
    private Player player;
    private List<Monster> monsters;
    private string configFilePath = "config.txt";

    public GameConfig(Player player, List<Monster> monsters)
    {
        this.player = player;
        this.monsters = monsters;
    }

    public void LoadConfiguration()
    {
        if (!File.Exists(configFilePath))
        {
            Console.WriteLine("Aucune configuration trouvée, chargement des valeurs par défaut.");
            return;
        }

        try
        {
            string[] lignes = File.ReadAllLines(configFilePath);
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
                    case "Monstres":
                        monsters.Clear();
                        var monstresList = value.Split(',').Select(m =>
                        {
                            var monstreParts = m.Split(':');
                            string nom = monstreParts[0].Trim();
                            int vieMonstre = int.Parse(monstreParts[1].Trim());
                            int degatsMonstre = int.Parse(monstreParts[2].Trim());
                            return new Monster(nom, vieMonstre, degatsMonstre);
                        });
                        monsters.AddRange(monstresList);
                        break;
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Erreur lors du chargement de la configuration : {ex.Message}");
        }
    }
}
