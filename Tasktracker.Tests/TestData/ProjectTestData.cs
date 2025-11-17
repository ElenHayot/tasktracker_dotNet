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
    public class ProjectTestData
    {
        public static CreateProjectDto CreateProjectData(int id = 1)
        {
            return new CreateProjectDto()
            {
                Title = $"Project {id}",
                Status = StatusEnum.New
            };
        }

        public static ProjectEntity ProjectEntityData(int id = 1)
        {
            return new ProjectEntity()
            {
                Id = id,
                Title = $"Project {1}",
                Status = StatusEnum.New
            };
        }
    }
}
