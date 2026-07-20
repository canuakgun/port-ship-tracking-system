namespace PortShipTrackingSystem.Core.Entities
{
    public class Cargo
    {
        public int CargoId{ get; set;}
        public int ShipId{ get; set;}
        public Ship Ship{ get; set;} = null!;
        public required string Description{ get; set;}
        public decimal WeightTon{ get; set;}
        public required string CargoType{ get; set;}
    }
}