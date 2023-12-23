namespace RUnIt.Moq
{
    public class It
    {
        public static T Any<T>()
        {
            if (typeof(T) is ValueType)
            {
                return default(T);
            }
            else
            {
                return Activator.CreateInstance<T>();
            }
        }
    }
}
