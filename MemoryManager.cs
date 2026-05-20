using System.Collections.Generic;

namespace CyberBotSA_part2
{
    public class MemoryManager
    {
        private Dictionary<string, string> memory = new Dictionary<string, string>();
        public void Store(string key, string value)
        {
            if (memory.ContainsKey(key))
                memory[key] = value;
            else
                memory.Add(key, value);
        }
        public string Retrieve(string key)
        {
            if (memory.ContainsKey(key))
                return memory[key];
            return null;
        }
        public bool Has(string key)
        {
            return memory.ContainsKey(key);
        }
    }
}
