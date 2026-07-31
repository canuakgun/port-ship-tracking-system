namespace PortShipTrackingSystem.Core.Entities
{
    public class CrewMember : BaseEntity
    {
        public int CrewId{ get; set;}
        public required string FirstName{ get; set;}
        public required string LastName{ get; set;}
        public required string Email{ get; set;}
        public required string PhoneNumber{ get; set;}
        public required string Role{ get; set;}
        public ICollection<ShipCrewAssignment> ShipCrewAssignments { get; set; } = new List<ShipCrewAssignment>();
    }
}