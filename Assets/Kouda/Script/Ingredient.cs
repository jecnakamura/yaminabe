public class Ingredient
{
    public int Id { get; set; }//ID
    public string Name { get; set; }//���O
    public string Type { get;  set; }//�W������
    public int Score { get; set; }//���̂��̂̃X�R�A
    //public float Compatibility { get; set; } //�����l

    public Ingredient(int id,string name, string type, int score)
    {
        Id = id;
        Name = name;
        Type = type;
        Score = score;
        //Compatibility = compatibility;
    }
}
