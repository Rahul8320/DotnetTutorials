namespace LearningDotnetAgain
{
    public class Person
    {
        public Person(string name, int age, string description = "")
        {
            this.Name = name;
            this.Age = age;
            this.Description = description;
        }
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public int Age { get; set; } = 0;
    }
}
