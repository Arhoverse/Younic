using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
public class Monster
{
    public string Nom { get; set; }
    public int Vie { get; set; }
    public int Degats { get; set; }

    public Monster(string nom, int vie, int degats)
    {
        Nom = nom;
        Vie = vie;
        Degats = degats;
    }
}
