public class Ingredient
{
    public int Id { get; set; }//ID
    public string Name { get; set; }//名前
    public string Type { get;  set; }//ジャンル
    public int Score { get; set; }//そのもののスコア
    //public float Compatibility { get; set; } //相性値

    public Ingredient(int id,string name, string type, int score)
    {
        Id = id;
        Name = name;
        Type = type;
        Score = score;
        //Compatibility = compatibility;
    }
}
