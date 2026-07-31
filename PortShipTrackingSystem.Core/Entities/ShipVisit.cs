using System.ComponentModel.DataAnnotations;

namespace PortShipTrackingSystem.Core.Entities
{
    public class ShipVisit : BaseEntity
    {
        public int VisitId{ get; set;}
        public int ShipId{ get; set;}
        public Ship Ship{ get; set;} = null!;
        public int PortId{ get; set;}
        public Port Port{ get; set;} = null!;
        public DateTime ArrivalDate{ get; set;}
        public DateTime DepartureDate{ get; set;}
        public required string Purpose{ get; set;}
        [Timestamp]
        public byte[] RowVersion { get; set; } = null!;
    }
}