namespace OS_and_BD_lab_2.Models
{
    public class Test<TInput, TOutput>
        where TInput : IDataInputOutput<TInput>
        where TOutput : IDataInputOutput<TOutput>
    {
        public TInput Input { get; set; }
        public TOutput Output { get; set; }
        public TOutput? Result { get; set; }
        public bool isSuccess { get; set; }
        public bool isRun { get; set; }

        public Test(TInput input, TOutput output)
        {
            Input = input;
            Output = output;
            Result = default(TOutput);
            isSuccess = false;
            isRun = false;
        }

        public Test(RawTest raw) : this(raw.Input, raw.Output) { }

        public RawTest getRawTest => new RawTest(Input, Output);

        public record RawTest(TInput Input, TOutput Output);
    }

    
}
