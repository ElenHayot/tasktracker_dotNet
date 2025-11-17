using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using tasktracker.DtoModels;
using tasktracker.Entities;
using tasktracker.Enums;

namespace Tasktracker.Tests.TestData
{
    public class TaskTestData
    {
        public static CreateTaskDto CreateTaskData(int id = 1, int projectId = 1, int userId = 0)
        {
            return new CreateTaskDto()
            {
                Title = $"Task {id}",
                ProjectId = projectId,
                UserId = userId,
                Status = StatusEnum.New
            };
        }

        public static TaskEntity TaskEntityData(int id = 1, int projectId = 1, int userId = 0)
        {
            return new TaskEntity()
            {
                Id = id,
                Title = $"Task {id}",
                ProjectId = projectId,
                UserId = userId,
                Status = StatusEnum.New
            };
        }
    }
}
