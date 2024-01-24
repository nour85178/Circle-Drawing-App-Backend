using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Security.AccessControl;

namespace CircleDrawingApp.Models
{
    //private: Accessible only within the same class or struct.Provides encapsulation by restricting access to the internal implementation details of a class.
    //protected: Accessible within the same class, derived classes, and the same assembly.Useful for implementing inheritance and allowing derived classes to access certain members of the base class.
    //public: keyword is an access modifier that defines the visibility of a class, method, property, or other member.
    public class Circle
    {
        //annotation to say that it is a primary key
        [Key]
        //autoincremented
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
