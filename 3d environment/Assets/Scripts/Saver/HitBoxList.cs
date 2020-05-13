
using System.Collections.Generic;
using System;




[Serializable]
public class HitBoxList
{
    // a global list of all the hiboxes in the game. When saving the list of hitboxes will be added to to this list before saving. When loading this list will get all the hitboxes and send the list to the loader
    public List<HitboxData> actors = new List<HitboxData>();
}
