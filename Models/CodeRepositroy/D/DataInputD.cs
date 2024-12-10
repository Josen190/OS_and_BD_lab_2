namespace OS_and_BD_lab_2.Models.CodeRepositroy.D
{
    public class DataInputD : IDataInputOutput<DataInputD>
    {
        public int boaLength { get; set; }
        public int obstaclesCount { get; set; }
        public List<(int, int)> obstacles { get; set; }
        public string movements { get; set; }

        public static DataInputD Parse(string s)
        {
            var data = new DataInputD();
            // Разделение строки на строки по новой линии
            var lines = s.Split(new[] { '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries);

            // Парсинг первой строки: длина удава и количество препятствий
            var firstLine = lines[0].Split(' ');
            data.boaLength = int.Parse(firstLine[0]);
            data.obstaclesCount = int.Parse(firstLine[1]);

            // Парсинг строк с координатами препятствий
            data.obstacles = new List<(int, int)>();
            for (int i = 1; i <= data.obstaclesCount; i++)
            {
                var coords = lines[i].Split(' ');
                int x = int.Parse(coords[0]);
                int y = int.Parse(coords[1]);
                data.obstacles.Add((x, y));
            }

            // Последняя строка: движения
            data.movements = lines[data.obstaclesCount + 1];
            return data;
        }

        public bool Equals(DataInputD dataIO)
        {
            throw new NotImplementedException();
        }

        public override string ToString()
        {
            var result = $"{boaLength} {obstaclesCount}\n";
            foreach (var (x, y) in obstacles)
            {
                result += $"{x} {y}\n";
            }
            result += movements;
            return result;
        }
    }
}
