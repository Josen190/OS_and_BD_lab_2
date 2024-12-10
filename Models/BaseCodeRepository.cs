using OS_and_BD_lab_2.Helpers;
using OS_and_BD_lab_2.Models;
using System.ComponentModel;
using System.Text.Json;

namespace OS_and_BD_lab_2.Models
{
    public abstract class BaseCodeRepository<TInput, TOutput>
        where TInput : IDataInputOutput<TInput>
        where TOutput : IDataInputOutput<TOutput>
    {

        public string Name { get; private set; }
        public List<Test<TInput, TOutput>> Tests { get; private set; }
        private string _filePath;

        public BaseCodeRepository()
        {
            Name = "D";
            _filePath = $"C:/Data/ISTU/OS_and_BD_lab_2/ReposTest/{Name}.json";
            Tests = new List<Test<TInput, TOutput>>();
            Load();

        }

        public abstract TOutput run(TInput input_);

        public void runTest(Test<TInput, TOutput> test)
        {
            test.isRun = true;
            test.Result = run(test.Input);
            test.isSuccess = test.Result.Equals(test.Output);
        }

        public void runTest(int index)
        {
            runTest(Tests[index]);
        }

        public void runAllTest()
        {
            Tests.ForEach(test => { runTest(test); });
        }

        private bool Save()
        {
            if (Tests == null)
            {
                return false;
            }

            // Преобразуем List<Test> в List<RawTest> для сериализации
            var rawTests = Tests.Select(test => test.getRawTest).ToList();

            try
            {
                // Сериализуем список rawTests в JSON
                string json = JsonSerializer.Serialize(rawTests, new JsonSerializerOptions
                {
                    Converters = { new TupleConverter() },
                    WriteIndented = true // для более читабельного формата JSON
                });

                // Записываем JSON в файл
                File.WriteAllText(_filePath, json);

                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка при сохранении файла: {ex.Message}");
                return false;
            }
        }

        private bool Load()
        {
            try
            {
                string json = File.ReadAllText(_filePath);
                var _tests = JsonSerializer.Deserialize<List<Test<TInput, TOutput>.RawTest>>(json, new JsonSerializerOptions
                {
                    Converters = { new TupleConverter() },
                    PropertyNameCaseInsensitive = true
                });
                if (_tests == null)
                {
                    return false;
                }
                Tests = _tests.Select(raw => new Test<TInput, TOutput>(raw)).ToList();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public bool AddTest(TInput input, TOutput output)
        {
            Tests.Add(new Test<TInput, TOutput>(input, output));
            return Save();
        }

        public bool DeleteTest(int index)
        {
            Tests.RemoveAt(index);
            return Save();
        }
    }
}