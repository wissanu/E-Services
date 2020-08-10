using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;

namespace UnitTest_Git_system
{
    [TestFixture]
    public class TestClass
    {
        private class Car
        {
            public string color { get; set; }

            public Car(string color)
            {
                this.color = color;
            }

            public Car()
            {
            }
        }

        [Test]
        public void testClass()
        {
            List<Car> cars = new List<Car>();
            var car1 = new Car("red");
            var car2 = new Car("green");
            var car3 = new Car("blue");
            cars.Add(car1);
            cars.Add(car2);
            cars.Add(car3);

            List<Car> cars2 = new List<Car>();
            cars2 = move(cars);
            Assert.AreEqual(1, cars2.Count());
            Assert.AreEqual(6, cars.Count());
        }

        private List<Car> move(List<Car> cars)
        {
            List<Car> carRemove = new List<Car>();
            carRemove.Add(cars.Where(c => c.color == "green").FirstOrDefault());
            cars.Add(carRemove.FirstOrDefault());
            cars.Add(new Car("black"));
            cars.Add(new Car("yellow"));
            return carRemove;
        }
    }
}