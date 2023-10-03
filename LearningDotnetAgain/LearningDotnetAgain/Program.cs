// See https://aka.ms/new-console-template for more information
using LearningDotnetAgain;

Console.WriteLine("Hello, World!");

// Console.WriteLine(MyEnum.Active.ToString());

// MyEnum value = MyEnum.Active;
// Console.WriteLine(value.ToString());

List<Person> list = new();

list.Add(new Person("Rahul", 22, "CSE-49"));
list.Add(new Person("Person1", 24, "CSE-86"));
list.Add(new Person("Prabir", 23, "CSE-50"));

foreach (var item in list)
{
    if(item.Age == 23)
    {
        item.Description = "You are a Senior";
    }
}


foreach (Person person in list)
{
    Console.WriteLine($"{person.Name} : {person.Age} -> {person.Description}");
}