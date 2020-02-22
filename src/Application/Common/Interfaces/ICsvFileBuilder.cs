using NanoCell.Application.TodoLists.Queries.ExportTodos;
using System.Collections.Generic;

namespace NanoCell.Application.Common.Interfaces
{
    public interface ICsvFileBuilder
    {
        byte[] BuildTodoItemsFile(IEnumerable<TodoItemRecord> records);
    }
}
