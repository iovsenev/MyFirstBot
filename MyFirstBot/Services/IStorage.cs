using MyFirstBot.models;

namespace MyFirstBot.Services
{
    internal interface IStorage
    {
        Session GetSession(long chatId);
    }
}
