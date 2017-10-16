using System;

namespace Modern_laba4
{
    [Serializable]
    public class Crew : Entity
    {
        public static int idCount;
        public string CarNumber { get; set; }
        public string DriverName { get; set; }
        public int LoadCapacity { get; set; }

        public Crew()
        {
            Id = idCount++;
            Deleted = false;
        }

        public override string ToString()
        {
            return string.Format("Водій: {0}\t номер авто: {1}\t вантажопідйомність: {2} кг", DriverName, CarNumber, LoadCapacity);
        }
    }
}
