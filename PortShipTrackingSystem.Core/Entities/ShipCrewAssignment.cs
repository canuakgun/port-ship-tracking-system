namespace PortShipTrackingSystem.Core.Entities
{
    public class ShipCrewAssignment : BaseEntity
    {
        public int AssignmentId{ get; set;}
        public int ShipId{ get; set;}
        public Ship Ship{ get; set;} = null!;
        public int CrewId{ get; set;}
        public CrewMember Crew{ get; set;} = null!;
        public DateTime AssignmentDate{ get; set;}
    }
}