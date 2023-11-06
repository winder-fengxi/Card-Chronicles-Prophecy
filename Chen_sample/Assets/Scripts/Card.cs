public class Card
{
    public int id;
    public string cardName;
    public int consume;
    public string introduce;

    //构造函数
    public Card(int _id, string _cardName, int _consume, string _introduce)
    {
        this.id = _id;
        this.cardName = _cardName;
        this.consume = _consume;
        this.introduce = _introduce;
    }
}

public class AbilityCard : Card
{
    //等级、属性

    public AbilityCard(int _id, string _cardName, int _consume, string _introduce) : base(_id, _cardName, _consume, _introduce) { }
}

public class AttackCard : Card
{
    public AttackCard(int _id, string _cardName, int _consume, string _introduce) : base(_id, _cardName, _consume, _introduce) { }
}