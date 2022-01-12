using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player
{
    public class Rootobject
    {
        public Layer[] layers { get; set; }
    }

    public class Layer
    {
        public string type { get; set; }
        public string path { get; set; }
        public Placement[] placement { get; set; }
    }

    public class Placement
    {
        public Position position { get; set; }
    }

    public class Position
    {
        public int x { get; set; }
        public int y { get; set; }
        public int width { get; set; }
        public int height { get; set; }

    }


}

