namespace TestAssembly
{
    internal class InternalClass : IInternalInterface
    {
        internal delegate void InternalDelegate();

        private delegate void PrivateDelegate();

        public interface INestedPublicInterface
        {
            void Test();
        }

        public void Test()
        {
        }
    }
}
