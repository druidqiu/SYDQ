using System;

namespace SYDQ.Infrastructure.Web.Mvc.Session
{
    [Serializable]
    public class Menu
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Area { get; set; }
        public string Action { get; set; }
        public string Controller { get; set; }
        public int ParentId { get; set; }
        public int SortNum { get; set; }
    }
}
