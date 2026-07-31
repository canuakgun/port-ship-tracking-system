namespace PortShipTrackingSystem.Core.Entities{

    public class Ship : BaseEntity
    {
        public int ShipId { get; set;}
        public required string Name{ get; set;}
        public required string IMO{ get; set;}
        public required string Type{ get; set;}
        public required string Flag{ get; set;}
        public int YearBuilt{ get; set;}

        public ICollection<ShipVisit> ShipVisits { get; set; } = new List<ShipVisit>();
        public ICollection<Cargo> Cargoes { get; set; } = new List<Cargo>();
        public ICollection<ShipCrewAssignment> ShipCrewAssignments { get; set; } = new List<ShipCrewAssignment>();
    }
}

