using System;

namespace Modern_laba4
{
    [Serializable]
    public class Entity
    {
        public int Id { get; set; }
        public bool Deleted { set; get; }
        public override string ToString()
        {
            return string.Format("{0}", Id.ToString());
        }
    }
}
