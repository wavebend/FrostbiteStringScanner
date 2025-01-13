namespace FrostbiteStringScanner
{
    public class ActionStep : IStringPipelineStep
    {
        private readonly Action<HashSet<string>> _action;

        public ActionStep(Action<HashSet<string>> action)
        {
            _action = action;
        }

        public HashSet<string> Execute(HashSet<string> input, HashSet<string> globalGenerated)
        {
            _action?.Invoke(input);
            return input; // By default, we just return the same set
        }
    }
}
