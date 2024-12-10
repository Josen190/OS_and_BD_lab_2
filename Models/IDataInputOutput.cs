namespace OS_and_BD_lab_2.Models
{
    public interface IDataInputOutput<T>
    {

        public abstract string ToString();

        public abstract static T Parse(string s);
        public abstract bool Equals(T dataIO);
    }
}
