using FormulaApp.Api.Models;

namespace FormulaApp.UnitTest.Fixtures;

public class FanFixtures
{
    public static List<Fan> GetFans() => [
        new(id: 1, name: "Abc", email: "abc@test.com"),
        new (id: 2,name:  "Admin",email:  "admin@test.com")
    ];
}
