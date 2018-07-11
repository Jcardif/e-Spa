using Microsoft.Azure.Mobile.Server;

namespace e_SpaBackend.DataObjects
{
    public class TodoItem : EntityData
    {
        public string Text { get; set; }

        public bool Complete { get; set; }
    }
}