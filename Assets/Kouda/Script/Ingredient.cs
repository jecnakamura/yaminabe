public class Ingredient
{
    public string Name { get; private set; }
    public string Type { get; private set; }
    public int Score { get; private set; }
    public float Compatibility { get; private set; }

    public Ingredient(string name, string type, int score, float compatibility)
    {
        Name = name;
        Type = type;
        Score = score;
        Compatibility = compatibility;
    }
}
