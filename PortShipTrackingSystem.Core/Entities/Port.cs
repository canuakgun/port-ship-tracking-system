namespace PortShipTrackingSystem.Core.Entities
{
    public class Port
    {
        public int PortId { get; set;}
        public required string Name { get; set;}
        public required string Country { get; set;}
        public required string City { get; set;}

        public ICollection<ShipVisit> ShipVisits { get; set; } = new List<ShipVisit>();
    }
}