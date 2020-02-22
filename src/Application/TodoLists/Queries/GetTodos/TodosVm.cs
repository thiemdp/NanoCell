using NanoCell.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;

namespace NanoCell.Application.TodoLists.Queries.GetTodos
{
    public class TodosVm
    {
        public IList<PriorityLevelDto> PriorityLevels =
            Enum.GetValues(typeof(PriorityLevel))
                .Cast<PriorityLevel>()
                .Select(p => new PriorityLevelDto { Value = (int)p, Name = p.ToString() })
                .ToList();

        public IList<TodoListDto> Lists { get; set; }
    }
}
