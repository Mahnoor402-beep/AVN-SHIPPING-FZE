namespace AVN_SHIPPING_FZE
{
    public class ShipmentItem
    {
        public int ID { get; set; }
        public string Name { get; set; }

        public ShipmentItem(int id, string name)
        {
            ID = id;
            Name = name;
        }

        public override string ToString() => Name;
    }
}