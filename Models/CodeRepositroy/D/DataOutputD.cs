namespace OS_and_BD_lab_2.Models.CodeRepositroy.D
{
    public class DataOutputD : IDataInputOutput<DataOutputD>
    {
        public (int, int) boa { get; set; }

        public static DataOutputD Parse(string s)
        {
            var data = new DataOutputD();

            var arr = s.Split(' ');
            (int, int) tempBoa;
            tempBoa.Item1 = int.Parse(arr[0]);
            tempBoa.Item2 = int.Parse(arr[1]);
            data.boa = tempBoa;

            return data;
        }

        public bool Equals(DataOutputD o)
        {
            return boa.Equals(o.boa);
        }

        public override string ToString() => $"{boa.Item1} {boa.Item2}";
    }
}
