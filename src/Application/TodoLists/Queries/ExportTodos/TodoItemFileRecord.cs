using NanoCell.Application.Common.Mappings;
using NanoCell.Domain.Entities;

namespace NanoCell.Application.TodoLists.Queries.ExportTodos
{
    public class TodoItemRecord : IMapFrom<TodoItem>
    {
        public string Title { get; set; }

        public bool Done { get; set; }
    }
}
