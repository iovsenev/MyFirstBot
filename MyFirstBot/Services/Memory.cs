using MyFirstBot.models;
using System.Collections.Concurrent;

namespace MyFirstBot.Services
{
    internal class Memory : IStorage
    {
        private readonly ConcurrentDictionary<long, Session> _sessions;

        public Memory()
        {
            _sessions = new ConcurrentDictionary<long, Session>();
        }

        public Session GetSession(long chatId)
        {
            if (_sessions.ContainsKey(chatId))
            {
                return _sessions[chatId];
            }

            var newSession = new Session()
            {
                Choise = "textlenght"
            };

            _sessions[chatId] = newSession;
            return newSession;
        }
    }
}
