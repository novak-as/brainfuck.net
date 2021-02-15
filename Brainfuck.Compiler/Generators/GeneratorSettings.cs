namespace Compiler.Generators
{
    public struct VisitorSettings
    {
        public int AvailableMemory { get; set; }
        public bool IsCycled { get; set; }

        public VisitorSettings(int availableMemory, bool isCycled)
        {
            AvailableMemory = availableMemory;
            IsCycled = isCycled;
        }
    }
}
