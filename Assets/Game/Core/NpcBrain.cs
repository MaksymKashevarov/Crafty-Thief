using System.Collections.Generic;

namespace Game.Core
{
    public class NpcBrain
    {
        private Dictionary<string, float> _attributes;
        public NpcBrain(Dictionary<string, float> attributes) 
        { 
            this._attributes = attributes;
        }

    }

}

