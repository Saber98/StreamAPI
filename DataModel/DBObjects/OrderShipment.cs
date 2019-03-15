using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataModel.DBObjects.Lookups;

namespace DataModel.DBObjects
{
    public class OrderShipment
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public int BillPersonId { get; set; }
        public Contact BillPerson { get; set; }
        [Required]
        public string BillAddress1 { get; set; }
        public string BillAddress2 { get; set; }
        [Required]
        public string BillCity { get; set; }
        [Required]
        public int BillStateId { get; set; }
        public virtual StateLookup BillState { get; set; }
        [Required]
        public string BillZip { get; set; }
        [Required]
        public int ShipPersonId { get; set; }
        public Contact ShipPerson { get; set; }
        [Required]
        public string ShipAddress1 { get; set; }
        public string ShipAddress2 { get; set; }
        [Required]
        public string ShipCity { get; set; }
        [Required]
        public int ShipStateId { get; set; }
        public virtual StateLookup ShipState { get; set; }
        [Required]
        public string ShipZip { get; set; }
        public string ShippingCarrier { get; set; }
        public string TrackingNumber { get; set; }
        [Required]
        public DateTime Created { get; set; }
        [Required]
        public string CreatedBy { get; set; }
        [Required]
        public DateTime LastModified { get; set; }
        [Required]
        public string LastModifiedBy { get; set; }
        [Required]
        public bool Archived { get; set; }
    }
}
