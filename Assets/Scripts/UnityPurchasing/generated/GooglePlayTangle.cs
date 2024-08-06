// WARNING: Do not modify! Generated file.

namespace UnityEngine.Purchasing.Security {
    public class GooglePlayTangle
    {
        private static byte[] data = System.Convert.FromBase64String("YkExj7TC0eXaUy1qToGF0Q3q2WInMupI+NJVLKuJZLLXDNdZoZzlbhaVm5SkFpWelhaVlZQ2/ZFdEsK4kK7v58OBA+dzhsi3Z1tM0EXyLQOO+WCdv8sLrAprvZrJx74xrVlTrydli07nqJP9u+FVYZhB5mWnzi5sSU1HtfCqF7ddGMln1puHjqBxCL6kFpW2pJmSnb4S3BJjmZWVlZGUl7MwV5nZX6YpjQ8qR4oCyXq9MH+lmYuXP+s+OetAUnlWD/YioeZpZ2K4ZsbWPvbOPOEmZHZStWMRNGLmL64421FM+2rcfECmZx1WDaiop9fsQ2oUBmUqayhvKUk0eRwmiRNIFmlNtDkuciHS2SisRhB9uAYrrSwgnTn27KJ/M0bQN5aXlZSV");
        private static int[] order = new int[] { 6,3,3,13,9,5,11,11,12,10,10,13,12,13,14 };
        private static int key = 148;

        public static readonly bool IsPopulated = true;

        public static byte[] Data() {
        	if (IsPopulated == false)
        		return null;
            return Obfuscator.DeObfuscate(data, order, key);
        }
    }
}
