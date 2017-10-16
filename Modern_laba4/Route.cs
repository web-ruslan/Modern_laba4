using System;

namespace Modern_laba4
{
    [Serializable]
    class Route : Entity
    {
        public static int idCount;
        public int RouteId { get; set; }
        public City CityFrom { get; set; }
        public City CityTo { get; set; }
        public int ArrivalTime { get; set; }

        public Route(int routeId)
        {
            Id = idCount++;
            RouteId = routeId;
            Deleted = false;
        }

        public override string ToString()
        {
            return string.Format("{0} - {1}\t в дорозі: {2} хв", CityFrom.Name, CityTo.Name, ArrivalTime);
        }
    }
}
