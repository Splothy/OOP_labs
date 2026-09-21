using System.Text;
using lab_2;

Console.OutputEncoding = Encoding.UTF8;

var snake = new Snake();
var cat = new Cat();
var dog = new Dog();

Animal[] animals = { snake, cat, dog };

foreach (Animal animal in animals)
{
  
    animal.Sound();
    animal.Walk();
    Console.WriteLine();
}
