using Planist.Exceptions;
using Planist.Features.Storage;
using Planist.Features.Storage.Entities;
using Planist.Features.Todo.Models;
using Planist.Features.Todo.ViewModels;
using Planist.Interfaces;
using Planist.Models;
using SQLite;

namespace Planist.Features.Todo
{
    public class TodoService : ITransientService
    {
        private readonly IPlanistDb PlanistDb;

        public TodoService(IPlanistDb planistDb)
        {
            this.PlanistDb = planistDb;
        }

        public async Task<PlanistResult> CreateTodoList(string name)
        {
            await PlanistDb.InsertAsync(new TodoList()
            {
                Name = name
            });

            return new PlanistResult();
        }

        public async Task<List<TodoListModel>> GetTodoLists()
        {
            List<TodoListModel> lists = [];

            // get all todo lists
            List<TodoList> results = await PlanistDb.TableAsync<TodoList>().ToListAsync();

            foreach(TodoList result in results)
            {
                TodoListModel list = new()
                {
                    Id = result.Id,
                    Name = result.Name
                };

                // get items related to this list
                list.TodoItems = await GetTodoListItems(list.Id);

                lists.Add(list);
            }

            return lists;
        }

        private async Task<List<TodoItemModel>> GetTodoListItems(int todoListId)
        {
            List<TodoItemModel> items = [];

            List<TodoItem> results = await PlanistDb.TableAsync<TodoItem>().Where(i => i.TodoListId == todoListId).ToListAsync();

            foreach (TodoItem result in results)
            {
                TodoItemModel item = new()
                {
                    Id = result.Id,
                    Text = result.Text
                };

                items.Add(item);
            }

            return items;
        }

        public async Task<TodoItem> AddItem(int todoListId, string text)
        {
            // check if the todo list exists
            TodoList? todoList = await PlanistDb.TableAsync<TodoList>().FirstOrDefaultAsync(l => l.Id == todoListId);
            if (todoList == null)
            {
                throw new PlanistResultException("The provided todo list does not exist");
            }

            // create the item against the list
            TodoItem item = new()
            {
                TodoListId = todoListId,
                Text = text
            };

            await PlanistDb.InsertAsync(item);

            return item;
        }
        
        public async Task<PlanistResult> RemoveItem(int todoItemId)
        {
            AsyncTableQuery<TodoItem> query = PlanistDb.TableAsync<TodoItem>().Where(i => i.Id == todoItemId);

            await PlanistDb.SoftDeleteAsync(query);

            return new PlanistResult();
        }
    }
}
