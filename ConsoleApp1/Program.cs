// See https://aka.ms/new-console-template for more information
using System.Reflection;

Test test1 = new() { P1 = 10, P2 = 20, P3 = 30 };
Test test2 = new() { P1 = 10, P2 = 30, P3 = 40 };

Console.WriteLine(string.Join(',', test1.IsPropertiesEqual(test2, "P1")));
Console.WriteLine(!test1.IsPropertiesEqual(test2, "P1").Any());
Console.WriteLine(string.Join(',', test1.IsPropertiesEqual(test2, "P1", "P2")));
Console.WriteLine(!test1.IsPropertiesEqual(test2, "P1", "P2").Any());
Console.WriteLine(string.Join(',', test1.IsPropertiesEqual(test2, "P1", "P2", "P3")));
Console.WriteLine(!test1.IsPropertiesEqual(test2, "P1", "P2", "P3").Any());


public class Test
{
    public int P1 { get; set; }
    public int P2 { get; set; }
    public int P3 { get; set; }
}

public static class TestExtensions
{
    public static IEnumerable<string> IsPropertiesEqual(this Test t1, Test t2, params string[] propertyNames)
    {
        Type? typ = t1.GetType();
        List<string> propertyDiff = new();

        foreach (PropertyInfo property in typ.GetProperties().Where(p => propertyNames.Contains(p.Name)))
        {
            PropertyInfo? pa = typ.GetProperties().SingleOrDefault(p => p.Name.Equals(property.Name));
            int o1 = (int)pa.GetValue(t1);
            int o2 = (int)pa.GetValue(t2);

            if (o1 != o2)
            {
                propertyDiff.Add(property.Name);
            }
        }

        return propertyDiff;
    }
}