using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FinAPI
{
    public class Car
    {
        public String Color { get; set; }
        public String Make { get; set; }

        public Car(String Color,String Make) {
            this.Color = Color;
            this.Make = Make;
        }
        public Car(){}
    }
}