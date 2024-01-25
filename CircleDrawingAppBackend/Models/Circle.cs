using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Security.AccessControl;

namespace CircleDrawingApp.Models
{
    public class Circle
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public DateTime TimeOfSubmission { get; set; }
        public int X { get; set; }
        public int Y { get; set; }
        public double Diameter { get; set; }
        public string Color { get; set; }
        public string SetId { get; set; }
    }
}
