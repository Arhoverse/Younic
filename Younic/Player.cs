using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class Player
{
    public int Vie { get; set; }
    public int Pieces { get; set; }
    public bool PotionVie { get; set; }
    public bool PotionForce { get; set; }
    public int Degats { get; set; }

    public Player()
    {
        Vie = 100;
        Pieces = 0;
        PotionVie = false;
        PotionForce = false;
        Degats = 10;
    }
}

